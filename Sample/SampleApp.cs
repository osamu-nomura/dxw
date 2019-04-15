using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dxw;


using static dxw.Helper;

namespace Sample
{
    #region 【Class ; SampleApp】
    /// <summary>
    /// サンプルアプリケーションクラス
    /// </summary>
    class SampleApp : BaseApplication
    {

        public List<int> Images { get; set; }
        public int EnableButton { get; set; }
        public int EnableButtonSelected { get; set; }
        public int DisableButton { get; set; }
        public int DisableButtonSelected { get; set; }

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="screenWidth">画面幅(ピクセル)</param>
        /// <param name="screenHeight">画面高さ(ピクセル)</param>
        /// <param name="colorBitDepth">色進度(BIT)</param>
        public SampleApp(int screenWidth = 640, int screenHeight = 480, ColorBitDepth colorBitDepth = ColorBitDepth.BitDepth32)
            : base(screenWidth, screenHeight, colorBitDepth)
        {
            Title = "Sample Application";
            Scenes.Add(new MainScene(this));
        }
        #endregion

        #region ■ Protected Methods

        #region - Init : 初期化処理
        /// <summary>
        /// 初期化処理
        /// </summary>
        protected override void Init()
        {
            base.Init();

            IsShowSystemInformation = true;

            // ストックの初期化
            Stock.Init();

            // 音量初期化
            SEVolume = Const.SE_VOLUME;
            BGMVolume = Const.BGM_VOLUME;

        }
        #endregion

        protected override bool Loading(ulong elapsedTime)
        {
            Images = LoadDivGraph("ufo.png", 24, 8, 3, 64, 64);

            EnableButton = LoadGraph("enable_button.png");
            EnableButtonSelected = LoadGraph("enable_button_selected.png");
            DisableButton = LoadGraph("disable_button.png");
            DisableButtonSelected = LoadGraph("disable_button_selected.png");

            return false;
        }

        #region - MessageLoopEndRound : ループ後処理
        /// <summary>
        /// ループ後処理
        /// </summary>
        protected override void MessageLoopPostProcess()
        {
            base.MessageLoopPostProcess();
            if (CheckOnKeyUp(KeyCode.KEY_Q))
                Quit();
        }
        #endregion

        #endregion
    }
    #endregion
}
