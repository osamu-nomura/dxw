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

        #region ■ Private Methods

        #region - CreateOwn : 自機の生成
        /// <summary>
        /// 自機の生成
        /// </summary>
        /// <returns>Sprite</returns>
        private Sprite CreateOwn()
        {
            var initX = App.ScreenWidth / 2 - 64;
            var initY = App.ScreenHeight - 82;

            var own = new Sprite(this);
            own.Rect = new Rectangle(initX, initY, 128, 32);
            own.OnDraw = v => DrawBox(v, App.ColorWhite, true);
            own.OnUpdate = v =>
            {
                // ←が押下されたら自機を左に移動
                if (App.CheckHitKey(KeyCode.KEY_LEFT))
                {
                    if (v.X > 128)
                        v.X = v.X - 10;
                }
                // →が押下されたら自機を右に移動
                if (App.CheckHitKey(KeyCode.KEY_RIGHT))
                {
                    if (v.X < App.ScreenWidth - 256)
                        v.X = v.X + 10;
                }

                // SPEACEが押下されたらミサイル発射
                if (App.CheckOnKeyUp(KeyCode.KEY_SPACE))
                {
                    App.AddMessageLoopPostProcess(() =>
                    {
                        AddSplite(CreateMissile(v.Center));
                    });
                }
            };
            return own;
        }
        #endregion

        #region - CreateMissile : ミサイルの生成
        /// <summary>
        /// ミサイルの生成
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private Sprite CreateMissile(Point pt)
        {
            var missile = new Sprite(this);
            missile.Rect = new Rectangle(pt.X - 2, pt.Y, 4, 10);
            missile.OnDraw = v => DrawBox(missile, App.ColorWhite, true);
            missile.OnUpdate = v =>
            {
                v.Y = v.Y - 10;
                v.Removed = (v.Y < 0);
            };
            return missile;
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

            // スプライトの生成
            AddSplite(CreateOwn());

        }
        #endregion

        #endregion
    }
    #endregion
}
