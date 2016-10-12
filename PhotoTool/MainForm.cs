using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetCharm.Image.Addins;

namespace PhotoTool
{
    public partial class MainForm : RibbonForm
    {
        private AddinHost addins = new AddinHost();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        private void ChangeTheme( RibbonTheme theme = RibbonTheme.Normal )
        {
            switch(theme)
            {
                case RibbonTheme.Black:
                    Theme.ColorTable = new RibbonProfesionalRendererColorTableBlack();
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
            status.BackColor = Theme.ColorTable.GetColor(RibbonColorPart.RibbonBackground);
            status.ForeColor = Theme.ColorTable.GetColor( RibbonColorPart.RibbonItemText_2013 );
            //ribbonMain.Refresh();
            //this.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="style"></param>
        private void ChangeStyle( RibbonOrbStyle style = RibbonOrbStyle.Office_2010 )
        {
            ribbonMain.OrbStyle = style;
            //ribbonMain.Refresh();
            //this.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apps"></param>
        private void AddAddinApp(List<IAddin> apps)
        {
            //ComponentResourceManager resources = new ComponentResourceManager( this.GetType());
            //ribbonMain.SuspendLayout();
            foreach ( IAddin addin in apps)
            {
                //RibbonButton btnAddin = new RibbonButton(Path.GetFileNameWithoutExtension(addin.Name));
                RibbonButton btnAddin = new RibbonButton();
                RibTabMainApp.Items.Add( btnAddin );

                if ( addin.LargeImage != null)
                    btnAddin.Image = addin.LargeImage;
                else
                    btnAddin.Image = addins.LargeImage;
                if ( addin.SmallImage != null )
                    btnAddin.SmallImage = addin.SmallImage;
                else
                    btnAddin.SmallImage = addins.SmallImage;

                btnAddin.Text = addin.DisplayName;
                btnAddin.ToolTip = addin.Description;
                btnAddin.ToolTipTitle = addin.Author;

                btnAddin.Value = addin.Name;
                btnAddin.Click += AddinClick;

                //resources.ApplyResources( btnAddin, addin.Name );
            }
            ribbonMain.ResumeLayout( true );
            ribbonMain.PerformLayout();
        }

        public void AddinClick(object sender, EventArgs e)
        {
            addins.Apps[(sender as RibbonButton).Value].Show(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load( object sender, EventArgs e )
        {
            //
            #region extracting icon from application to this form window
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
            #endregion

            ChangeStyle( RibbonOrbStyle.Office_2010 );
            ChangeTheme( RibbonTheme.Halloween );

            addins.Scan();
            AddAddinApp( addins.Apps.Select( kvp => kvp.Value ).ToList() );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemeBlack_Click( object sender, EventArgs e )
        {
            var obj = sender as RibbonButton;
            var value = Int32.Parse( obj.Value );
            ChangeTheme( (RibbonTheme) value );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdStyle2010_Click( object sender, EventArgs e )
        {
            var obj = sender as RibbonButton;
            var value = Int32.Parse( obj.Value );
            ChangeStyle( (RibbonOrbStyle) value );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribOptBtnExit_Click( object sender, EventArgs e )
        {
            Close();
        }

        private void cmdActionReScan_Click( object sender, EventArgs e )
        {
            addins.Scan();
            foreach( KeyValuePair<string, IAddin> kv in addins.Actions)
            {
                //kv.Value.LargeImage
            }
        }
    }
}
