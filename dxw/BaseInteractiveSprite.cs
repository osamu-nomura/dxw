using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static dxw.Helper;

namespace dxw
{
    #region 【Class : BaseInteractiveSprite】
    /// <summary>
    /// 対話型スプライトクラス
    /// </summary>
    public class BaseInteractiveSprite : BaseSprite
    {
        #region ■ Properties

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

        #region - TouchPoint : タッチされた座標
        /// <summary>
        /// タッチされた座標
        /// </summary>
        public Point? TouchPoint { get; set; } = null;
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

        #region - IsHover : マウスカーソルが領域内にある？
        /// <summary>
        /// マウスカーソルが領域内にある？
        /// </summary>
        public bool IsHover
        {
            get
            {
                if (App?.MousePoint != null)
                    return CheckPointInRegion(App.MousePoint.Value);
                return false;
            }
        }
        #endregion

        #endregion

        #region ■ Constructor

        #region - Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        /// <param name="app">アプリケーション</param>
        public BaseInteractiveSprite(BaseApplication app)
            : base(app)
        {
        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクタ(2)
        /// </summary>
        /// <param name="scene">シーン</param>
        public BaseInteractiveSprite(BaseScene scene)
            : base(scene)
        {
        }
        #endregion

        #region - Constructor(3)
        /// <summary>
        /// コンストラクタ(3)
        /// </summary>
        /// <param name="parent">親スプライト</param>
        public BaseInteractiveSprite(BaseSprite parent)
            : base(parent)
        {

        }
        #endregion

        #endregion

        #region ■ Protected Methods

        #region - TouchDown : タッチ or マウスで押された
        /// <summary>
        /// タッチ or マウスで押された
        /// </summary>
        protected virtual void TouchDown()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - TouchUp : タッチ or マウスが離された
        /// <summary>
        /// タッチ or マウスが離された
        /// </summary>
        public virtual void TouchUp()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - TouchLeave : タッチ or マウスが領域を外れた
        /// <summary>
        /// タッチ or マウスが領域を外れた
        /// </summary>
        public virtual void TouchLeave()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - TouchContinue : タッチ or マウスダウンが継続中
        /// <summary>
        /// タッチ or マウスダウンが継続中
        /// </summary>
        public virtual void TouchContinue()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

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
            TouchPoint = null;
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

            if (App == null || Disabled)
                return;
            
            if (TouchId == null)
            {
                // タッチ or 左マウスがボタン上で押された？
                var input = App.Inputs.FirstOrDefault(i => CheckPointInRegion(i.Point) && i.IsMouseLeftButtonDown);
                if (input != null)
                {
                    TouchId = input.Id;
                    TouchPoint = input.Point;
                    TouchStartTime = App?.ElapsedTime;
                    TouchDown();
                }
            }
            else
            {
                // タッチ済なら、タッチIDをトラッキングする
                var input = App.Inputs.FirstOrDefault(i => i.Id == TouchId);
                if (input != null)
                {
                    // 領域から外れた！
                    if (!CheckPointInRegion(input.Point) || !input.IsMouseLeftButtonDown)
                    {
                        TouchLeave();
                        TouchId = null;
                        TouchPoint = null;
                        TouchStartTime = null;
                    }
                    else
                    {
                        // 領域内ならタッチ座標を更新する
                        TouchPoint = input.Point;
                        TouchContinue();
                    }
                }
                else
                {
                    // 指が離された！
                    TouchUp();
                    TouchId = null;
                    TouchPoint = null;
                    TouchStartTime = null;
                }
            }
        }
        #endregion

        #endregion
    }
    #endregion
}
