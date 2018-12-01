using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxw
{
    #region 【Interface : ISpriteMotion】
    /// <summary>
    /// スプライトのモーション定義インターフェイス
    /// </summary>
    public interface ISpriteMotion
    {
        #region ■ Methods

        #region - Update : スプライトの状態を更新する
        /// <summary>
        /// スプライトの状態を更新する
        /// </summary>
        /// <param name="sprite">スプライト</param>
        void Update(Sprite sprite);
        #endregion

        #endregion
    }
    #endregion
}
