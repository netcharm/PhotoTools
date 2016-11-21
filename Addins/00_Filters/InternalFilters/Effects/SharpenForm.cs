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
    public partial class SharpenForm : Form
    {
        internal AddinHost host;
        private IAddin addin;
        private Image thumb = null;
        private Image thumbBackup = null;

        private SharpenMode sharpenMode = SharpenMode.Normal;
        internal ParamItem ParamMode
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "SharpenMode";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = sharpenMode.GetType();
                pi.Value = sharpenMode;
                return ( pi );
            }
            set { sharpenMode = (SharpenMode) value.Value; }
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
        private float gdiAmount = 50f;
        internal ParamItem ParamGdiAmount
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "GdiAmount";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = gdiAmount.GetType();
                pi.Value = gdiAmount;
                return ( pi );
            }
            set { gdiAmount = (float) value.Value; }
        }

        public SharpenForm()
        {
            InitializeComponent();
        }

        public SharpenForm( IAddin filter )
        {
            this.addin = filter;
            InitializeComponent();

            toolTip.ToolTipTitle = addin.DisplayName;
            AddinUtils.Translate( addin, this, toolTip );
        }

        private void SharpenForm_Load( object sender, EventArgs e )
        {
            thumb = AddinUtils.CreateThumb( addin.ImageData, imgPreview.Size );
            imgPreview.Image = thumb;

            if ( sharpenMode == SharpenMode.Normal )
            {
                btnModeNormal.PerformClick();
            }
            else if ( sharpenMode == SharpenMode.Gaussian )
            {
                btnModeGaussian.PerformClick();
            }
            else if ( sharpenMode == SharpenMode.GDI )
            {
                btnModeGdi.PerformClick();
            }

            edGaussianSigma.Value = Convert.ToDecimal( gaussianSigma );
            edGaussianSize.Value = Convert.ToDecimal( gaussianSize );
            edGaussianThreshold.Value = Convert.ToDecimal( gaussianThreshold );

            edGdiRatio.Value = Convert.ToDecimal( gdiRatio );
            edGdiAmount.Value = Convert.ToDecimal( gdiAmount );
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

        private void btnMode_Click( object sender, EventArgs e )
        {
            if ( sender == btnModeNormal )
            {
                sharpenMode = SharpenMode.Normal;
                grpGaussianParams.Enabled = false;
                grpGdiParams.Enabled = false;

                grpGaussianParams.Visible = false;
                grpGdiParams.Visible = false;
            }
            else if ( sender == btnModeGaussian )
            {
                sharpenMode = SharpenMode.Gaussian;
                grpGaussianParams.Enabled = true;
                grpGdiParams.Enabled = false;

                grpGaussianParams.Visible = true;
                grpGdiParams.Visible = false;
            }
            else if ( sender == btnModeGdi )
            {
                sharpenMode = SharpenMode.GDI;
                grpGaussianParams.Enabled = false;
                grpGdiParams.Enabled = true;

                grpGaussianParams.Visible = false;
                grpGdiParams.Visible = true;
            }
            imgPreview.Image = addin.Apply( thumb );
        }

        private void edMode_ValueChanged( object sender, EventArgs e )
        {
            gaussianSigma = Convert.ToDouble(edGaussianSigma.Value);
            gaussianSize = Convert.ToInt32( edGaussianSize.Value );
            gaussianThreshold = Convert.ToInt32( edGaussianThreshold.Value );
            gdiRatio = (float)Convert.ToDouble( edGdiRatio.Value );
            gdiAmount = (float) Convert.ToDouble( edGdiAmount.Value );

            imgPreview.Image = addin.Apply( thumb );
        }

    }
}
