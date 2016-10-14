using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetCharm.Image.Addins;

namespace InternalFilters
{
    public partial class RotateForm : Form
    {
        private AddinHost host;
        private IAddin addin;

        public RotateForm()
        {
            InitializeComponent();
        }

        public RotateForm( AddinHost host )
        {
            this.host = host;
            InitializeComponent();
        }

        public RotateForm( IAddin filter )
        {
            this.addin = filter;
            InitializeComponent();
        }

        internal ParamItem GetMode()
        {
            throw new NotImplementedException();
        }

        internal ParamItem GetAngle()
        {
            throw new NotImplementedException();
        }
    }
}
