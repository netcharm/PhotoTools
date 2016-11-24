using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogTest
{
    public partial class MainForm : Form
    {
        private bool fontApplyTest;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnColorDialog_Click( object sender, EventArgs e )
        {
            NetCharm.Common.ColorDialogEx dlgColor = new NetCharm.Common.ColorDialogEx();
            dlgColor.Apply += new System.EventHandler( dlgColor_Apply );
            dlgColor.Color = Color.Red;
            if ( dlgColor.ShowDialog() == DialogResult.OK )
            {
                //option.TextColor = dlgColor.Color.ToHtml();
                //Preview();
            }
            else
            {
                //option.TextColor = c;
                //Preview();
            }
        }

        private void dlgColor_Apply( object sender, EventArgs e )
        {
            fontApplyTest = true;

            //string c = option.TextColor;

            //option.TextColor = dlgColor.Color.ToHtml();
            //Preview();
            //option.TextColor = c;

            fontApplyTest = false;
        }

        private void btnFontDialog_Click( object sender, EventArgs e )
        {
            NetCharm.Common.FontDialogEx dlgFont = new NetCharm.Common.FontDialogEx();
            dlgFont.Apply += new System.EventHandler( dlgFont_Apply );
            //dlgFont.Color = color;
            if ( dlgFont.ShowDialog() == DialogResult.OK )
            {
                //option.TextColor = dlgColor.Color.ToHtml();
                //Preview();
            }
            else
            {
                //option.TextColor = c;
                //Preview();
            }
            return;
        }

        private void dlgFont_Apply( object sender, EventArgs e )
        {
            fontApplyTest = true;

            //option.TextFont = dlgFont.Font.ToString();
            //option.TextFontStyle = dlgFont.Font.Style;

            //Preview();
            fontApplyTest = false;
        }

        private void btnFontDialogSystem_Click( object sender, EventArgs e )
        {
            FontDialog dlgFont = new FontDialog();
            dlgFont.ShowDialog();
        }
    }
}
