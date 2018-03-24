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

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="prev">直前シーン</param>
        /// <param name="next">次シーン</param>
        public BaseTransition(BaseScene prev, BaseScene next)
        {
            App = prev.App;
            PrevScene = prev;
            NextScene = next;
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

        #region ■ Methods

        #region - Proc : トランジション処理（Abstract)
        /// <summary>
        /// トランジション処理（Abstract)
        /// </summary>
        /// <returns>True : 処理継続 / False : 終了</returns>
        public abstract bool Proc();
        #endregion

        #endregion
    }
    #endregion
}