using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxw
{
    #region 【Class : Venctor】
    /// <summary>
    /// ベクトルクラス
    /// </summary>
    public class Vector
    {
        #region ■ Properties

        #region - X : X方向
        /// <summary>
        /// X方向
        /// </summary>
        public int X { get; set; } = 0;
        #endregion

        #region - Y : Y方向
        /// <summary>
        /// Y方向
        /// </summary>
        public int Y { get; set; } = 0;
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x">X方向</param>
        /// <param name="y">Y方向</param>
        public Vector(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }
        #endregion
    }
    #endregion
}
