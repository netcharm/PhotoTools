using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhotoTool
{
    public partial class LogForm : Form
    {
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
            StringBuilder sb = new StringBuilder();
            sb.AppendLine( edLog.Text.Trim() );
            sb.AppendLine( text.Trim() );
            edLog.Text = sb.ToString();
        }
    }
}
