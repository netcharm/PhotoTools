using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetCharm.Image.Addins;

namespace InternalFilters.Effects
{
    public partial class InvertForm : Form
    {
        internal AddinHost host;
        private IAddin addin;
        private Image thumb = null;
        private Image thumbBackup = null;

        private InvertMode mode = InvertMode.Normal;
        internal ParamItem ParamMode
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "InvertMode";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = mode.GetType();
                pi.Value = mode;
                return ( pi );
            }
            set { mode = (InvertMode) Convert.ToInt32( value.Value ); }
        }

        public InvertForm()
        {
            InitializeComponent();
        }

        public InvertForm( IAddin filter )
        {
            this.addin = filter;
            InitializeComponent();

            toolTip.ToolTipTitle = addin.DisplayName;
            AddinUtils.Translate( addin, this, toolTip );
        }

        private void InvertForm_Load( object sender, EventArgs e )
        {
            thumb = AddinUtils.CreateThumb( addin.ImageData, imgPreview.Size );
            imgPreview.Image = addin.Apply( thumb );
        }

        private void btnOriginal_MouseDown( object sender, MouseEventArgs e )
        {
            thumbBackup = imgPreview.Image;
            imgPreview.Image = thumb;
        }

        private void btnOriginal_MouseUp( object sender, MouseEventArgs e )
        {
            if ( thumbBackup is Image )
                imgPreview.Image = thumbBackup;
        }


    }
}
