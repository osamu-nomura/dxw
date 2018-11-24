using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxw
{
    #region 【Struct Point】
    /// <summary>
    /// 点構造体
    /// </summary>
    public struct Point
    {
        #region ■ Members
        /// <summary>
        /// X座標(px)
        /// </summary>
        public readonly int X;
        /// <summary>
        /// Y座標(px)
        /// </summary>
        public readonly int Y;
        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x">X座標(px)</param>
        /// <param name="y">Y座標(px)</param>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        #endregion

        #region ■ Operator Overload

        #region - Plus Operator : Point + Vector
        /// <summary>
        /// Point + Vector
        /// </summary>
        /// <param name="pt">Point</param>
        /// <param name="v">Vector</param>
        /// <returns>Point</returns>
        public static Point operator +(Point pt, Vector v)
            => new Point(pt.X + (int)Math.Ceiling(v.X), pt.Y + (int)Math.Ceiling(v.Y));
        #endregion

        #region - Plus Operator : Vector + Point
        /// <summary>
        /// Vector + Point
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="pt">Point</param>
        /// <returns>Point</returns>
        public static Point operator +(Vector v, Point pt)
            => new Point(pt.X + (int)Math.Ceiling(v.X), pt.Y + (int)Math.Ceiling(v.Y));
        #endregion

        #endregion

    }
    #endregion
    }
