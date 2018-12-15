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

        private Sprite CreateSprite(int x, int y, double vx, double vy)
        {
            var sprite = new Sprite(this, new Rectangle(x, y, 10, 10));
            sprite.Motion = new VectorMotion(new Vector(vx, vy), App.ScreenRect,
                (sender, args) => {
                    if (args.IsCollisionSprite)
                    {
                        var targetMotion = (args.TargetSprite as Sprite).Motion as VectorMotion;
                        var vec = targetMotion.Vector;
                        targetMotion.Vector = RoundVector(targetMotion.Vector + args.Vector * 2);
                        return RoundVector(args.Vector + vec * 2);
                    }
                    else
                        return args.Vector.Flip(args.IsCollisionHorizontal, args.IsCollisionVertical);
                }
            );
            sprite.OnDraw = v =>
            {
                DrawBox(v, Stock.Colors.Red, true);
            };
            return sprite;
        }

        #endregion

        #region ■ protected Methods

        #region - DrawFrameBeforeSpriteDrawing : フレームを描画する（スプライト描画前）
        /// <summary>
        /// フレームを描画する（スプライト描画前）
        /// </summary>
        protected override void DrawFrameBeforeSpriteDrawing()
        {
            base.DrawFrameBeforeSpriteDrawing();
            FillBackground(Stock.Colors.Yellow);
            SetDrawingWindow(App.ScreenRect.Scaling(-100));
            FillBackground(Stock.Colors.White);
            DrawString(10, 10, "TEST TEST TEST", Stock.Colors.Red);
        }
        #endregion

        #region - DrawFrameAfterEffectDrawing : フレームを描画する（効果描画後）
        /// <summary>
        /// フレームを描画する（効果描画後）
        /// </summary>
        protected override void DrawFrameAfterEffectDrawing()
        {
            base.DrawFrameAfterEffectDrawing();
            ClearDrawingWindow();
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
            var r = new Random(1000);
            foreach (var n in Enumerable.Range(0, 100))
            {
                AddSplite(CreateSprite(
                    GetRand(App.ScreenWidth),
                    GetRand(App.ScreenHeight),
                    r.NextDouble(),
                    r.NextDouble()));
            }
        }
        #endregion

        #endregion
    }
    #endregion
}
