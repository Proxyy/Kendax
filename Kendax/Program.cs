using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Kendax
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(args));
        }
    }
}
