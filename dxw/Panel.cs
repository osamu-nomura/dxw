using hsb.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static dxw.Helper;

namespace dxw
{
    #region 【Class : Panel】
    /// <summary>
    /// パネルクラス
    /// </summary>
    public class Panel : BaseSprite
    {
        #region ■ Properties

        #region - Sprites : スプライトリスト
        /// <summary>
        /// スプライトリスト
        /// </summary>
        public List<BaseSprite> Sprites { get; private set; } = new List<BaseSprite>();
        #endregion

        #region - EnableCollisionCheck : 衝突判定の有効・無効
        /// <summary>
        /// 衝突判定の有効・無効
        /// </summary>
        public bool EnableCollisionCheck { get; set; } = false;
        #endregion

        #endregion

        #region ■ Delegates

        #region - OnLoadCompleted : リソースのロードが完了した
        /// <summary>
        /// リソースのロードが完了した
        /// </summary>
        public Action<Panel> OnLoadCompleted = null;
        #endregion

        #region - OnUpdateBeforeSpriteUpdate : 状態の更新（スプライト更新前）
        /// <summary>
        /// 状態の更新（スプライト更新前）
        /// </summary>
        public Action<Panel> OnUpdateBeforeSpriteUpdate = null;
        #endregion

        #region - OnUpdateAfterSpriteUpdate : 状態の更新（スプライト更新後）
        /// <summary>
        /// 状態の更新（スプライト更新後）
        /// </summary>
        public Action<Panel> OnUpdateAfterSpriteUpdate = null;
        #endregion

        #region - OnDrawBeforeSpriteDrawing : パネルの描画（スプライト描画前）
        /// <summary>
        /// パネルの描画（スプライト描画前）
        /// </summary>
        public Action<Panel> OnDrawBeforeSpriteDrawing = null;
        #endregion

        #region - OnDrawAfterSpriteDrawing : パネルの描画（スプライト描画後）
        /// <summary>
        /// パネルの描画（スプライト描画後）
        /// </summary>
        public Action<Panel> OnDrawAfterSpriteDrawing = null;
        #endregion

        #region - OnDrawAfterEffectDrawing : パネルを描画（効果描画後）
        /// <summary>
        /// パネルを描画（効果描画後）
        /// </summary>
        public Action<Panel> OnDrawAfterEffectDrawing = null;
        #endregion

        #endregion

        #region ■ Constructor

        #region - Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        /// <param name="app">アプリケーション</param>
        /// <param name="rect">矩形</param>
        public Panel(BaseApplication app, Rectangle rect = null)
            : base(app, rect)
        {

        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクタ(2)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="rect">矩形</param>
        public Panel(BaseScene scene, Rectangle rect = null)
            : base(scene, rect)
        {
        }
        #endregion

        #region - Constructor(3)
        /// <summary>
        /// コンストラクタ(3)
        /// </summary>
        /// <param name="parent">親スプライト</param>
        /// <param name="rect">矩形</param>
        public Panel(BaseSprite parent, Rectangle rect = null)
            : base(parent, rect)
        {
        }
        #endregion

        #endregion

        #region ■ Protected Methods

        #region - UpdateBeforeSpriteUpdate : 状態の更新（スプライト更新前）
        /// <summary>
        /// 状態の更新（スプライト更新前）
        /// </summary>
        protected virtual void UpdateBeforeSpriteUpdate()
        {
            OnUpdateAfterSpriteUpdate?.Invoke(this);
        }
        #endregion

        #region - UpdateAfterSpriteUpdate : 状態の更新（スプライト更新後）
        /// <summary>
        /// 状態の更新（スプライト更新後）
        /// </summary>
        protected void UpdateAfterSpriteUpdate()
        {
            OnUpdateAfterSpriteUpdate?.Invoke(this);
        }
        #endregion

        #region - DrawBeforeSpriteDrawing : パネルの描画（スプライト描画前）
        /// <summary>
        /// パネルの描画（スプライト描画前）
        /// </summary>
        protected virtual void DrawBeforeSpriteDrawing()
        {
            OnDrawBeforeSpriteDrawing?.Invoke(this);
        }
        #endregion

        #region - DrawAfterSpriteDrawing : パネルの描画（スプライト描画後）
        /// <summary>
        /// パネルの描画（スプライト描画後）
        /// </summary>
        protected virtual void DrawAfterSpriteDrawing()
        {
            OnDrawAfterSpriteDrawing?.Invoke(this);
        }
        #endregion

        #region - DrawAfterEffectDrawing : パネルを描画する（効果描画後）
        /// <summary>
        /// パネルを描画する（効果描画後）
        /// </summary>
        protected virtual void DrawAfterEffectDrawing()
        {
            OnDrawAfterEffectDrawing?.Invoke(this);
        }
        #endregion

        #region - CollisionCheck : スプライトの衝突判定処理
        /// <summary>
        /// スプライトの衝突判定処理
        /// </summary>
        protected void CollisionCheck()
        {
            for (var i = 0; i < Sprites.Count; i++)
            {
                var sprite = Sprites[i];
                for (var j = i + 1; j < Sprites.Count; j++)
                {
                    var target = Sprites[j];
                    if (sprite.ColisionRect.CheckCollision(target.ColisionRect))
                    {
                        if (sprite.CollisionSprite != target)
                        {
                            sprite.CollisionSprite = target;
                            sprite.Collision(target);
                        }
                    }
                    else
                    {
                        if (sprite.CollisionSprite == target)
                            sprite.CollisionSprite = null;
                    }
                }
            }
        }
        #endregion

        #endregion

        #region ■ Public Methods

        #region - LoadCompleted : リソースのロードが完了した
        /// <summary>
        /// リソースのロードが完了した
        /// </summary>
        public override void LoadCompleted()
        {
            base.LoadCompleted();
            // 所有するスプライトに対しても順次、LoadCompletedを伝播
            Sprites.ForEach(f => f.LoadCompleted());
            OnLoadCompleted?.Invoke(this);
        }
        #endregion

        #region - Update : 状態の更新
        /// <summary>
        /// 状態の更新
        /// </summary>
        public override void Update()
        {
            base.Update();
            UpdateBeforeSpriteUpdate();
            // 所有するスプライトの状態を順次更新する
            Sprites.Where(sprite => !sprite.Disabled).ForEach(sprite => sprite.Update());
            //所有するスプライト間の衝突をチェックする
            if (EnableCollisionCheck)
            {
                CollisionCheck();
            }
            UpdateAfterSpriteUpdate();
        }
        #endregion

        #region - Draw : パネルを描画する
        /// <summary>
        /// パネルを描画する
        /// </summary>
        public override void Draw()
        {
            base.Draw();
            SetDrawingWindow(this, true, ()  =>
            {
                // フレームを描画（スプライト描画前）
                DrawBeforeSpriteDrawing();
                // スプライトを描画
                Sprites.Where(sprites => sprites.Visible).ForEach(sprite => sprite.Draw());
                // フレームを描画（スプライト描画後）
                DrawAfterSpriteDrawing();
                // 効果を描画
                Sprites.Where(sprites => sprites.Visible).ForEach(sprite => sprite.DrawEffect());
                // フレームを描画（エフェクト描画後）
                DrawAfterEffectDrawing();
            });
        }
        #endregion

        #region - AddSplite : スプライトをシーンに追加する。
        /// <summary>
        /// スプライトをシーンに追加する。
        /// </summary>
        /// <param name="sprite"></param>
        public T AddSplite<T>(T sprite) where T : BaseSprite
        {
            if (App.IsLoadCompleted)
                sprite.LoadCompleted();
            Sprites.Add(sprite);
            return sprite;
        }
        #endregion

        #region - SortSpite : スプライトをOrder順に並び替える
        /// <summary>
        /// スプライトをOrder順に並び替える
        /// </summary>
        protected void SortSpite()
        {
            Sprites.Sort((s1, s2) => s1.Order.CompareTo(s2.Order));
        }
        #endregion

        #region - Removed : パネルがコンテナから削除された
        /// <summary>
        /// パネルがコンテナから削除された
        /// </summary>
        public override void Removed()
        {
            base.Removed();
            // 削除フラグをセットされたスプライトを削除する
            Sprites.ForEach(s => s.Removed());
        }
        #endregion

        #endregion
    }
    #endregion
}
