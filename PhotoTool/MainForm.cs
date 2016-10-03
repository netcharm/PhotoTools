using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoTool
{
    public partial class MainForm : RibbonForm
    {
        private void ChangeTheme( RibbonTheme theme = RibbonTheme.Normal )
        {
            switch(theme)
            {
                case RibbonTheme.Black:
                    Theme.ColorTable = (RibbonProfesionalRendererColorTable) new RibbonProfesionalRendererColorTableBlack();
                    break;
                case RibbonTheme.Green:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTableGreen();
                    break;
                case RibbonTheme.Halloween:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTableHalloween();
                    break;
                case RibbonTheme.JellyBelly:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTableJellyBelly();
                    break;
                case RibbonTheme.Purple:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTablePurple();
                    break;
                case RibbonTheme.Blue:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTableBlue();
                    break;
                case RibbonTheme.Normal:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTable();
                    break;
                default:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTable();
                    break;
            }
            ribbonMain.Refresh();
            this.Refresh();
        }

        private void ChangeStyle( RibbonOrbStyle style = RibbonOrbStyle.Office_2010 )
        {
            ribbonMain.OrbStyle = style;
            ribbonMain.Refresh();
            this.Refresh();
        }

        public MainForm()
        {
            InitializeComponent();
            ChangeStyle( RibbonOrbStyle.Office_2010 );
            ChangeTheme( RibbonTheme.Halloween );
        }

        private void MainForm_Load( object sender, EventArgs e )
        {
            //
        }

        private void cmdThemeBlack_Click( object sender, EventArgs e )
        {
            var obj = sender as RibbonButton;
            var value = Int32.Parse( obj.Value );
            ChangeTheme( (RibbonTheme) value );
        }

        private void cmdStyle2010_Click( object sender, EventArgs e )
        {
            var obj = sender as RibbonButton;
            var value = Int32.Parse( obj.Value );
            ChangeStyle( (RibbonOrbStyle) value );
        }
    }
}
