using System;
using System.Collections.Generic;
using System.Linq;
using hsb.Extensions;

using static dxw.Helper;


namespace dxw
{
    #region 【Class : BaseSlider】
    /// <summary>
    /// スライダークラス
    /// </summary>
    public class Slider : BaseInteractiveSprite
    {
        #region ■ Members
        /// <summary>
        /// 値
        /// </summary>
        private int _value = 0;
        /// <summary>
        /// ステップ数
        /// </summary>
        private int? _step = null;
        #endregion

        #region ■ Properties

        #region - Value : 値
        /// <summary>
        /// 値
        /// </summary>
        public int Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    UpdateHandlePosition();
                }
            }
        }
        #endregion

        #region - Min : 最小値
        /// <summary>
        /// 最小値
        /// </summary>
        public int Min { get; set; } = 0;
        #endregion

        #region - Max : 最大値
        /// <summary>
        /// 最大値
        /// </summary>
        public int Max { get; set; } = 100;
        #endregion

        #region - Step : ステップ数
        /// <summary>
        /// ステップ数
        /// </summary>
        public int? Step
        {
            get { return _step; }
            set
            {
                if (_step != value)
                {
                    _step = value;
                    Value = NormalizeValue(Value);
                }
            }
        }
        #endregion

        #region - Orientation : 方向
        /// <summary>
        /// 方向
        /// </summary>
        public Orientation Orientation { get; set; } = Orientation.Horizontal;
        #endregion

        #region - Handle : ハンドル
        /// <summary>
        /// ハンドル
        /// </summary>
        public BaseSprite Handle { get; set; } = null;
        #endregion

        #region - BackgroundImage : 背景イメージ
        /// <summary>
        /// 背景イメージ
        /// </summary>
        public int? BackgroundImage { get; set; } = null;
        #endregion

        #endregion

        #region ■ Delegate

        #region - OnValueChange : 値が変更された
        /// <summary>
        /// 値が変更された
        /// </summary>
        public Action<Slider> OnValueChange { get; set; } = null;
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        ///  コンストラクター
        /// </summary>
        public Slider(BaseScene scene) 
                : base(scene)
        {
            Value = 0;
            Step = null;
        }
        #endregion

        #region ■ Private Methods

        #region - CalcValue : 入力座標からスライダーの値を算出する
        /// <summary>
        /// 入力座標からスライダーの値を算出する
        /// </summary>
        /// <param name="point">座標</param>
        /// <returns>スライダー値</returns>
        private int CalcValue(Point point)
        {
            if (Orientation == Orientation.Horizontal)
            {
                var pos = point.X - X;
                if (pos == 0)
                    return Min;
                else if (pos == Width)
                    return Max;
                else
                    return Convert.ToInt32(Math.Floor(pos * ((double)(Max - Min) / Width)));
            }
            else
            {
                var pos = point.Y - Y;
                if (pos == 0)
                    return Min;
                else if (pos == Height)
                    return Max;
                else
                    return Convert.ToInt32(Math.Floor(pos * ((double)(Max - Min) / Height)));
            }
        }
        #endregion

        #region - NormalizePoint : 座標をエリア内に丸める
        /// <summary>
        /// 座標をエリア内に丸める
        /// </summary>
        /// <param name="point">座標</param>
        /// <returns>丸めた座標</returns>
        private Point NormalizePoint(Point point)
        {
            var x = (point.X < X) ? X : (point.X > X2 ? X2 : point.X);
            var y = (point.Y < Y) ? Y : (point.Y > Y2 ? Y2 : point.Y);
            return new Point(x, y);
        }
        #endregion

        #region - NormalizeValue : 値をStep値に応じて丸める
        /// <summary>
        /// 値をStep値に応じて丸める
        /// </summary>
        /// <param name="value">値</param>
        /// <returns>Stepに合わせた値</returns>
        public int NormalizeValue(int value)
        {
            //Stepが指定されていなければそのままの値
            if (!Step.HasValue)
                return value;

            var mod = Value % Step.Value;
            return Value - mod + ((mod > (Step / 2)) ? Step.Value : 0);
        }
        #endregion

        #endregion

        #region ■ Protected Methods

        #region - UpdateHandlePosition : ハンドル位置の更新
        /// <summary>
        /// ハンドル位置の更新
        /// </summary>
        protected void UpdateHandlePosition()
        {
            if (Handle == null)
                return;

            if (Orientation == Orientation.Horizontal)
            {
                if (Value <= Min)
                    Handle.X = X - (Handle.Width / 2);
                else if (Value >= Max)
                    Handle.X = X2 - (Handle.Width / 2);
                else
                {
                    var x = Convert.ToInt32(Math.Floor(Value * ((double)Width / (Max - Min))));
                    if (x > Width) x = Width;
                    Handle.X =  X + x - (Handle.Width / 2);
                }
                Handle.Y = Y + (Height / 2) - (Handle.Height / 2);
            }
            else
            {
                Handle.X = X + (Width / 2) - (Handle.Width / 2);
                if (Value == Min)
                    Handle.Y = Y - (Handle.Height / 2);
                else if (Value == Max)
                    Handle.Y = Y2 - (Handle.Height / 2);
                else
                {
                    var y = Convert.ToInt32(Math.Floor(Value * ((double)Height / (Max - Min))));
                    if (y > Height) y = Height;
                    Handle.Y = Y + y + (Handle.Height / 2);
                }
            }
        }
        #endregion

        #region - ValueChanged : 値が変更された
        /// <summary>
        /// 値が変更された
        /// </summary>
        protected virtual void ValueChanged()
            => OnValueChange?.Invoke(this);
        #endregion

        #region - TouchDown : タッチ or マウスで押された
        /// <summary>
        /// タッチ or マウスで押された
        /// </summary>
        protected override void TouchDown()
        {
            base.TouchDown();
            if (TouchPoint.HasValue)
                Value = CalcValue(TouchPoint.Value);
        }
        #endregion

        #region - TouchUp : タッチ or マウスが離された
        /// <summary>
        /// タッチ or マウスが離された
        /// </summary>
        public override void TouchUp()
        {
            base.TouchUp();
            if (TouchPoint.HasValue)
            {
                Value = NormalizeValue(Value);
                ValueChanged();
            }
        }
        #endregion

        #region - TouchLeave : タッチ or マウスが領域を外れた
        /// <summary>
        /// タッチ or マウスが領域を外れた
        /// </summary>
        public override void TouchLeave()
        {
            base.TouchLeave();
            if (TouchPoint.HasValue)
            {
                Value = NormalizeValue(CalcValue(NormalizePoint(TouchPoint.Value)));
                ValueChanged();
            }
        }
        #endregion

        #region - TouchContinue : タッチ or マウスダウンが継続中
        /// <summary>
        /// タッチ or マウスダウンが継続中
        /// </summary>
        public override void TouchContinue()
        {
            base.TouchContinue();
            if (TouchPoint.HasValue)
            {
                Value = CalcValue(TouchPoint.Value);
            }
        }
        #endregion

        #region - Draw : スライダーを描画する
        /// <summary>
        /// スライダーを描画する
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            if (BackgroundImage.HasValue)
                DrawGraph(LeftTop, BackgroundImage.Value, true);
            Handle?.Draw();
        }
        #endregion

        #region - DrawEffect : 効果を描画する
        /// <summary>
        /// 効果を描画する
        /// </summary>
        public override void DrawEffect()
        {
            base.DrawEffect();
            Handle?.DrawEffect();
        }
        #endregion

        #endregion
    }
    #endregion
}
