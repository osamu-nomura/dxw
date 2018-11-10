using System;
using static dxw.Helper;

namespace dxw
{
    #region 【Class : PushButton】
    /// <summary>
    ///  画像プッシュボタンクラス
    /// </summary>
    public class PushButton : BaseInteractiveSprite
    {
        #region ■ Members
        /// <summary>
        /// 標準画像
        /// </summary>
        private int _imageHandle = 0;

        /// <summary>
        /// サイズを画像サイズに合わせる。
        /// </summary>
        private bool _fitSize = true;
        #endregion

        #region ■ Properties

        #region - TappedSoundHandle : タップ音
        /// <summary>
        /// タップ音
        /// </summary>
        public int TappedSoundHandle { get; set; } = 0;
        #endregion

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
        public int PushedImageHandle { get; set; } = 0;
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

        #region ■ Delegate

        #region - OnTapped : タップされた
        /// <summary>
        /// タップされた
        /// </summary>
        public Action<PushButton> OnTapped = null;
        #endregion

        #region - OnDraw : スプライトを描画する
        /// <summary>
        /// スプライトを描画する
        /// </summary>
        public Action<PushButton> OnDraw = null;
        #endregion

        #region - OnDrawEffect : 効果を描画する
        /// <summary>
        /// 効果を描画する
        /// </summary>
        public Action<PushButton> OnDrawEffect = null;
        #endregion

        #endregion

        #region ■ Constructor

        #region - Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        /// <param name="app">アプリケーション</param>
        public PushButton(BaseApplication app)
            : base(app)
        {

        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクタ(2)
        /// </summary>
        /// <param name="scene">シーン</param>
        public PushButton(BaseScene scene)
            : base(scene)
        {

        }
        #endregion

        #region - Constructor(3)
        /// <summary>
        /// コンストラクター(3)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="x">座標(px)</param>
        /// <param name="imageHandle">通常画像</param>
        /// <param name="pushedImageHandle">押下時の画像</param>
        /// <param name="callback">タップされた時のコールバック</param>
        public PushButton(BaseScene scene, Point pt, int imageHandle, int pushedImageHandle, 
                            Action<PushButton> callback = null) 
                : base(scene)
        {
            LeftTop = pt;
            ImageHandle = imageHandle;
            PushedImageHandle = pushedImageHandle;
            OnTapped = callback;
        }
        #endregion

        #endregion

        #region ■ Protected Methods

        #region - TouchUp : タッチ or マウスが離された
        /// <summary>
        /// タッチ or マウスが離された
        /// </summary>
        public override void TouchUp()
        {
            base.TouchUp();
            if (TappedSoundHandle != 0)
                PlaySound(TappedSoundHandle, PlayType.Back, App?.SEVolume ?? 0);
            OnTapped?.Invoke(this);
        }
        #endregion

        #endregion

        #region ■ Public Methods

        #region - Draw ; ボタンを描画する
        /// <summary>
        /// ボタンを描画する
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            if (OnDraw != null)
                OnDraw(this);
            else
                DrawGraph(X, Y, IsDown ? PushedImageHandle : ImageHandle, true);
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
