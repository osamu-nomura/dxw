using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxw
{
    #region 【Struct : Venctor】
    /// <summary>
    /// ベクトル構造体
    /// </summary>
    public struct Vector
    {
        #region ■ Properties

        #region - X : X方向
        /// <summary>
        /// X方向
        /// </summary>
        public int X { get; set; }
        #endregion

        #region - Y : Y方向
        /// <summary>
        /// Y方向
        /// </summary>
        public int Y { get; set; }
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

        #region ■ Operator Overload

        #region + Multiplication Operator : Vecror * int
        /// <summary>
        /// Vecror * int
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="s">int</param>
        /// <returns>Vector</returns>
        public static Vector operator *(Vector v, int s)
            => new Vector(v.X * s, v.Y * s);
        #endregion

        #endregion
    }
    #endregion
}
