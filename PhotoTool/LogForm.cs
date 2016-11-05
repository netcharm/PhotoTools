using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetCharm.Image.Addins;

namespace PhotoTool
{
    public partial class LogForm : Form
    {
        internal AddinHost Host;
        private StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 
        /// </summary>
        public LogForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogForm_Load( object sender, EventArgs e )
        {
            #region extracting icon from application to this form window
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        internal void Log(string text)
        {
            sb.AppendLine( text.Trim() );
            edLog.Text = sb.ToString();
        }

        private void btnClear_Click( object sender, EventArgs e )
        {
            sb.Clear();
            edLog.Clear();
        }

        private void btnAddinError_Click( object sender, EventArgs e )
        {
            foreach(var kv in Host.NotLoadedAddin)
            {
                sb.AppendLine( $"<Addin> {kv.Key} : {kv.Value}" );                
            }
            edLog.Text = sb.ToString();
        }

    }
}
