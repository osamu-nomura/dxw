using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using dxw;
using hsb.Extensions;

using static dxw.Helper;

namespace Sample
{
    #region 【Class : MainScene】
    /// <summary>
    /// メインシーン
    /// </summary>
    class MainScene : BaseScene
    {
        #region ■ Properties

        private Sprite MyShip { get; set; }
        private Sprite Enemy { get; set; }
        private Vector MyShipVector { get; set; }

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="app"></param>
        public MainScene(SampleApp app)
            : base(app)
        {
            MyShipVector = Vec(0.1, 0.0);
            MyShip = new Sprite(this, Rect(10, 50, 10, 10));
            MyShip.OnDraw = s =>
            {
                s.DrawFrame(Stock.Colors.Red, true);
            };
            MyShip.OnUpdate = s =>
            {
                s.LeftTop += (MyShipVector * App.WrapTime);
                if (s.LeftTop.X < 0 || (s.RightTop.X) > App.ScreenWidth)
                {
                    MyShipVector = Vec(MyShipVector.X * -1, 0);
                }
            };
            AddSplite(MyShip);

            Enemy = new Sprite(this, Rect(10, 300, 10, 10));
            Enemy.OnDraw = s =>
            {
                s.DrawFrame(Stock.Colors.Yellow, true);
            };
            AddSplite(Enemy);
        }
        #endregion

        #region ■ Private Methods

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

        protected override void DrawFrameBeforeSpriteDrawing()
        {
            base.DrawFrameBeforeSpriteDrawing();
            FillBackground(Stock.Colors.Black);
        }

        #endregion
    }
    #endregion
}
