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
        public double X { get; set; }
        #endregion

        #region - Y : Y方向
        /// <summary>
        /// Y方向
        /// </summary>
        public double Y { get; set; }
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x">X方向</param>
        /// <param name="y">Y方向</param>
        public Vector(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }
        #endregion

        #region ■ Methods

        #region - ModX : ベクトルのX軸成分を変更する
        /// <summary>
        /// ベクトルのX軸成分を変更する
        /// </summary>
        /// <param name="n">変更値</param>
        /// <returns>Vector</returns>
        public Vector ModX(double n)
            => new Vector(n, Y);
        #endregion

        #region - ModY : ベクトルのY軸成分を変更する
        /// <summary>
        /// ベクトルのY軸成分を変更する
        /// </summary>
        /// <param name="n">変更値</param>
        /// <returns>Vector</returns>
        public Vector ModY(double n)
            => new Vector(X, n);
        #endregion

        #region - Flip : ベクトルを反転させる
        /// <summary>
        /// ベクトルを反転させる
        /// </summary>
        /// <param name="x">X軸成分を反転させる</param>
        /// <param name="y">y軸成分を反転させる</param>
        /// <returns>Vector</returns>
        public Vector Flip(bool x = true, bool y = true)
            => new Vector(X * (x ? -1.0d : 1.0d), Y * (y ? -1.0d : 1.0d));
        #endregion

        #region - FlipHorizontal : ベクトルを水平方向に反転させる
        /// <summary>
        /// ベクトルを水平方向に反転させる
        /// </summary>
        /// <returns>Vector</returns>
        public Vector FlipHorizontal()
            => new Vector(X * -1.0d, Y);
        #endregion

        #region - FlipVertical : ベクトルを垂直方向に反転させる
        /// <summary>
        /// ベクトルを垂直方向に反転させる
        /// </summary>
        /// <returns>Vector</returns>
        public Vector FlipVertical()
            => new Vector(X, Y * -1.0d);
        #endregion

        #endregion

        #region ■ Operator Overload

        #region - Plus Operator : Vector + Vector
        /// <summary>
        /// Vector + Vector
        /// </summary>
        /// <param name="v1">Vector1</param>
        /// <param name="v2">Vector2</param>
        /// <returns>Vector</returns>
        public static Vector operator +(Vector v1, Vector v2)
            => new Vector(v1.X + v2.X, v1.Y + v2.Y);
        #endregion

        #region - Plus Operator : Vector + double
        /// <summary>
        /// Vector + double
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="n">double</param>
        /// <returns>Vector</returns>
        public static Vector operator +(Vector v, double n)
            => new Vector(v.X + n, v.Y + n);
        #endregion

        #region - Plus Operator : double + Vector
        /// <summary>
        /// double + Vector
        /// </summary>
        /// <param name="n">double</param>
        /// <param name="v">Vector</param>
        /// <returns>Vector</returns>
        public static Vector operator +(double n, Vector v)
            => new Vector(v.X + n, v.Y + n);
        #endregion

        #region - Multiplication Operator : Vector * double
        /// <summary>
        /// Vector * double
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="n">double</param>
        /// <returns>Vector</returns>
        public static Vector operator *(Vector v, double n)
            => new Vector(v.X * n, v.Y * n);
        #endregion

        #region - Multiplication Operator : double * Vector
        /// <summary>
        /// double * Vector
        /// </summary>
        /// <param name="n">double</param>
        /// <param name="v">Vector</param>
        /// <returns>Vector</returns>
        public static Vector operator *(double n, Vector v)
            => v * n;
        #endregion

        #endregion
    }
    #endregion
}
