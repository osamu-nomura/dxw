using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxw
{
    #region 【Class ; BaseSprite】
    /// <summary>
    /// スプライトクラス
    /// </summary>
    public class BaseSprite
    {
        #region ■ Properties

        #region - Sceen : シーン
        /// <summary>
        /// シーン
        /// </summary>
        public BaseScene Sceen { get; protected set; }
        #endregion

        #region - X ; X座標
        /// <summary>
        /// X座標
        /// </summary>
        public int X { get; set; }
        #endregion

        #region - Y : Y座標
        /// <summary>
        /// Y座標
        /// </summary>
        public int Y { get; set; }
        #endregion

        #region - Width : 幅
        /// <summary>
        /// 幅
        /// </summary>
        public int Width { get; set; }
        #endregion

        #region - Height : 高さ
        /// <summary>
        /// 高さ
        /// </summary>
        public int Height { get; set; }
        #endregion

        #region - X2 : X2座標
        /// <summary>
        /// X2座標
        /// </summary>
        public int X2 { get { return X + Width; } }
        #endregion

        #region - Y2 : Y2座標
        /// <summary>
        /// Y2座標
        /// </summary>
        public int Y2 { get { return Y + Height; } }
        #endregion

        #region - Disable : 無効か？
        /// <summary>
        /// 無効か？
        /// </summary>
        public bool Disable { get; set; }
        #endregion

        #region - Visible : 表示対象か？
        /// <summary>
        /// 表示対象か？
        /// </summary>
        public bool Visible { get; set; }
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="x">X座標(px)</param>
        /// <param name="y">Y座標(px)</param>
        /// <param name="width">幅(px)</param>
        /// <param name="height">高さ(px)</param>
        public BaseSprite(BaseScene scene, int x, int y, int width, int height)
        {
            Sceen = scene;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Disable = false;
            Visible = true;
        }
        #endregion

        #region ■ Methods

        #region - LoadCompleted : リソースのロードが完了
        /// <summary>
        /// リソースのロードが完了
        /// </summary>
        public virtual void LoadCompleted()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - Update (Virtual) : 状態を更新する。
        /// <summary>
        /// 状態を更新する。
        /// </summary>
        public virtual void Update()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - Draw (Virtual) ; スプライトを描画する
        /// <summary>
        /// スプライトを描画する
        /// </summary>
        public virtual void Draw()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - DrawEffect : 効果を描画する
        /// <summary>
        /// 効果を描画する
        /// </summary>
        public virtual void DrawEffect()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - CheckPointInRegion : ポイントが領域内かどうか判定
        /// <summary>
        /// ポイントが領域内かどうか判定
        /// </summary>
        /// <param name="x">X軸</param>
        /// <param name="y">Y軸</param>
        /// <returns>True;領域内 / False:領域外</returns>
        public bool CheckPointInRegion(int x, int y)
        {
            return x >= X && x <= X2 && y >= Y && y <= Y2;
        }
        #endregion

        #region - CheckCollision : 衝突判定
        /// <summary>
        /// 衝突判定
        /// </summary>
        /// <param name="target">対象スプライト</param>
        /// <returns>True : 諸突 / False : 衝突していない</returns>
        public bool CheckCollision(BaseSprite target)
        {
            return (Math.Abs(X - target.X) < Width / 2 + target.Width / 2) &&
                   (Math.Abs(Y - target.Y) < Height / 2 + target.Height / 2);
        }
        #endregion

        #endregion
    }
    #endregion
}
