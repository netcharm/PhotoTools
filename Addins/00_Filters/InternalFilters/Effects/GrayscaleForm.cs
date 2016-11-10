using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using NetCharm.Image.Addins;

namespace InternalFilters.Effects
{
    public partial class GrayscaleForm : Form
    {
        internal AddinHost host;
        private IAddin addin;
        private Image thumb = null;
        private Image thumbBackup = null;

        /// <summary>
        /// 
        /// </summary>
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
        private ColorMatrix colorMatrix = null;
        public ParamItem ParamColorMatrix
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "ColorMatrix";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = colorMatrix is ColorMatrix ? colorMatrix.GetType() : typeof( ColorMatrix );
                pi.Value = colorMatrix;
                return ( pi );
            }
            internal set { colorMatrix = (ColorMatrix) value.Value; }
        }
        private string colorMatrixFile = "";
        public ParamItem ParamColorMatrixFile
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "ColorMatrixFile";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = colorMatrixFile.GetType();
                pi.Value = colorMatrixFile;
                return ( pi );
            }
            internal set { colorMatrixFile = (string) value.Value; }
        }

        public GrayscaleForm()
        {
            InitializeComponent();
        }

        public GrayscaleForm( IAddin filter )
        {
            this.addin = filter;
            InitializeComponent();

            toolTip.ToolTipTitle = addin.DisplayName;
            AddinUtils.Translate( addin, this, toolTip );

            thumb = AddinUtils.CreateThumb( addin.ImageData, imgPreview.Size );
            imgPreview.Image = thumb;

            cbGrayMode.DataSource = Enum.GetValues( typeof( GrayscaleMode ) );
        }

        private void GrayscaleForm_Load( object sender, EventArgs e )
        {
            cbGrayMode.SelectedIndex = 0;
        }

        private void cbGrayMode_SelectedIndexChanged( object sender, EventArgs e )
        {
            //grayscaleMode = (GrayscaleMode) cbGrayMode.SelectedIndex;
            Enum.TryParse( cbGrayMode.SelectedValue.ToString(), out grayscaleMode );
            addin.Params[ParamGrayscaleMode.Name] = ParamGrayscaleMode;

            if ( grayscaleMode == GrayscaleMode.TestMatrix )
            {
                var dlgOpen = new OpenFileDialog();
                dlgOpen.Filter = "ColorMatrix File( *.cm ) | *.cm";
                if ( dlgOpen.ShowDialog() == DialogResult.OK )
                {
                    colorMatrixFile = dlgOpen.FileName;
                    addin.Params[ParamColorMatrixFile.Name] = ParamColorMatrixFile;

                    //var json = File.ReadAllText( $"{dlgOpen.FileName}" );

                    //JavaScriptSerializer serializer  = new JavaScriptSerializer();
                    //colorMatrix = (ColorMatrix) serializer.Deserialize(json , typeof(ColorMatrix));
                    //addin.Params[ParamColorMatrix.Name] = ParamColorMatrix;
                }
            }
            imgPreview.Image = addin.Apply( thumb );
        }

        private void btnOriginal_MouseDown( object sender, MouseEventArgs e )
        {
            thumbBackup = imgPreview.Image;
            imgPreview.Image = thumb;
        }

        private void btnOriginal_MouseUp( object sender, MouseEventArgs e )
        {
            imgPreview.Image = thumbBackup is Image ? thumbBackup : thumb;
        }
    }
}
