using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxw
{
    #region 【Interface : ISpriteDrawing】
    /// <summary>
    /// スプライトの描画定義インターフェイス
    /// </summary>
    public interface ISpriteDrawing
    {
        #region ■ Methods

        #region - Draw : スプライトを描画する
        /// <summary>
        /// スプライトを描画する
        /// </summary>
        /// <param name="sprite">Sprite</param>
        void Draw(Sprite sprite);
        #endregion

        #region - DrawEffect : 効果を描画する
        /// <summary>
        /// 効果を描画する
        /// </summary>
        /// <param name="sprite">Sprite</param>
        void DrawEffect(Sprite sprite);
        #endregion

        #endregion
    }
    #endregion
}
