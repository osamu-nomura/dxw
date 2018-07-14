using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static dxw.Helper;

namespace dxw
{
    #region 【Class : SlideTransition】
    /// <summary>
    /// スライドトランジション
    /// </summary>
    public class SlideTransition : BaseTransition
    {
        #region ■ Properties

        #region - Orientation : 方向
        /// <summary>
        /// 方向
        /// </summary>
        private TransitionOrientation Orientation { get; set; }
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="prev">直前シーン</param>
        /// <param name="next">次シーン</param>
        /// <param name="time">トランジション時間</param>
        /// <param name="orientation">方向</param>
        public SlideTransition(BaseScene prev, BaseScene next, ulong time, TransitionOrientation orientation) 
            : base(prev, next, time)
        {
            Orientation = orientation;
        }
        #endregion

        #region ■ Protected Methods

        #region - Update : 更新処理
        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="elapsedTime">経過時間</param>
        protected override void Update(ulong elapsedTime)
        {
            base.Update(elapsedTime);

            DrawGraph(0, 0, GetPrevSceneGraph(), false);
            var rate = elapsedTime / (double)TransitionTime;

            var x = 0;
            var y = 0;
            switch (Orientation)
            {
                case TransitionOrientation.Up:
                    y = App.ScreenHeight - (int)Math.Ceiling(App.ScreenHeight * rate);
                    break;
                case TransitionOrientation.Down:
                    y = (App.ScreenHeight * -1) + (int)Math.Ceiling(App.ScreenHeight * rate);
                    break;
                case TransitionOrientation.Left:
                    x = App.ScreenWidth - (int)Math.Ceiling(App.ScreenWidth * rate);
                    break;
                case TransitionOrientation.Right:
                    x = (App.ScreenWidth * -1) + (int)Math.Ceiling(App.ScreenWidth * rate);
                    break;
            }
            DrawGraph(x, y, GetNextSceneGraph(), false);
        }

        #endregion

        #endregion
    }
    #endregion
}
