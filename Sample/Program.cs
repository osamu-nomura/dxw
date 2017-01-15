﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using dxw;

using static Sample.Const;

namespace Sample
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // アプリケーションオブジェクトの生成
            var app = new SampleApp(SCREEN_WIDTH, SCREEN_HEIGHT, ColorBitDepth.BitDepth32);
            // アプリケーションの実行
            app.Run(WindowMode.Window);
        }
    }
}
