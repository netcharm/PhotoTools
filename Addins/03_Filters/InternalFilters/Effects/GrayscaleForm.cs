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
    public partial class GrayscaleForm : Form
    {
        internal AddinHost host;
        private IAddin addin;
        private GrayscaleMode grayscaleMode;
        private Image thumb = null;

        /// <summary>
        /// 
        /// </summary>
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

            //cbGrayMode.Items.Clear();
            //cbGrayMode.Items.Add( AddinUtils._( addin, AddinUtils.T( "None" ) ) );
            //cbGrayMode.Items.Add( AddinUtils._( addin, AddinUtils.T( "Grayscale" ) ) );
            //cbGrayMode.Items.Add( AddinUtils._( addin, AddinUtils.T( "Sepia" ) ) );
            //cbGrayMode.Items.Add( AddinUtils._( addin, AddinUtils.T( "Tawawa" ) ) );
            //cbGrayMode.Items.Add( AddinUtils._( addin, AddinUtils.T( "Custom" ) ) );
        }

        private void GrayscaleForm_Load( object sender, EventArgs e )
        {
            cbGrayMode.SelectedIndex = 0;
        }

        private void cbGrayMode_SelectedIndexChanged( object sender, EventArgs e )
        {
            grayscaleMode = (GrayscaleMode) cbGrayMode.SelectedIndex;
            if ( !addin.Params.ContainsKey( ParamGrayscaleMode.Name ) )
                addin.Params.Add( ParamGrayscaleMode.Name, ParamGrayscaleMode );
            else
                addin.Params[ParamGrayscaleMode.Name] = ParamGrayscaleMode;

            imgPreview.Image = addin.Apply( thumb );
        }

    }
}
