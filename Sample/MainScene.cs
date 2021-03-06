﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using dxw;
using hsb.Extensions;

using DxLibDLL;
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

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="app"></param>
        public MainScene(SampleApp app)
            : base(app)
        {
            var motion = new ParabolaMotion(FPt(20.0d, 450.0d), FPt(600.0d, 300.0d), 3000, Vec(0, 1000.0d / 9000000), 1.0d);
            MyShip = new Sprite(this, Rect(20, 450, 10, 10))
            {
                Motion = motion,
                OnDraw = s =>
                {
                    s.DrawFrame(Stock.Colors.Red, true);
                }
            };
            AddSplite(MyShip);

            Enemy = new Sprite(this, Rect(600, 300, 10, 10))
            {
                OnDraw = s =>
                {
                    s.DrawFrame(Stock.Colors.Yellow, true);
                }
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

            DrawStringVertical(50, 30, "ABCDEFJHIJK", -10, Stock.Colors.Yellow, Stock.Font);

        }

        #endregion
    }
    #endregion
}
