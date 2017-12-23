using System;
using static dxw.Helper;


namespace dxw
{
    #region 【Class : ImageStateButton】
    /// <summary>
    /// 　画像ステートボタン
    /// </summary>
    public class ImageStateButton : BasePushButton
    {
        #region ■ Members
        /// <summary>
        /// 標準画像
        /// </summary>
        private int _imageHandle = 0;
        /// <summary>
        /// 押下時画像
        /// </summary>
        private int _pushedImageHandle = 0;
        /// <summary>
        /// 選択時画像
        /// </summary>
        private int _selectedImageHandle = 0;
        /// <summary>
        /// 選択・押下時画像
        /// </summary>
        private int _pushedSelectedImageHandle = 0;
        /// <summary>
        /// ボタンサイズを画像サイズに合わせる。
        /// </summary>
        private bool _fitButtonSize = false;
        #endregion

        #region ■ Properties

        #region - ImageHandle : 標準画像
        /// <summary>
        /// 標準画像
        /// </summary>
        public int ImageHandle
        {
            get { return _imageHandle; }
            set
            {
                if (_imageHandle != value)
                {
                    _imageHandle = value;
                    if (FitButtonSize)
                        SetImageSize(_imageHandle);
                }
            }
        }
        #endregion

        #region - PushedImageHandle : 押下時画像
        /// <summary>
        /// 押下時画像
        /// </summary>
        public int PushedImageHandle
        {
            get { return _pushedImageHandle; }
            set { _pushedImageHandle = value; }
        }
        #endregion

        #region - SelectedImageHandle : 選択時画像
        /// <summary>
        /// 選択時画像
        /// </summary>
        public int SelectedImageHandle
        {
            get { return _selectedImageHandle;  }
            set { _selectedImageHandle = value; }
        }
        #endregion

        #region - PushedSelectedImageHandle : 選択押下時画像
        /// <summary>
        /// 選択押下時画像
        /// </summary>
        public int PushedSelectedImageHandle
        {
            get { return _pushedSelectedImageHandle; }
            set { _pushedSelectedImageHandle = value; }
        }
        #endregion

        #region - Selected : 選択中？
        /// <summary>
        /// 選択中？
        /// </summary>
        public bool Selected { get; set; }
        #endregion

        #region - FitButtonSize : ボタンのサイズを背景画像サイズに合わせる
        /// <summary>
        /// ボタンのサイズを背景画像サイズに合わせる
        /// </summary>
        public bool FitButtonSize
        {
            get { return _fitButtonSize; }
            set
            {
                if (_fitButtonSize != value)
                {
                    _fitButtonSize = value;
                    if (_fitButtonSize)
                        SetImageSize(_imageHandle);
                }
            }
        }
        #endregion

        #endregion

        #region ■ Delegate

        #region - Initializer : 初期化処理
        /// <summary>
        /// 初期化処理 - アプリケーションのロード完了後に呼ばれる。
        /// </summary>
        private Action<ImageStateButton> Initializer { get; set; }
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="x">X座標(px)</param>
        /// <param name="y">Y座標(px)</param>ram>
        /// <param name="callback">コールバック - アプリケーションのロード完了後に呼ばれる</param>
        public ImageStateButton(BaseScene scene, int x, int y, Action<ImageStateButton> callback = null) 
                : base(scene, x, y, 0, 0)
        {
            Selected = false;
            Initializer = callback;
            if (scene?.App.IsLoadCompleted == true)
                Initializer?.Invoke(this);
        }
        #endregion

        #region ■ Protected Methods

        #region - InternaleTapped : ボタンがタップされた（内部処理）
        /// <summary>
        /// ボタンがタップされた（内部処理）
        /// </summary>
        /// <returns>true : Tappedイベントを発生させる / false : 発生させない</returns>
        protected override bool InternaleTapped()
        {
            Selected = !Selected;
            return true;
        }
        #endregion

        #endregion

        #region ■ Methods

        #region - Draw ; ボタンを描画する
        /// <summary>
        /// ボタンを描画する
        /// </summary>
        public override void Draw()
        {
            if (Selected)
                DrawGraph(X, Y, IsDown ? PushedSelectedImageHandle : SelectedImageHandle, true);
            else
                DrawGraph(X, Y, IsDown ? PushedImageHandle : ImageHandle, true);
        }
        #endregion

        #region - アプリケーションのロードが完了した。
        /// <summary>
        /// アプリケーションのロードが完了した。  
        /// </summary>
        public override void LoadCompleted()
        {
            base.LoadCompleted();

            // 初期化処理の実行
            Initializer?.Invoke(this);
        }
        #endregion

        #endregion
    }
    #endregion
}
