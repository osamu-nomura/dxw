using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static dxw.Helper;

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

        #region - LeftBottom : 左下座標
        /// <summary>
        /// 左下座標
        /// </summary>
        public Point LeftBottom
        {
            get { return new Point(X, Y2); }
            set
            {
                X = value.X;
                Y2 = value.Y;
            }
        }
        #endregion

        #region - RightTop : 右上座標
        /// <summary>
        /// 右上座標
        /// </summary>
        public Point RightTop
        {
            get { return new Point(X2, Y); }
            set
            {
                X2 = value.X;
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

        #region - Center : 中心点
        /// <summary>
        /// 中心点
        /// </summary>
        public Point Center
        {
            get { return new Point(X + (Width / 2), Y + (Height / 2)); }
            set
            {
                X = value.X - (Width / 2);
                Y = value.Y - (Height / 2);
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

        #region - SizeRectangle : 原点基準の矩形でサイズを返す
        /// <summary>
        /// 原点基準の矩形でサイズを返す
        /// </summary>
        public Rectangle SizeRectangle
        {
            get { return new Rectangle(0, 0, Width, Height);  }
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
            Set(rect);
        }
        #endregion

        #endregion

        #region ■ Methods

        #region - Set : 指定した位置に合わせる
        /// <summary>
        /// 指定した位置に合わせる
        /// </summary>
        /// <param name="pt">座標</param>
        /// <param name="isCenter">中心基準？</param>
        public void Set(Point pt, bool isCenter = false)
        {
            if (isCenter)
                Center = pt;
            else
                LeftTop = pt;
        }
        #endregion

        #region - Set : 指定サイズに合わせる
        /// <summary>
        /// 指定サイズに合わせる
        /// </summary>
        /// <param name="size">矩形サイズ</param>
        public void Set(RectangleSize size)
        {
            Size = size;
        }
        #endregion

        #region - Set : 指定した矩形の位置とサイズに合わせる
        /// <summary>
        /// 指定した矩形の位置とサイズに合わせる
        /// </summary>
        /// <param name="rect">矩形</param>
        public void Set(Rectangle rect)
        {
            LeftTop = rect?.LeftTop ?? new Point(0,0);
            Size = rect?.Size ?? new RectangleSize(0,0);
        }
        #endregion

        #region - Scaling : 拡大・縮小した矩形を返す
        /// <summary>
        /// 拡大・縮小した矩形を返す
        /// </summary>
        /// <param name="n">拡大するサイズ(px)</param>
        /// <param name="origin">基準点</param>
        /// <returns>拡大・縮小した矩形</returns>
        public Rectangle Scaling(int n, RectangleOrigin origin = RectangleOrigin.Center)
        {
            switch (origin)
            {
                case RectangleOrigin.Center:
                    return new Rectangle(X - n / 2, Y - n / 2, Width + n, Height + n);
                case RectangleOrigin.LeftTop:
                    return new Rectangle(X, Y, Width + n, Height + n);
                case RectangleOrigin.RightTop:
                    return new Rectangle(X - n, Y, Width + n, Height + n);
                case RectangleOrigin.LeftBottom:
                    return new Rectangle(X, Y - n, Width + n, Height + n);
                case RectangleOrigin.RightBottom:
                    return new Rectangle(X - n, Y - n, Width + n, Height + n);
                default:
                    throw new ApplicationException("この行が実行されることはない！");
            }
        }
        #endregion

        #region - SetImageSize : サイズを画像サイズに合わせる。
        /// <summary>
        /// サイズを画像サイズに合わせる。
        /// </summary>
        public void SetImageSize(int imageHandle)
        {
            // 画像サイズを取得して自身のサイズにセットする。
            var size = GetGraphSize(imageHandle);
            if (size.HasValue)
                Size = size.Value;
        }
        #endregion

        #region - CheckPointInHorizontalRegion : ポイントが水平領域内かどうか判定
        /// <summary>
        /// ポイントが水平領域内かどうか判定
        /// </summary>
        /// <param name="x">X座標</param>
        /// <returns>True;領域内 / False:領域外</returns>
        public bool CheckPointInHorizontalRegion(int x)
            => x >= X && x <= X2;
        #endregion

        #region - CheckPointInHorizontalRegion : ポイントが水平領域内かどうか判定
        /// <summary>
        /// ポイントが水平領域内かどうか判定
        /// </summary>
        /// <param name="pt">座標</param>
        /// <returns>True;領域内 / False:領域外</returns>
        public bool CheckPointInHorizontalRegion(Point pt)
            => pt.X >= X && pt.X <= X2;
        #endregion

        #region - CheckPointInVerticalRegion : ポイントが垂直領域内かどうか判定
        /// <summary>
        /// ポイントが垂直領域内かどうか判定
        /// </summary>
        /// <param name="y">Y座標</param>
        /// <returns>True;領域内 / False:領域外</returns>
        public bool CheckPointInVerticalRegion(int y)
            => y >= Y && y <= Y2;
        #endregion

        #region - CheckPointInVerticalRegion : ポイントが垂直領域内かどうか判定
        /// <summary>
        /// ポイントが垂直領域内かどうか判定
        /// </summary>
        /// <param name="pt">座標</param>
        /// <returns>True;領域内 / False:領域外</returns>
        public bool CheckPointInVerticalRegion(Point pt)
            => pt.Y >= Y && pt.Y <= Y2;
        #endregion

        #region - CheckPointInRegion : ポイントが領域内かどうか判定
        /// <summary>
        /// ポイントが領域内かどうか判定
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <returns>True;領域内 / False:領域外</returns>
        public bool CheckPointInRegion(int x, int y)
            => x >= X && x <= X2 && y >= Y && y <= Y2;
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

        #region - CheckCollision : 衝突判定
        /// <summary>
        /// 衝突判定
        /// </summary>
        /// <param name="target">対象矩形</param>
        /// <returns>True : 諸突 / False : 衝突していない</returns>
        public bool CheckCollision(Rectangle target)
        {
            return (Math.Abs(X - target.X) < Width / 2 + target.Width / 2) &&
                   (Math.Abs(Y - target.Y) < Height / 2 + target.Height / 2);
        }
        #endregion

        #region - DrawBox : 矩形を描画する
        /// <summary>
        /// 矩形を描画する
        /// </summary>
        /// <param name="color">色</param>
        /// <param name="isFill">塗りつぶす？</param>
        public void DrawBox(uint color, bool isFill = false)
            => Helper.DrawBox(this, color, isFill);
        #endregion

        #region - FillBox : 矩形を塗りつぶす
        /// <summary>
        /// 矩形を塗りつぶす
        /// </summary>
        /// <param name="color">色</param>
        public void FillBox(uint color)
            => Helper.DrawBox(this, color, true);
        #endregion

        #endregion
    }
    #endregion
}
