using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace dxw
{
    #region 【BaseApplication】
    /// <summary>
    /// 基底アプリケーションクラス
    /// </summary>
    public class BaseApplication
    {
        #region ■ Members

        #region - _measuredTime : 基準計測時間
        /// <summary>
        /// 基準計測時間(ms)
        /// </summary>
        private int _measuredTime = 0;
        #endregion

        #region - _flipKeyBuff : キー入力保持用バッファ
        /// <summary>
        /// キー入力保持用バッファ
        /// </summary>
        private byte[] _flipKeyBuff = new byte[256];
        #endregion

        #region - _flopKeyBuff : キー入力保持用バッファ
        /// <summary>
        /// キー入力保持用バッファ
        /// </summary>
        private byte[] _flopKeyBuff = new byte[256];
        #endregion

        #region - _keyDowns : 押下されたキー
        /// <summary>
        /// 押下されたキー
        /// </summary>
        private byte[] _keyDowns = new byte[256];
        #endregion

        #region - _keyUps : 離されたキー
        /// <summary>
        /// 離されたキー
        /// </summary>
        private byte[] _keyUps = new byte[256];
        #endregion

        #endregion

        #region ■ Properties

        #region - ScreenWidth : 画面サイズ 幅
        /// <summary>
        /// 画面サイズ 幅（ピクセル）
        /// </summary>
        public int ScreenWidth { get; private set; }
        #endregion

        #region - ScreenHeight : 画面サイズ 高さ
        /// <summary>
        /// 画面サイズ 高さ（ピクセル）
        /// </summary>
        public int ScreenHeight { get; private set; }
        #endregion

        #region - IsTerminate : アプリケーション終了フラグ
        /// <summary>
        /// アプリケーション終了フラグ
        /// </summary>
        public bool IsTerminate { get; set; } = false;
        #endregion

        #region - _flipFlop : フリップフロップ値
        /// <summary>
        /// フリップフロップ値 - フレーム単位で反転する
        /// </summary>
        public bool FlipFlop { get; private set; } = true;
        #endregion

        #region - ElapsedTime : 経過時間
        /// <summary>
        /// アプリケーション開始からの経過時間(ms)
        /// </summary>
        public ulong ElapsedTime { get; private set; } = 0L;
        #endregion

        #region - FPS : フレーム数/秒
        /// <summary>
        /// フレーム数/秒
        /// </summary>
        public double FPS { get; private set; } = 0d;
        #endregion

        #endregion

        #region ■ Delegates

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="screenWidth">画面幅(ピクセル)</param>
        /// <param name="screenHeight">画面高さ(ピクセル)</param>
        /// <param name="colorBitDepth">色進度(BIT)</param>
        public BaseApplication(int screenWidth = 640, int screenHeight = 480, ColorBitDepth colorBitDepth = ColorBitDepth.BitDepth32)
        {
            DX.SetGraphMode(screenWidth, screenHeight, (int)colorBitDepth);
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }
        #endregion

        #region ■ Private Methods

        #region - UpdateElapsedTime : 経過時間を更新
        /// <summary>
        /// アプリケーション開始からの経過時間を更新する
        /// </summary>
        private void UpdateElapsedTime()
        {
            var t = DX.GetNowCount();
            var wrap = (t >= _measuredTime) ? (t - _measuredTime) : (int.MaxValue - _measuredTime + t);
            ElapsedTime += (ulong)wrap;
            FPS = (wrap != 0) ? 1000.0d / wrap : 0.0d;
            _measuredTime = t;
        }
        #endregion

        #region - UpdateKeyState : キーボードの状態を取得する
        /// <summary>
        /// キーボードの状態を取得する
        /// </summary>
        public void UpdateKeyState()
        {
            var prevKeyBuff = FlipFlop ? _flopKeyBuff : _flipKeyBuff;
            var curKeyBuff = FlipFlop ? _flipKeyBuff : _flopKeyBuff;

            DX.GetHitKeyStateAll(out curKeyBuff[0]);
            for (var i = 0; i < 256; i++)
            {
                _keyDowns[i] = 0;
                _keyUps[i] = 0;
                if (prevKeyBuff[i] != curKeyBuff[i])
                {
                    if (curKeyBuff[i] == 1)
                        _keyDowns[i] = 1;
                    else
                        _keyUps[i] = 1;
                }
            }
        }
        #endregion

        #endregion

        #region ■ Protected Methods

        #region - Update : 更新処理
        /// <summary>
        /// 更新処理
        /// </summary>
        protected virtual void Update()
        {

        }
        #endregion

        #region - DrawFrame : フレームを描画
        /// <summary>
        /// フレームを描画
        /// </summary>
        protected virtual void DrawFrame()
        {

        }
        #endregion

        #endregion

        #region ■ Public Methods

        #region - Run : アプリケーションの実行
        /// <summary>
        /// アプリケーションの実行
        /// </summary>
        /// <param name="windowMode">Windowモードで起動する</param>
        public virtual void Run(WindowMode windowMode)
        {
            // Windowモードの設定
            DX.ChangeWindowMode((int)windowMode);

            // DXライブラリの初期化
            if (DX.DxLib_Init() == -1)
                throw new ApplicationException("DX Library initialize error!");

            try
            {
                // 裏画面に描画するよう設定
                DX.SetDrawScreen(DX.DX_SCREEN_BACK);

                _measuredTime = DX.GetNowCount(); 

                // メッセージループ
                while (DX.ProcessMessage() == 0 && !IsTerminate)
                {
                    // 状態の更新
                    UpdateElapsedTime();
                    UpdateKeyState();

                    // 描画画面をクリア
                    DX.ClearDrawScreen();

                    // 更新処理
                    Update();

                    // フレームを描画
                    DrawFrame();

                    // 裏画面を表画面に転送
                    DX.ScreenFlip();

                    FlipFlop = !FlipFlop;
                }
            }
            finally
            {
                // DXライブラリの終了処理
                DX.DxLib_End();
            }
        }
        #endregion

        #region - Quit : アプリケーションを終了する
        /// <summary>
        /// アプリケーションを終了する
        /// </summary>
        public virtual void Quit()
        {
            IsTerminate = true;
        }
        #endregion

        #region - CheckHitKey : キーボードが押下されているかチェック
        /// <summary>
        /// キーボードが押下されているかチェック
        /// </summary>
        /// <param name="keyCode">キーコード</param>
        /// <returns>True ;  押下されている / False : されていない</returns>
        public bool CheckHitKey(int keyCode)
        {
            var curKeyBuff = FlipFlop ? _flipKeyBuff : _flopKeyBuff;
            if (keyCode >= 0 && keyCode < 256)
                return curKeyBuff[keyCode] == 1;
            else
                return false;
        }
        #endregion

        #region - CheckOnKeyDown : キーボードが押下されたかチェック
        /// <summary>
        /// キーボードが押下されたかチェック
        /// </summary>
        /// <param name="keyCode">キーコード</param>
        /// <returns>True ;  押下されている / False : されていない</returns>
        public bool CheckOnKeyDown(int keyCode)
        {
            if (keyCode >= 0 && keyCode < 256)
                return _keyDowns[keyCode] == 1;
            else
                return false;
        }
        #endregion

        #region - CheckOnKeyUp : キーボードが離されたかチェック
        /// <summary>
        /// キーボードが離されたかチェック
        /// </summary>
        /// <param name="keyCode">キーコード</param>
        /// <returns>True ;  押下されている / False : されていない</returns>
        public bool CheckOnKeyUp(int keyCode)
        {
            if (keyCode >= 0 && keyCode < 256)
                return _keyUps[keyCode] == 1;
            else
                return false;
        }
        #endregion

        #endregion
    }
    #endregion
}
