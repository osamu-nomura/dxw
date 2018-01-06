using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using hsb.Extensions;

using static dxw.Helper;

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

        #region - _title : タイトル
        /// <summary>
        /// タイトル
        /// </summary>
        private string _title = string.Empty;
        #endregion

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

        #region - _requestSuspendUpdateFrame : フレーム更新停止要求フラグ
        /// <summary>
        /// フレーム更新停止要求フラグ
        /// </summary>
        private bool _requestSuspendUpdateFrame = false;
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
        private int? _requestSceneNo = null;
        #endregion

        #region - _requestScene : シーン変更要求シーン
        /// <summary>
        /// シーン変更要求シーン
        /// </summary>
        private BaseScene _requestScene = null;
        #endregion

        #region - _dialogScene : ダイアログシーン
        /// <summary>
        /// ダイアログシーン
        /// </summary>
        private BaseScene _dialogScene = null;
        #endregion

        #region - _isCallParentUpdateFrame : ダイアログ表示時親のUpdateFrameを呼び出す？
        /// <summary>
        /// ダイアログ表示時親のUpdateFrameを呼び出す？
        /// </summary>
        private bool _isCallParentUpdateFrame = false;
        #endregion

        #region - _archiveFilePassword : アーカイブファイルのパスワード
        /// <summary>
        /// アーカイブファイルのパスワード
        /// </summary>
        private string _archiveFilePassword = null;
        #endregion

        #region - _messageLoopPostProcessQueue : メッセージループ後処理キュー
        /// <summary>
        /// メッセージループ後処理キュー
        /// </summary>
        private Queue<Action> _messageLoopPostProcessQueue = new Queue<Action>();
        #endregion

        #endregion

        #region ■ Properties

        #region - Title : タイトル
        /// <summary>
        /// タイトル
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    SetMainWindowText(_title);
                }
            }
        }
        #endregion

        #region - WindowMode : ウィンドウモード
        /// <summary>
        /// ウィンドウモード
        /// </summary>
        public WindowMode WindowMode { get; private set; } = WindowMode.Window;
        #endregion

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

        #region - ScreenSize : 画面サイズ
        /// <summary>
        /// 画面サイズ
        /// </summary>
        public RectangleSize ScreenSize
        {
            get { return new RectangleSize(ScreenWidth, ScreenHeight);  }
        }
        #endregion

        #region - ScreenRect : 画面矩形
        /// <summary>
        /// 画面矩形
        /// </summary>
        public Rectangle ScreenRect
        {
            get { return new Rectangle(0, 0, ScreenWidth, ScreenHeight);  }
        }
        #endregion

        #region - IsTerminate : アプリケーション終了フラグ
        /// <summary>
        /// アプリケーション終了フラグ
        /// </summary>
        public bool IsTerminate { get; set; } = false;
        #endregion

        #region - FlipFlop : フリップフロップ値
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

        #region - MousePoint : マウス座標
        /// <summary>
        /// マウス座標
        /// </summary>
        public Point? MousePoint { get; private set; } = null;
        #endregion

        #region - IsPause : 一時停止中フラグ
        /// <summary>
        /// 一時停止中フラグ
        /// </summary>
        public bool IsPause { get; private set; } = false;
        #endregion

        #region - IsSuspendUpdateFrame : フレーム更新処理停止中フラグ
        /// <summary>
        /// フレーム更新処理停止中フラグ
        /// </summary>
        public bool IsSuspendUpdateFrame { get; private set; } = false;
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

        #region - IsLoadCompleted : ロードが完了した？
        /// <summary>
        /// ロードが完了した？
        /// </summary>
        public bool IsLoadCompleted { get; protected set; } = false;
        #endregion

        #region - ArchiveFilePassword : アーカイブファイルのパスワード
        /// <summary>
        /// アーカイブファイルのパスワード
        /// </summary>
        public string ArchiveFilePassword
        {
            get { return _archiveFilePassword; }
            set
            {
                if (_archiveFilePassword != value)
                {
                    _archiveFilePassword = value;
                    SetDXArchiveKeyString(_archiveFilePassword);
                }
            }
        }
        #endregion

        #endregion

        #region ■ Delegates

        #endregion

        #region ■ Constructor

        #region - Constructor(1)
        /// <summary>
        /// コンストラクター(1)
        /// </summary>
        /// <param name="screenWidth">画面幅(ピクセル)</param>
        /// <param name="screenHeight">画面高さ(ピクセル)</param>
        /// <param name="colorBitDepth">色深度(BIT)</param>
        public BaseApplication(int screenWidth = 640, int screenHeight = 480, ColorBitDepth colorBitDepth = ColorBitDepth.BitDepth32)
        {
            SetGraphMode(screenWidth, screenHeight, colorBitDepth);
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクター(2)
        /// </summary>
        /// <param name="screenWidth">画面幅(ピクセル)</param>
        /// <param name="screenHeight">画面高さ(ピクセル)</param>
        /// <param name="colorBitDepth">色深度(BIT)</param>
        public BaseApplication(RectangleSize screenSize, ColorBitDepth colorBitDepth = ColorBitDepth.BitDepth32)
        {
            SetGraphMode(screenSize, colorBitDepth);
            ScreenWidth = screenSize.Width;
            ScreenHeight = screenSize.Height;
        }
        #endregion

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

            var t = GetNowCount();
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

            if (GetHitKeyStateAll(curKeyBuff))
            {
                for (var i = 0; i < curKeyBuff.Length; i++)
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
        }
        #endregion

        #region - UpdateInputState : タッチ＆マウスの入力状態を更新する
        /// <summary>
        /// タッチ＆マウスの入力状態を更新する
        /// </summary>
        public void UpdateInputState()
        {
            Inputs.Clear();
            // タッチの状態を取得する
            Inputs.AddRange(GetTouchInputs());
            // マウスの状態を取得
            Inputs.AddWithoutNull(GetMouseInput());
            MousePoint = GetMousePoint();
        }
        #endregion

        #region - ShowSystemInformation : システム情報を表示
        /// <summary>
        /// システム情報を表示
        /// </summary>
        private void ShowSystemInformation()
        {
            DrawString(5, 5, $"FPS:{FPS:##0.0} ELAPS TIME:{ElapsedTime:#,##0}ms", _colorWhite, _systemFontHandle);
            if (IsShowInputStatus)
            {
                var keyBuff = (FlipFlop) ? _flipKeyBuff : _flopKeyBuff;
                DrawString(5, 25, $"KeyBuff:{string.Join("", keyBuff.Select(n => n.ToString()))}", _colorWhite, _systemFontHandle);
                for (var i = 0; i < Inputs.Count; i++)
                {
                    var device = (Inputs[i].Device == DeviceType.Touch) ? "Touch" : "Mouse";
                    DrawString(5, 45 + (i * 20), $"input;[{device}] (X:{Inputs[i].Point.X} Y:{Inputs[i].Point.Y} LeftButtonDown: {Inputs[i].IsMouseLeftButtonDown})", _colorWhite, _systemFontHandle);
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
            SetUseASyncLoadFlag(true);
            try
            {
                var continueLoadiing = true;
                var startTime = ElapsedTime;
                while (ProcessMessage() && !IsTerminate)
                {
                    UpdateElapsedTime();

                    // ロード処理
                    var loadingElapsedTime = ElapsedTime - startTime;
                    if (continueLoadiing)
                        continueLoadiing = Loading(loadingElapsedTime);
                    else if (GetASyncLoadNum() == 0)
                        break;

                    // 描画画面をクリア
                    ClearDrawScreen();
                    // ローディング中画面の描画
                    DrawLodingFrame(loadingElapsedTime);
                    ScreenFlip();
                }
                IsLoadCompleted = true;
                LoadCompleted();
            }
            finally
            {
                // 非同期読み込みを解除
               SetUseASyncLoadFlag(false);
            }
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
            _systemFontHandle = CreateFont("Meiryo", 14, 3, FontType.AntiAlias);
            _colorWhite = GetColor(255, 255, 255);
            _colorBlack = GetColor(0, 0, 0);
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
            if (!IsSuspendUpdateFrame)
            {
                if (_dialogScene != null)
                {
                    if (_isCallParentUpdateFrame)
                        CurrentScene?.UpdateFrame();
                    _dialogScene.UpdateFrame();
                }
                else
                    CurrentScene?.UpdateFrame();
            }
        }
        #endregion

        #region - DrawFrame : フレームを描画
        /// <summary>
        /// フレームを描画
        /// </summary>
        protected virtual void DrawFrame()
        {
            CurrentScene?.DrawFrame();
            _dialogScene?.DrawFrame();
        }
        #endregion

        #region - MessageLoopPreProcess : ループ前処理
        /// <summary>
        /// ループ前処理
        /// </summary>
        protected virtual void MessageLoopPreProcess()
        {
            // 状態の更新
            UpdateElapsedTime();
            UpdateKeyState();
            UpdateInputState();

            // シーン変更
            if (_requestScene != null)
            {
                CurrentScene = _requestScene;
                _requestScene = null;
            }
            else
            {
                if (_requestSceneNo.HasValue)
                {
                    CurrentScene = Scenes[_requestSceneNo.Value];
                    _requestSceneNo = null;
                }
            }

            // 描画画面をクリア
            ClearDrawScreen();
        }
        #endregion

        #region - MessageLoopPostProcess : ループ後処理
        /// <summary>
        /// ループ後処
        /// </summary>
        protected virtual void MessageLoopPostProcess()
        {
            // 追加された後処理を実行
            _messageLoopPostProcessQueue.ForEach(act => act.Invoke());
            _messageLoopPostProcessQueue.Clear();

            // システム情報表示
            if (IsShowSystemInformation)
                ShowSystemInformation();

            // 裏画面を表画面に転送
            ScreenFlip();

            FlipFlop = !FlipFlop;

            // 一時停止処理
            if (_requestPause && !IsPause)
                IsPause = true;
            if (_requestSuspendUpdateFrame && !IsSuspendUpdateFrame)
                IsSuspendUpdateFrame = true;
            if (!_requestPause && IsPause)
            {
                IsPause = false;
                _measuredTime = GetNowCount();
            }
            if (!_requestSuspendUpdateFrame && IsSuspendUpdateFrame)
                IsSuspendUpdateFrame = false;
        }
        #endregion

        #region - FillBackground : 背景を指定色で塗りつぶす
        /// <summary>
        /// 背景を指定色で塗りつぶす
        /// </summary>
        /// <param name="color">指定色</param>
        protected void FillBackground(uint color) 
            => DrawBox(ScreenRect, color, true);
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
            if (ChangeWindowMode(windowMode))
                WindowMode = windowMode;

            // DXライブラリの初期化
            if (!InitializeDxLibrary())
                throw new ApplicationException("DX Library initialize error!");

            try
            {
                // 初期化処理
                Init();

                // 裏画面に描画するよう設定
                SetDrawScreen(DrawScreen.Background);

                _measuredTime = GetNowCount();

                // リソースのロード
                LoadResource();

                // カレントシーンの設定
                _requestSceneNo = (Scenes.Count > 0) ? (int?)0 : null;

                // メッセージループ
                while (ProcessMessage() && !IsTerminate)
                {
                    // 前処理
                    MessageLoopPreProcess();

                    // フレーム更新処理
                    UpdateFrame();

                    // タスクの実行
                    _taskList.RemoveAll(task => task.Proc.Invoke(task.Id, task.RequestTime, this));

                    // フレームを描画
                    DrawFrame();

                    // 後処理
                    MessageLoopPostProcess();
                }
            }
            finally
            {
                // DXライブラリの終了処理
                TerminateDxLibrary();
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
        /// <param name="suspendFrameUpdate">フレームの更新処理も停止する</param>
        public void Pause(bool suspendFrameUpdate = false)
        {
            _requestPause = true;
            _requestSuspendUpdateFrame = suspendFrameUpdate;
        }
        #endregion

        #region - Resume : アプリケーションを一時停止から再開する
        /// <summary>
        /// アプリケーションを一時停止から再開する
        /// </summary>
        public void Resume()
        {
            _requestPause = false;
            _requestSuspendUpdateFrame = false;
        }
        #endregion

        #region - CheckHitKey : キーボードが押下されているかチェック
        /// <summary>
        /// キーボードが押下されているかチェック
        /// </summary>
        /// <param name="keyCode">キーコード</param>
        /// <returns>True ;  押下されている / False : されていない</returns>
        public bool CheckHitKey(KeyCode keyCode)
        {
            var curKeyBuff = FlipFlop ? _flipKeyBuff : _flopKeyBuff;
            if ((int)keyCode >= 0 && (int)keyCode < 256)
                return curKeyBuff[(int)keyCode] == 1;
            else
                return false;
        }
        #endregion

        #region - HitKeys : キーボードが押下されているキーのキーコードを取得する
        /// <summary>
        /// キーボードが押下されているキーのキーコードを取得する
        /// </summary>
        /// <returns>押下されているキーコードのリスト</returns>
        public List<KeyCode> HitKeys()
        {
            var curKeyBuff = FlipFlop ? _flipKeyBuff : _flopKeyBuff;
            return Enumerable.Range(0, 255).Where(n => curKeyBuff[n] == 1).Select(n => (KeyCode)n).ToList();
        }
        #endregion

        #region - CheckOnKeyDown : キーボードが押下されたかチェック
        /// <summary>
        /// キーボードが押下されたかチェック
        /// </summary>
        /// <param name="keyCode">キーコード</param>
        /// <returns>True ;  押下されている / False : されていない</returns>
        public bool CheckOnKeyDown(KeyCode keyCode)
        {
            if ((int)keyCode >= 0 && (int)keyCode < 256)
                return _keyDowns[(int)keyCode] == 1;
            else
                return false;
        }
        #endregion

        #region - GetKeyDowns : キーボードが押下されたキーのキーコードを取得する
        /// <summary>
        /// キーボードが押下されたキーのキーコードを取得する
        /// </summary>
        /// <returns>キーコードのリスト</returns>
        public List<KeyCode> GetKeyDowns()
        {
            return Enumerable.Range(0, 255).Where(n => _keyDowns[n] == 1).Select(n => (KeyCode)n).ToList();
        }
        #endregion

        #region - CheckOnKeyUp : キーボードが離されたかチェック
        /// <summary>
        /// キーボードが離されたかチェック
        /// </summary>
        /// <param name="keyCode">キーコード</param>
        /// <returns>True ;  押下されている / False : されていない</returns>
        public bool CheckOnKeyUp(KeyCode keyCode)
        {
            if ((int)keyCode >= 0 && (int)keyCode < 256)
                return _keyUps[(int)keyCode] == 1;
            else
                return false;
        }
        #endregion

        #region - GetKeyUps : キーボードが離されたキーのキーコードを取得する
        /// <summary>
        /// キーボードが離されたキーのキーコードを取得する
        /// </summary>
        /// <returns>キーコードのリスト</returns>
        public List<KeyCode> GetKeyUps()
        {
            return Enumerable.Range(0, 255).Where(n => _keyUps[n] == 1).Select(n => (KeyCode)n).ToList();
        }
        #endregion

        #region - GetHitNumberKey : 押下された数字キーを返す
        /// <summary>
        /// 押下された数字キーを数値で返す
        /// </summary>
        /// <returns>押下された数字</returns>
        public string GetHitNumberKey()
        {
            if (CheckOnKeyUp(KeyCode.KEY_0) || CheckOnKeyUp(KeyCode.KEY_NUMPAD0))
                return "0";
            if (CheckOnKeyUp(KeyCode.KEY_1) || CheckOnKeyUp(KeyCode.KEY_NUMPAD1))
                return "1";
            if (CheckOnKeyUp(KeyCode.KEY_2) || CheckOnKeyUp(KeyCode.KEY_NUMPAD2))
                return "2";
            if (CheckOnKeyUp(KeyCode.KEY_3) || CheckOnKeyUp(KeyCode.KEY_NUMPAD3))
                return "3";
            if (CheckOnKeyUp(KeyCode.KEY_4) || CheckOnKeyUp(KeyCode.KEY_NUMPAD4))
                return "4";
            if (CheckOnKeyUp(KeyCode.KEY_5) || CheckOnKeyUp(KeyCode.KEY_NUMPAD5))
                return "5";
            if (CheckOnKeyUp(KeyCode.KEY_6) || CheckOnKeyUp(KeyCode.KEY_NUMPAD6))
                return "6";
            if (CheckOnKeyUp(KeyCode.KEY_7) || CheckOnKeyUp(KeyCode.KEY_NUMPAD7))
                return "7";
            if (CheckOnKeyUp(KeyCode.KEY_8) || CheckOnKeyUp(KeyCode.KEY_NUMPAD8))
                return "8";
            if (CheckOnKeyUp(KeyCode.KEY_9) || CheckOnKeyUp(KeyCode.KEY_NUMPAD9))
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

        #region - AddMessageLoopPostProcess : メッセージループ後処理に処理を追加する
        /// <summary>
        /// メッセージループ後処理に処理を追加する
        /// </summary>
        /// <param name="act">処理</param>
        public void AddMessageLoopPostProcess(Action act)
        {
            _messageLoopPostProcessQueue.Enqueue(act);
        }
        #endregion

        #region - ChangeScene : シーンを変更する(1)
        /// <summary>
        /// シーンを変更する(1)
        /// </summary>
        /// <param name="sceneNo">シーン番号</param>
        public void ChangeScene(int sceneNo)
        {
            _requestSceneNo = sceneNo;
        }
        #endregion

        #region - ChangeScene : シーンを変更する(2)
        /// <summary>
        /// シーンを変更する(2)
        /// </summary>
        /// <param name="scene">シーン</param>
        public void ChangeScene(BaseScene scene)
        {
            _requestScene = scene;
        }
        #endregion

        #region - ShowDialog : ダイアログを表示する(1)
        /// <summary>
        /// ダイアログを表示する(1)
        /// </summary>
        /// <param name="dlgScene">ダイアログ用シーン</param>
        /// <param name="isCallParentUpdateFrame">ダイアログ表示時に親のUpdateFrameを呼び出す？</param>
        public void ShowDialog(BaseScene dlgScene, bool isCallParentUpdateFrame)
        {
            _dialogScene = dlgScene;
            _isCallParentUpdateFrame = isCallParentUpdateFrame;
            if (CurrentScene != null)
                CurrentScene.OnShowDialog(_dialogScene);
        }
        #endregion

        #region - ShowDialog : ダイアログを表示する(2)
        /// <summary>
        /// ダイアログを表示する(1)
        /// </summary>
        /// <param name="dlgSceneNo">ダイアログ用シーンNo</param>
        /// <param name="isCallParentUpdateFrame">ダイアログ表示時に親のUpdateFrameを呼び出す？</param>
        public void ShowDialog(int dlgSceneNo, bool isCallParentUpdateFrame)
        {
            ShowDialog(Scenes[dlgSceneNo], isCallParentUpdateFrame);
        }
        #endregion

        #region - HideDialog : ダイアログを非表示にする
        /// <summary>
        /// ダイアログを非表示にする
        /// </summary>
        public void HideDialog()
        {
            if (_dialogScene != null)
            {
                if (CurrentScene != null)
                    CurrentScene.OnHideDialog(_dialogScene);
                _dialogScene = null;
                _isCallParentUpdateFrame = false;
            }
        }
        #endregion

        #region - SaveSettings : 設定値を保存する
        /// <summary>
        /// 設定値を保存する
        /// </summary>
        public virtual void SaveSettings()
        {
            // 派生クラスで実装する
        }
        #endregion

        #region - SaveScreenImage : 画面をファイルに保存する
        /// <summary>
        /// 画面をファイルに保存する
        /// </summary>
        /// <param name="path">保存ファイル名</param>
        public void SaveScreenImage(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
            else
            {
                var directoryPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
            }
            SaveDrawScreen(0, 0, ScreenWidth, ScreenHeight, path);
        }
        #endregion

        #endregion
    }
    #endregion
}
