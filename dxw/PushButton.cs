using System;
using static dxw.Helper;

namespace dxw
{
    #region 【Class : PushButton】
    /// <summary>
    ///  画像プッシュボタンクラス
    /// </summary>
    class PushButton : BasePushButton
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
        /// サイズを画像サイズに合わせる。
        /// </summary>
        private bool _fitSize = true;
        #endregion

        #region ■ Properties

        #region - ImageHandle : 標準画像
        /// <summary>
        /// 標準画像
        /// </summary>
        public int ImageHandle
        {
            get { return _imageHandle;  }
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

        #region - FitButtonSize : ボタンのサイズを画像サイズに合わせる
        /// <summary>
        /// ボタンのサイズを画像サイズに合わせる
        /// </summary>
        public bool FitButtonSize
        {
            get { return _fitSize; }
            set
            {
                if (_fitSize != value)
                {
                    _fitSize = value;
                    if (_fitSize)
                        SetImageSize(_imageHandle);
                }
            }
        }
        #endregion

        #endregion

        #region ■ Constructor

        #region - Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        public PushButton()
            : base()
        {

        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクター(2)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="x">X座標(px)</param>
        /// <param name="y">Y座標(px)</param>ram>
        /// <param name="imageHandle">通常画像</param>
        /// <param name="pushedImageHandle">押下時の画像</param>
        /// <param name="callback">タップされた時のコールバック</param>
        public PushButton(BaseScene scene, int x, int y, int imageHandle, int pushedImageHandle, 
                            Action<BasePushButton> callback = null) 
                : base(scene, x, y, 0, 0, callback)
        {
            ImageHandle = imageHandle;
            PushedImageHandle = pushedImageHandle;
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
            DrawGraph(X, Y, IsDown ? PushedImageHandle : ImageHandle, true);
        }
        #endregion

        #endregion
    }
    #endregion
}
