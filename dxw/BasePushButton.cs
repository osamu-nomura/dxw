using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DxLibDLL;

using static dxw.Helper;

namespace dxw
{
    #region 【Class : PushButton】
    /// <summary>
    /// プッシュボタンクラス
    /// </summary>
    public class BasePushButton : BaseSprite
    {
        #region ■ Members
        /// <summary>
        /// 有効？
        /// </summary>
        private bool _enabled = true;
        #endregion

        #region ■ Properties

        #region - TouchId : タップされたタッチID
        /// <summary>
        /// タップされたタッチID
        /// </summary>
        public int? TouchId { get; set; } = null;
        #endregion

        #region - TapStartTime : タップされた時刻
        /// <summary>
        /// タップされた時刻
        /// </summary>
        public ulong? TouchStartTime { get; set; } = null;
        #endregion

        #region - IsDown : タップ中？
        /// <summary>
        /// タップ中？
        /// </summary>
        public bool IsDown
        {
            get { return TouchStartTime.HasValue; }
        }
        #endregion

        #region - TappedSoundHandle : タップ音
        /// <summary>
        /// タップ音
        /// </summary>
        public virtual int TappedSoundHandle { get;  } = 0;
        #endregion

        #region - Enabled : 有効？
        /// <summary>
        ///  有効？
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    TouchId = null;
                    TouchStartTime = null;
                }
            }
        }
        #endregion

        #region - Disabled : 無効？
        /// <summary>
        /// 無効？
        /// </summary>
        public bool Disabled
        {
            get { return !Enabled; }
            set { Enabled = !value; }
        }
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        ///  コンストラクター
        /// </summary>
        /// <param name="x">X座標(px)</param>
        /// <param name="y">Y座標(px)</param>
        /// <param name="width">幅(px)</param>
        /// <param name="height">高さ(px)</param>
        /// <param name="callback">タップされた時のコールバック</param>
        public BasePushButton(BaseScene scene, int x, int y, int width, int height, Action<BasePushButton> callback = null) 
                : base(scene, x, y, width, height)
        {
            if (callback != null)
                Tapped = callback;
        }
        #endregion

        #region ■ Delegates

        #region - Tapped : ボタンがタップされた
        /// <summary>
        /// ボタンがタップされた
        /// </summary>
        public Action<BasePushButton> Tapped { get; set; } = null;
        #endregion

        #endregion

        #region ■ Protected Methods

        #region - InternaleTapped : ボタンがタップされた（内部処理）
        /// <summary>
        /// ボタンがタップされた（内部処理）
        /// </summary>
        /// <returns></returns>
        protected virtual bool InternaleTapped()
        {
            return true;
        }
        #endregion

        #endregion

        #region ■ Methods

        #region - Update : 状態を更新する。
        /// <summary>
        /// 状態を更新する。
        /// </summary>
        /// <param name="app">アプリケーション</param>
        public override void Update()
        {
            base.Update();

            if (Disabled)
                return;

            if (TouchId == null)
            {
                // タッチ or 左マウスがボタン上で押された？
                var input = Sceen.App.Inputs.FirstOrDefault(i => CheckPointInRegion(i.X, i.Y) && i.IsMouseLeftButtonDown);
                if (input != null)
                {
                    TouchId = input.Id;
                    TouchStartTime = Sceen.App.ElapsedTime;
                }
            }
            else
            {
                // タッチ済なら、タッチIDをトラッキングする
                var input = Sceen.App.Inputs.FirstOrDefault(i => i.Id == TouchId);
                if (input != null)
                {
                    // 領域から外れた！
                    if (!CheckPointInRegion(input.X, input.Y) || !input.IsMouseLeftButtonDown)
                    {
                        TouchId = null;
                        TouchStartTime = null;
                    }
                }
                else
                {
                    // 指が離された！
                    TouchId = null;
                    TouchStartTime = null;
                    if (TappedSoundHandle != 0)
                        PlaySound(TappedSoundHandle, DX.DX_PLAYTYPE_BACK, Sceen.App.SEVolume);
                    if (InternaleTapped())
                        Tapped?.Invoke(this);
                }
            }
        }
        #endregion

        #endregion
    }
    #endregion
}
