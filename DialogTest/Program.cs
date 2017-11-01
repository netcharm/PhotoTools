using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DialogTest.Properties;

namespace DialogTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( true );

            //if(Environment.CommandLine)
            if ( args.Length>0)
            {
                if ( args[0].StartsWith( "Color", StringComparison.CurrentCultureIgnoreCase ) )
                //if (string.Equals(args[0], "Color", StringComparison.CurrentCultureIgnoreCase))
                {
                    NetCharm.Common.ColorDialog dlgColor = new NetCharm.Common.ColorDialog();
                    dlgColor.Icon = Resources.ColorDialog;
                    dlgColor.ShowInTaskbar = true;
                    dlgColor.ShowIcon = true;
                    dlgColor.ShowApply = false;
                    dlgColor.Color = Color.White;
                    if ( dlgColor.ShowDialog() == DialogResult.OK )
                    {
                    }
                }
                else if( args[0].StartsWith( "Font", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    NetCharm.Common.FontDialog dlgFont = new NetCharm.Common.FontDialog();
                    dlgFont.Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
                    dlgFont.ShowInTaskbar = true;
                    dlgFont.ShowIcon = true;
                    dlgFont.ShowApply = false;
                    dlgFont.SelectedFont = SystemFonts.DefaultFont;
                    dlgFont.FontSize = 14f;
                    if ( dlgFont.ShowDialog() == DialogResult.OK )
                    {
                    }
                }
                else
                {
                    Application.Run( new MainForm() );
                }
            }
            else
            {
                Application.Run( new MainForm() );
            }
        }
    }
}
