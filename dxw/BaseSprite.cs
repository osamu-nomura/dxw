using System;

using static dxw.Helper;

namespace dxw
{
    #region 【Class ; BaseSprite】
    /// <summary>
    /// スプライトクラス
    /// </summary>
    public class BaseSprite : Rectangle
    {
        #region ■ Members
        /// <summary>
        /// 有効？
        /// </summary>
        private bool _enabled = true;
        private Rectangle _colisionRect = null;
        #endregion

        #region ■ Properties

        #region - ID : ID値
        /// <summary>
        /// ID値
        /// </summary>
        public string ID { get; set; } = Guid.NewGuid().ToString();
        #endregion

        #region - Order : 並び順
        /// <summary>
        /// 並び順
        /// </summary>
        public int Order { get; set; }
        #endregion

        #region - App : アプリケーション
        /// <summary>
        /// アプリケーション
        /// </summary>
        public BaseApplication App { get; protected set; } = null;
        #endregion

        #region - Sceen : シーン
        /// <summary>
        /// シーン
        /// </summary>
        public BaseScene Sceen { get; protected set; } = null;
        #endregion

        #region - Parent : 親スプライト
        /// <summary>
        /// 親スプライト
        /// </summary>
        public BaseSprite Parent { get; private set; } = null;
        #endregion

        #region - Enabled : 有効？
        /// <summary>
        ///  有効？
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    ChangeEnabled(_enabled);
                }
            }
        }
        #endregion

        #region - Disabled : 無効？
        /// <summary>
        /// 無効？
        /// </summary>
        public bool Disabled
        {
            get { return !Enabled; }
            set { Enabled = !value; }
        }
        #endregion

        #region - Visible : 表示対象か？
        /// <summary>
        /// 表示対象か？
        /// </summary>
        public bool Visible { get; set; } = true;
        #endregion

        #region - Tag :  タグ
        /// <summary>
        /// タグ
        /// </summary>
        public Object Tag { get; set; } = null;
        #endregion

        #region - Rect : 矩形を返す
        /// <summary>
        /// 矩形を返す
        /// </summary>
        public Rectangle Rect
        {
            get { return this as Rectangle;  }
            set
            {
                X = value?.X ?? 0;
                Y = value?.Y ?? 0;
                Width = value?.Width ?? 0;
                Height = value?.Height ?? 0;
            }
        }
        #endregion

        #region - ColisionRect : 衝突判定用の矩形を返す
        /// <summary>
        /// 衝突判定用の矩形を返す
        /// </summary>
        public Rectangle ColisionRect
        {
            get { return _colisionRect ?? Rect;  }
            set { _colisionRect = value;  }
        }
        #endregion

        #region - CollisionSprite : 衝突対象スプライト
        /// <summary>
        /// 衝突対象スプライト
        /// </summary>
        public BaseSprite CollisionSprite { get; set; } = null;
        #endregion

        #region - Remove : 削除する？
        /// <summary>
        /// ループの終了時に削除する
        /// </summary>
        public bool Remove { get; set; }
        #endregion

        #endregion

        #region ■ Constructor

        #region - Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        /// <param name="app">アプリケーション</param>
        /// <param name="rect">矩形</param>
        public BaseSprite(BaseApplication  app, Rectangle rect = null)
            : base(rect)
        {
            App = app;
        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクタ(2)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="rect">矩形</param>
        public BaseSprite(BaseScene scene, Rectangle rect = null)
            : base(rect)
        {
            Sceen = scene;
            App = scene?.App;
        }
        #endregion

        #region - COnstructor (3)
        /// <summary>
        /// コンストラクタ(3)
        /// </summary>
        /// <param name="parent">親スプライト</param>
        /// <param name="rect">矩形</param>
        public BaseSprite(BaseSprite parent, Rectangle rect = null)
            : base (rect)
        {
            Parent = parent;
            Sceen = parent.Sceen;
            App = parent.App;
        }
        #endregion

        #endregion

        #region ■ Protected Methods

        #region - ChangeEnabled : 有効無効が変更された
        /// <summary>
        /// 有効無効が変更された
        /// </summary>
        /// <param name="enabled">有効？</param>
        protected virtual  void ChangeEnabled(bool enabled)
        {
            // 派生クラスでオーバーロードする
        }
        #endregion

        #endregion

        #region ■ Public Methods

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

        #region - Collision (Virtual) : 他のスプライトと衝突した
        /// <summary>
        /// 他のスプライトと衝突した
        /// </summary>
        /// <param name="target">対象スプライト</param>
        public virtual void Collision(BaseSprite target)
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

        #region - Removed : コンテナから削除された
        /// <summary>
        /// コンテナから削除された
        /// </summary>
        public virtual void Removed()
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #endregion
    }
    #endregion
}
