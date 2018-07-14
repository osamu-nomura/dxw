using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DxLibDLL;

using static dxw.Helper;

namespace dxw
{
    #region 【Abstract Calss : BaseTransition】
    /// <summary>
    /// 基底トランジションクラス
    /// </summary>
    public abstract class BaseTransition
    {
        #region ■ Properties

        #region - App : アプリケーション
        /// <summary>
        /// アプリケーション
        /// </summary>
        public BaseApplication App { get; set; }
        #endregion

        #region - PrevScene : 直前シーン
        /// <summary>
        /// 直前シーン
        /// </summary>
        public BaseScene PrevScene { get; set; }
        #endregion

        #region - NextScene : 次シーン
        /// <summary>
        /// 次シーン
        /// </summary>
        public BaseScene NextScene { get; set; }
        #endregion

        #region - TransitionTime : トランジション時間
        /// <summary>
        /// トランジション時間
        /// </summary>
        protected ulong TransitionTime { get; set; }
        #endregion

        #region - StartTime : 開始時間
        /// <summary>
        /// 開始時間
        /// </summary>
        protected ulong StartTime { get; set; }
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="prev">直前シーン</param>
        /// <param name="next">次シーン</param>
        /// <param name="time">トランジション時間</param>
        public BaseTransition(BaseScene prev, BaseScene next, ulong time)
        {
            App = prev.App;
            PrevScene = prev;
            NextScene = next;
            TransitionTime = time;
            StartTime = App.ElapsedTime;
        }
        #endregion

        #region ■ Proected Methods

        #region - GetPrevSceneGraph : 直前シーンの画面を取得
        /// <summary>
        /// 直前シーンの画面を取得
        /// </summary>
        /// <returns>グラフィックハンドル</returns>
        protected int GetPrevSceneGraph()
        {
            return CreateDrawableGraph(App.ScreenSize, true, () => { PrevScene?.DrawFrame(); });
        }
        #endregion

        #region - GetNextSceneGraph : 次シーンの画面を取得
        /// <summary>
        /// 次シーンの画面を取得
        /// </summary>
        /// <returns>グラフィックハンドル</returns>
        protected int GetNextSceneGraph()
        {
            return CreateDrawableGraph(App.ScreenSize, true, () => { NextScene?.DrawFrame(); });
        }
        #endregion

        #endregion

        #region ■ Protected Methods

        #region - BeginTransion : トランジション開始
        /// <summary>
        /// トランジション開始
        /// </summary>
        protected virtual void BeginTransion()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - Update : 更新処理
        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="elapsedTime">経過時間</param>
        protected virtual void Update(ulong elapsedTime)
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - EndTransition : トランジション終了
        /// <summary>
        /// トランジション終了
        /// </summary>
        protected virtual void EndTransition()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #endregion

        #region ■ Methods

        #region - Proc : トランジション処理（virtual)
        /// <summary>
        /// トランジション処理（virtual)
        /// </summary>
        /// <returns>True : 処理継続 / False : 終了</returns>
        public virtual bool Proc()
        {
            // 経過時間を算出
            var elapsedTime = App.ElapsedTime - StartTime;

            BeginTransion();

            // トランジション時間に達した場合は処理終了
            if (elapsedTime > TransitionTime)
            {
                EndTransition();
                return false;
            }

            Update(elapsedTime);
            return true;
        }
        #endregion

        #endregion
    }
    #endregion
}