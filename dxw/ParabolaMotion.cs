using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static dxw.Helper;

namespace dxw
{
    #region 【Class : ParabolaMotion】
    /// <summary>
    /// 放物線モーションクラス
    /// </summary>
    public class ParabolaMotion : ISpriteMotion
    {
        #region 【Inner Class : ArrivalEventArgs】
        /// <summary>
        /// 到着イベント引数
        /// </summary>
        public class ArrivalEventArgs
        {
            #region ■ Properties

            #region - Sender : 送信元スプライト
            /// <summary>
            /// 送信元スプライト
            /// </summary>
            public Sprite Sender { get; set; }
            #endregion

            #region - Motion : モーション
            /// <summary>
            /// モーション
            /// </summary>
            public ParabolaMotion Motion { get; set; }
            #endregion

            #endregion

        }
        #endregion

        #region 【Inner Class : CollisionEventArgs】
        /// <summary>
        /// 衝突イベント引数
        /// </summary>
        public class CollisionEventArgs : EventArgs
        {
            #region ■ Properties

            #region - Sender : 送信元スプライト
            /// <summary>
            /// 送信元スプライト
            /// </summary>
            public Sprite Sender { get; set; }
            #endregion

            #region - Motion : モーション
            /// <summary>
            /// モーション
            /// </summary>
            public ParabolaMotion Motion { get; set; }
            #endregion

            #region - TargetSprite : 対象スプライト
            /// <summary>
            /// 対象スプライト
            /// </summary>
            public BaseSprite TargetSprite { get; set; }
            #endregion

            #endregion
        }
        #endregion


        #region ■ Properties

        #region - StartPosition : 出発位置
        /// <summary>
        /// 出発位置
        /// </summary>
        public FPoint StartPosition { get; private set; }
        #endregion

        #region - GoalPosition : 到着位置
        /// <summary>
        /// 到着位置
        /// </summary>
        public FPoint GoalPosition { get; private set; }
        #endregion

        #region - ArrivalTime : 到着時間(mm秒）
        /// <summary>
        /// 到着時間(mm秒）
        /// </summary>
        public int ArrivalTime { get; private set; }
        #endregion

        #region - Gravity : 重力
        /// <summary>
        /// 重力
        /// </summary>
        public Vector Gravity { get; private set; }
        #endregion

        #region - CurrentPosition : 現在位置
        /// <summary>
        /// 現在位置
        /// </summary>
        public FPoint CurrentPosition { get; private set; }
        #endregion

        #region - Vector : ベクトル
        /// <summary>
        /// ベクトル
        /// </summary>
        public Vector? Vector { get; private set; }
        #endregion

        #region - ElapsedTime : 経過時間
        /// <summary>
        /// 経過時間
        /// </summary>
        public int ElapsedTime { get; private set; }
        #endregion

        #endregion

        #region ■ Delegates

        #region - OnArrival : スプライトが到着地点に到着した
        /// <summary>
        /// スプライトが到着地点に到着した
        /// </summary>
        public Action<ArrivalEventArgs> OnArrival { get; set; }
        #endregion

        #region - OnCollision : スプライトに衝突した
        /// <summary>
        /// スプライトに衝突した
        /// </summary>
        public Action<CollisionEventArgs> OnCollision { get; set; }
        #endregion

        #endregion
   
        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="startPosition">出発位置</param>
        /// <param name="goalPosition">到着位置</param>
        /// <param name="arrivalTime">到着時刻</param>
        /// <param name="gravity">重力</param>
        public ParabolaMotion(FPoint startPosition, FPoint goalPosition, int arrivalTime, Vector gravity)
        {
            StartPosition = startPosition;
            GoalPosition = goalPosition;
            ArrivalTime = arrivalTime;
            Gravity = gravity;
            CurrentPosition = StartPosition;
            var downX = gravity.X * Math.Pow(ArrivalTime, 2) * 0.5d;
            var downY = gravity.Y * Math.Pow(ArrivalTime, 2) * 0.5d;
            var pos = FPt(goalPosition.X - downX, goalPosition.Y - downY);
            Vector = StartPosition.Vector(pos) / ArrivalTime;
            ElapsedTime = 0;
        }
        #endregion

        #region ■ Methods

        #region - Update : スプライトの状態を更新する
        /// <summary>
        /// スプライトの状態を更新する
        /// </summary>
        /// <param name="sprite">スプライト</param>
        public void Update(Sprite sprite)
        {
            // 経過時間の更新
            var wrapTime = sprite.App.WrapTime;
            ElapsedTime += wrapTime;
            // 現在位置の算出
            if (Vector.HasValue)
            {
                var down = Gravity * Math.Pow(ElapsedTime, 2) * 0.5d;
                var newPos = StartPosition + (Vector.Value * ElapsedTime) + down;
                if (GoalPosition.Contact(newPos, 1.0d))
                {
                    CurrentPosition = GoalPosition;
                    Vector = null;
                    OnArrival?.Invoke(new ArrivalEventArgs
                    {
                        Sender = sprite,
                        Motion = this
                    });
                }
                else
                    CurrentPosition = newPos;
            }
            sprite.LeftTop = (Point)CurrentPosition;
        }
        #endregion

        #region - Collision : スプライトが他のスプライトに衝突した
        /// <summary>
        /// スプライトが他のスプライトに衝突した
        /// </summary>
        /// <param name="sprite">スプライト</param>
        /// <param name="target">衝突した対象</param>
        public void Collision(Sprite sprite, BaseSprite target)
        {
            OnCollision?.Invoke(new CollisionEventArgs
            {
                Sender = sprite,
                Motion = this,
                TargetSprite = target
            });
        }
        #endregion

        #endregion
    }
    #endregion
}
