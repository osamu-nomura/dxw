using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxw
{

    #region 【Class : CollisionEventArgs】
    /// <summary>
    /// 衝突イベント引数
    /// </summary>
    public class CollisionEventArgs : EventArgs
    {
        #region ■ Properties

        #region - Vector : ベクター
        /// <summary>
        /// ベクター
        /// </summary>
        public Vector Vector { get; set; }
        #endregion

        #region - Region : 領域
        /// <summary>
        /// 領域
        /// </summary>
        public Rectangle Region { get; set; }
        #endregion

        #region - Point : 座標
        /// <summary>
        /// 座標
        /// </summary>
        public Point Point { get; set; }
        #endregion

        #region - IsCollisionHorizontal : 水平領域が衝突した
        /// <summary>
        /// 水平領域が衝突した
        /// </summary>
        public bool IsCollisionHorizontal { get; set; }
        #endregion

        #region - IsCollisionVertical : 垂直領域が衝突した
        /// <summary>
        /// 垂直領域が衝突した
        /// </summary>
        public bool IsCollisionVertical { get; set; }
        #endregion

        #endregion
    }
    #endregion


    #region 【Class : VectorMotion】
    /// <summary>
    /// ベクターモーションクラス
    /// </summary>
    public class VectorMotion : ISpriteMotion
    {
        #region ■ Properties

        #region - Vector : ベクター
        /// <summary>
        /// ベクター
        /// </summary>
        public Vector Vector { get; set; }
        #endregion

        #region - Region : 領域
        /// <summary>
        /// 領域
        /// </summary>
        public Rectangle Region { get; set; }
        #endregion

        #endregion

        #region ■ Delegates

        #region - OnCollision : 領域に衝突した
        /// <summary>
        /// 領域に衝突した
        /// </summary>
        public Func<Sprite, CollisionEventArgs, Vector?> OnCollision { get; set; }
        #endregion

        #endregion

        #region ■ Constructor

        #region - Constructor(1)
        /// <summary>
        /// コンストラクタ(2)
        /// </summary>
        /// <param name="vector">ベクター</param>
        public VectorMotion(Vector vector)
        {
            Vector = vector;
        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクタ2
        /// </summary>
        /// <param name="vector">ベクター</param>
        /// <param name="region">領域</param>
        public VectorMotion(Vector vector, Rectangle region, Func<Sprite, CollisionEventArgs, Vector?> callback)
        {
            Vector = vector;
            Region = region;
            OnCollision = callback;
        }
        #endregion

        #endregion

        #region ■ Methods

        #region - Update : スプライトの状態を更新する
        /// <summary>
        /// スプライトの状態を更新する
        /// </summary>
        /// <param name="sprite">スプライト</param>
        public void Update(Sprite sprite)
        {
            var newPos = sprite.LeftTop + (Vector * sprite.App.WrapTime);
            if (Region != null && OnCollision != null)
            {
                var isCollisionHorizontal = !Region.CheckPointInHorizontalRegion(newPos);
                var isCollisionVertical = !Region.CheckPointInVerticalRegion(newPos);
                if (isCollisionHorizontal || isCollisionVertical)
                {
                    Vector = OnCollision(sprite, new CollisionEventArgs
                    {
                        Vector = Vector,
                        Region = Region,
                        Point = newPos,
                        IsCollisionHorizontal = isCollisionHorizontal,
                        IsCollisionVertical = isCollisionVertical
                    }) ?? Vector;
                    newPos = sprite.LeftTop + (Vector * sprite.App.WrapTime);
                }
            }
            sprite.LeftTop = newPos;
        }
        #endregion

        #endregion
    }
    #endregion
}
