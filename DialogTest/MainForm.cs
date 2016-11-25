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

        private void btnColorDialogSystem_Click( object sender, EventArgs e )
        {
            ColorDialog dlgColor = new ColorDialog();
            if ( dlgColor.ShowDialog() == DialogResult.OK )
            {
                btnColorDialogSystem.BackColor = dlgColor.Color;
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

        private void btnColorDialog_Click( object sender, EventArgs e )
        {
            NetCharm.Common.ColorDialog dlgColor = new NetCharm.Common.ColorDialog();
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

        private void dlgColorEx_Apply( object sender, EventArgs e )
        {
            MessageBox.Show( "Color Apply" );
        }

        private void btnColorDilogEx_Click( object sender, EventArgs e )
        {
            //NetCharm.Common.Controls.ColorDialogEx dlgColorEx = new NetCharm.Common.Controls.ColorDialogEx();
            if ( dlgColorEx.ShowDialog( Color.Blue ) == DialogResult.OK )
            {
                var colors = dlgColorEx.CustomColors;
            }
        }

        private void btnFontDialogSystem_Click( object sender, EventArgs e )
        {
            FontDialog dlgFont = new FontDialog();
            dlgFont.ShowDialog();
        }

        private void dlgFont_Apply( object sender, EventArgs e )
        {
            fontApplyTest = true;

            //option.TextFont = dlgFont.Font.ToString();
            //option.TextFontStyle = dlgFont.Font.Style;
            MessageBox.Show( "Font Apply" );
            //Preview();
            fontApplyTest = false;
        }

        private void btnFontDialog_Click( object sender, EventArgs e )
        {
            NetCharm.Common.FontDialog dlgFont = new NetCharm.Common.FontDialog();
            dlgFont.Apply += new System.EventHandler( dlgFont_Apply );
            //dlgFont.Color = color;
            dlgFont.Font = SystemFonts.DefaultFont;
            dlgFont.FontSize = 12;
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

        private void btnFontDialogEx_Click( object sender, EventArgs e )
        {
            dlgFontEx.Font = SystemFonts.DefaultFont;
            dlgFontEx.Size = 12;
            if ( dlgFontEx.ShowDialog() == DialogResult.OK )
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

    }
}
