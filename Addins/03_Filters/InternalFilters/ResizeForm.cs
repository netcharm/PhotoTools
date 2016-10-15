using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetCharm.Image.Addins;

namespace InternalFilters
{
    public partial class ResizeForm : Form
    {
        private AddinHost host;
        private IAddin addin;

        public ResizeForm()
        {
            InitializeComponent();
        }

        public ResizeForm( AddinHost host )
        {
            this.host = host;
            InitializeComponent();
        }

        public ResizeForm( IAddin filter )
        {
            this.addin = filter;
            InitializeComponent();
        }

        private void InternalFilterResizeForm_Load( object sender, EventArgs e )
        {            
            //
        }

        internal ParamItem GetWidth(string name)
        {
            ParamItem pi = new ParamItem();
            pi.Name = name;
            pi.DisplayName = AddinUtils._( addin, name );
            pi.Type =  Convert.ToInt32( edWidth.Value ).GetType();
            pi.Value = Convert.ToInt32( edWidth.Value );
            return ( pi );
        }

        internal ParamItem GetHeight(string name)
        {
            ParamItem pi = new ParamItem();
            pi.Name = name;
            pi.DisplayName = AddinUtils._( addin, name );
            pi.Type = Convert.ToInt32( edHeight.Value ).GetType();
            pi.Value = Convert.ToInt32( edHeight.Value );
            return ( pi );
        }

        internal void SetWidth( ParamItem paramItem )
        {
            edWidth.Value = Convert.ToDecimal(paramItem.Value);
        }

        internal void SetHeight( ParamItem paramItem )
        {
            edHeight.Value = Convert.ToDecimal( paramItem.Type );
        }

        internal void SetWidth( int width )
        {
            edWidth.Value = width;
        }

        internal void SetHeight( int height )
        {
            edHeight.Value = height;
        }
    }
}
