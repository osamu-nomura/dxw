using System;
using System.Collections.Generic;
using System.Linq;
using hsb.Extensions;

using static dxw.Helper;

namespace dxw
{
    #region 【Class  BaseScene】
    /// <summary>
    ///  基底シーンクラス
    /// </summary>
    public class BaseScene
    {
        #region ■ Properties

        #region - App : アプリケーションオブジェクト
        /// <summary>
        /// アプリケーションオブジェクト
        /// </summary>
        public BaseApplication App { get; protected set; }
        #endregion

        #region - IsAttach : シーンがアタッチされている
        /// <summary>
        /// シーンがアタッチされている
        /// </summary>
        public bool IsAttach
        {
            get { return AttachiTime.HasValue; }
        }
        #endregion

        #region - AttachiTime : アタッチ開始時刻
        /// <summary>
        /// アタッチ開始時刻
        /// </summary>
        public ulong? AttachiTime { get; set; } = null;
        #endregion

        #region - ElapsedTime : 経過時間(mm秒)
        /// <summary>
        /// アタッチされてからの経過時間をmm秒で返す。
        /// 　アタッチされていない場合は-1
        /// </summary>
        public ulong? ElapsedTime
        {
            get
            {
                if (AttachiTime.HasValue)
                    return App.ElapsedTime - AttachiTime;
                else
                    return null;
            }
        }
        #endregion

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

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseScene(BaseApplication app)
        {
            App = app;
        }
        #endregion

        #region ■ Protected Methods

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
            Sprites.Sort((s1,s2)=> s1.Order.CompareTo(s2.Order));
        }
        #endregion

        #region - UpdateFrameBeforeSpriteUpdate : フレームの更新（スプライト更新前）
        /// <summary>
        /// フレームの更新（スプライト更新前）
        /// </summary>
        protected virtual void UpdateFrameBeforeSpriteUpdate()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - UpdateFrameAfterSpriteUpdate : フレームの更新（スプライト更新後）
        /// <summary>
        /// フレームの更新（スプライト更新後）
        /// </summary>
        protected void UpdateFrameAfterSpriteUpdate()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - DrawFrameBeforeSpriteDrawing : フレームの描画（スプライト描画前）
        /// <summary>
        /// フレームの描画（スプライト描画前）
        /// </summary>
        protected virtual void DrawFrameBeforeSpriteDrawing()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - DrawFrameAfterSpriteDrawing : フレームの描画（スプライト描画後）
        /// <summary>
        /// フレームの描画（スプライト描画後）
        /// </summary>
        protected virtual void DrawFrameAfterSpriteDrawing()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - DrawFrameAfterEffectDrawing : フレームを描画する（効果描画後）
        /// <summary>
        /// フレームを描画する（効果描画後
        /// </summary>
        protected virtual void DrawFrameAfterEffectDrawing()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - FillBackground : 背景を指定色で塗りつぶす
        /// <summary>
        /// 背景を指定色で塗りつぶす
        /// </summary>
        /// <param name="color">指定色</param>
        protected void FillBackground(uint color)
        {
            App.FillBackground(color);
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

        #region ■ Public Method

        #region - LoadCompleted :リソースのロードが完了した。
        /// <summary>
        /// リソースのロードが完了した。
        /// </summary>
        public virtual void LoadCompleted()
        {
            Sprites.ForEach(f => f.LoadCompleted());
        }
        #endregion

        #region - AttachiCurrent ; シーンがカレントシーンにアタッチされた。
        /// <summary>
        /// シーンがカレントシーンにアタッチされた。
        /// </summary>
        /// <param name="app">アプリケーション</param>
        /// <param name="prevScene">直前のカレントシーン</param>
        public virtual void AttachiCurrent(BaseScene prevScene)
        {
            AttachiTime = App.ElapsedTime;
        }
        #endregion

        #region - DettachCurrent : シーンがカレントからデタッチされた。
        /// <summary>
        /// シーンがカレントからデタッチされた。
        /// </summary>
        /// <param name="app">アプリケーション</param>
        /// <param name="nextScene">次のカレントシーン</param>
        public virtual void DettachCurrent(BaseScene nextScene)
        {
            AttachiTime = null;
        }
        #endregion

        #region - MessageLoopPreProcess : ループ前処理
        /// <summary>
        /// ループ前処理
        /// </summary>
        public virtual void MessageLoopPreProcess()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - MessageLoopPostProcess : ループ後処理
        /// <summary>
        /// ループ後処理
        /// </summary>
        public virtual void MessageLoopPostProcess()
        {
            // 削除フラグをセットされたスプライトを削除する
            Sprites.ForEach(s =>
            {
                var panel = s as Panel;
                if (panel != null)
                {
                    panel.Sprites.Where(c => c.Remove).ForEach(c => c.Removed());
                    panel.Sprites.RemoveAll(c => c.Remove);
                }
                if (s.Remove)
                    s.Removed();
            });
            Sprites.RemoveAll(s => s.Remove);
        }
        #endregion

        #region - UpdateFrame: 状態を更新する
        /// <summary>
        /// 状態を更新する。
        /// </summary>
        public virtual void UpdateFrame()
        {
            UpdateFrameBeforeSpriteUpdate();
            // 所有するスプライトの状態を順次更新する
            Sprites.Where(sprite => !sprite.Disabled).ForEach(sprite => sprite.Update());
            //所有するスプライト間の衝突をチェックする
            if (EnableCollisionCheck)
            {
                CollisionCheck();
            }
            UpdateFrameAfterSpriteUpdate();
        }
        #endregion

        #region - DrawFrame : フレームを描画する。
        /// <summary>
        /// フレームを描画する。
        /// </summary>
        public virtual void DrawFrame()
        {
            // フレームを描画（スプライト描画前）
            DrawFrameBeforeSpriteDrawing();
            // スプライトを描画
            Sprites.Where(sprites => sprites.Visible).ForEach(sprite => sprite.Draw());
            // フレームを描画（スプライト描画後）
            DrawFrameAfterSpriteDrawing();
            // 効果を描画
            Sprites.Where(sprites => sprites.Visible).ForEach(sprite => sprite.DrawEffect());
            // フレームを描画（エフェクト描画後）
            DrawFrameAfterEffectDrawing();
        }
        #endregion

        #region - OnShowDialog : ダイアログが表示された
        /// <summary>
        /// ダイアログが表示された
        /// </summary>
        /// <param name="dialog">ダイアログ</param>
        public virtual void OnShowDialog(BaseScene dialog)
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - OnHideDialog : ダイアログが非表示になった
        /// <summary>
        /// ダイアログが非表示になった
        /// </summary>
        /// <param name="dialog">ダイアログ</param>
        public virtual void OnHideDialog(BaseScene dialog)
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #endregion
    }
    #endregion
}
