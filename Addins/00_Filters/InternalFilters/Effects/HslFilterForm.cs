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
    public partial class HslFilterForm : Form
    {
        internal AddinHost host;
        private IAddin addin;
        private Image thumb = null;
        private Image thumbBackup = null;

        private HslFilterMode mode = HslFilterMode.Normal;
        private int hslHue = 180;
        private float hslSat = 1.0f;
        private float hslLum = 0.5f;
        private float hslTol = 5.0f;
        private bool Procesing = false;
        private bool ColorPicking = false;
        private bool ColorChanging = false;
        private bool PointInImage = false;

        internal ParamItem ParamMode
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "HslFilterMode";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = mode.GetType();
                pi.Value = mode;
                return ( pi );
            }
            set { mode = (HslFilterMode) value.Value; }
        }
        internal ParamItem ParamHue
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "HslHue";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = hslHue.GetType();
                pi.Value = hslHue;
                return ( pi );
            }
            set { hslHue = (int) value.Value; }
        }
        internal ParamItem ParamSaturation
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "HslSaturation";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = hslSat.GetType();
                pi.Value = hslSat;
                return ( pi );
            }
            set { hslSat = (float) Convert.ToDouble( value.Value ); }
        }
        internal ParamItem ParamLuminance
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "HslLuminance";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = hslLum.GetType();
                pi.Value = hslLum;
                return ( pi );
            }
            set { hslLum = (float) Convert.ToDouble( value.Value ); }
        }
        internal ParamItem ParamTolerance
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "HslTolerance";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = hslTol.GetType();
                pi.Value = hslTol;
                return ( pi );
            }
            set { hslTol = (float) Convert.ToDouble( value.Value ); }
        }
        private GrayscaleMode grayscaleMode;
        public ParamItem ParamGrayscaleMode
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "GrayscaleMode";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = grayscaleMode.GetType();
                pi.Value = grayscaleMode;
                return ( pi );
            }
            internal set { grayscaleMode = (GrayscaleMode) value.Value; }
        }

        public HslFilterForm()
        {
            InitializeComponent();
        }

        public HslFilterForm( IAddin filter )
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

        private void HslFilterForm_Load( object sender, EventArgs e )
        {
            thumb = AddinUtils.CreateThumb( addin.ImageData, imgPreview.Size );
            imgPreview.Image = thumb;

            cbGrayMode.DataSource = Enum.GetValues( typeof( GrayscaleMode ) );
            edSat.Color = edHue.ToRgb( hslSat, hslLum );
            //edLum.Color = edHue.ToRgb( hslSat, hslLum );
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

        private void cbGrayMode_SelectedIndexChanged( object sender, EventArgs e )
        {
            Enum.TryParse( cbGrayMode.SelectedValue.ToString(), out grayscaleMode );
            thumbBackup = addin.Apply( thumb );
            imgPreview.Image = thumbBackup is Image ? thumbBackup : thumb;
        }

        private void edHsl_ValueChanged( object sender, EventArgs e )
        {
            if(!ColorChanging)
            {
                ColorChanging = true;
                hslHue = Convert.ToInt32( edHue.Value );
                hslSat = (float) Convert.ToDouble( edSat.Value );
                hslLum = (float) Convert.ToDouble( edLum.Value );
                hslTol = (float) Convert.ToDouble( edTol.Value );

                Color c = edHue.ToRgb( hslSat, hslLum );
                edSat.Color = c;
                ColorChanging = false;
                //edLum.Color = c;

                //edHue.NubColor = c;
                //edSat.NubColor = c;
                //edLum.NubColor = c;
            }

            //if ( ColorPicking )
            //    thumbBackup = addin.Apply( thumb );
            //else
            //    imgPreview.Image = addin.Apply( thumb );
            if (!ColorPicking && !ColorChanging)
                imgPreview.Image = addin.Apply( thumb );
        }

        private void scpPicker_ColorChanged( object sender, EventArgs e )
        {
            if ( PointInImage )
            {
                hslHue = (int) Math.Round( scpPicker.Color.GetHue() );
                hslSat = scpPicker.Color.GetSaturation();
                hslLum = scpPicker.Color.GetBrightness();

                edHue.Value = Convert.ToDecimal( scpPicker.Color.GetHue() );
                edSat.Value = Convert.ToDecimal( scpPicker.Color.GetSaturation() );
                edLum.Value = Convert.ToDecimal( scpPicker.Color.GetBrightness() );
            }
        }

        private void scpPicker_MouseDown( object sender, MouseEventArgs e )
        {
            ColorPicking = true;
            imgPreview.Image = thumb;
            PointInImage = false;
        }

        private void scpPicker_MouseUp( object sender, MouseEventArgs e )
        {
            //var sp = scpPicker.PointToScreen(e.Location);
            //var mp = imgPreview.PointToClient( sp );
            //var ip = imgPreview.PointToImage( mp );
            if(PointInImage)
            {
                ColorPicking = false;
                thumbBackup = addin.Apply( thumb );
                imgPreview.Image = thumbBackup is Image ? thumbBackup : thumb;
            }
        }

        private void scpPicker_MouseMove( object sender, MouseEventArgs e )
        {
            var sp = scpPicker.PointToScreen(e.Location);
            var mp = imgPreview.PointToClient( sp );
            var ip = imgPreview.PointToImage( mp );
            if ( ip.X >= 0 && ip.X < thumb.Width && ip.Y >= 0 && ip.Y < thumb.Height )
                PointInImage = true;
            else
                PointInImage = false;
        }
    }
}
