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

            MyShipVector = new Vector(0.1, 0.0);
            MyShip = new Sprite(this, new Rectangle(10, 50, 50, 50));
            MyShip.OnDraw = s =>
            {
                s.DrawFrame(Stock.Colors.Red, true);
            };
            MyShip.OnUpdate = s =>
            {
                if (App.CheckOnKeyDown(KeyCode.KEY_L))
                    MyShipVector = MyShipVector.ModDirection(0.0d.DegreeToRadian());
                if (App.CheckOnKeyDown(KeyCode.KEY_R))
                    MyShipVector = MyShipVector.ModDirection(180.0d.DegreeToRadian());
                if (App.CheckOnKeyDown(KeyCode.KEY_U))
                    MyShipVector = MyShipVector.ModDirection(-90.0d.DegreeToRadian());
                if (App.CheckOnKeyDown(KeyCode.KEY_D))
                    MyShipVector = MyShipVector.ModDirection(90.0d.DegreeToRadian());
                var Pos = s.LeftTop + (MyShipVector * App.WrapTime);
                s.LeftTop = Pos;
            };

            AddSplite(MyShip);
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
