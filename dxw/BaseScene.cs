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

        #region - Update : 更新処理
        /// <summary>
        /// 更新前処理
        /// </summary>
        protected virtual void Update()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - Updated : 更新後処理
        /// <summary>
        /// 更新後処理
        /// </summary>
        protected void Updated()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - DrawBackground : 背景を描画する
        /// <summary>
        /// 背景を描画する
        /// </summary>
        protected virtual void DrawBackground()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region - DrawForeground : 前景を描画する
        /// <summary>
        /// 前景を描画する
        /// </summary>
        protected virtual void DrawForeground()
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
            Sprites.RemoveAll(s => s.Removed);
        }
        #endregion

        #region - Update: 状態を更新する
        /// <summary>
        /// 状態を更新する。
        /// </summary>
        public virtual void UpdateFrame()
        {
            Update();
            // 所有するスプライトの状態を順次更新する
            Sprites.Where(sprite => !sprite.Disabled).ForEach(sprite => sprite.Update());
            Updated();
        }
        #endregion

        #region - DrawFrame : フレームを描画する。
        /// <summary>
        /// フレームを描画する。
        /// </summary>
        public virtual void DrawFrame()
        {
            // 背景を描画
            DrawBackground();
            // スプライトを描画
            Sprites.Where(sprites => sprites.Visible).ForEach(sprite => sprite.Draw());
            // 効果を描画
            Sprites.Where(sprites => sprites.Visible).ForEach(sprite => sprite.DrawEffect());
            // 前景を描画する
            DrawForeground();
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
