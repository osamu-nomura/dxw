using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxw
{
    #region ■ Class ; InputInfo
    /// <summary>
    /// 入力情報クラス
    /// </summary>
    public class InputInfo
    {
        #region ■ Properties

        #region - Device : 入力デバイス
        /// <summary>
        /// 入力デバイス
        /// </summary>
        public DeviceType Device { get; private set; }
        #endregion

        #region - DeviceNo : 入力デバイス番号
        /// <summary>
        /// 入力デバイス番号
        /// </summary>
        public int DeviceNo { get; private set; }
        #endregion

        #region - Id : Touch ID
        /// <summary>
        /// Touch ID
        /// </summary>
        public int Id { get; private set; } = 0;
        #endregion

        #region - Point : 座標
        /// <summary>
        /// 座標
        /// </summary>
        public Point Point = new Point(0, 0);
        #endregion

        #region - Buttons : ボタン状態
        /// <summary>
        /// ボタン状態
        /// </summary>
        public int Buttons { get; private set; }
        #endregion

        #region - IsMouseLeftButtonDown : マウスの左ボタンが押下されている
        /// <summary>
        /// マウスの左ボタンが押下されている
        /// </summary>
        public bool IsMouseLeftButtonDown
        {
            get { return (Buttons & (int)MouseInput.Left) != 0; }
        }
        #endregion

        #region - IsMouseRightButtonDown : マウスの右ボタンが押下されている
        /// <summary>
        /// マウスの右ボタンが押下されている
        /// </summary>
        public bool IsMouseRightButtonDown
        {
            get { return (Buttons & (int)MouseInput.Right) != 0; }
        }
        #endregion

        #endregion

        #region  ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="device">デバイス</param>
        /// <param name="deviceNo">デバイス番号</param>
        /// <param name="id">入力ID</param>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <param name="buttons">ボタン状態</param>
        public InputInfo(DeviceType device, int deviceNo, int id, int x, int y, int buttons)
        {
            Device = device;
            DeviceNo = deviceNo;
            Id = id;
            Point = new Point(x, y);
            Buttons = buttons;
        }
        #endregion
    }
    #endregion

}
