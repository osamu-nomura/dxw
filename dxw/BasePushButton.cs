using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static dxw.Helper;

namespace dxw
{
    #region 【Class : BasePushButton】
    /// <summary>
    /// プッシュボタンクラス
    /// </summary>
    public class BasePushButton : BaseSprite
    {
        #region ■ Properties

        #region - TouchableArea タッチ可能エリア
        /// <summary>
        /// 
        /// </summary>
        public List<Rectangle> TouchableArea { get; set; } = null;
        #endregion

        #region - TouchAreaIndex : タッチエリアインデックス
        /// <summary>
        /// タッチエリアインデックス
        /// </summary>
        public int? TouchAreaIndex { get; set; } = null;
        #endregion

        #region - TouchId : タップされたタッチID
        /// <summary>
        /// タップされたタッチID
        /// </summary>
        public int? TouchId { get; set; } = null;
        #endregion

        #region - TouchStartTime : タップされた時刻
        /// <summary>
        /// タップされた時刻
        /// </summary>
        public ulong? TouchStartTime { get; set; } = null;
        #endregion

        #region - TouchPosition : タッチされた座標
        /// <summary>
        /// タッチされた座標
        /// </summary>
        public Point? TouchPosition
        {
            get
            {
                if (TouchPositionX.HasValue && TouchPositionY.HasValue)
                    return new Point(TouchPositionX.Value, TouchPositionY.Value);
                return null;
            }
            set
            {
                if (value != null)
                {
                    TouchPositionX = value.Value.X;
                    TouchPositionY = value.Value.Y;
                }
                else
                {
                    TouchPositionX = null;
                    TouchPositionY = null;
                }
            }
        }
        #endregion

        #region - TouchPositionX : タップされたX座標(px)
        /// <summary>
        /// タップされたX座標(px)
        /// </summary>
        public int? TouchPositionX { get; set; } = null;
        #endregion

        #region - TouchPositionY : タップされたY座標(px)
        /// <summary>
        /// タップされたY座標(px)
        /// </summary>
        public int? TouchPositionY { get; set; } = null;
        #endregion

        #region - IsDown : タップ中？
        /// <summary>
        /// タップ中？
        /// </summary>
        public bool IsDown
        {
            get { return TouchStartTime.HasValue; }
        }
        #endregion

        #region - TappedSoundHandle : タップ音
        /// <summary>
        /// タップ音
        /// </summary>
        public int TappedSoundHandle { get; set; } = 0;
        #endregion

        #endregion

        #region ■ Constructor

        #region - Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        public BasePushButton()
            : base()
        {

        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        ///  コンストラクター(2)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="x">X座標(px)</param>
        /// <param name="y">Y座標(px)</param>
        /// <param name="width">幅(px)</param>
        /// <param name="height">高さ(px)</param>
        /// <param name="callback">タップされた時のコールバック</param>
        public BasePushButton(BaseScene scene, int x, int y, int width, int height, Action<BasePushButton> callback = null) 
                : base(scene, x, y, width, height)
        {
            if (callback != null)
                Tapped = callback;
        }
        #endregion

        #region - Constructor(3)
        /// <summary>
        /// コンストラクタ(3)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="rect">矩形(px)</param>
        /// <param name="callback">タップされた時のコールバック</param>
        public BasePushButton(BaseScene scene, Rectangle rect, Action<BasePushButton> callback = null)
            : this(scene, rect.X, rect.Y, rect.Width, rect.Height, callback)
        {

        }
        #endregion

        #region - Constructor(4)
        /// <summary>
        /// コンストラクタ(4)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="leftTop">左上座標(px)</param>
        /// <param name="size">矩形サイズ(px)</param>
        /// <param name="callback">タップされた時のコールバック</param>
        public BasePushButton(BaseScene scene, Point leftTop, RectangleSize size, Action<BasePushButton> callback = null)
            : this(scene, leftTop.X, leftTop.Y, size.Width, size.Height, callback)
        {

        }
        #endregion

        #region - Constructor(5)
        /// <summary>
        /// コンストラクタ(5)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="leftTop">左上座標(px)</param>
        /// <param name="rightButtom">右下座標(px)</param>
        /// <param name="callback">タップされた時のコールバック</param>
        public BasePushButton(BaseScene scene, Point leftTop, Point rightButtom, Action<BasePushButton> callback = null)
            : this(scene, new Rectangle(leftTop, rightButtom), callback)
        {

        }
        #endregion

        #endregion

        #region ■ Delegates

        #region - Tapped : ボタンがタップされた
        /// <summary>
        /// ボタンがタップされた
        /// </summary>
        public Action<BasePushButton> Tapped { get; set; } = null;
        #endregion

        #endregion

        #region ■ Protected Methods

        #region - ChangeEnabled : 有効無効が変更された
        /// <summary>
        /// 有効無効が変更された
        /// </summary>
        /// <param name="enabled">有効？</param>
        protected override void ChangeEnabled(bool enabled)
        {
            base.ChangeEnabled(enabled);
            TouchId = null;
            TouchStartTime = null;
        }
        #endregion

        #region - InternaleTapped : ボタンがタップされた（内部処理）
        /// <summary>
        /// ボタンがタップされた（内部処理）
        /// </summary>
        /// <returns>true : Tappedイベントを発生させる / false : 発生させない</returns>
        protected virtual bool InternaleTapped()
        {
            return true;
        }
        #endregion

        #endregion

        #region ■ Methods

        #region - Update : 状態を更新する。
        /// <summary>
        /// 状態を更新する。
        /// </summary>
        /// <param name="app">アプリケーション</param>
        public override void Update()
        {
            base.Update();

            if (Disabled)
                return;

            if (TouchId == null)
            {
                // タッチ or 左マウスがボタン上で押された？
                var input = Sceen.App.Inputs.FirstOrDefault(i => CheckPointInRegion(i.X, i.Y) && i.IsMouseLeftButtonDown);
                if (input != null)
                {
                    if (TouchableArea?.Count > 0)
                    {
                        var x = input.X - X;
                        var y = input.Y - Y;
                        var n = TouchableArea.FindIndex(r => r.CheckPointInRegion(x, y));
                        if (n > 0)
                        {
                            TouchAreaIndex = n;
                            TouchId = input.Id;
                            TouchStartTime = Sceen.App.ElapsedTime;
                            TouchPositionX = input.X;
                            TouchPositionY = input.Y;
                        }
                    }
                    else
                    {
                        TouchId = input.Id;
                        TouchStartTime = Sceen.App.ElapsedTime;
                        TouchPositionX = input.X;
                        TouchPositionY = input.Y;
                    }
                }
            }
            else
            {
                // タッチ済なら、タッチIDをトラッキングする
                var input = Sceen.App.Inputs.FirstOrDefault(i => i.Id == TouchId);
                if (input != null)
                {
                    if (TouchAreaIndex.HasValue)
                    {
                        // 領域から外れた！
                        if (!TouchableArea[TouchAreaIndex.Value].CheckPointInRegion(input.X, input.Y) || !input.IsMouseLeftButtonDown)
                        {
                            TouchAreaIndex = null;
                            TouchId = null;
                            TouchStartTime = null;
                            TouchPositionX = null;
                            TouchPositionY = null;
                        }
                        else
                        {
                            // 領域内ならタッチ座標を更新する
                            TouchPositionX = input.X;
                            TouchPositionY = input.Y;
                        }
                    }
                    else
                    {
                        // 領域から外れた！
                        if (!CheckPointInRegion(input.X, input.Y) || !input.IsMouseLeftButtonDown)
                        {
                            TouchId = null;
                            TouchStartTime = null;
                            TouchPositionX = null;
                            TouchPositionY = null;
                        }
                        else
                        {
                            // 領域内ならタッチ座標を更新する
                            TouchPositionX = input.X;
                            TouchPositionY = input.Y;
                        }
                    }
                }
                else
                {
                    // 指が離された！
                    if (TappedSoundHandle != 0)
                        PlaySound(TappedSoundHandle, PlayType.Back, Sceen.App.SEVolume);
                    if (InternaleTapped())
                        Tapped?.Invoke(this);
                    TouchAreaIndex = null;
                    TouchId = null;
                    TouchStartTime = null;
                    TouchPositionX = null;
                    TouchPositionY = null;
                }
            }
        }
        #endregion

        #endregion
    }
    #endregion
}
