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
            => new Point(pt.X + (int)Math.Floor(v.X), pt.Y + (int)Math.Floor(v.Y));
        #endregion

        #region - Plus Operator : Vector + Point
        /// <summary>
        /// Vector + Point
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="pt">Point</param>
        /// <returns>Point</returns>
        public static Point operator +(Vector v, Point pt)
            => new Point(pt.X + (int)Math.Floor(v.X), pt.Y + (int)Math.Floor(v.Y));
        #endregion

        #region - (FPoint) Operator
        /// <summary>
        /// (Fpoint) ← Point
        /// </summary>
        /// <param name="pt"></param>
        public static explicit operator FPoint(Point pt)
            => new FPoint(pt.X, pt.Y);
        #endregion

        #endregion

    }
    #endregion

    #region 【Class : FPoint】
    /// <summary>
    /// 点構造体(実数版）
    /// </summary>
    public struct FPoint
    {
        #region ■ Members
        /// <summary>
        /// X座標
        /// </summary>
        public readonly double X;
        /// <summary>
        /// Y座標
        /// </summary>
        public readonly double Y;
        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        public FPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
        #endregion

        #region ■ Operator Overload

        #region - Plus Operator : FPoint + Vector
        /// <summary>
        /// FPoint + Vector
        /// </summary>
        /// <param name="pt">FPoint</param>
        /// <param name="v">Vector</param>
        /// <returns>Point</returns>
        public static FPoint operator +(FPoint pt, Vector v)
            => new FPoint(pt.X + v.X, pt.Y + v.Y);
        #endregion

        #region - Plus Operator : Vector + FPoint
        /// <summary>
        /// Vector + FPoint
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="pt">FPoint</param>
        /// <returns>Point</returns>
        public static FPoint operator +(Vector v, FPoint pt)
            => new FPoint(pt.X + v.X, pt.Y + v.Y);
        #endregion

        #region - (Point) Operator
        /// <summary>
        /// (Point) ← FPoint
        /// </summary>
        /// <param name="pt">FPoint</param>
        public static explicit operator Point (FPoint pt)
            => new Point((int)Math.Floor(pt.X), (int)Math.Floor(pt.Y));
        #endregion

        #endregion
    }
    #endregion
}
