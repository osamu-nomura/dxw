using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace dxw
{
    #region 【Class ; BaseApplication】
    /// <summary>
    /// 基底アプリケーションクラス
    /// </summary>
    public class BaseApplication
    {
        #region ■ Internal Classes

        #region 【Class : Task】
        /// <summary>
        /// タスククラス
        /// </summary>
        private class Task
        {
            #region ■ Properties

            #region - Id : ID値
            /// <summary>
            /// ID値
            /// </summary>
            public string Id { get; set; }
            #endregion

            #region - RequestTime : リクエスト時間
            /// <summary>
            /// リクエスト時間
            /// </summary>
            public ulong RequestTime { get; set; }
            #endregion

            #region - Proc : タスクプロセス
            /// <summary>
            /// タスクプロセス
            /// </summary>
            public Func<string, ulong, BaseApplication, bool> Proc { get; set; }
            #endregion

            #endregion
        }
        #endregion

        #endregion


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

        #region - _taskList : 処理要求リクエストリスト
        /// <summary>
        /// 処理要求リクエストリスト
        /// </summary>
        private List<Task> _taskList { get; set; } = new List<Task>();
        #endregion

        #region - _systemFontHandle : システムフォントハンドル
        /// <summary>
        /// システムフォントハンドル
        /// </summary>
        private int _systemFontHandle = 0;
        #endregion

        #region - _colorWhite : 白色
        /// <summary>
        /// 白色
        /// </summary>
        private uint _colorWhite = 0;
        #endregion

        #region - _colorBlack : 黒色
        /// <summary>
        /// 黒色
        /// </summary>
        private uint _colorBlack = 0;
        #endregion

        #region - _requestPause : 一時停止要求フラグ
        /// <summary>
        /// 一時停止要求フラグ
        /// </summary>
        private bool _requestPause = false;
        #endregion

        #region - _currentScene : 現在描画対象シーン
        /// <summary>
        /// 現在描画対象シーン
        /// </summary>
        private BaseScene _currentScene = null;
        #endregion

        #region - _requestSceneNo : シーン変更要求No
        /// <summary>
        /// シーン変更要求No
        /// </summary>
        public int? _requestSceneNo = null;
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

        #region - IsShowSystemInformation : システム情報表示フラグ
        /// <summary>
        /// システム情報表示フラグ
        /// </summary>
        public bool IsShowSystemInformation { get; set; }
        #endregion

        #region - IsShowInputStatus : 入力ステータス情報表示フラグ
        /// <summary>
        /// 入力ステータス情報表示フラグ
        /// </summary>
        public bool IsShowInputStatus { get; set; } = false;
        #endregion

        #region - Inputs : 入力情報
        /// <summary>
        /// 入力情報
        /// </summary>
        public List<InputInfo> Inputs { get; private set; } = new List<InputInfo>();
        #endregion

        #region - IsPause : 一時停止中フラグ
        /// <summary>
        /// 一時停止中フラグ
        /// </summary>
        public bool IsPause { get; private set; } = false;
        #endregion

        #region - Scenes : シーンリスト
        /// <summary>
        /// シーンリスト
        /// </summary>
        protected List<BaseScene> Scenes { get; set; } = new List<BaseScene>();
        #endregion

        #region - CurrentScene : 現在描画対象のシーン
        /// <summary>
        /// 現在描画対象のシーン
        /// </summary>
        public BaseScene CurrentScene
        {
            get { return _currentScene; }
            set
            {
                if (_currentScene != value)
                {
                    _currentScene?.DettachCurrent(value);
                    var prevScene = _currentScene;
                    _currentScene = value;
                    _currentScene?.AttachiCurrent(prevScene);
                    SceneChanged(prevScene, _currentScene);
                }
            }
        }
        #endregion

        #region - SEVolume : 効果音音量
        /// <summary>
        /// 効果音音量
        /// </summary>
        public int SEVolume { get; set; }
        #endregion

        #region - BGMVolue : BGM音量
        /// <summary>
        /// BGM音量
        /// </summary>
        public int BGMVolume { get; set; }
        #endregion

        #region - SystemFont : システムフォント
        /// <summary>
        /// システムフォント
        /// </summary>
        public int SystemFont { get { return _systemFontHandle; } }
        #endregion

        #region - ColorWhite : 白色
        /// <summary>
        /// 白色
        /// </summary>
        public uint ColorWhite { get { return _colorWhite; } }
        #endregion

        #region - ColorBlack : 黒色
        /// <summary>
        /// 黒色
        /// </summary>
        public uint ColorBlack { get { return _colorBlack; } }
        #endregion

        #region - Random : 乱数発生器
        /// <summary>
        /// 乱数発生器
        /// </summary>
        public Random Random { get; private set; } = new Random();
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
            // 一時停止中なら更新はなし
            if (IsPause)
                return;

            var t = DX.GetNowCount();
            var wrap = (t >= _measuredTime) ? (t - _measuredTime) : (int.MaxValue - _measuredTime + t);
            ElapsedTime += (ulong)wrap;
            FPS = (wrap != 0) ? 1000.0d / wrap : 0.0d;
            _measuredTime = t;
        }
        #endregion

        #region - UpdateKeyState : キーボードの状態を更新する
        /// <summary>
        /// キーボードの状態を更新する
        /// </summary>
        public void UpdateKeyState()
        {
            var prevKeyBuff = FlipFlop ? _flopKeyBuff : _flipKeyBuff;
            var curKeyBuff = FlipFlop ? _flipKeyBuff : _flopKeyBuff;

            DX.GetHitKeyStateAll(curKeyBuff);
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

        #region - UpdateInputState : タッチ＆マウスの入力状態を更新する
        /// <summary>
        /// タッチ＆マウスの入力状態を更新する
        /// </summary>
        public void UpdateInputState()
        {
            int x, y, id, device, buttons;

            Inputs.Clear();
            for (var i = 0; i < DX.GetTouchInputNum(); i++)
            {
                DX.GetTouchInput(i, out x, out y, out id, out device);
                Inputs.Add(new InputInfo(DeviceType.Touch, id, x, y, DX.MOUSE_INPUT_LEFT));
            }
            // マウスの状態を取得
            if ((buttons = DX.GetMouseInput()) != 0)
            {
                DX.GetMousePoint(out x, out y);
                Inputs.Add(new InputInfo(DeviceType.Mouse, 0, x, y, buttons));
            }
        }
        #endregion

        #region - ShowSystemInformation : システム情報を表示
        /// <summary>
        /// システム情報を表示
        /// </summary>
        private void ShowSystemInformation()
        {
            DX.DrawStringToHandle(5, 5, $"FPS:{FPS:##0.0} ELAPS TIME:{ElapsedTime:#,##0}ms", _colorWhite, _systemFontHandle);
            if (IsShowInputStatus)
            {
                var keyBuff = (FlipFlop) ? _flipKeyBuff : _flopKeyBuff;
                DX.DrawStringToHandle(5, 25, $"KeyBuff:{string.Join("", keyBuff.Select(n => n.ToString()))}", _colorWhite, _systemFontHandle);
                for (var i = 0; i < Inputs.Count; i++)
                {
                    var device = (Inputs[i].Device == DeviceType.Touch) ? "Touch" : "Mouse";
                    DX.DrawStringToHandle(5, 45 + (i * 20), $"input;[{device}] (X:{Inputs[i].X} Y:{Inputs[i].Y})", _colorWhite, _systemFontHandle);
                }
            }
        }
        #endregion

        #region - LoadResource : リソースのロード処理を行う。
        /// <summary>
        /// リソースのロード処理を行う。
        /// </summary>
        private void LoadResource()
        {
            // 事前に読み込みたいリソースをロードする
            PreLoading();

            // リソースを非同期でロードする
            DX.SetUseASyncLoadFlag(DX.TRUE);

            var continueLoadiing = true;
            var startTime = ElapsedTime;
            while (DX.ProcessMessage() == 0 && !IsTerminate)
            {
                UpdateElapsedTime();

                // ロード処理
                var loadingElapsedTime = ElapsedTime - startTime;
                if (continueLoadiing)
                    continueLoadiing = Loading(loadingElapsedTime);
                else if (DX.GetASyncLoadNum() == 0)
                    break;

                // 描画画面をクリア
                DX.ClearDrawScreen();
                // ローディング中画面の描画
                DrawLodingFrame(loadingElapsedTime);
            }
            LoadCompleted();

            // 非同期読み込みを解除
            DX.SetUseASyncLoadFlag(DX.FALSE);

        }
        #endregion

        #endregion

        #region ■ Protected Methods

        #region - Init : 初期化処理
        /// <summary>
        /// DXライブラリ初期化後に行う必要のある初期化処理
        /// </summary>
        protected virtual  void Init()
        {
            _systemFontHandle = DX.CreateFontToHandle("Meiryo", 14, 3, DX.DX_FONTTYPE_ANTIALIASING);
            _colorWhite = DX.GetColor(255, 255, 255);
            _colorBlack = DX.GetColor(0, 0, 0);
        }
        #endregion

        #region - SceneChanged : シーンが変更された
        /// <summary>
        /// シーンが変更された
        /// </summary>
        /// <param name="prevScene">直前のシーン</param>
        /// <param name="nextScene">次のシーン</param>
        protected virtual void SceneChanged(BaseScene prevScene, BaseScene nextScene)
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - PreLoading: 事前ローディング処理
        /// <summary>
        /// 事前ローディング処理
        /// </summary>
        protected virtual void PreLoading()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - Loading : リソースのロード処理
        /// <summary>
        /// リソースのロード処理
        /// </summary>
        /// <param name="elapsedTime">経過時間(ms)</param>
        /// <returns>True ; ロード処理の継続 / False : ロード処理の終了</returns>
        protected virtual bool Loading(ulong elapsedTime)
        {
            return false;
        }
        #endregion

        #region - DrawLodingFrame : ローディング中のフレームを描画
        /// <summary>
        /// ローディング中のフレームを描画
        /// </summary>
        /// <param name="elapsedTime">経過時間(ms)</param>
        protected virtual void DrawLodingFrame(ulong elapsedTime)
        {
            // 派生クラスでオーバーライド
        }
        #endregion

        #region - LoadCompleted : リソースのロードが完了
        /// <summary>
        /// リソースのロードが完了
        /// </summary>
        protected virtual void LoadCompleted()
        {
            Scenes.ForEach(scene => scene.LoadCompleted());
        }
        #endregion

        #region - UpdateFrame : フレーム更新処理
        /// <summary>
        /// 更新処理
        /// </summary>
        protected virtual void UpdateFrame()
        {
            CurrentScene?.UpdateFrame();
        }
        #endregion

        #region - DrawFrame : フレームを描画
        /// <summary>
        /// フレームを描画
        /// </summary>
        protected virtual void DrawFrame()
        {
            CurrentScene?.DrawFrame();
        }
        #endregion

        #region - MessageLoopBeginRound : ループ前処理
        /// <summary>
        /// ループ前処理
        /// </summary>
        protected virtual void MessageLoopBeginRound()
        {
            // 状態の更新
            UpdateElapsedTime();
            UpdateKeyState();
            UpdateInputState();

            // シーン変更
            if (_requestSceneNo.HasValue)
            {
                CurrentScene = Scenes[_requestSceneNo.Value];
                _requestSceneNo = null;
            }

            // 描画画面をクリア
            DX.ClearDrawScreen();
        }
        #endregion

        #region - MessageLoopEndRound : ループ後処理
        /// <summary>
        /// ループ後処
        /// </summary>
        protected virtual void MessageLoopEndRound()
        {
            // システム情報表示
            if (IsShowSystemInformation)
                ShowSystemInformation();

            // 裏画面を表画面に転送
            DX.ScreenFlip();

            FlipFlop = !FlipFlop;

            // 一時停止処理
            if (_requestPause && !IsPause)
                IsPause = true;
            if (!_requestPause && IsPause)
            {
                IsPause = false;
                _measuredTime = DX.GetNowCount();
            }
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
                // 初期化処理
                Init();

                // 裏画面に描画するよう設定
                DX.SetDrawScreen(DX.DX_SCREEN_BACK);

                _measuredTime = DX.GetNowCount();

                // リソースのロード
                LoadResource();

                // カレントシーンの設定
                _requestSceneNo = (Scenes.Count > 0) ? (int?)0 : null;

                // メッセージループ
                while (DX.ProcessMessage() == 0 && !IsTerminate)
                {
                    // 前処理
                    MessageLoopBeginRound();

                    // フレーム更新処理
                    UpdateFrame();

                    // タスクの実行
                    _taskList.RemoveAll(task => task.Proc.Invoke(task.Id, task.RequestTime, this));

                    // フレームを描画
                    DrawFrame();

                    // 後処理
                    MessageLoopEndRound();
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

        #region - Pause : アプリケーションを一時停止させる
        /// <summary>
        /// アプリケーションを一時停止させる
        /// </summary>
        public void Pause()
        {
            _requestPause = true;
        }
        #endregion

        #region - Resume : アプリケーションを一時停止から再開する
        /// <summary>
        /// アプリケーションを一時停止から再開する
        /// </summary>
        public void Resume()
        {
            _requestPause = false;
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

        #region - HitKeys : キーボードが押下されているキーのキーコードを取得する
        /// <summary>
        /// キーボードが押下されているキーのキーコードを取得する
        /// </summary>
        /// <returns>押下されているキーコードのリスト</returns>
        public List<int> HitKeys()
        {
            var curKeyBuff = FlipFlop ? _flipKeyBuff : _flopKeyBuff;
            var buff = new List<int>();
            for (var i = 0; i < 255; i++)
            {
                if (curKeyBuff[i] == 1)
                    buff.Add(i);
            }
            return buff;
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

        #region - GetHitNumberKey : 押下された数字キーを返す
        /// <summary>
        /// 押下された数字キーを数値で返す
        /// </summary>
        /// <returns>押下された数字</returns>
        public string GetHitNumberKey()
        {
            if (CheckOnKeyUp(DX.KEY_INPUT_0) || CheckOnKeyUp(DX.KEY_INPUT_NUMPAD0))
                return "0";
            if (CheckOnKeyUp(DX.KEY_INPUT_1) || CheckOnKeyUp(DX.KEY_INPUT_NUMPAD1))
                return "1";
            if (CheckOnKeyUp(DX.KEY_INPUT_2) || CheckOnKeyUp(DX.KEY_INPUT_NUMPAD2))
                return "2";
            if (CheckOnKeyUp(DX.KEY_INPUT_3) || CheckOnKeyUp(DX.KEY_INPUT_NUMPAD3))
                return "3";
            if (CheckOnKeyUp(DX.KEY_INPUT_4) || CheckOnKeyUp(DX.KEY_INPUT_NUMPAD4))
                return "4";
            if (CheckOnKeyUp(DX.KEY_INPUT_5) || CheckOnKeyUp(DX.KEY_INPUT_NUMPAD5))
                return "5";
            if (CheckOnKeyUp(DX.KEY_INPUT_6) || CheckOnKeyUp(DX.KEY_INPUT_NUMPAD6))
                return "6";
            if (CheckOnKeyUp(DX.KEY_INPUT_7) || CheckOnKeyUp(DX.KEY_INPUT_NUMPAD7))
                return "7";
            if (CheckOnKeyUp(DX.KEY_INPUT_8) || CheckOnKeyUp(DX.KEY_INPUT_NUMPAD8))
                return "8";
            if (CheckOnKeyUp(DX.KEY_INPUT_9) || CheckOnKeyUp(DX.KEY_INPUT_NUMPAD9))
                return "9";
            return null;
        }
        #endregion

        #region - AddTask : タスクの追加
        /// <summary>
        /// タスクの追加
        /// </summary>
        /// <param name="proc">リクエストタスク</param>
        public string AddTask(Func<string, ulong, BaseApplication, bool> proc)
        {
            var guid = Guid.NewGuid().ToString();
            _taskList.Add(new Task { Id = guid, RequestTime = ElapsedTime, Proc = proc });
            return guid;
        }
        #endregion

        #region - RemoveTask : タスクの削除
        /// <summary>
        /// タスクの削除
        /// </summary>
        /// <param name="taskId">タスクID</param>
        public void RemoveTask(string taskId)
        {
            _taskList.RemoveAll(task => task.Id == taskId);
        }
        #endregion

        #region - ChangeScene : シーンを変更する
        /// <summary>
        /// シーンを変更する
        /// </summary>
        /// <param name="sceneNo">シーン番号</param>
        public void ChangeScene(int sceneNo)
        {
            _requestSceneNo = sceneNo;
        }
        #endregion

        #endregion
    }
    #endregion
}
