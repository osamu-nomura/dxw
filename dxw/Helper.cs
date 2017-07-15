using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using DxLibDLL;

namespace dxw
{
    #region 【Static Class : Helper】
    /// <summary>
    /// ヘルパーメソッド
    /// </summary>
    public static class Helper
    {
        #region ■ DLL Import
        /// <summary>
        /// SetActiveStateChangeCallBackFunction
        /// DXライブラリのC#用DLLの定義では利用しづらいので直接DxLib.dllからインポートする
        /// </summary>
        /// <param name="Callback">コールバック関数のポインタ</param>
        /// <param name="UserData">コールバック関数に渡すユーザーデータ</param>
        /// <returns></returns>
        [DllImport("DxLib.dll", EntryPoint = "dx_SetActiveStateChangeCallBackFunction")]
        extern unsafe static int SetActiveStateChangeCallBackFunction(DX.SetActiveStateChangeCallBackFunctionCallback Callback, IntPtr UserData);
        #endregion

        #region ■ Members
        /// <summary>
        /// SetActiveStateChangeCallBackFunctionのコールバック関数
        /// </summary>
        private static unsafe DX.SetActiveStateChangeCallBackFunctionCallback _callback = null;
        /// <summary>
        /// WindowのActiveStatusが変更されたときに呼ばれるコールバック
        /// </summary>
        private static Action<int> _activeStateChanged = null;
        #endregion

        #region ■ Private Methods

        #region - SetActiveStateChangeCallBack : SetActiveStateChangeCallBackFunctionに渡すコールバック
        /// <summary>
        /// SetActiveStateChangeCallBackFunctionに渡すコールバック
        /// </summary>
        /// <param name="activeState">アクティブステート</param>
        /// <param name="userData">ユーザデータ</param>
        /// <returns>int</returns>
        private static unsafe int SetActiveStateChangeCallBack(int activeState, void* userData)
        {
            _activeStateChanged?.Invoke(activeState);
            return 0;
        }
        #endregion

        #endregion

        #region ■ Public Methods

        #region - PlaySound : 音声を再生する
        /// <summary>
        /// 音声を再生する
        /// </summary>
        /// <param name="handle">リソースハンドル</param>
        /// <param name="playType">再生形式</param>
        /// <param name="volume">音量</param>
        public static void PlaySound(int handle, int playType, int volume)
        {
            DX.ChangeNextPlayVolumeSoundMem(volume, handle);
            var ret = DX.PlaySoundMem(handle, playType, DX.TRUE);
        }
        #endregion

        #region - ChangeSoundVolume : 音量を変更する
        /// <summary>
        /// 音量を変更する
        /// </summary>
        /// <param name="handle">リソースハンドル</param>
        /// <param name="newVolume">音量</param>
        public static void ChangeSoundVolume(int handle, int newVolume)
        {
            if (DX.CheckSoundMem(handle) == DX.TRUE)
                DX.ChangeVolumeSoundMem(newVolume, handle);
        }
        #endregion

        #region - StopSound : 音声を停止する
        /// <summary>
        /// 音声を停止する
        /// </summary>
        /// <param name="handles">リソースハンドル</param>
        public static void StopSound(params int[] handles)
        {
            foreach (var h in handles)
            {
                if (DX.CheckSoundMem(h) == DX.TRUE)
                    DX.StopSoundMem(h);
            }
        }
        #endregion

        #region - IsPlayingSound : 音声が再生中？
        /// <summary>
        /// 音声が再生中？
        /// </summary>
        /// <param name="handle">リソースハンドル</param>
        /// <returns>Trye : 再生中 / False : 再生中でない</returns>
        public static bool IsPlayingSound(int handle)
        {
            return (DX.CheckSoundMem(handle) == DX.TRUE);
        }
        #endregion

        #region - LoadFile : ファイルをバイト配列に読み込む
        /// <summary>
        /// ファイルをバイト配列に読み込む
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <returns>バイト配列</returns>
        public static unsafe byte[] LoadFile(string path)
        {
            // ファイルサイズを取得
            var fileSize = DX.FileRead_size(path);
            if (fileSize > 0)
            {
                // 必要な領域を確保
                var buff = new byte[fileSize];

                // ファイルのOPEN
                var fileHandle = DX.FileRead_open(path);
                try
                {
                    fixed (byte *p = buff)
                    {
                        DX.FileRead_read(new IntPtr(p), (int)fileSize, fileHandle);
                    }
                }
                finally
                {
                    DX.FileRead_close(fileHandle);
                }
                return buff;
            }
            return null;
        }
        #endregion

        #region - SetActiveStateChangedCallback : アクティブステートが変更された場合に呼び出されるコールバックを登録する
        /// <summary>
        /// アクティブステートが変更された場合に呼び出されるコールバックを登録する
        /// </summary>
        /// <param name="callback">コールバック</param>
        public static unsafe void SetActiveStateChangedCallback(Action<int> callback)
        {
            _activeStateChanged = callback;
            _callback = SetActiveStateChangeCallBack;
            SetActiveStateChangeCallBackFunction(_callback, IntPtr.Zero);
        }
        #endregion

        #endregion
    }
    #endregion
}
