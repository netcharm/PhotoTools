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
        private IAddin Addin = null;

        private static Image ImgSrc = null;
        private static Image ImgDst = null;
        public Image ImageData
        {
            get { ImgDst = imgEditor.Image; return ( ImgDst ); }
            set { ImgSrc = value; SetSizeMode(); imgEditor.Image = ImgSrc; }
        }

        private bool ShiftPressed = false;
        PointF pO = new Point(0,0);

        /// <summary>
        /// 
        /// </summary>
        public EditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="addin"></param>
        /// <param name="image"></param>
        public EditorForm( AddinHost host, IAddin addin, Image image = null )
        {
            Host = host;
            Addin = addin;

            ImgSrc = image;
            ImgDst = image;
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Image GetImage()
        {
            return ( ImgDst );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        public static void SetImage(Image image)
        {
            ImgSrc = image;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetSizeMode()
        {
            if(ImgSrc is Image)
            {
                RectangleF rectSel = imgEditor.SelectionRegion;
                if ( imgEditor.ClientSize.Width >= ImgSrc.Width && imgEditor.ClientSize.Height >= ImgSrc.Height )
                {
                    imgEditor.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Normal;
                }
                else
                {
                    imgEditor.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Fit;
                }
                imgEditor.SelectionRegion = rectSel;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorForm_Load( object sender, EventArgs e )
        {
            imgEditor.Image = ImgSrc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgEditor_SizeChanged( object sender, EventArgs e )
        {
            SetSizeMode();
        }

        private void imgEditor_ImageChanged( object sender, EventArgs e )
        {
            //
        }

        private void imgEditor_Click( object sender, EventArgs e )
        {
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgEditor_DoubleClick( object sender, EventArgs e )
        {
            imgEditor.SelectNone();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgEditor_MouseDown( object sender, MouseEventArgs e )
        {
            pO = imgEditor.PointToImage( e.X, e.Y );
            pO.X = (float) Math.Round( pO.X );
            pO.Y = (float) Math.Round( pO.Y );
        }

        private void imgEditor_MouseUp( object sender, MouseEventArgs e )
        {
            //
        }

        private void imgEditor_MouseMove( object sender, MouseEventArgs e )
        {
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgEditor_KeyDown( object sender, KeyEventArgs e )
        {
            ShiftPressed = e.Shift;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgEditor_KeyUp( object sender, KeyEventArgs e )
        {
            ShiftPressed = e.Shift;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgEditor_Selecting( object sender, Cyotek.Windows.Forms.ImageBoxCancelEventArgs e )
        {
            RectangleF rect = imgEditor.SelectionRegion;

            if ( ShiftPressed )
            {
                float ms = Math.Min(rect.Width, rect.Height);

                if ( rect.X < pO.X )
                    rect.X = pO.X - ms;
                rect.Width = ms;

                if ( rect.Y < pO.Y )
                    rect.Y = pO.Y - ms;
                rect.Height = ms;
            }
            rect.X = (float) Math.Round( rect.X );
            rect.Y = (float) Math.Round( rect.Y );
            rect.Width = (float) Math.Round( rect.Width );
            rect.Height = (float) Math.Round( rect.Height );

            imgEditor.SelectionRegion = rect;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgEditor_Selected( object sender, EventArgs e )
        {
            RectangleF rect = imgEditor.SelectionRegion;

            if ( ShiftPressed)
            {
                float ms = Math.Min(rect.Width, rect.Height);

                if ( rect.X < pO.X )
                    rect.X = pO.X - ms;
                rect.Width = ms;

                if ( rect.Y < pO.Y )
                    rect.Y = pO.Y - ms;
                rect.Height = ms;
            }
            rect.X = (float) Math.Round( rect.X );
            rect.Y = (float) Math.Round( rect.Y );
            rect.Width = (float) Math.Round( rect.Width );
            rect.Height = (float) Math.Round( rect.Height );

            imgEditor.SelectionRegion = rect;
        }
    }
}
