using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dxw;

using static dxw.Helper;

namespace Sample
{
    #region 【Class : MainScene】
    /// <summary>
    /// メインシーン
    /// </summary>
    class MainScene : BaseScene
    {
        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="app"></param>
        public MainScene(SampleApp app)
            : base(app)
        {

        }
        #endregion

        #region ■ protected Methods

        #region - DrawBackground : 背景を描画する
        /// <summary>
        /// 背景を描画する
        /// </summary>
        protected override void DrawBackground()
        {
            base.DrawBackground();
            FillBackground(App.ColorWhite);

            var s = "SAMPLE";
            var size = GetDrawStringSize(s, Stock.Font);
            var x = (App.ScreenWidth - size.Width) / 2;
            var y = (App.ScreenHeight - size.Height) / 2;
            DrawString(x, y, s, Stock.Colors.Red, Stock.Font);
        }
        #endregion

        #endregion

        #region ■ Public Methods

        #region - LoadCompleted : ロードが完了した
        /// <summary>
        /// ロードが完了した
        /// </summary>
        public override void LoadCompleted()
        {
            base.LoadCompleted();
        }
        #endregion

        #endregion
    }
    #endregion
}
