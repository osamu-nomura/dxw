using System;
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

        private SpriteAnimation anime { get; set; }

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="app"></param>
        public MainScene(SampleApp app)
            : base(app)
        {
            anime = new SpriteAnimation();
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
                        var targetMotion = (args.TargetSprite as Sprite).Motion as VectorMotion;
                        var vec = targetMotion.Vector;
                        targetMotion.Vector = RoundVector(vec.Collision(args.Vector));
                        return RoundVector(args.Vector.Collision(vec));
                    }
                    else
                        return args.Vector.Flip(args.IsCollisionHorizontal, args.IsCollisionVertical);
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

            var panel = AddSplite(new Panel(this, new Rectangle(20, 20, 400, 400)));
            panel.EnableCollisionCheck = true;
            panel.OnDrawBeforeSpriteDrawing = p => DrawBox(p.SizeRectangle, Stock.Colors.Black, true);

            var r = new Random(1000);
            foreach (var n in Enumerable.Range(0, 10))
            {
                panel.AddSplite(CreateSprite(panel, GetRand(panel.Width), GetRand(panel.Height),
                    r.NextDouble(), r.NextDouble()));
            }

            anime.FrameRate = 30;
            anime.ImageHandles.Add((App as SampleApp).Images[9]);
            anime.ImageHandles.Add((App as SampleApp).Images[10]);
            anime.ImageHandles.Add((App as SampleApp).Images[11]);
            anime.ImageHandles.Add((App as SampleApp).Images[12]);
            anime.ImageHandles.Add((App as SampleApp).Images[13]);
            anime.ImageHandles.Add((App as SampleApp).Images[14]);
            anime.ImageHandles.Add((App as SampleApp).Images[15]);
            anime.ImageHandles.Add((App as SampleApp).Images[16]);
            anime.ImageHandles.Add((App as SampleApp).Images[17]);
            anime.ImageHandles.Add((App as SampleApp).Images[18]);
            anime.ImageHandles.Add((App as SampleApp).Images[19]);
            anime.ImageHandles.Add((App as SampleApp).Images[20]);
            anime.ImageHandles.Add((App as SampleApp).Images[21]);
            anime.ImageHandles.Add((App as SampleApp).Images[22]);
            anime.ImageHandles.Add((App as SampleApp).Images[23]);

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
                DrawString(20, 430, $"Sprite Count:{cnt}", Stock.Colors.Black);
            }
        }

        protected override void DrawFrameAfterEffectDrawing()
        {
            var h = anime.GetImageHandle(App.WrapTime);
            DrawGraph(100, 100, h, true);
        }

        #endregion
    }
    #endregion
}
