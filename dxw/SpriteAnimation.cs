using System.Collections.Generic;

namespace dxw
{
    #region 【Class : SpriteAnimation】
    /// <summary>
    /// スプライト用アニメーションクラス
    /// </summary>
    public class SpriteAnimation
    {
        #region ■ Properties

        #region - ImageHandles : アニメーション用の画像ハンドルのリスト
        /// <summary>
        /// アニメーション用の画像ハンドルのリスト
        /// </summary>
        public List<int> ImageHandles { get; set; } = null;
        #endregion

        #region - CurrentImageHandle : 現在表示対象となる画像ハンドルへのインデックス
        /// <summary>
        /// 現在表示対象となる画像ハンドルへのインデックス
        /// </summary>
        public int CurrentImageHandleIndex { get; set; } = 0;
        #endregion

        #region - FrameRate : フレームレート
        /// <summary>
        /// フレームレート
        /// </summary>
        public int FrameRate { get; set; } = 8;
        #endregion

        #region - WrapTime : 前回描画からの経過秒数(ms)
        /// <summary>
        /// 前回画像変更からの経過秒数(ms)
        /// </summary>
        public int WrapTime { get; private set; } = 0;
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SpriteAnimation()
        {
            // 特に何もすることはない
        }
        #endregion

        #region ■ Methods

        #region - Rest : アニメーションをリセットする
        /// <summary>
        /// アニメーションをリセットする
        /// </summary>
        public void Reset()
        {
            CurrentImageHandleIndex = 0;
            WrapTime = 0;
        }
        #endregion

        #region - GetImageHandle : 画像ハンドルを取得する
        /// <summary>
        /// 画像ハンドルを取得する
        /// </summary>
        /// <param name="wrapTime">前回描画時からの経過時間(ms)</param>
        /// <returns>画像ハンドル</returns>
        public int GetImageHandle(int wrapTime)
        {
            if ((ImageHandles?.Count ?? 0) == 0)
                return 0;

            var n = (WrapTime + wrapTime) / (1000 / FrameRate);
            if (n > 0)
            {
                CurrentImageHandleIndex = (CurrentImageHandleIndex + n >= ImageHandles.Count) ?
                    (CurrentImageHandleIndex + n) % ImageHandles.Count : CurrentImageHandleIndex + n;
                WrapTime = 0;
            }
            else
                WrapTime += wrapTime;
            return ImageHandles[CurrentImageHandleIndex];
        }
        #endregion

        #endregion

    }
    #endregion
}