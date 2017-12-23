using static dxw.Helper;

namespace dxw
{
    #region 【Class : Splite】
    /// <summary>
    /// スプライトクラス
    /// </summary>
    public class Sprite : BaseSprite
    {
        #region ■ Members
        /// <summary>
        /// 画像
        /// </summary>
        private int _imageHandle = 0;
        /// <summary>
        /// サイズを画像サイズに合わせる。
        /// </summary>
        private bool _fitSize = true;
        #endregion

        #region ■ Properties

        #region - ImageHandle : 画像
        /// <summary>
        /// 画像
        /// </summary>
        public int ImageHandle
        {
            get { return _imageHandle; }
            set
            {
                if (_imageHandle != value)
                {
                    _imageHandle = value;
                    if (FitSize)
                        SetImageSize(_imageHandle);
                }
            }
        }
        #endregion

        #region - FitSize : サイズを画像サイズに合わせる
        /// <summary>
        /// サイズを画像サイズに合わせる
        /// </summary>
        public bool FitSize
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
        public Sprite()
            : base()
        {
        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクタ(2)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="x">X座標(px)</param>
        /// <param name="y">Y座標(px)</param>
        /// <param name="imageHandle">画像ハンドル</param>
        public Sprite(BaseScene scene, int x, int y, int imageHandle)
            : base (scene, x, y, 0, 0)
        {
            ImageHandle = imageHandle;
        }
        #endregion

        #region - Constructor(3)
        /// <summary>
        /// /コンストラクタ(2)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="leftTop">座標(px)</param>
        /// <param name="imageHandle">画像ハンドル</param>
        public Sprite(BaseScene scene, Point leftTop, int imageHandle)
            : this(scene, leftTop.X, leftTop.Y, imageHandle)
        {
        }
        #endregion

        #endregion

        #region ■ Public Methods

        #region - Draw : スプライトを描画する
        /// <summary>
        /// スプライトを描画する
        /// </summary>
        public override void Draw()
        {
            DrawGraph(X, Y, ImageHandle, true);
        }
        #endregion

        #endregion
    }
    #endregion
}
