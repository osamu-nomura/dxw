using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;
using hsb.Extensions;

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
        public BaseSprite AddSplite(BaseSprite sprite)
        {
            Sprites.Add(sprite);
            return sprite;
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

        #region - Update: 状態を更新する
        /// <summary>
        /// 状態を更新する。
        /// </summary>
        public virtual void UpdateFrame()
        {
            Update();
            // 所有するスプライトの状態を順次更新する
            Sprites.Where(sprite => !sprite.Disable && sprite.Visible).ForEach(sprite => sprite.Update());
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

        #endregion
    }
    #endregion
}
