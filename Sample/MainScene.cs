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

        #region - Btn : ボタン
        /// <summary>
        /// ボタン
        /// </summary>
        private PushButton Btn { get; set; }
        #endregion

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

        private PushButton CreateButton()
        {
            return new PushButton(this)
            {
                Width = 100,
                Height = 50,
                X = (App.ScreenWidth - 100) / 2,
                Y = (App.ScreenHeight - 50) / 2,
                OnDraw = b =>
                {
                    if (b.IsDown)
                    {
                        DrawBox(b, Stock.Colors.Black, true);
                        DrawString(b, HAlignment.Center, VAlignment.Middle, "Push", Stock.Colors.White, Stock.Font);
                    }
                    else
                    {
                        DrawBox(b, Stock.Colors.White, true);
                        DrawString(b, HAlignment.Center, VAlignment.Middle, "Push", Stock.Colors.Black, Stock.Font);
                    }
                },
                OnTapped = b =>
                {
                    App.Transition(new FadeTransition(this, App.GetScene(1), 1000));
                }
            };
        }


        #endregion

        #region ■ protected Methods

        #region - UpdateFrame : フレームを更新する
        /// <summary>
        /// フレームを更新する
        /// </summary>
        public override void UpdateFrame()
        {
            base.UpdateFrame();
        }
        #endregion

        #region - DrawBackground : 背景を描画する
        /// <summary>
        /// 背景を描画する
        /// </summary>
        protected override void DrawBackground()
        {
            base.DrawBackground();
            FillBackground(Stock.Colors.Red);
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

            Btn = AddSplite(CreateButton());
        }
        #endregion

        #endregion
    }
    #endregion
}
