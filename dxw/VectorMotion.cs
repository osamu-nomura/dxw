using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dxw
{
    #region 【Class : VectorMotion】
    /// <summary>
    /// ベクターモーションクラス
    /// </summary>
    public class VectorMotion : ISpriteMotion
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

            #region - Position : 座標
            /// <summary>
            /// 座標
            /// </summary>
            public FPoint Position { get; set; }
            #endregion

            #region - TargetSprite : 対象スプライト
            /// <summary>
            /// 対象スプライト
            /// </summary>
            public BaseSprite TargetSprite { get; set; }
            #endregion

            #region - IsCollisionSprite : 他のスプライトと衝突した
            /// <summary>
            /// 他のスプライトと衝突した
            /// </summary>
            public bool IsCollisionSprite { get; set; }
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

        #region ■ Properties

        #region - Position : ポジション
        /// <summary>
        /// ポジション
        /// </summary>
        public FPoint Position { get; set; }
        #endregion

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
        /// <param name="position">位置</param>
        /// <param name="vector">ベクター</param>
        public VectorMotion(FPoint position, Vector vector)
        {
            Position = position;
            Vector = vector;
        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクタ2
        /// </summary>
        /// <param name="position">位置</param>
        /// <param name="vector">ベクター</param>
        /// <param name="region">領域</param>
        /// <param name="callback">コールバック</param>
        public VectorMotion(FPoint position, Vector vector, Rectangle region, Func<Sprite, CollisionEventArgs, Vector?> callback)
        {
            Position = position;
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
            var newPos = Position + (Vector * sprite.App.WrapTime);
            if (Region != null && OnCollision != null)
            {
                var isCollisionHorizontal = !Region.CheckPointInHorizontalRegion((Point)newPos);
                var isCollisionVertical = !Region.CheckPointInVerticalRegion((Point)newPos);
                if (isCollisionHorizontal || isCollisionVertical)
                {
                    Vector = OnCollision(sprite, new CollisionEventArgs
                    {
                        Vector = Vector,
                        Region = Region,
                        Position = newPos,
                        TargetSprite = null,
                        IsCollisionSprite = false,
                        IsCollisionHorizontal = isCollisionHorizontal,
                        IsCollisionVertical = isCollisionVertical
                    }) ?? Vector;
                    var newX = newPos.X < 0 ? 0 : newPos.X > Region.Width  ? Region.Width : newPos.X;
                    var newY = newPos.Y < 0 ? 0 : newPos.Y > Region.Height  ? Region.Height : newPos.Y;
                    newPos = new FPoint(newX, newY);
                }
                else
                {
                    Vector = Vector * 0.99d;
                }
            }
            Position = newPos;
            sprite.LeftTop = (Point)Position;
        }
        #endregion

        #region - Collision : 他のスプライトと衝突した
        /// <summary>
        /// 他のスプライトと衝突した
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="target"></param>
        public void Collision(Sprite sprite, BaseSprite target)
        {
            Vector = OnCollision(sprite, new CollisionEventArgs
            {
                Vector = Vector,
                Region = Region,
                Position = Position,
                TargetSprite = target,
                IsCollisionSprite = true,
                IsCollisionHorizontal = false,
                IsCollisionVertical = false
            }) ?? Vector;
        }
        #endregion

        #endregion
    }
    #endregion
}
