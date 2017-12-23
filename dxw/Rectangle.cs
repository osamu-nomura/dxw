using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxw
{
    #region 【Class : Rectangle】
    /// <summary>
    /// 矩形クラス
    /// </summary>
    public class Rectangle
    {
        #region ■ Properties

        #region - X ; X座標
        /// <summary>
        /// X座標
        /// </summary>
        public int X { get; set; }
        #endregion

        #region - Y : Y座標
        /// <summary>
        /// Y座標
        /// </summary>
        public int Y { get; set; }
        #endregion

        #region - Width : 幅
        /// <summary>
        /// 幅
        /// </summary>
        public int Width { get; set; }
        #endregion

        #region - Height : 高さ
        /// <summary>
        /// 高さ
        /// </summary>
        public int Height { get; set; }
        #endregion

        #region - X2 : X2座標
        /// <summary>
        /// X2座標
        /// </summary>
        public int X2
        {
            get { return X + Width; }
            set { Width = value - X; }
        }
        #endregion

        #region - Y2 : Y2座標
        /// <summary>
        /// Y2座標
        /// </summary>
        public int Y2
        {
            get { return Y + Height; }
            set { Height = value - Height; }
        }
        #endregion

        #region - LeftTop : 左上座標
        /// <summary>
        /// 左上座標
        /// </summary>
        public Point LeftTop
        {
            get { return new Point(X, Y);  }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }
        #endregion

        #region - RightBottom : 右下座標
        /// <summary>
        /// 右下座標
        /// </summary>
        public Point RightBottom
        {
            get { return new Point(X2, Y2); }
            set
            {
                X2 = value.X;
                Y2 = value.Y;
            }
        }
        #endregion

        #region - Size : サイズ
        /// <summary>
        /// 矩形のサイズ
        /// </summary>
        public RectangleSize Size
        {
            get { return new RectangleSize(Width, Height);  }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }
        #endregion

        #endregion

        #region ■ Constructor

        #region - Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクタ(2)
        /// </summary>
        /// <param name="leftTop">左上座標</param>
        /// <param name="rightBottom">右下座標</param>
        public Rectangle(Point leftTop, Point rightBottom)
        {
            LeftTop = leftTop;
            RightBottom = rightBottom;
        }
        #endregion

        #region - Constructor(3)
        /// <summary>
        /// コンストラクタ(3)
        /// </summary>
        /// <param name="pt">位置</param>
        /// <param name="size">サイズ</param>
        public Rectangle(Point pt, RectangleSize size)
        {
            LeftTop = pt;
            Size = size;
        }
        #endregion

        #region - Constructor(4)
        /// <summary>
        /// コンストラクタ(4)
        /// </summary>
        /// <param name="rect">矩形</param>
        public Rectangle(Rectangle rect)
        {
            X = rect.X;
            Y = rect.Y;
            Width = rect.Width;
            Height = rect.Height;
        }
        #endregion

        #endregion

        #region ■ Methods

        #region - CheckPointInRegion : ポイントが領域内かどうか判定
        /// <summary>
        /// ポイントが領域内かどうか判定
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <returns>True;領域内 / False:領域外</returns>
        public bool CheckPointInRegion(int x, int y)
        {
            return x >= X && x <= X2 && y >= Y && y <= Y2;
        }
        #endregion

        #region - CheckPointInRegion : ポイントが領域内かどうか判定
        /// <summary>
        /// CheckPointInRegion
        /// </summary>
        /// <param name="pt">座標</param>
        /// <returns>True;領域内 / False:領域外</returns>
        public bool CheckPointInRegion(Point pt)
            => CheckPointInRegion(pt.X, pt.Y);

        #endregion

        #region - Set : 指定した矩形の位置とサイズに合わせる
        /// <summary>
        /// 指定した矩形の位置とサイズに合わせる
        /// </summary>
        /// <param name="rect">矩形</param>
        public void Set(Rectangle rect)
        {
            LeftTop = rect.LeftTop;
            Size = rect.Size;
        }
        #endregion

        #endregion
    }
    #endregion
}
