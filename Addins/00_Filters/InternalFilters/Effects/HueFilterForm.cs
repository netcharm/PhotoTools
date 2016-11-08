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
    public partial class HueFilterForm : Form
    {
        internal AddinHost host;
        private IAddin addin;
        private Image thumb = null;
        private Image thumbBackup = null;

        private HueFilterMode mode = HueFilterMode.Normal;
        internal ParamItem ParamMode
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "HueFilterMode";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = mode.GetType();
                pi.Value = mode;
                return ( pi );
            }
            set { mode = (HueFilterMode) value.Value; }
        }
        private int hueValue = 180;
        internal ParamItem ParamValue
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "HueFilterValue";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = hueValue.GetType();
                pi.Value = hueValue;
                return ( pi );
            }
            set { hueValue = (int) value.Value; }
        }

        public HueFilterForm()
        {
            InitializeComponent();
        }

        public HueFilterForm( IAddin filter )
        {
            InitializeComponent();

            this.addin = filter;
            this.Text = addin.DisplayName;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            toolTip.ToolTipTitle = addin.DisplayName;
            AddinUtils.Translate( addin, this, toolTip );
        }

        private void HueFilterForm_Load( object sender, EventArgs e )
        {
            thumb = AddinUtils.CreateThumb( addin.ImageData, imgPreview.Size );

            edHue.Step = 1;
            edHue.Value = 180;
            imgPreview.Image = addin.Apply( thumb );
        }

        private void btnOriginal_Click( object sender, EventArgs e )
        {
            if ( btnOriginal.Checked )
            {
                thumbBackup = imgPreview.Image;
                imgPreview.Image = thumb;
            }
            else
            {
                if ( thumbBackup is Image )
                    imgPreview.Image = thumbBackup;
            }
        }

        private void edHue_ValueChanged( object sender, EventArgs e )
        {
            hueValue = Convert.ToInt32( edHue.Value );
            imgPreview.Image = addin.Apply( imgPreview.Image );
        }

    }
}
