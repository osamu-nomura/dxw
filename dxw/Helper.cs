using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using DxLibDLL;

namespace dxw
{
    #region 【StringSize】
    /// <summary>
    /// 文字列描画サイズ
    /// </summary>
    public struct StringSize
    {
        /// <summary>
        /// 幅
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 高さ
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int LineCount { get; set; }
    }
    #endregion

    #region 【JoyPadState】
    /// <summary>
    /// ジョイパッド状態
    /// </summary>
    public struct JoyPadState
    {
        /// <summary>
        /// 各ボタン状態
        /// </summary>
        public byte[] Buttons;
        /// <summary>
        /// 右トリガ
        /// </summary>
        public byte RightTrigger;
        /// <summary>
        /// 左トリガ
        /// </summary>
        public byte LeftTrigger;
        /// <summary>
        /// 左スティックY軸
        /// </summary>
        public short ThumbLY;
        /// <summary>
        /// 左スティックX軸
        /// </summary>
        public short ThumbLX;
        /// <summary>
        /// 右スティックX軸
        /// </summary>
        public short ThumbRX;
        /// <summary>
        /// 右スティックY軸
        /// </summary>
        public short ThumbRY;

        /// <summary>
        /// ボタンが押下されている？
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private bool IsDownButton(int n) => (Buttons?[n] ?? 0) == 1;

        /// <summary>
        /// デジタル方向キー上
        /// </summary>
        public bool DPadUp => IsDownButton(DX.XINPUT_BUTTON_DPAD_UP);
        /// <summary>
        /// デジタル方向キー下
        /// </summary>
        public bool DPadDown => IsDownButton(DX.XINPUT_BUTTON_DPAD_DOWN);
        /// <summary>
        /// デジタル方向キー左
        /// </summary>
        public bool DPadLeft => IsDownButton(DX.XINPUT_BUTTON_DPAD_LEFT);
        /// <summary>
        /// デジタル方向キー右
        /// </summary>
        public bool DPadRight => IsDownButton(DX.XINPUT_BUTTON_DPAD_RIGHT);
        /// <summary>
        /// スタートボタン
        /// </summary>
        public bool Start => IsDownButton(DX.XINPUT_BUTTON_START);
        /// <summary>
        /// バックボタン
        /// </summary>
        public bool Back => IsDownButton(DX.XINPUT_BUTTON_BACK);
        /// <summary>
        /// 左スティック
        /// </summary>
        public bool LeftThumb => IsDownButton(DX.XINPUT_BUTTON_LEFT_THUMB);
        /// <summary>
        /// 右スティック
        /// </summary>
        public bool RightThumb => IsDownButton(DX.XINPUT_BUTTON_RIGHT_THUMB);
        /// <summary>
        /// 左肩ボタン
        /// </summary>
        public bool LeftShoulder => IsDownButton(DX.XINPUT_BUTTON_LEFT_SHOULDER);
        /// <summary>
        /// 右肩ボタン
        /// </summary>
        public bool RightShoulder => IsDownButton(DX.XINPUT_BUTTON_RIGHT_SHOULDER);
        /// <summary>
        /// A ボタン
        /// </summary>
        public bool A => IsDownButton(DX.XINPUT_BUTTON_A);
        /// <summary>
        /// Bボタン
        /// </summary>
        public bool B => IsDownButton(DX.XINPUT_BUTTON_B);
        /// <summary>
        /// Xボタン
        /// </summary>
        public bool X => IsDownButton(DX.XINPUT_BUTTON_X);
        /// <summary>
        /// Yボタン
        /// </summary>
        public bool Y => IsDownButton(DX.XINPUT_BUTTON_Y);
    }
    #endregion

    #region 【Static Class : Helper】
    /// <summary>
    /// ヘルパーメソッド
    /// </summary>
    public static class Helper
    {
        #region ■ DLL Import
        /// <summary>
        /// SetActiveStateChangeCallBackFunction
        /// DXライブラリのC#用DLLの定義では利用しづらいので直接DxLib.dllからインポートする
        /// </summary>
        /// <param name="Callback">コールバック関数のポインタ</param>
        /// <param name="UserData">コールバック関数に渡すユーザーデータ</param>
        /// <returns></returns>
        [DllImport("DxLib.dll", EntryPoint = "dx_SetActiveStateChangeCallBackFunction")]
        extern unsafe static int SetActiveStateChangeCallBackFunction(DX.SetActiveStateChangeCallBackFunctionCallback Callback, IntPtr UserData);
        /// <summary>
        /// GetJoypadXInputStateで使用する構造体
        /// </summary>
        private unsafe struct _XInputState
        {
            public fixed byte Buttons[16];
            public byte RightTrigger;
            public byte LeftTrigger;
            public short ThumbLY;
            public short ThumbLX;
            public short ThumbRX;
            public short ThumbRY;
        }
        /// <summary>
        /// GetJoypadXInputState
        /// DXライブラリのC#用DLLの定義では利用しづらいので直接DxLib.dllからインポートする
        /// </summary>
        /// <param name="padNo">取得したいジョイパッドの番号</param>
        /// <param name="state">ジョイパッドのステータス(OUT)</param>
        /// <returns>0 : 正常 / 非0 : エラー</returns>
        [DllImport("DxLib.dll", EntryPoint = "dx_GetJoypadXInputState")]
        extern unsafe static int GetJoypadXInputState(int padNo, out _XInputState state);
        #endregion

        #region ■ Members
        /// <summary>
        /// SetActiveStateChangeCallBackFunctionのコールバック関数
        /// </summary>
        private static unsafe DX.SetActiveStateChangeCallBackFunctionCallback _callback = null;
        /// <summary>
        /// WindowのActiveStatusが変更されたときに呼ばれるコールバック
        /// </summary>
        private static Action<int> _activeStateChanged = null;
        /// <summary>
        /// マウスカーソルが表示されているか？
        /// </summary>
        private static bool _visibleMousrCursor = true;
        #endregion

        #region ■ Private Methods

        #region - SetActiveStateChangeCallBack : SetActiveStateChangeCallBackFunctionに渡すコールバック
        /// <summary>
        /// SetActiveStateChangeCallBackFunctionに渡すコールバック
        /// </summary>
        /// <param name="activeState">アクティブステート</param>
        /// <param name="userData">ユーザデータ</param>
        /// <returns>int</returns>
        private static unsafe int SetActiveStateChangeCallBack(int activeState, void* userData)
        {
            _activeStateChanged?.Invoke(activeState);
            return 0;
        }
        #endregion

        #region - DXBool : DXライブラリの真偽値に変換する
        /// <summary>
        /// DXライブラリの真偽値に変換する
        /// </summary>
        /// <param name="sw">真偽値</param>
        /// <returns>1:真 / 0:偽</returns>
        private static int DXBool(bool sw) => sw ? DX.TRUE : DX.FALSE;
        #endregion

        #region - NormarizeVolume :　音量の正規化
        /// <summary>
        /// 音量の正規化 (0～250の範囲に収める）
        /// </summary>
        /// <param name="volume">音量</param>
        /// <returns>正規化された音量</returns>
        private static int NormarizeVolume(int volume)
        {
            if (volume < 0)
                return 0;
            if (volume > 250)
                return 250;
            return volume;
        }
        #endregion

        #endregion

        #region ■ Public Methods

        #region - LoadFile : ファイルをバイト配列に読み込む
        /// <summary>
        /// ファイルをバイト配列に読み込む
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>バイト配列</returns>
        public static unsafe byte[] LoadFile(string path)
        {
            // ファイルサイズを取得
            var fileSize = DX.FileRead_size(path);
            if (fileSize > 0)
            {
                // 必要な領域を確保
                var buff = new byte[fileSize];

                // ファイルのOPEN
                var fileHandle = DX.FileRead_open(path);
                try
                {
                    fixed (byte *p = buff)
                    {
                        DX.FileRead_read(new IntPtr(p), (int)fileSize, fileHandle);
                    }
                }
                finally
                {
                    DX.FileRead_close(fileHandle);
                }
                return buff;
            }
            return null;
        }
        #endregion

        #region ☆　システム関連

        #region - SetActiveStateChangedCallback : アクティブステートが変更された場合に呼び出されるコールバックを登録する
        /// <summary>
        /// アクティブステートが変更された場合に呼び出されるコールバックを登録する
        /// </summary>
        /// <param name="callback">コールバック</param>
        public static unsafe void SetActiveStateChangedCallback(Action<int> callback)
        {
            _activeStateChanged = callback;
            _callback = SetActiveStateChangeCallBack;
            SetActiveStateChangeCallBackFunction(_callback, IntPtr.Zero);
        }
        #endregion

        #region - InitializeDxLibrary : DXライブラリの初期化
        /// <summary>
        /// DXライブラリの初期化
        /// </summary>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool InitializeDxLibrary() 
            => DX.DxLib_Init() == 0;
        #endregion

        #region - TerminateDxLibrary : DXライブラリの終了処理
        /// <summary>
        /// DXライブラリの終了処理
        /// </summary>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool TerminateDxLibrary()
            => DX.DxLib_End() == 0;
        #endregion

        #region - ProcessMessage : ウインドウのメッセージを処理する
        /// <summary>
        /// ウインドウのメッセージを処理する
        /// </summary>
        /// <returns>true: 成功 / False: 失敗</returns>
        public static bool ProcessMessage() 
            => DX.ProcessMessage() == 0;
        #endregion

        #endregion

        #region ☆　ウィンドウモード関連関数

        #region - SetMainWindowText : ウインドウのタイトルを変更する
        /// <summary>
        /// ウインドウのタイトルを変更する
        /// </summary>
        /// <param name="s">タイトル文字列</param>
        public static void SetMainWindowText(string s)
        {
            DX.SetMainWindowText(s);
        }
        #endregion

        #region - ChangeWindowMode : ウインドウモード・フルスクリーンモードの変更を行う
        /// <summary>
        /// ウインドウモード・フルスクリーンモードの変更を行う
        /// </summary>
        /// <param name="windowMode">ウィンドウモード</param>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool ChangeWindowMode(WindowMode windowMode)
        {
            _visibleMousrCursor = (windowMode == WindowMode.Window);
            return DX.ChangeWindowMode((int)windowMode) == DX.DX_CHANGESCREEN_OK;
        }
        #endregion

        #endregion

        #region ☆ グラフィックデータ制御関数

        #region - CreateDrawableGraph : 描画可能なグラフィックを生成する。
        /// <summary>
        /// 描画可能なグラフィックを生成する。
        /// </summary>
        /// <param name="width">幅(PX)</param>
        /// <param name="height">高さ(PX)</param>
        /// <param name="useAlphaChannel">アルファチャネルの有効化</param>
        /// <returns>グラフィックハンドル</returns>
        public static int CreateDrawableGraph(int width, int height, bool useAlphaChannel = false) 
            => DX.MakeScreen(width, height, DXBool(useAlphaChannel));
        #endregion

        #region - CreateDrawableGraph : 描画可能なグラフィックを生成する。
        /// <summary>
        /// 描画可能なグラフィックを生成する。
        /// </summary>
        /// <param name="size">サイズ</param>
        /// <param name="useAlphaChannel">アルファチャネルの有効化</param>
        /// <returns>グラフィックハンドル</returns>
        public static int CreateDrawableGraph(RectangleSize size, bool useAlphaChannel = false)
            => DX.MakeScreen(size.Width, size.Height, DXBool(useAlphaChannel));
        #endregion

        #region - CreateDrawableGraph : 描画可能なグラフィックを生成する。
        /// <summary>
        ///  描画可能なグラフィックを生成する。
        /// </summary>
        /// <param name="width">幅(px)</param>
        /// <param name="height">高さ(px)</param>
        /// <param name="useAlphaChannel">アルファチャネルの有効化</param>
        /// <param name="callback">コールバック</param>
        /// <returns>グラフィックハンドル</returns>
        public static int CreateDrawableGraph(int width, int height, bool useAlphaChannel, Action callback)
        {
            var graph = CreateDrawableGraph(width, height, useAlphaChannel);
            if (graph >= 0)
            {
                if (callback != null)
                {
                    DX.SetDrawScreen(graph);
                    try
                    {
                        callback();
                    }
                    catch
                    {
                        DX.DeleteGraph(graph);
                        throw;
                    }
                    finally
                    {
                        DX.SetDrawScreen(DX.DX_SCREEN_BACK);
                    }
                }
            }
            return graph;
        }
        #endregion

        #region - CreateDrawableGraph : 描画可能なグラフィックを生成する。
        /// <summary>
        ///  描画可能なグラフィックを生成する。
        /// </summary>
        /// <param name="size">サイズ</param>
        /// <param name="useAlphaChannel">アルファチャネルの有効化</param>
        /// <param name="callback">コールバック</param>
        /// <returns>グラフィックハンドル</returns>
        public static int CreateDrawableGraph(RectangleSize size, bool useAlphaChannel, Action callback)
            => CreateDrawableGraph(size.Width, size.Height, useAlphaChannel, callback);
        #endregion

        #region - DeleteGraph : グラフィックをメモリから削除する。
        /// <summary>
        /// グラフィックをメモリから削除する。
        /// </summary>
        /// <param name="handle">グラフィックハンドル</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool DeleteGraph(int handle)
        {
            return DX.DeleteGraph(handle) == 0;
        }
        #endregion

        #region - DrawGraph : メモリに読み込んだグラフィックの描画
        /// <summary>
        /// メモリに読み込んだグラフィックの描画
        /// </summary>
        /// <param name="x">X座標(px)</param>
        /// <param name="y">Y座標(px)</param>
        /// <param name="handle">グラフィックハンドル</param>
        /// <param name="enableTranslate">透過を有効にする</param>
        /// <returns>True : 成功 . False : 失敗</returns>
        public static bool DrawGraph(int x, int y, int handle, bool enableTranslate = false)
        {
            if (handle == 0)
                return false;
            return DX.DrawGraph(x, y, handle, DXBool(enableTranslate)) == 0;
        }
        #endregion

        #region - DrawGraph : メモリに読み込んだグラフィックの描画
        /// <summary>
        /// メモリに読み込んだグラフィックの描画
        /// </summary>
        /// <param name="pt">座標</param>
        /// <param name="handle">グラフィックハンドル</param>
        /// <param name="enableTranslate">透過を有効にする</param>
        /// <returns>True : 成功 . False : 失敗</returns>
        public static bool DrawGraph(Point pt, int handle, bool enableTranslate = false)
            => DrawGraph(pt.X, pt.Y, handle, enableTranslate);
        #endregion

        #region - DrawExtendGraph : メモリに読みこんだグラフィックの拡大縮小描画
        /// <summary>
        /// メモリに読みこんだグラフィックの拡大縮小描画
        /// </summary>
        /// <param name="x1">左上座標X軸(px)</param>
        /// <param name="y1">左上座標Y軸(px)</param>
        /// <param name="x2">右下座標X軸(px)</param>
        /// <param name="y2">右下座標Y軸(px)</param>
        /// <param name="handle">グラフィックハンドル</param>
        /// <param name="enableTranslate">透過を有効にする</param>
        /// <returns>True : 成功 . False : 失敗</returns>
        public static bool DrawExtendGraph(int x1, int y1, int x2, int y2, int handle, bool enableTranslate = false)
        {
           return  DX.DrawExtendGraph(x1, y1, x2, y2, handle, DXBool(enableTranslate)) == 0;
        }
        #endregion

        #region - LoadGraph : 画像ファイルのメモリへの読みこみ
        /// <summary>
        /// 画像ファイルのメモリへの読みこみ
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>グラフィックハンドル</returns>
        public static int LoadGraph(string fileName)
        {
            return DX.LoadGraph(fileName);
        }
        #endregion

        #region - GetGraphSize : グラフィックのサイズを取得する
        /// <summary>
        /// グラフィックのサイズを取得する
        /// </summary>
        /// <param name="handle">グラフィックハンドル</param>
        /// <returns>サイズ</returns>
        public static RectangleSize? GetGraphSize(int handle)
        {
            if (handle != 0 && DX.GetGraphSize(handle, out var width, out var height) == 0)
            {
                return new RectangleSize(width, height);
            }
            return null;
        }
        #endregion

        #region - SetDrawBrendMode : 描画の際のブレンドモードをセットする
        /// <summary>
        /// 描画の際のブレンドモードをセットする
        /// </summary>
        /// <param name="brendMode">ブレンドモード</param>
        /// <param name="blendParam">パラメーター値</param>
        /// <returns>true: 成功 / False: 失敗</returns>
        public static bool SetDrawBrendMode(BrendMode brendMode, int blendParam)
            => DX.SetDrawBlendMode((int)brendMode, blendParam) == 0;
        #endregion

        #region - DrawBrend : 指定したブレンドモードで描画を実行する
        /// <summary>
        /// 指定したブレンドモードで描画を実行する
        /// </summary>
        /// <param name="brendMode">ブレンドモード</param>
        /// <param name="blendParam">パラメーター値</param>
        /// <param name="acttion">描画アクション</param>
        public static void DrawBrend(BrendMode brendMode, int blendParam, Action acttion)
        {
            DX.SetDrawBlendMode((int)brendMode, blendParam);
            try
            {
                acttion?.Invoke();
            }
            finally
            {
                DX.SetDrawBlendMode((int)BrendMode.NoBrend, 0);
            }
        }
        #endregion

        #region - SetDrawBright : 	描画輝度をセット
        /// <summary>
        /// 	描画輝度をセット
        /// </summary>
        /// <param name="redBright">赤描画輝度(0-255)</param>
        /// <param name="greenBright">緑描画輝度(0-255)</param>
        /// <param name="blueBright">青描画輝度(0-255)</param>
        /// <returns>true: 成功 / False: 失敗</returns>
        public static bool SetDrawBright(int redBright, int greenBright, int blueBright)
            => DX.SetDrawBright(redBright, greenBright, blueBright) == 0;
        #endregion

        #endregion

        #region ☆ 図形描画関数

        #region - DrawLine : 直線を描画する
        /// <summary>
        /// 直線を描画する
        /// </summary>
        /// <param name="x1">起点X座標(px)</param>
        /// <param name="y1">起点Y座標(px)</param>
        /// <param name="x2">終点X座標(px)</param>
        /// <param name="y2">終点Y座標(px)</param>
        /// <param name="color">色</param>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool DrawLine(int x1, int y1, int x2, int y2, uint color)
            => DX.DrawLine(x1, y1, x2, y2, color) == 0;
        #endregion

        #region - DrawLine : 直線を描画する
        /// <summary>
        /// 直線を描画する
        /// </summary>
        /// <param name="pt1">起点座標(px)</param>
        /// <param name="pt2">終点座標(px)</param>
        /// <param name="color">色</param>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool DrawLine(Point pt1, Point pt2, uint color)
            => DX.DrawLine(pt1.X, pt1.Y, pt2.X, pt2.Y, color) == 0;
        #endregion

        #region - DrawAntiAliasingLine : 直線を描画する(アンチエリアス適用）
        /// <summary>
        /// 直線を描画する(アンチエリアス適用）
        /// </summary>
        /// <param name="x1">起点X座標(px)</param>
        /// <param name="y1">起点Y座用(px)</param>
        /// <param name="x2">終点X座標(px)</param>
        /// <param name="y2">終点Y座標(px)</param>
        /// <param name="color">色</param>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool DrawAntiAliasingLine(float x1, float y1, float x2, float y2, uint color)
            => DX.DrawLineAA(x1, y1, x2, y2, color) == 0;
        #endregion

        #region - DrawBox : 矩形を描画する
        /// <summary>
        /// 矩形を描画する
        /// </summary>
        /// <param name="x">X軸(px)</param>
        /// <param name="y">Y軸(px)</param>
        /// <param name="width">幅(px)</param>
        /// <param name="height">高さ(px)</param>
        /// <param name="color">色</param>
        /// <param name="isFill">塗りつぶす？</param>
        public static bool DrawBox(int x, int y, int width, int height, uint color, bool isFill = false)
        {
            return DX.DrawBox(x, y, x + width, y + height, color, DXBool(isFill)) == 0;
        }
        #endregion

        #region - DrawBox : 矩形を描画する
        /// <summary>
        /// 矩形を描画する
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="color">色</param>
        /// <param name="isFill">塗りつぶす？</param>
        public static bool DrawBox(Rectangle rect, uint color, bool isFill = false)
            => DrawBox(rect.X, rect.Y, rect.Width, rect.Height, color, isFill);
        #endregion

        #region - DrawBox : 矩形を描画する
        /// <summary>
        /// 矩形を描画する
        /// </summary>
        /// <param name="pt">座標</param>
        /// <param name="size">サイズ</param>
        /// <param name="color">色</param>
        /// <param name="isFill">塗りつぶす？</param>
        public static bool DrawBox(Point pt, RectangleSize size, uint color, bool isFill = false)
            => DrawBox(pt.X, pt.Y, size.Width, size.Height, color, isFill);
        #endregion

        #region - DrawBox : 矩形を描画する
        /// <summary>
        /// 矩形を描画する
        /// </summary>
        /// <param name="leftTop">左上座標</param>
        /// <param name="rightBottom">右下座標</param>
        /// <param name="color">色</param>
        /// <param name="isFill">塗りつぶす？</param>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool DrawBox(Point leftTop, Point rightBottom, uint color, bool isFill = false)
            => DrawBox(new Rectangle(leftTop, rightBottom), color, isFill);
        #endregion

        #region - DrawAntiAliasingBox : 矩形を描画する
        /// <summary>
        /// 矩形を描画する
        /// </summary>
        /// <param name="x">X座上(px)</param>
        /// <param name="y">Y座ｈ上(px)</param>
        /// <param name="width">幅(px)</param>
        /// <param name="height">高さ(px)</param>
        /// <param name="color">色</param>
        /// <param name="isFill">塗りつぶす？</param>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool DrawAntiAliasingBox(float x, float y, float width, float height, uint color, bool isFill = false)
            => DX.DrawBoxAA(x, y, x + width, y + height, color,DXBool(isFill)) == 0;

        #endregion

        #endregion

        #region ☆ 文字描画関係関数

        #region - DrawString : 文字列を描画する
        /// <summary>
        /// 文字列を描画する
        /// </summary>
        /// <param name="x">X座標(px)</param>
        /// <param name="y">Y座標(px)</param>
        /// <param name="s">描画文字列</param>
        /// <param name="color">色</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool DrawString(int x, int y, string s, uint color) 
            => DX.DrawString(x, y, s, color) == 0;
        #endregion

        #region - DrawString : 文字列を描画する
        /// <summary>
        /// 文字列を描画する
        /// </summary>
        /// <param name="pt">座標</param>
        /// <param name="s">描画文字列</param>
        /// <param name="color">色</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool DrawString(Point pt, string s, uint color)
            => DrawString(pt.X, pt.Y, s, color);
        #endregion

        #region - DrawString : 文字列を描画する
        /// <summary>
        /// 文字列を描画する
        /// </summary>
        /// <param name="x">X座標(px)</param>
        /// <param name="y">Y座標(px)</param>
        /// <param name="s">描画文字列</param>
        /// <param name="color">色</param>
        /// <param name="font">フォントハンドル</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool DrawString(int x, int y, string s, uint color, int font)
        {
            return DX.DrawStringToHandle(x, y, s, color, font) == 0;
        }
        #endregion

        #region - DrawString : 文字列を描画する
        /// <summary>
        /// 文字列を描画する
        /// </summary>
        /// <param name="pt">座標</param>
        /// <param name="s">描画文字列</param>
        /// <param name="color">色</param>
        /// <param name="font">フォントハンドル</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool DrawString(Point pt, string s, uint color, int font)
            => DrawString(pt.X, pt.Y, s, color, font);
        #endregion

        #region - DrawString : 文字列を描画する
        /// <summary>
        /// 文字列を描画する
        /// </summary>
        /// <param name="x">X座標(px)</param>
        /// <param name="y">Y座標(px)</param>
        /// <param name="width">幅</param>
        /// <param name="textAlign">テキストアラインメント</param>
        /// <param name="s">描画文字列</param>
        /// <param name="color">色</param>
        /// <param name="font">フォントハンドル</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool DrawString(int x, int y, int width, HAlignment textAlign, string s, uint color, int font)
        {
            var stringWidth = GetDrawStringWidth(s, font);
            var offset = 0;
            if (textAlign == HAlignment.Center)
                offset = (width - stringWidth) / 2;
            else if (textAlign == HAlignment.Right)
                offset = (width - stringWidth);
            return DrawString(x, y + offset, s, color, font);
        }
        #endregion

        #region - DrawString : 文字列を描画する
        /// <summary>
        /// 文字列を描画する
        /// </summary>
        /// <param name="rect">描画領域（px）</param>
        /// <param name="hAlign">水平アライメント</param>
        /// <param name="vAlign">垂直アライメント</param>
        /// <param name="s">描画文字列</param>
        /// <param name="color">色</param>
        /// <param name="font">フォントハンドル</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool DrawString(Rectangle rect, HAlignment hAlign, VAlignment vAlign, string s, uint color, int font)
        {
            var size = GetDrawStringSize(s, font);
            var hOffset = 0;
            if (hAlign == HAlignment.Center)
                hOffset = (rect.Width - size.Width) / 2;
            else if (hAlign == HAlignment.Right)
                hOffset = (rect.Width - size.Width);
            var vOffset = 0;
            if (vAlign == VAlignment.Middle)
                vOffset = (rect.Height - size.Height) / 2;
            else if (vAlign == VAlignment.Bottom)
                vOffset = (rect.Height - size.Height);
            return DrawString(rect.X + hOffset, rect.Y + vOffset, s, color, font);
        }
        #endregion

        #region - GetDrawStringWidth 文字列を描画したさいの幅を取得する
        /// <summary>
        /// 文字列を描画したさいの幅を取得する
        /// </summary>
        /// <param name="s">文字列</param>
        /// <param name="font">フォントハンドル</param>
        /// <returns>描画幅(px)</returns>
        public static int GetDrawStringWidth(string s, int font = 0)
        {
            if (font != 0)
                return DX.GetDrawStringWidthToHandle(s, s.Length, font);
            else
                return DX.GetDrawStringWidth(s, s.Length);
        }
        #endregion

        #region - GetDrawStringSize : 文字列を描画したさいのサイズを取得する
        /// <summary>
        /// 文字列を描画したさいのサイズを取得する
        /// </summary>
        /// <param name="s">文字列</param>
        /// <param name="font">フォントハンドル</param>
        /// <returns>描画サイズ</returns>
        public static StringSize GetDrawStringSize(string s, int font = 0)
        {
            int width, height, lineCount;
            if (font != 0)
                DX.GetDrawStringSizeToHandle(out width, out height, out lineCount, s, s.Length, font);
            else
                DX.GetDrawStringSize(out width, out height, out lineCount, s, s.Length);
            return new StringSize { Width = width, Height = height, LineCount = lineCount };
        }
        #endregion

        #region - CreateFont : フォントを作製する
        /// <summary>
        /// フォントを作製する
        /// </summary>
        /// <param name="fontName">フォント名(null: default)</param>
        /// <param name="size">サイズ(px -1:default)</param>
        /// <param name="thick">太さ(0-9 -1:default)</param>
        /// <param name="fontType">フォント種別</param>
        /// <returns>フォントハンドル</returns>
        public static int CreateFont(string fontName, int size, int thick, FontType fontType) 
            => DX.CreateFontToHandle(fontName, size, thick, (int)fontType);
        #endregion

        #endregion

        #region ☆ その他画面操作系関数

        #region - SetGraphMode : 画面モードの変更
        /// <summary>
        /// 画面モードの変更
        /// </summary>
        /// <param name="screenWidth">画面幅(px)</param>
        /// <param name="screenHeight">画面高さ(px)</param>
        /// <param name="colorBitDepth">色深度(bit)</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool SetGraphMode(int screenWidth, int screenHeight, ColorBitDepth colorBitDepth) 
            => DX.SetGraphMode(screenWidth, screenHeight, (int)colorBitDepth) == DX.DX_CHANGESCREEN_OK;
        #endregion

        #region - SetGraphMode : 画面モードの変更
        /// <summary>
        /// 画面モードの変更
        /// </summary>
        /// <param name="screenSize">画面サイズ</param>
        /// <param name="colorBitDepth">色深度(bit)</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool SetGraphMode(RectangleSize screenSize, ColorBitDepth colorBitDepth)
            => SetGraphMode(screenSize.Width, screenSize.Height, colorBitDepth);
        #endregion

        #region - GetColor : 色コードを取得する
        /// <summary>
        /// 色コードを取得する
        /// </summary>
        /// <param name="red">赤</param>
        /// <param name="green">緑</param>
        /// <param name="blue">青</param>
        /// <returns>色コード</returns>
        public static uint GetColor(int red, int green, int blue) 
            => DX.GetColor(red, green, blue);
        #endregion

        #region - ClearDrawScreen : 画面に描かれたものを消去する
        /// <summary>
        /// 画面に描かれたものを消去する
        /// </summary>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool ClearDrawScreen() 
            => DX.ClearDrawScreen() == 0;
        #endregion

        #region - ScreenFlip : 裏画面を表画面に転送する
        /// <summary>
        /// 裏画面を表画面に転送する
        /// </summary>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool ScreenFlip() 
            => DX.ScreenFlip() == 0;
        #endregion

        #region - SetDrawScreen : 描画先グラフィック領域の指定
        /// <summary>
        /// 描画先グラフィック領域の指定
        /// </summary>
        /// <param name="screen">描画スクリーン</param>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool SetDrawScreen(DrawScreen screen)
            => DX.SetDrawScreen((int)screen) == 0;
        #endregion

        #region - SetDrawScreen : 描画先グラフィック領域の指定
        /// <summary>
        /// 描画先グラフィック領域の指定
        /// </summary>
        /// <param name="screenHandle">描画先グラフィックハンドル</param>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool SetDrawScreen(int screenHandle)
            => DX.SetDrawScreen(screenHandle) == 0;
        #endregion

        #endregion

        #region ☆　キーボード入力関連関数

        #region - GetHitKeyStateAll : キーボードのすべてのキーの押下状態を取得する
        /// <summary>
        /// キーボードのすべてのキーの押下状態を取得する
        /// </summary>
        /// <param name="keyStateBuf"></param>
        /// <returns>キーの押下状態を保持したバイト配列</returns>
        public static bool GetHitKeyStateAll(byte[] keyStateBuf) 
            => DX.GetHitKeyStateAll(keyStateBuf) == 0;
        #endregion

        #endregion

        #region ☆ タッチパネル入力関連関数

        #region - GetTouchInputNum : タッチされている箇所の数を取得する
        /// <summary>
        /// タッチされている箇所の数を取得する
        /// </summary>
        /// <returns>タッチされている箇所の数</returns>
        public static int GetTouchInputNum() 
            => DX.GetTouchInputNum();
        #endregion

        #region - GetTouchInput : タッチされている箇所の情報を取得する
        /// <summary>
        /// タッチされている箇所の情報を取得する
        /// </summary>
        /// <param name="inputNo">タッチされている箇所の番号</param>
        /// <returns>入力情報</returns>
        public static InputInfo GetTouchInput(int inputNo)
        {
            if (DX.GetTouchInput(inputNo, out var x, out var y, out var id, out var device) == 0)
            {
                return new InputInfo(DeviceType.Touch, device, id, x, y, (int)MouseInput.Left);
            }
            return null;
        }
        #endregion

        #region - GetTouchInputs : すべてのタッチされている情報を取得する
        /// <summary>
        /// すべてのタッチされている情報を取得する
        /// </summary>
        /// <returns>入力情報の列挙</returns>
        public static IEnumerable<InputInfo> GetTouchInputs() 
            => Enumerable.Range(0, GetTouchInputNum())
                         .Select(n => GetTouchInput(n))
                         .Where(info => info != null);
        #endregion

        #endregion

        #region ☆ マウス入力関連関数 

        #region - GetMousePoint : マウスカーソルの位置を取得する
        /// <summary>
        /// マウスカーソルの位置を取得する
        /// </summary>
        /// <returns>マウスカーソル座標</returns>
        public static Point? GetMousePoint()
        {
            if (DX.GetMousePoint(out var x, out var y) == 0)
                return new Point(x, y);
            return null;
        }
        #endregion

        #region - GetMouseInput : マウスの入力情報を取得する
        /// <summary>
        /// マウスの入力情報を取得する
        /// </summary>
        /// <returns>入力情報</returns>
        public static InputInfo GetMouseInput()
        {
            var buttons = DX.GetMouseInput();
            if (buttons != 0)
            {
                if (DX.GetMousePoint(out var x, out var y) == 0)
                    return new InputInfo(DeviceType.Mouse, 0, 0, x, y, buttons);
            }
            return null;
        }
        #endregion

        #region - SetMouseDispFlag : マウスカーソルの表示設定フラグのセット
        /// <summary>
        /// マウスカーソルの表示設定フラグのセット
        /// </summary>
        /// <param name="isShow"></param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool SetMouseDispFlag(bool isShow)
        {
            _visibleMousrCursor = isShow;
            return DX.SetMouseDispFlag(DXBool(isShow)) == 0;
        }
        #endregion

        #region - ShowMouseCursor : マウスカーソルを表示する
        /// <summary>
        /// マウスカーソルを表示する
        /// </summary>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool ShowMouseCursor() => SetMouseDispFlag(true);
        #endregion

        #region - HideMouseCursor : マウスカーソルを非表示にする
        /// <summary>
        /// マウスカーソルを非表示にする
        /// </summary>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool HideMouseCursor() => SetMouseDispFlag(false);
        #endregion

        #region - IsMouseCursorVisible : マウスカーソルが表示されている？
        /// <summary>
        /// マウスカーソルが表示されている？
        /// </summary>
        public static bool IsMouseCursorVisible
        {
            get { return _visibleMousrCursor;  }
        }
        #endregion

        #endregion

        #region ☆ ジョイパッド入力関連関数

        #region - GetJoyPadNum : ジョイパッドが接続されている数を取得する
        /// <summary>
        /// ジョイパッドが接続されている数を取得する
        /// </summary>
        /// <returns>ジョイパッドの接続数</returns>
        public static int GetJoyPadNum()
        {
            return DX.GetJoypadNum();
        }
        #endregion

        #region - GetJoypadState : ジョイパッドの状態を取得する
        /// <summary>
        /// ジョイパッドの状態を取得する
        /// </summary>
        /// <param name="padNo">取得したいジョイパッドの番号</param>
        /// <returns>ジョイパッドの状態を保持したJoyPadState構造体</returns>
        public static unsafe JoyPadState? GetJoypadState(int padNo)
        {
            if (GetJoypadXInputState(padNo, out _XInputState state) == 0)
            {
                var buttons = new byte[16];
                Marshal.Copy((IntPtr)state.Buttons, buttons, 0, 16);
                return new JoyPadState
                {
                    Buttons = buttons,
                    RightTrigger = state.RightTrigger,
                    LeftTrigger = state.LeftTrigger,
                    ThumbLX = state.ThumbLX,
                    ThumbLY = state.ThumbLY,
                    ThumbRX = state.ThumbRX,
                    ThumbRY = state.ThumbRY
                };
            }
            return null;
        }
        #endregion

        #region - SetJoypadDeadZone : ジョイパッドの方向入力の無効範囲を設定する
        /// <summary>
        /// ジョイパッドの方向入力の無効範囲を設定する
        /// </summary>
        /// <param name="padNo">取得したいジョイパッドの番号</param>
        /// <param name="zone">無効範囲( 0.0 ～ 1.0 )、デフォルト値は 0.35</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool SetJoypadDeadZone(int padNo, double zone)
        {
            return DX.SetJoypadDeadZone(padNo, zone) == 0;
        }
        #endregion

        #region - StartJoypadVibration : ジョイパッドの振動を開始する
        /// <summary>
        /// ジョイパッドの振動を開始する
        /// </summary>
        /// <param name="padNo">取得したいジョイパッドの番号</param>
        /// <param name="power">振動の強さ(0～1000)</param>
        /// <param name="time">振動させる時間(ミリ秒単位) (-1 で StopJoypadVibration が呼ばれるまで振動し続ける)</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool StartJoypadVibration(int padNo, int power, int time)
        {
            return DX.StartJoypadVibration(padNo, power, time) == 0;
        }
        #endregion

        #region - StopJoypadVibration : ジョイパッドの振動を停止する
        /// <summary>
        /// ジョイパッドの振動を停止する
        /// </summary>
        /// <param name="padNo">取得したいジョイパッドの番号</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool StopJoypadVibration(int padNo)
        {
            return DX.StopJoypadVibration(padNo) == 0;
        }
        #endregion

        #endregion

        #region ☆ 音利用関数

        #region - PlaySound : 音声を再生する
        /// <summary>
        /// 音声を再生する
        /// </summary>
        /// <param name="handle">リソースハンドル</param>
        /// <param name="playType">再生形式</param>
        /// <param name="volume">音量</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool PlaySound(int handle, PlayType playType, int volume)
        {
            DX.ChangeNextPlayVolumeSoundMem(NormarizeVolume(volume), handle);
            return DX.PlaySoundMem(handle, (int)playType, DX.TRUE) == 0;
        }
        #endregion

        #region - ChangeSoundVolume : 音量を変更する
        /// <summary>
        /// 音量を変更する
        /// </summary>
        /// <param name="handle">リソースハンドル</param>
        /// <param name="newVolume">音量</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool ChangeSoundVolume(int handle, int newVolume)
        {
            if (DX.CheckSoundMem(handle) == DX.TRUE)
                return DX.ChangeVolumeSoundMem(NormarizeVolume(newVolume), handle) == 0;
            return false;
        }
        #endregion

        #region - StopSound : 音声を停止する
        /// <summary>
        /// 音声を停止する
        /// </summary>
        /// <param name="handles">リソースハンドル</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static void StopSound(params int[] handles)
        {
            foreach (var h in handles)
            {
                if (DX.CheckSoundMem(h) == DX.TRUE)
                    DX.StopSoundMem(h);
            }
        }
        #endregion

        #region - IsPlayingSound : 音声が再生中？
        /// <summary>
        /// 音声が再生中？
        /// </summary>
        /// <param name="handle">リソースハンドル</param>
        /// <returns>Trye : 再生中 / False : 再生中でない</returns>
        public static bool IsPlayingSound(int handle)
        {
            return (DX.CheckSoundMem(handle) == DX.TRUE);
        }
        #endregion

        #region - LoadSound : 音声ファイルの読み込み
        /// <summary>
        /// 音声ファイルの読み込み
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>リソースハンドル</returns>
        public static int LoadSound(string fileName)
        {
            return DX.LoadSoundMem(fileName); ;
        }
        #endregion

        #endregion

        #region ☆　時間関係の関数

        #region - GetNowCount : ミリ秒単位の精度を持つカウンタの現在値を得る
        /// <summary>
        /// ミリ秒単位の精度を持つカウンタの現在値を得る
        /// </summary>
        /// <returns>	Windowsが起動してから経過時間(mm秒)</returns>
        public static int GetNowCount() 
            => DX.GetNowCount();
        #endregion

        #endregion

        #region ☆　非同期読み込み関係

        #region - SetUseASyncLoadFlag : 非同期読み込みを行うかどうかを設定する
        /// <summary>
        /// 非同期読み込みを行うかどうかを設定する
        /// </summary>
        /// <param name="sw">フラグ True: 非同期読み込みを行う / False : 非同期読み込みを行わない</param>
        /// <returns>True : 成功 / False : 失敗</returns>
        public static bool SetUseASyncLoadFlag(bool sw) 
            => DX.SetUseASyncLoadFlag(DXBool(sw)) == 0;
        #endregion

        #region - GetASyncLoadNum : 非同期読み込み中の処理の数を取得する
        /// <summary>
        /// 非同期読み込み中の処理の数を取得する
        /// </summary>
        /// <returns>非同期読み込み中の件数</returns>
        public static int GetASyncLoadNum() 
            => DX.GetASyncLoadNum();
        #endregion

        #endregion

        #region ☆　アーカイブファイル関連

        #region - SetDXArchiveKeyString : ＤＸアーカイブファイルの鍵文字列を設定する
        /// <summary>
        /// ＤＸアーカイブファイルの鍵文字列を設定する
        /// </summary>
        /// <param name="key">鍵文字列</param>
        /// <returns>True :  成功 / False : 失敗</returns>
        public static bool SetDXArchiveKeyString(string key)
        {
            return DX.SetDXArchiveKeyString(key) == 0;
        }
        #endregion

        #region - SetUseDXArchiveFlag : ＤＸアーカイブファイルの読み込み機能を使うかどうかを設定する
        /// <summary>
        /// ＤＸアーカイブファイルの読み込み機能を使うかどうかを設定する
        /// </summary>
        /// <param name="sw">フラグ True: 読み込み機能を使用する / False: 使用しない</param>
        /// <returns>True :  成功 / False : 失敗</returns>
        public static bool SetUseDXArchiveFlag(bool sw)
        {
            return DX.SetUseDXArchiveFlag(DXBool(sw)) == 0;
        }
        #endregion

        #endregion

        #region ☆　その他

        #region - Distance : 2点間の距離を算出する
        /// <summary>
        /// 2点間の距離を算出する
        /// </summary>
        /// <param name="x1">点1のX座標</param>
        /// <param name="y1">点1のY座標</param>
        /// <param name="x2">点2のX座標</param>
        /// <param name="y2">点2のY座標</param>
        /// <returns>距離</returns>
        public static double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }
        #endregion

        #region - Distance : 2点間の距離を算出する
        /// <summary>
        /// 2点間の距離を算出する
        /// </summary>
        /// <param name="p1">点1の座標</param>
        /// <param name="p2">点2の座標</param>
        /// <returns>距離</returns>
        public static double Distance(Point p1, Point p2)
        {
            return Distance(p1.X, p1.Y, p2.X, p2.Y);
        }
        #endregion

        #region - SaveDrawScreen : 現在描画対象になっている画面をＢＭＰ形式で保存する
        /// <summary>
        /// 現在描画対象になっている画面をＢＭＰ形式で保存する
        /// </summary>
        /// <param name="x1">保存対象の左上X座標(px)</param>
        /// <param name="y1">保存対象の左上Y座標(px)</param>
        /// <param name="x2">保存対象の右下X座標(px)</param>
        /// <param name="y2">保存対象の右下Y座標(px)</param>
        /// <param name="fileName">保存先のファイル名</param>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool SaveDrawScreen(int x1, int y1, int x2, int y2, string fileName)
            => DX.SaveDrawScreen(x1, y1, x2, y2, fileName) == 0;
        #endregion

        #region - SaveDrawScreen : 現在描画対象になっている画面をＢＭＰ形式で保存する
        /// <summary>
        /// 現在描画対象になっている画面をＢＭＰ形式で保存する
        /// </summary>
        /// <param name="leftTop">保存対象の左上座標</param>
        /// <param name="rightBottom">保存対象の右下座標</param>
        /// <param name="fileName">保存先のファイル名</param>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool SaveDrawScreen(Point leftTop, Point rightBottom, string fileName)
            => DX.SaveDrawScreen(leftTop.X, leftTop.Y, rightBottom.X, rightBottom.Y, fileName) == 0;
        #endregion

        #region - SaveDrawScreen : 現在描画対象になっている画面をＢＭＰ形式で保存する
        /// <summary>
        /// 現在描画対象になっている画面をＢＭＰ形式で保存する
        /// </summary>
        /// <param name="rect">保存対象の矩形</param>
        /// <param name="fileName">保存先のファイル名</param>
        /// <returns>True: 成功 / False: 失敗</returns>
        public static bool SaveDrawScreen(Rectangle rect, string fileName)
            => DX.SaveDrawScreen(rect.X, rect.Y, rect.X2, rect.Y2, fileName) == 0;
        #endregion

        #endregion

        #endregion
    }
    #endregion
}
