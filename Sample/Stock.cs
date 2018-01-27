using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dxw;

using static dxw.Helper;

namespace Sample
{
    #region 【Static Class : Stock】
    /// <summary>
    /// ストックオブジェクト管理クラス
    /// </summary>
    static class Stock
    {
        #region 【Inner Static Class : Colors】
        /// <summary>
        /// 色オブジェクト管理クラス
        /// </summary>
        public static class Colors
        {
            #region ■ Static Properties
            /// <summary>
            /// 赤
            /// </summary>
            public static uint Red { get; private set; } = 0;
            #endregion

            #region ■ Statuc Methods

            #region - Init : 初期化処理
            /// <summary>
            /// 初期化処理
            /// </summary>
            public static void Init()
            {
                Red = GetColor(255, 0, 0);
            }
            #endregion

            #endregion
        }
        #endregion

        #region ■ Statuc Properties
        /// <summary>
        /// フォント
        /// </summary>
        public static int Font { get; private set; } = 0;
        #endregion

        #region ■ Static Methods

        #region - Init : 初期化処理
        /// <summary>
        /// 初期化処理
        /// </summary>
        public static void Init()
        {
            Font = CreateFont("Meiryo", 24, 3, FontType.AntiAlias);
            Colors.Init();
        }
        #endregion

        #endregion
    }
    #endregion
}
