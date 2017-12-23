using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxw
{
    #region  【struct RectangleSize】
    /// <summary>
    /// 矩形サイズ
    /// </summary>
    public struct RectangleSize
    {
        #region 　■ Members
        /// <summary>
        /// 幅(px)
        /// </summary>
        public readonly int Width;
        /// <summary>
        /// 高さ(px)
        /// </summary>
        public readonly int Height;
        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public RectangleSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
        #endregion
    }
    #endregion
}
