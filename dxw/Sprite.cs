
using System;
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

        #region - Motion : スプライトのモーション定義
        /// <summary>
        /// スプライトのモーション定義
        /// </summary>
        public ISpriteMotion Motion { get; set; } = null;
        #endregion

        #endregion

        #region ■ Delegate

        #region - OnUpdate : 状態を更新する
        /// <summary>
        /// 状態を更新する
        /// </summary>
        public Action<Sprite> OnUpdate = null;
        #endregion

        #region - OnCollision : 他のスプライトが衝突した
        /// <summary>
        /// 他のスプライトが衝突した
        /// </summary>
        public Action<Sprite, BaseSprite> OnCollision = null;
        #endregion

        #region - OnDraw : スプライトを描画する
        /// <summary>
        /// スプライトを描画する
        /// </summary>
        public Action<BaseSprite> OnDraw = null;
        #endregion

        #region - OnDrawEffect : 効果を描画する
        /// <summary>
        /// 効果を描画する
        /// </summary>
        public Action<BaseSprite> OnDrawEffect = null;
        #endregion

        #endregion

        #region ■ Constructor

        #region - Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        /// <param name="app">アプリケーション</param>
        /// <param name="rect">矩形</param>
        public Sprite(BaseApplication app, Rectangle rect = null)
            : base(app, rect)
        {
        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクタ(2)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="rect">矩形</param>
        public Sprite(BaseScene scene, Rectangle rect = null)
            : base(scene, rect)
        {
        }
        #endregion

        #region - Constructor(3)
        /// <summary>
        /// コンストラクタ(3)
        /// </summary>
        /// <param name="parent">親スプライト</param>
        /// <param name="rect">矩形</param>
        public Sprite(BaseSprite parent, Rectangle rect = null)
            : base (parent, rect)
        {

        }
        #endregion

        #region - Constructor(4)
        /// <summary>
        /// コンストラクタ(4)
        /// </summary>
        /// <param name="app">アプリケーション</param>
        /// <param name="leftTop">左上座標</param>
        /// <param name="imageHandle">画像ハンドル</param>
        /// <param name="callback">コールバック</param>
        public Sprite(BaseApplication app, Point leftTop, int imageHandle, Action<Sprite> callback = null)
            : this(app)
        {
            LeftTop = leftTop;
            ImageHandle = imageHandle;
            OnUpdate = callback;
        }
        #endregion

        #region - Constructor(5)
        /// <summary>
        /// コンストラクタ(5)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="leftTop">左上座標</param>
        /// <param name="imageHandle">画像ハンドル</param>
        /// <param name="callback">コールバック</param>
        public Sprite(BaseScene scene, Point leftTop, int imageHandle, Action<Sprite> callback = null)
            : this(scene)
        {
            LeftTop = leftTop;
            ImageHandle = imageHandle;
            OnUpdate = callback;
        }
        #endregion

        #region - Constructor(6)
        /// <summary>
        /// コンストラクタ(6)
        /// </summary>
        /// <param name="parent">親スプライト</param>
        /// <param name="leftTop">左上座標</param>
        /// <param name="imageHandle">画像ハンドル</param>
        /// <param name="callback">コールバック</param>
        public Sprite(BaseSprite parent, Point leftTop, int imageHandle, Action<Sprite> callback = null)
            : this(parent)
        {
            LeftTop = leftTop;
            ImageHandle = imageHandle;
            OnUpdate = callback;
        }
        #endregion

        #region - Constructor(7)
        /// <summary>
        /// コンストラクタ(7)
        /// </summary>
        /// <param name="app">アプリケーション</param>
        /// <param name="leftTop">左上座標</param>
        /// <param name="imageHandle">画像ハンドル</param>
        /// <param name="motion">モーション定義</param>
        public Sprite(BaseApplication app, Point leftTop, int imageHandle, ISpriteMotion motion)
            : this(app)
        {
            LeftTop = leftTop;
            ImageHandle = imageHandle;
            Motion = motion;
        }
        #endregion

        #region - Constructor(8)
        /// <summary>
        /// コンストラクタ(8)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="leftTop">左上座標</param>
        /// <param name="imageHandle">画像ハンドル</param>
        /// <param name="motion">モーション定義</param>
        public Sprite(BaseScene scene, Point leftTop, int imageHandle, ISpriteMotion motion)
            : this(scene)
        {
            LeftTop = leftTop;
            ImageHandle = imageHandle;
            Motion = motion;
        }
        #endregion

        #region - Constructor(9)
        /// <summary>
        /// コンストラクタ(9)
        /// </summary>
        /// <param name="parent">親スプライト</param>
        /// <param name="leftTop">左上座標</param>
        /// <param name="imageHandle">画像ハンドル</param>
        /// <param name="motion">モーション定義</param>
        public Sprite(BaseSprite parent, Point leftTop, int imageHandle, ISpriteMotion motion)
            : this(parent)
        {
            LeftTop = leftTop;
            ImageHandle = imageHandle;
            Motion = motion;
        }
        #endregion        #endregion

        #endregion

        #region ■ Public Methods

        #region - Update : 状態を更新する
        /// <summary>
        /// 状態を更新する
        /// </summary>
        public override void Update()
        {
            base.Update();
            if (Motion != null)
                Motion.Update(this);
            else
                OnUpdate?.Invoke(this);
        }
        #endregion

        #region - Colision : 他のスプライトが衝突した
        /// <summary>
        /// 他のスプライトが衝突した
        /// </summary>
        /// <param name="target">対象スプライト/param>
        public override void Collision(BaseSprite target)
        {
            base.Collision(target);
            if (Motion != null)
                Motion.Colision(this, target);
            else
                OnCollision?.Invoke(this, target);
        }
        #endregion

        #region - Draw : スプライトを描画する
        /// <summary>
        /// スプライトを描画する
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            if (OnDraw != null)
                OnDraw(this);
            else
                DrawGraph(X, Y, ImageHandle, true);
        }
        #endregion

        #region - DrawEffect : 効果を描画する
        /// <summary>
        /// 効果を描画する
        /// </summary>
        public override void DrawEffect()
        {
            base.DrawEffect();
            OnDrawEffect?.Invoke(this);
        }
        #endregion

        #endregion
    }
    #endregion
}
