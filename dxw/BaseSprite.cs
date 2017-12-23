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
        #endregion

        #region ■ Properties

        #region - ID : ID値
        /// <summary>
        /// ID値
        /// </summary>
        public string ID { get; set; } = Guid.NewGuid().ToString();
        #endregion

        #region - Sceen : シーン
        /// <summary>
        /// シーン
        /// </summary>
        public BaseScene Sceen { get; protected set; } = null;
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

        #endregion

        #region ■ Constructor

        #region - Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        public BaseSprite()
            : base(0, 0, 0, 0)
        {
        }
        #endregion

        #region - Constructor(2)
        /// <summary>
        /// コンストラクター(2)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="x">X座標(px)</param>
        /// <param name="y">Y座標(px)</param>
        /// <param name="width">幅(px)</param>
        /// <param name="height">高さ(px)</param>
        public BaseSprite(BaseScene scene, int x, int y, int width, int height)
            : base (x, y, width, height)
        {
            Sceen = scene;
        }
        #endregion

        #region - Constructor(3)
        /// <summary>
        /// コンストラクタ(3)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="leftTop">左上座標(px)</param>
        /// <param name="size">矩形サイズ(px)</param>
        public BaseSprite(BaseScene scene, Point leftTop, RectangleSize size)
            : this(scene, leftTop.X, leftTop.Y, size.Width, size.Height)
        {
        }
        #endregion

        #region - Constructor(4)
        /// <summary>
        /// コンストラクタ(4)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="rect">矩形</param>
        public BaseSprite(BaseScene scene, Rectangle rect)
            : this(scene, rect.X, rect.Y, rect.Width, rect.Height)
        {
        }
        #endregion

        #region - Constructor(5)
        /// <summary>
        /// コンストラクタ(5)
        /// </summary>
        /// <param name="scene">シーン</param>
        /// <param name="leftTop">左上座標(px)</param>
        /// <param name="rightBottom">右下座標(px)</param>
        public BaseSprite(BaseScene scene, Point leftTop, Point rightBottom)
            : this(scene, new Rectangle(leftTop, rightBottom))
        {

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

        #region - SetImageSize : サイズを画像サイズに合わせる。
        /// <summary>
        /// サイズを画像サイズに合わせる。
        /// </summary>
        public void SetImageSize(int imageHandle)
        {
            // 画像サイズからボタンのサイズを取得する。
            var size = GetGraphSize(imageHandle);
            if (size.HasValue)
                Size = size.Value;
        }
        #endregion

        #endregion
    }
    #endregion
}
