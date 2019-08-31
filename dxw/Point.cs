using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hsb.Extensions;

namespace dxw
{
    #region 【Struct Point】
    /// <summary>
    /// 点構造体
    /// </summary>
    public readonly struct Point
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

        #region ■ Methods

        #region - Vector : 2点を結ぶベクトルを返す
        /// <summary>
        /// 2点を結ぶベクトルを返す
        /// </summary>
        /// <param name="target">対象となるポイント</param>
        /// <returns>Vector</returns>
        public Vector Vector(Point target)
            => new Vector(target.X - X, target.Y - Y);
        #endregion

        #region - Distance : 2点の距離を返す
        /// <summary>
        /// 2点の距離を返す
        /// </summary>
        /// <param name="target">対象となるポイント</param>
        /// <returns>距離</returns>
        public double Distance(Point target)
            => Helper.Distance(this, target);
        #endregion

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
        /// <param name="pt">Point</param>
        /// <returns>Vector</returns>
        public static explicit operator FPoint(Point pt)
            => new FPoint(pt.X, pt.Y);
        #endregion

        #region - (Vector) Operator
        /// <summary>
        /// (Vector) ← Point
        /// </summary>
        /// <param name="pt">Point</param>
        /// <returns>Vector</returns>
        public static explicit operator Vector(Point pt)
            => new Vector(pt.X, pt.Y);
        #endregion
        #endregion
    }
    #endregion

    #region 【struct : FPoint】
    /// <summary>
    /// 点構造体(実数版）
    /// </summary>
    public readonly struct FPoint
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

        #region ■ Methods

        #region - Vector : 2点を結ぶベクトルを返す
        /// <summary>
        /// 2点を結ぶベクトルを返す
        /// </summary>
        /// <param name="target">対象となるポイント</param>
        /// <returns>Vector</returns>
        public Vector Vector(FPoint target)
            => new Vector(target.X - X, target.Y - Y);
        #endregion

        #region - Distance : 2点の距離を返す
        /// <summary>
        /// 2点の距離を返す
        /// </summary>
        /// <param name="target">対象となるポイント</param>
        /// <returns>距離</returns>
        public double Distance(FPoint target)
            => Helper.Distance(this, target);
        #endregion

        #region - Contact : 2点が接触した
        /// <summary>
        /// 2点が接触した
        /// </summary>
        /// <param name="target">対象</param>
        /// <param name="toleranceError">許容誤差</param>
        /// <returns>True : 接触している / False : 接触していない</returns>
        public bool Contact(FPoint target, double toleranceError = 0.0d)
            => Math.Abs(X - target.X) < toleranceError && Math.Abs(Y - target.Y) < toleranceError;
        #endregion

        #endregion

        #region ■ Operator Overload

        #region - Plus Operator : FPoint + Vector
        /// <summary>
        /// FPoint + Vector
        /// </summary>
        /// <param name="pt">FPoint</param>
        /// <param name="v">Vector</param>
        /// <returns>FPoint</returns>
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
            => new Point((int)pt.X.Floor(), (int)pt.Y.Floor());
        #endregion

        #region - (Vector) Operator
        /// <summary>
        /// (Vector) ← FPoint
        /// </summary>
        /// <param name="pt">FPoint</param>
        /// <returns>Vector</returns>
        public static explicit operator Vector(FPoint pt)
            => new Vector(pt.X, pt.Y);
        #endregion
        #endregion
    }
    #endregion
}
