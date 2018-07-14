using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static dxw.Helper;

namespace dxw
{
    #region 【Class : FadeTransition】
    /// <summary>
    /// 暗転トランジション
    /// </summary>
    public class FadeTransition : BaseTransition
    {
        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="prev">直前シーン</param>
        /// <param name="next">次シーン</param>
        /// <param name="time">トランジション時間</param>
        public FadeTransition(BaseScene prev, BaseScene next, ulong time) : base(prev, next, time)
        {
            // 特にすることはない
        }
        #endregion

        #region ■ Protected Methods

        #region - EndTransition : トランジション終了
        /// <summary>
        /// トランジション終了
        /// </summary>
        protected override void EndTransition()
        {
            base.EndTransition();
            SetDrawBright(255, 255, 255);
        }
        #endregion

        #region - Update : トランジション処理
        /// <summary>
        /// トランジション処理
        /// </summary>
        protected override void Update(ulong elapsedTime)
        {
            base.Update(elapsedTime);
            if (elapsedTime < TransitionTime / 2)
            {
                // 前半は直前シーンをフェードアウト
                var hGraph = GetPrevSceneGraph();
                var rate = elapsedTime / (double)(TransitionTime / 2);
                var bright = 255 - (int)Math.Ceiling(255.0 * rate);
                System.Diagnostics.Debug.WriteLine($"ElapsedTime: {elapsedTime} Rate: {rate} Bright: {bright}");
                SetDrawBright(bright, bright, bright);
                DrawGraph(0, 0, hGraph, false);
            }
            else
            {
                // 後半は次シーンをフェードイン
                var hGraph = GetNextSceneGraph();
                var rate = (TransitionTime - elapsedTime) / (double)(TransitionTime / 2);
                var bright = 255 - (int)Math.Ceiling(255.0 * rate);
                System.Diagnostics.Debug.WriteLine($"ElapsedTime: {elapsedTime} Rate: {rate} Bright: {bright}");
                SetDrawBright(bright, bright, bright);
                DrawGraph(0, 0, hGraph, false);
            }
        }
        #endregion

        #endregion
    }
    #endregion
}
