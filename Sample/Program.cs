using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using dxw;

using hsb.Extensions;
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
            var r = 135.0d.DegreeToRadian();
            var vec1 = new Vector(1, 1).ModMagnitude(1.0d);
            var vec2 = vec1.ModDirection(r);

            var w = 1.0d * Math.Cos(135.0d.DegreeToRadian());
            var h = 1.0d * Math.Sin(135.0d.DegreeToRadian());

            // アプリケーションオブジェクトの生成
            var app = new SampleApp(SCREEN_WIDTH, SCREEN_HEIGHT, ColorBitDepth.BitDepth32)
            {
                IsShowSystemInformation = false,
                IsShowInputStatus = false
            };

            // アプリケーションの実行
            app.Run(WindowMode.Window);
        }
    }
}
