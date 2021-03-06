﻿using System;
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
    public readonly struct Vector
    {
        #region ■ Fields & Properties

        #region - X : X方向
        /// <summary>
        /// X方向
        /// </summary>
        public readonly double X;
        #endregion

        #region - Y : Y方向
        /// <summary>
        /// Y方向
        /// </summary>
        public readonly double Y;
        #endregion

        #region - Magnitude : ベクトルの大きさ
        /// <summary>
        /// ベクトルの大きさ
        /// </summary>
        public double Magnitude
        {
            get { return Math.Sqrt((X * X) + (Y * Y));  }
        }
        #endregion

        #region - Direction : ベクトルの方向
        /// <summary>
        /// ベクトルの方向
        /// </summary>
        public double Direction
        {
            get { return Math.Atan(Y / X); }
        }
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="x">X方向</param>
        /// <param name="y">Y方向</param>
        public Vector(double x = 0.0d, double y = 0.0d)
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

        #region - ModMagnitude : ベクトルの大きさを変更する
        /// <summary>
        /// ベクトルの大きさを変更する
        /// </summary>
        /// <param name="n">変更値</param>
        /// <returns>Vector</returns>
        public Vector ModMagnitude(double n)
            => new Vector(Math.Cos(Direction) * n, Math.Sin(Direction) * n);
        #endregion

        #region - ModDirection : ベクトルの方向を変更する
        /// <summary>
        /// ベクトルの方向を変更する
        /// </summary>
        /// <param name="n"><変更値/param>
        /// <returns>Vector</returns>
        public Vector ModDirection(double n)
            => new Vector(Math.Cos(n) * Magnitude, Math.Sin(n) * Magnitude);
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

        #region - Collision : ベクトルが衝突した
        /// <summary>
        /// ベクトルが衝突した
        /// </summary>
        /// <param name="target">対象ベクトル</param>
        /// <returns>衝突後のベクトル</returns>
        public Vector Collision(Vector target)
            => this + target * 2;
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

        #region - Division Operator : Vector / double
        /// <summary>
        /// Vector / double
        /// </summary>
        /// <param name="v">Vector</param>
        /// <param name="n">Double</param>
        /// <returns>Vector</returns>
        public static Vector operator /(Vector v, double n)
            => new Vector(v.X / n, v.Y / n);
        #endregion

        #endregion

        #region ■ Static Methods

        #region - CreateByMagnitudeAndDirection : 大きさと方向からVectorを生成する
        /// <summary>
        /// 大きさと方向からVectorを生成する
        /// </summary>
        /// <param name="magnitude">ベクトルの大きさ</param>
        /// <param name="direction">方向</param>
        /// <returns>Vector</returns>
        public static Vector CreateByMagnitudeAndDirection(double magnitude, double direction)
            => new Vector(Math.Cos(direction) * magnitude, Math.Sin(direction) * magnitude);
        #endregion

        #region - CreateByPoint2Point : 始点と終点よりVectorを生成する
        /// <summary>
        /// 始点と終点よりVectorを生成する
        /// </summary>
        /// <param name="pt1">始点</param>
        /// <param name="pt2">終点</param>
        /// <returns>Vector</returns>
        public static Vector CreateByPoint2Point(FPoint pt1, FPoint pt2)
            => pt1.Vector(pt2);
        #endregion

        #endregion
    }
    #endregion
}
