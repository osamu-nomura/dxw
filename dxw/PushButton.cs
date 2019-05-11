using System;
using System.Collections.Generic;
using hsb.Extensions;
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
        /// サイズを画像サイズに合わせる。
        /// </summary>
        private bool _fitSize = true;
        #endregion

        #region ■ Properties

        #region - State : ステート
        /// <summary>
        /// ステート
        /// </summary>
        public bool? State { get; set; } = null;
        #endregion

        #region - TappedSoundHandle : タップ音
        /// <summary>
        /// タップ音
        /// </summary>
        public int TappedSoundHandle { get; set; } = 0;
        #endregion

        #region - ImageHandles : 標準画像(ステート別）
        /// <summary>
        /// 標準画像(ステート別）
        /// </summary>
        public Dictionary<bool, int> ImageHandles { get; private set; } = new Dictionary<bool, int>();
        #endregion

        #region - PushedImageHandles : 押下時画像(ステート別)
        /// <summary>
        /// 押下時画像(ステート別)
        /// </summary>
        public Dictionary<bool, int> PushedImageHandles { get; private set; } = new Dictionary<bool, int>();
        #endregion

        #region - ImageHandle : 標準画像
        /// <summary>
        /// 標準画像
        /// </summary>
        public int ImageHandle
        {
            get { return ImageHandles.Get(false, 0);  }
            set
            {
                if (ImageHandles.Get(false, 0) != value)
                {
                    ImageHandles[false] = value;
                    if (value != 0 && FitButtonSize)
                        SetImageSize(value);
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
            get { return PushedImageHandles.Get(false, 0);  }
            set { PushedImageHandles[false] = value;  }
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
                        SetImageSize(ImageHandle);
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
        /// コンストラクタ(3)
        /// </summary>
        /// <param name="parent">親スプライト</param>
        public PushButton(BaseSprite parent)
            : base(parent)
        {
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
                PlaySound(TappedSoundHandle, PlayType.Back, App?.SEVolume ?? 50);

            if (State.HasValue)
                State = !State.Value;

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
            {
                var h = IsDown ? PushedImageHandles.Get(State ?? false, PushedImageHandle) : ImageHandles.Get(State ?? false, ImageHandle);
                DrawGraph(X, Y, h, true);
            }
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
