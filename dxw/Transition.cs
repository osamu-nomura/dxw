using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static dxw.Helper;

namespace dxw
{
    #region 【Class : Transition】
    /// <summary>
    /// 汎用トランジションクラス
    /// </summary>
    public class Transition : BaseTransition
    {
        #region ■ Members
        /// <summary>
        /// トランジション処理本体
        /// </summary>
        private readonly Action<ulong> _update;
        /// <summary>
        /// トランジション終了時処理
        /// </summary>
        private readonly Action _endTransition;
        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="prev">直前シーン</param>
        /// <param name="next">次シーン</param>
        /// <param name="time">トランジション時間</param>
        /// <param name="updateAction">トランジション処理本体</param>
        /// <param name="endTransitionAction">トランジション終了時処理</param>
        public Transition(BaseScene prev, BaseScene next, ulong time, Action<ulong> updateAction, Action endTransitionAction = null)
            : base (prev, next, time)
        {
            _update = updateAction;
            _endTransition = endTransitionAction;
        }
        #endregion

        #region ■ Protected Methods

        #region - Update : トランジション処理
        /// <summary>
        /// トランジション処理
        /// </summary>
        /// <param name="elapsedTime">経過時間</param>
        protected override void Update(ulong elapsedTime)
        {
            base.Update(elapsedTime);
            _update?.Invoke(elapsedTime);
        }
        #endregion

        #region - EndTransition : トランジション終了
        /// <summary>
        /// トランジション終了
        /// </summary>
        protected override void EndTransition()
        {
            base.EndTransition();
            _endTransition?.Invoke();
        }
        #endregion

        #endregion
    }
    #endregion
}
