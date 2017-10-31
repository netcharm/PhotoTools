using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeICON
{
    public partial class MainForm : Form
    {
        private Icon toIcon(Image src)
        {
            Icon result = null;

            using ( MemoryStream msr = new MemoryStream() )
            {
                if ( src is Image )
                {
                    Image dst = new Bitmap(src.Width, src.Height, PixelFormat.Format32bppArgb);
                    dst.Save( msr, ImageFormat.Icon );
                }

                new Bitmap()
                //result.
            }


            return ( result );
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load( object sender, EventArgs e )
        {

        }
    }
}
