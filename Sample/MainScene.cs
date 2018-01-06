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

        #region - CreatePushButton : プッシュボタンの生成
        /// <summary>
        /// プッシュボタンの生成
        /// </summary>
        /// <returns>PushButton</returns>
        private PushButton CreatePushButton()
        {
            var btn = new PushButton(this);
            btn.Rect = new Rectangle(100, 100, 200, 50);
            btn.OnDraw = b =>
            {
                DrawBox(b, App.ColorWhite, true);
            };
            btn.OnTapped = b =>
            {
                App.Quit();
            };
            return btn;
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
            AddSplite(CreatePushButton());

        }
        #endregion

        #endregion
    }
    #endregion
}
