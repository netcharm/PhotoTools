using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetCharm.Image.Addins;

namespace InternalFilters
{
    public partial class EditorForm : Form
    {
        private AddinHost Host = null;

        private static Image ImgSrc = null;
        private static Image ImgDst = null;
        public Image ImageData
        {
            get { ImgDst = imgEditor.Image; return ( ImgDst ); }
            set { ImgSrc = value; imgEditor.Image = ImgSrc; }
        }

        public EditorForm()
        {
            InitializeComponent();
        }

        public EditorForm( AddinHost host, Image image = null )
        {
            Host = host;
            ImgSrc = image;
            ImgDst = image;
            InitializeComponent();
        }

        public static Image GetImage()
        {
            return ( ImgDst );
        }

        public static void SetImage(Image image)
        {
            ImgSrc = image;
        }

        private void EditorForm_Load( object sender, EventArgs e )
        {
            imgEditor.Image = ImgSrc;
        }
    }
}
