using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using dxw;

namespace Sample
{
    #region 【Class ; SampleApp】
    /// <summary>
    /// サンプルアプリケーションクラス
    /// </summary>
    class SampleApp : BaseApplication
    {
        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="screenWidth">画面幅(ピクセル)</param>
        /// <param name="screenHeight">画面高さ(ピクセル)</param>
        /// <param name="colorBitDepth">色進度(BIT)</param>
        public SampleApp(int screenWidth = 640, int screenHeight = 480, ColorBitDepth colorBitDepth = ColorBitDepth.BitDepth32)
            : base(screenWidth, screenHeight, colorBitDepth)
        {

        }
        #endregion

        #region ■ Protected Methods

        #region - Update : 更新処理
        /// <summary>
        /// 更新処理
        /// </summary>
        protected override void UpdateFrame()
        {
            base.UpdateFrame();
            if (CheckOnKeyUp(KeyCode.KEY_Q))
                Quit();
            if (CheckOnKeyUp(KeyCode.KEY_1))
            {
                AddTask((id, tm, app) =>
                {
                    var ms = ElapsedTime - tm;
                    if (ms >= 5000)
                    {
                        Quit();
                        return true;
                    }
                    return false;
                });
            }
        }
        #endregion

        #region - DrawFrame : フレームを描画
        /// <summary>
        /// フレームを描画
        /// </summary>
        protected override void DrawFrame()
        {
            base.DrawFrame();
            DX.DrawBox(0, 0, ScreenWidth, ScreenHeight, DX.GetColor(255, 0, 0), DX.TRUE);

            var sec = ElapsedTime / 1000.0;
            DX.DrawString(70, 70, sec.ToString(), DX.GetColor(255, 255, 255));
        }
        #endregion

        #endregion
    }
    #endregion
}
