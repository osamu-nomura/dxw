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


        #endregion

        #region ■ protected Methods

        #region - DrawBackground : 背景を描画する
        /// <summary>
        /// 背景を描画する
        /// </summary>
        protected override void DrawBackground()
        {
            base.DrawBackground();
            FillBackground(Stock.Colors.White);
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
            var sprite = new Sprite(this, new Rectangle(0, 50, 20, 20));
            sprite.Motion = new VectorMotion(new Vector(0.5, 0.7), App.ScreenRect, 
                (sender,args) => args.Vector.Flip(args.IsCollisionHorizontal, args.IsCollisionVertical)
            );
            sprite.OnDraw = v =>
            {
                DrawBox(v, Stock.Colors.Red, true);
            };
            AddSplite(sprite);
        }
        #endregion

        #endregion
    }
    #endregion
}
