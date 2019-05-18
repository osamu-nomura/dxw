using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxw
{
    #region 【Interface : ISpriteImageSource】
    /// <summary>
    /// スプライトのイメージソースインターフェイス
    /// </summary>
    public interface ISpriteImageSource
    {
        #region ■ Methods

        #region - GetImageHandle : 画像ハンドルを取得する
        /// <summary>
        /// 画像ハンドルを取得する
        /// </summary>
        /// <param name="sprite">スプライト</param>
        /// <returns>画像ハンドル</returns>
        int GetImageHandle(Sprite sprite);
        #endregion
        #endregion
    }
    #endregion
}
