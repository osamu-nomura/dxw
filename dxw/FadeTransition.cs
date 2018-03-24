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
        #region ■ Properties

        #region - TransitionTime : トランジション時間
        /// <summary>
        /// トランジション時間
        /// </summary>
        private ulong TransitionTime { get; set; }
        #endregion

        #region - StartTime : 開始時間
        /// <summary>
        /// 開始時間
        /// </summary>
        private ulong StartTime { get; set; }
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="prev">直前シーン</param>
        /// <param name="next">次シーン</param>
        /// <param name="time">トランジション時間</param>
        public FadeTransition(BaseScene prev, BaseScene next, ulong time) : base(prev, next)
        {
            TransitionTime = time;
            StartTime = App.ElapsedTime;
        }
        #endregion

        #region - Proc : トランジション処理
        /// <summary>
        /// トランジション処理
        /// </summary>
        /// <returns>True : 処理継続 / False : 終了</returns>
        public override bool Proc()
        {
            // 経過時間を算出
            var elapsedTime = App.ElapsedTime - StartTime;

            // トランジション時間に達した場合は処理終了
            if (elapsedTime > TransitionTime)
            {
                SetDrawBright(255, 255, 255);
                return false;
            }

            if (elapsedTime < TransitionTime / 2)
            {
                // 前半は直前シーンをフェードアウト
                var rate = elapsedTime / (double)(TransitionTime / 2);
                var bright = 255 - (int)Math.Ceiling(255.0 * rate);
                SetDrawBright(bright, bright, bright);
                var hGraph = GetPrevSceneGraph();
                DrawGraph(0, 0, hGraph, false);
                System.Diagnostics.Debug.WriteLine($"ElapsedTime:{elapsedTime} / Rate:{rate} / Bright:{bright}");
            }
            else
            {
                // 後半は次シーンをフェードイン
                var rate = (TransitionTime - elapsedTime) / (double)(TransitionTime / 2);
                var bright = 255 - (int)Math.Ceiling(255.0 * rate);
                SetDrawBright(bright, bright, bright);
                var hGraph = GetNextSceneGraph();
                DrawGraph(0, 0, hGraph, false);
            }
            return true;
        }
        #endregion
    }
    #endregion
}
