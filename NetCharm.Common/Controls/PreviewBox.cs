using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetCharm.Image.Addins.Controls
{
    public partial class PreviewBox : UserControl
    {
        public PreviewBox()
        {
            InitializeComponent();
            imageActions.ImageBox = Preview;
        }
    }
}
