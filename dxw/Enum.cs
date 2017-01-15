using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxw
{
    #region ■ Enums

    #region - WindowMode : ウィンドウモード
    /// <summary>
    /// ウィンドウモード
    /// </summary>
    public enum WindowMode
    {
        FullScreen = 0,     // 全画面モード
        Window = 1          // Windowモード
    }
    #endregion

    #region - DeviceType : デバイス種別
    /// <summary>
    /// デバイス種別
    /// </summary>
    public enum DeviceType
    {
        Mouse = 0,  // マウス
        Touch = 1   // タッチ
    }
    #endregion

    #region - ColorBitDepth : 色BIT数
    /// <summary>
    /// 色BIT数
    /// </summary>
    public enum ColorBitDepth
    {
        BitDepth16 = 16,
        BitDepth32 = 32
    };
    #endregion

    #region - Orientation : 方向
    /// <summary>
    /// 方向
    /// </summary>
    public enum Orientation
    {
        Horizontal = 0, // 水平
        Vertical = 1,   // 垂直
        Both = 2        // 両方
    }
    #endregion

    #endregion
}
