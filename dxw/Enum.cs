using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DxLibDLL;

namespace dxw
{
    #region ■ Enums

    #region - WindowMode : ウィンドウモード
    /// <summary>
    /// ウィンドウモード
    /// </summary>
    public enum WindowMode
    {
        FullScreen = 0,     // 全画面モード
        Window = 1          // Windowモード
    }
    #endregion

    #region - DeviceType : デバイス種別
    /// <summary>
    /// デバイス種別
    /// </summary>
    public enum DeviceType
    {
        Mouse = 0,  // マウス
        Touch = 1   // タッチ
    }
    #endregion

    #region - ColorBitDepth : 色BIT数
    /// <summary>
    /// 色BIT数
    /// </summary>
    public enum ColorBitDepth
    {
        BitDepth16 = 16,
        BitDepth32 = 32
    };
    #endregion

    #region - Orientation : 向き
    /// <summary>
    /// 向き
    /// </summary>
    public enum Orientation
    {
        Horizontal = 0, // 水平
        Vertical = 1,   // 垂直
        Both = 2        // 両方
    }
    #endregion

    #region - PlayType : 再生形式
    /// <summary>
    /// 再生形式
    /// </summary>
    public enum PlayType
    {
        Normal = DX.DX_PLAYTYPE_NORMAL,
        Back = DX.DX_PLAYTYPE_BACK,
        Loop = DX.DX_PLAYTYPE_LOOP
    }
    #endregion

    #region - MouseInput : マウスボタン種別
    /// <summary>
    /// マウスボタン種別
    /// </summary>
    public enum MouseInput
    {
        Left = DX.MOUSE_INPUT_LEFT,     // 左ボタン
        Right = DX.MOUSE_INPUT_RIGHT,   // 右ボタン
        Middle = DX.MOUSE_INPUT_MIDDLE  // 中央ボタン
    }
    #endregion

    #region - FontType : フォント種別
    /// <summary>
    /// フォント種別
    /// </summary>
    public enum FontType
    {
        Normal = DX.DX_FONTTYPE_NORMAL,                             // ノーマルフォント
        Edge = DX.DX_FONTTYPE_EDGE,                                 // エッジつきフォント
        AntiAlias = DX.DX_FONTTYPE_ANTIALIASING,                    // アンチエイリアスフォント
        AntiAlias4x4 = DX.DX_FONTTYPE_ANTIALIASING_4X4,             // アンチエイリアスフォント( 4x4サンプリング )
        AntiAlias8x8 = DX.DX_FONTTYPE_ANTIALIASING_8X8,             // アンチエイリアスフォント( 8x8サンプリング )
        AntiAliasEdge4x4 = DX.DX_FONTTYPE_ANTIALIASING_EDGE_4X4,    // アンチエイリアス＆エッジ付きフォント( 4x4サンプリング )
        AntiAliasEdge8x8 = DX.DX_FONTTYPE_ANTIALIASING_EDGE_8X8     // アンチエイリアス＆エッジ付きフォント( 8x8サンプリング )
    };
    #endregion

    #region - DrawScreen : 描画スクリーン
    /// <summary>
    /// 描画スクリーン
    /// </summary>
    public enum DrawScreen
    {
        Front = DX.DX_SCREEN_FRONT,         // 表画面
        Background = DX.DX_SCREEN_BACK      // 裏画面
    }
    #endregion

    #region - KeyCode : キーコード
    /// <summary>
    /// キーコード
    /// </summary>
    public enum KeyCode
    {
        KEY_ESCAPE = DX.KEY_INPUT_ESCAPE,
        KEY_1 = DX.KEY_INPUT_1,
        KEY_2 = DX.KEY_INPUT_2,
        KEY_3 = DX.KEY_INPUT_3,
        KEY_4 = DX.KEY_INPUT_4,
        KEY_5 = DX.KEY_INPUT_5,
        KEY_6 = DX.KEY_INPUT_6,
        KEY_7 = DX.KEY_INPUT_7,
        KEY_8 = DX.KEY_INPUT_8,
        KEY_9 = DX.KEY_INPUT_9,
        KEY_0 = DX.KEY_INPUT_0,
        KEY_MINUS = DX.KEY_INPUT_MINUS,
        KEY_BACK = DX.KEY_INPUT_BACK,
        KEY_TAB = DX.KEY_INPUT_TAB,
        KEY_Q = DX.KEY_INPUT_Q,
        KEY_W = DX.KEY_INPUT_W,
        KEY_E = DX.KEY_INPUT_E,
        KEY_R = DX.KEY_INPUT_R,
        KEY_T = DX.KEY_INPUT_T,
        KEY_Y = DX.KEY_INPUT_Y,
        KEY_U = DX.KEY_INPUT_U,
        KEY_I = DX.KEY_INPUT_I,
        KEY_O = DX.KEY_INPUT_O,
        KEY_P = DX.KEY_INPUT_P,
        KEY_LBRACKET = DX.KEY_INPUT_LBRACKET,
        KEY_RBRACKET = DX.KEY_INPUT_RBRACKET,
        KEY_RETURN = DX.KEY_INPUT_RETURN,
        KEY_LCONTROL = DX.KEY_INPUT_LCONTROL,
        KEY_A = DX.KEY_INPUT_A,
        KEY_S = DX.KEY_INPUT_S,
        KEY_D = DX.KEY_INPUT_D,
        KEY_F = DX.KEY_INPUT_F,
        KEY_G = DX.KEY_INPUT_G,
        KEY_H = DX.KEY_INPUT_H,
        KEY_J = DX.KEY_INPUT_J,
        KEY_K = DX.KEY_INPUT_K,
        KEY_L = DX.KEY_INPUT_L,
        KEY_SEMICOLON = DX.KEY_INPUT_SEMICOLON,
        KEY_LSHIFT = DX.KEY_INPUT_LSHIFT,
        KEY_BACKSLASH = DX.KEY_INPUT_BACKSLASH,
        KEY_Z = DX.KEY_INPUT_Z,
        KEY_X = DX.KEY_INPUT_X,
        KEY_C = DX.KEY_INPUT_C,
        KEY_V = DX.KEY_INPUT_V,
        KEY_B = DX.KEY_INPUT_B,
        KEY_N = DX.KEY_INPUT_N,
        KEY_M = DX.KEY_INPUT_M,
        KEY_COMMA = DX.KEY_INPUT_COMMA,
        KEY_PERIOD = DX.KEY_INPUT_PERIOD,
        KEY_SLASH = DX.KEY_INPUT_SLASH,
        KEY_RSHIFT = DX.KEY_INPUT_RSHIFT,
        KEY_MULTIPLY = DX.KEY_INPUT_MULTIPLY,
        KEY_LALT = DX.KEY_INPUT_LALT,
        KEY_SPACE = DX.KEY_INPUT_SPACE,
        KEY_CAPSLOCK = DX.KEY_INPUT_CAPSLOCK,
        KEY_F1 = DX.KEY_INPUT_F1,
        KEY_F2 = DX.KEY_INPUT_F2,
        KEY_F3 = DX.KEY_INPUT_F3,
        KEY_F4 = DX.KEY_INPUT_F4,
        KEY_F5 = DX.KEY_INPUT_F5,
        KEY_F6 = DX.KEY_INPUT_F6,
        KEY_F7 = DX.KEY_INPUT_F7,
        KEY_F8 = DX.KEY_INPUT_F8,
        KEY_F9 = DX.KEY_INPUT_F9,
        KEY_F10 = DX.KEY_INPUT_F10,
        KEY_NUMLOCK = DX.KEY_INPUT_NUMLOCK,
        KEY_SCROLL = DX.KEY_INPUT_SCROLL,
        KEY_NUMPAD7 = DX.KEY_INPUT_NUMPAD7,
        KEY_NUMPAD8 = DX.KEY_INPUT_NUMPAD8,
        KEY_NUMPAD9 = DX.KEY_INPUT_NUMPAD9,
        KEY_SUBTRACT = DX.KEY_INPUT_SUBTRACT,
        KEY_NUMPAD6 = DX.KEY_INPUT_NUMPAD6,
        KEY_NUMPAD4 = DX.KEY_INPUT_NUMPAD4,
        KEY_NUMPAD5 = DX.KEY_INPUT_NUMPAD5,
        KEY_ADD = DX.KEY_INPUT_ADD,
        KEY_NUMPAD1 = DX.KEY_INPUT_NUMPAD1,
        KEY_NUMPAD2 = DX.KEY_INPUT_NUMPAD2,
        KEY_NUMPAD3 = DX.KEY_INPUT_NUMPAD3,
        KEY_NUMPAD0 = DX.KEY_INPUT_NUMPAD0,
        KEY_DECIMAL = DX.KEY_INPUT_DECIMAL,
        KEY_F11 = DX.KEY_INPUT_F11,
        KEY_F12 = DX.KEY_INPUT_F12,
        KEY_KANA = DX.KEY_INPUT_KANA,
        KEY_CONVERT = DX.KEY_INPUT_CONVERT,
        KEY_NOCONVERT = DX.KEY_INPUT_NOCONVERT,
        KEY_YEN = DX.KEY_INPUT_YEN,
        KEY_PREVTRACK = DX.KEY_INPUT_PREVTRACK,
        KEY_AT = DX.KEY_INPUT_AT,
        KEY_COLON = DX.KEY_INPUT_COLON,
        KEY_NUMPADENTER = DX.KEY_INPUT_NUMPADENTER,
        KEY_RCONTROL = DX.KEY_INPUT_RCONTROL,
        KEY_KANJI = DX.KEY_INPUT_KANJI,
        KEY_DIVIDE = DX.KEY_INPUT_DIVIDE,
        KEY_SYSRQ = DX.KEY_INPUT_SYSRQ,
        KEY_RALT = DX.KEY_INPUT_RALT,
        KEY_PAUSE = DX.KEY_INPUT_PAUSE,
        KEY_HOME = DX.KEY_INPUT_HOME,
        KEY_UP = DX.KEY_INPUT_UP,
        KEY_PGUP = DX.KEY_INPUT_PGUP,
        KEY_LEFT = DX.KEY_INPUT_LEFT,
        KEY_RIGHT = DX.KEY_INPUT_RIGHT,
        KEY_END = DX.KEY_INPUT_END,
        KEY_DOWN = DX.KEY_INPUT_DOWN,
        KEY_PGDN = DX.KEY_INPUT_PGDN,
        KEY_INSERT = DX.KEY_INPUT_INSERT,
        KEY_DELETE = DX.KEY_INPUT_DELETE,
        KEY_LWIN = DX.KEY_INPUT_LWIN,
        KEY_RWIN = DX.KEY_INPUT_RWIN,
        KEY_APPS = DX.KEY_INPUT_APPS
    }
    #endregion

    #region - BrendMode ; ブレンドモード
    /// <summary>
    /// ブレンドモード
    /// </summary>
    public enum BrendMode
    {
        NoBrend = DX.DX_BLENDMODE_NOBLEND,      // ノーブレンド（デフォルト）
        Alpha = DX.DX_BLENDMODE_ALPHA,          // αブレンド
        Add = DX.DX_BLENDMODE_ADD,              // 加算ブレンド
        Subtract = DX.DX_BLENDMODE_SUB,         // 減算ブレンド
        Multiple = DX.DX_BLENDMODE_MULA,        // 乗算ブレンド
        Invert = DX.DX_BLENDMODE_INVSRC,        // 反転
        PMAAlpha = DX.DX_BLENDMODE_PMA_ALPHA,   // 乗算済みα用のαブレンド
        PMAAdd = DX.DX_BLENDMODE_PMA_ADD,       // 乗算済みα用の加算ブレンド
        PMASubtract = DX.DX_BLENDMODE_PMA_SUB,  // 乗算済みα用の減算ブレンド
        PMAInvert = DX.DX_BLENDMODE_PMA_INVSRC  // 乗算済みα用の反転ブレンド
    }
    #endregion

    #region - HAlignment : 水平アライメント
    /// <summary>
    /// 水平アライメント
    /// </summary>
    public enum HAlignment
    {
        Left,       // 左詰め
        Center,     // センタリング
        Right       // 右詰め
    }
    #endregion

    #region - VAlignment : 垂直アライメント
    /// <summary>
    /// 垂直アライメント
    /// </summary>
    public enum VAlignment
    {
        Top,        // 上端
        Middle,     // 中央
        Bottom      // 下端
    }
    #endregion

    #region - TransitionOrientation : トランジション方向
    /// <summary>
    /// トランジション方向
    /// </summary>
    public enum TransitionOrientation
    {
        Up,     // 下から上
        Down,   // 上から下
        Left,   // 右から左
        Right   // 左から右
    }
    #endregion

    #region - RectangleOrigin : 矩形基準点
    /// <summary>
    /// 矩形基準点
    /// </summary>
    public enum RectangleOrigin
    {
        Center,         // 中央
        LeftTop,        // 左上
        RightTop,       // 右上
        LeftBottom,     // 左下
        RightBottom     // 右下
    }
    #endregion

    #region - Direction : 方向
    /// <summary>
    /// 方向
    /// </summary>
    public enum Direction
    {
        Up = 0,     // 上
        Down = 1,   // 下
        Left = 2,   // 左
        Right = 3   // 右
    }
    #endregion

    #region - VerticalDirection : 垂直方向
    /// <summary>
    /// 垂直方向
    /// </summary>
    public enum VerticalDirection
    {
        Up = 0,     // 上
        Down = 1    // 下
    }
    #endregion

    #region - HorizontalDirection : 水平方向
    /// <summary>
    /// 水平方向
    /// </summary>
    public enum HorizontalDirection
    {
        Left = 2,   // 左
        Right = 3   // 右
    }
    #endregion

    #endregion
}
