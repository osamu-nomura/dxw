using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace dxw
{
    #region ■ Class ; TouchInfo
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

        #region - Id : Touch ID
        /// <summary>
        /// Touch ID
        /// </summary>
        public int Id { get; private set; } = 0;
        #endregion

        #region - X : X座標
        /// <summary>
        /// X座標
        /// </summary>
        public int X { get; private set; } = 0;
        #endregion

        #region - Y : Y座標
        /// <summary>
        /// Y座標
        /// </summary>
        public int Y { get; private set; } = 0;
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
            get { return (Buttons & DX.MOUSE_INPUT_LEFT) != 0; }
        }
        #endregion

        #region - IsMouseRightButtonDown : マウスの右ボタンが押下されている
        /// <summary>
        /// マウスの右ボタンが押下されている
        /// </summary>
        public bool IsMouseRightButtonDown
        {
            get { return (Buttons & DX.MOUSE_INPUT_RIGHT) != 0; }
        }
        #endregion

        #endregion

        #region  ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="device">デバイス</param>
        /// <param name="id">入力ID</param>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <param name="buttons">ボタン状態</param>
        public InputInfo(DeviceType device, int id, int x, int y, int buttons)
        {
            Device = device;
            Id = id;
            X = x;
            Y = y;
            Buttons = buttons;
        }
        #endregion
    }
    #endregion

}
