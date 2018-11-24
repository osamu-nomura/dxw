﻿
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

        #region - Vector : ベクトル
        /// <summary>
        /// ベクトル
        /// </summary>
        public Vector? Vector { get; set; } = null;
        #endregion

        #endregion

        #region ■ Delegate

        #region - OnUpdate : 状態を更新する
        /// <summary>
        /// 状態を更新する
        /// </summary>
        public Action<Sprite> OnUpdate = null;
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
        public Sprite(BaseApplication app)
            : base(app)
        {
        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクタ(2)
        /// </summary>
        /// <param name="scene">シーン</param>
        public Sprite(BaseScene scene)
            : base(scene)
        {
        }
        #endregion

        #region - Constructor(3)
        /// <summary>
        /// コンストラクタ(3)
        /// </summary>
        /// <param name="app">アプリケーション</param>
        /// <param name="leftTop">左上座標</param>
        /// <param name="vec">ベクトル</param>
        /// <param name="imageHandle">画像ハンドル</param>
        /// <param name="callback">コールバック</param>
        public Sprite(BaseApplication app, Point leftTop, Vector? vec, int imageHandle, Action<Sprite> callback = null)
            : this(app)
        {
            LeftTop = leftTop;
            Vector = vec;
            ImageHandle = imageHandle;
            OnUpdate = callback;
        }
        #endregion

        #region - Constructor(4)
        /// <summary>
        /// コンストラクタ(4)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="leftTop">左上座標</param>
        /// <param name="vec">ベクトル</param>
        /// <param name="imageHandle">画像ハンドル</param>
        /// <param name="callback">コールバック</param>
        public Sprite(BaseScene scene, Point leftTop, Vector? vec, int imageHandle, Action<Sprite> callback = null)
            : this(scene)
        {
            LeftTop = leftTop;
            Vector = vec;
            ImageHandle = imageHandle;
            OnUpdate = callback;
        }
        #endregion

        #endregion

        #region ■ Public Methods

        #region - Update : 状態を更新する
        /// <summary>
        /// 状態を更新する
        /// </summary>
        public override void Update()
        {
            base.Update();
            OnUpdate?.Invoke(this);
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

        #region - NewPos : 経過時間に応じた新しい位置を取得する
        /// <summary>
        /// 経過時間に応じた新しい位置(LeftTop)を取得する
        /// </summary>
        /// <param name="s">経過時間</param>
        /// <returns>Point</returns>
        public Point NewPos(double s)
            => Vector.HasValue ? LeftTop + (Vector.Value * s) : LeftTop;
        #endregion

        #region - NewPos : 経過時間に応じた新しい位置を取得する
        /// <summary>
        /// 経過時間に応じた新しい位置(LeftTop)を取得する
        /// 指定した矩形に衝突した場合は反転する
        /// </summary>
        /// <param name="s">経過時間</param>
        /// <param name="range">範囲</param>
        /// <param name="r">反発係数</param>
        /// <returns>Point</returns>
        public Point NewPos(double s, Rectangle range, double r = 1.0)
        {
            if (!Vector.HasValue)
                return LeftTop;

            var pt = NewPos(s);
            var x = pt.X < 0 || range.Width < pt.X;
            var y = pt.Y < 0 || range.Height < pt.Y;
            if (x || y)
            {
                Vector = Vector.Value.Flip(x, y) * r;
                pt = NewPos(s);
            }
            return pt;
        }
        #endregion

        #endregion
    }
    #endregion
}
