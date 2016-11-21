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
    public partial class BlurForm : Form
    {
        internal AddinHost host;
        private IAddin addin;
        private Image thumb = null;
        private Image thumbBackup = null;

        private BlurMode blurMode = BlurMode.Normal;
        internal ParamItem ParamMode
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "BlurMode";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = blurMode.GetType();
                pi.Value = blurMode;
                return ( pi );
            }
            set { blurMode = (BlurMode) value.Value; }
        }
        private double gaussianSigma = 1.4;
        internal ParamItem ParamGaussianSigma
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "GaussianSigma";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = gaussianSigma.GetType();
                pi.Value = gaussianSigma;
                return ( pi );
            }
            set { gaussianSigma = (double) value.Value; }
        }
        private int gaussianSize = 5;
        internal ParamItem ParamGaussianSize
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "GaussianSize";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = gaussianSize.GetType();
                pi.Value = gaussianSize;
                return ( pi );
            }
            set { gaussianSize = (int) value.Value; }
        }
        private int gaussianThreshold = 0;
        internal ParamItem ParamGaussianThreshold
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "GaussianThreshold";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = gaussianThreshold.GetType();
                pi.Value = gaussianThreshold;
                return ( pi );
            }
            set { gaussianThreshold = (int) value.Value; }
        }
        private int boxSize = 3;
        internal ParamItem ParamBoxSize
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "BoxSizeH";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = boxSize.GetType();
                pi.Value = boxSize;
                return ( pi );
            }
            set { boxSize = (int) value.Value; }
        }
        private float gdiRatio = 1.5f;
        internal ParamItem ParamGdiRatio
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "GdiRatio";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = gdiRatio.GetType();
                pi.Value = gdiRatio;
                return ( pi );
            }
            set { gdiRatio = (float) value.Value; }
        }

        public BlurForm()
        {
            InitializeComponent();
        }

        public BlurForm( IAddin filter )
        {
            this.addin = filter;
            InitializeComponent();

            toolTip.ToolTipTitle = addin.DisplayName;
            AddinUtils.Translate( addin, this, toolTip );

            thumb = AddinUtils.CreateThumb( addin.ImageData, imgPreview.Size );
            imgPreview.Image = thumb;
        }

        private void BlurForm_Load( object sender, EventArgs e )
        {
            if ( blurMode == BlurMode.Normal )
            {
                btnModeNormal.PerformClick();
            }
            else if (blurMode == BlurMode.Gaussian)
            {
                btnModeGaussian.PerformClick();
            }
            else if ( blurMode == BlurMode.Box )
            {
                btnModeBox.PerformClick();
            }
            else if ( blurMode == BlurMode.GDI )
            {
                btnModeGdi.PerformClick();
            }

            edGaussianSigma.Value = Convert.ToDecimal( gaussianSigma );
            edGaussianSize.Value = Convert.ToDecimal( gaussianSize );
            edGaussianThreshold.Value = Convert.ToDecimal( gaussianThreshold );
            edBoxSize.Value = Convert.ToDecimal( boxSize );
            edGdiRatio.Value = Convert.ToDecimal( gdiRatio );
        }

        private void btnOriginal_Click( object sender, EventArgs e )
        {
            if(btnOriginal.Checked)
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

        private void btnMode_Click( object sender, EventArgs e )
        {
            layoutParams.SuspendLayout();
            if ( sender == btnModeNormal )
            {
                blurMode = BlurMode.Normal;

                grpGaussianParams.Enabled = false;
                grpBoxParams.Enabled = false;
                grpGdiParams.Enabled = false;

                grpGaussianParams.Visible = false;
                grpBoxParams.Visible = false;
                grpGdiParams.Visible = false;
            }
            else if ( sender == btnModeGaussian )
            {
                blurMode = BlurMode.Gaussian;

                grpGaussianParams.Enabled = true;
                grpBoxParams.Enabled = false;
                grpGdiParams.Enabled = false;

                grpGaussianParams.Visible = true;
                grpBoxParams.Visible = false;
                grpGdiParams.Visible = false;
            }
            else if ( sender == btnModeBox )
            {
                blurMode = BlurMode.Box;

                grpGaussianParams.Enabled = false;
                grpBoxParams.Enabled = true;
                grpGdiParams.Enabled = false;

                grpGaussianParams.Visible = false;
                grpBoxParams.Visible = true;
                grpGdiParams.Visible = false;
            }
            else if ( sender == btnModeGdi )
            {
                blurMode = BlurMode.GDI;
                grpGaussianParams.Enabled = false;
                grpBoxParams.Enabled = false;
                grpGdiParams.Enabled = true;

                grpGaussianParams.Visible = false;
                grpBoxParams.Visible = false;
                grpGdiParams.Visible = true;
            }
            layoutParams.ResumeLayout();
            imgPreview.Image = addin.Apply( thumb );
        }

        private void edMode_ValueChanged( object sender, EventArgs e )
        {
            gaussianSigma = Convert.ToDouble( edGaussianSigma.Value );
            gaussianSize = Convert.ToInt32( edGaussianSize.Value );
            gaussianThreshold = Convert.ToInt32( edGaussianThreshold.Value );
            boxSize = Convert.ToInt32( edBoxSize.Value );
            gdiRatio = (float)Convert.ToDouble( edGdiRatio.Value );

            imgPreview.Image = addin.Apply( thumb );
        }

    }
}
