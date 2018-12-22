﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        #region ■ Properties

        #endregion

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

        #region ■ Private Methods

        private Vector RoundVector(Vector v)
        {
            return new Vector(
                v.X > 0.9d ? 0.9d : (v.X < -0.9d ? -0.9d : v.X),
                v.Y > 0.9d ? 0.9d : (v.Y < -0.9d ? -0.9d : v.Y)
                );
        }

        private Sprite CreateSprite(Panel panel, int x, int y, double vx, double vy)
        {
            var sprite = new Sprite(panel, new Rectangle(x, y, 10, 10));
            sprite.Motion = new VectorMotion(new FPoint(x, y), new Vector(vx, vy), panel.SizeRectangle.Scaling(-10,RectangleOrigin.LeftTop),
                (sender, args) => {
                    if (args.IsCollisionSprite)
                    {
                        /*
                        var targetMotion = (args.TargetSprite as Sprite).Motion as VectorMotion;
                        var vec = targetMotion.Vector;
                        targetMotion.Vector = RoundVector(targetMotion.Vector + args.Vector * 2);
                        return RoundVector(args.Vector + vec * 2);
                        */
                        args.TargetSprite.Remove = true;
                        return args.Vector;
                    }
                    else
                    {
                        return args.Vector.Flip(args.IsCollisionHorizontal, args.IsCollisionVertical) 
                                    * (1 + (new Random()).NextDouble());
                    }
                }
            );
            sprite.OnDraw = v => v.FillBox(Stock.Colors.Red);
            return sprite;
        }

        #endregion

        #region ■ Public Methods

        #region - LoadCompleted : ロードが完了した
        /// <summary>
        /// ロードが完了した
        /// </summary>
        public override void LoadCompleted()
        {
            base.LoadCompleted();

            var panel = AddSplite(new Panel(this, new Rectangle(20, 20, 600, 400)));
            panel.EnableCollisionCheck = true;
            panel.OnDrawBeforeSpriteDrawing = p => DrawBox(p.SizeRectangle, Stock.Colors.Black, true);

            var r = new Random(1000);
            foreach (var n in Enumerable.Range(0, 100))
            {
                panel.AddSplite(CreateSprite(panel, GetRand(panel.Width), GetRand(panel.Height),
                    r.NextDouble(), r.NextDouble()));
            }
        }
        #endregion

        protected override void DrawFrameBeforeSpriteDrawing()
        {
            base.DrawFrameBeforeSpriteDrawing();
            FillBackground(Stock.Colors.White);
            var panel = Sprites.Find(s => s is Panel) as Panel;
            if (panel != null)
            {
                var cnt = panel.Sprites.Count;
                DrawString(10, 420, $"Sprite Count:{cnt}", Stock.Colors.Black);
            }
        }

        #endregion
    }
    #endregion
}
