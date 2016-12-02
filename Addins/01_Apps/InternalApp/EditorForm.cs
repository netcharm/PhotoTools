using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetCharm.Image;
using NetCharm.Image.Addins;

namespace InternalFilters
{
    public partial class EditorForm : Form
    {
        private AddinHost Host = null;
        private IAddin Addin = null;

        private static PropertyItem[] propertyItems = new PropertyItem[]{};
        private static Image ImgSrc = null;
        private static Image ImgDst = null;
        public Image ImageData
        {
            get
            {
                if ( imgEditor.Image is Image )
                {
                    if ( propertyItems is PropertyItem[] )
                    {
                        ImgDst = new Bitmap( imgEditor.Image );
                        foreach ( var pi in propertyItems )
                        {
                            ImgDst.SetPropertyItem( pi );
                        }
                    }
                    else
                        ImgDst = imgEditor.Image;
                }
                return ( ImgDst );
            }
            set
            {
                if ( imgEditor.Image is Image ) HistoryUndo.Push( imgEditor.Image );
                else if ( value is Image )
                {
                    HistoryUndo.Push( value );
                    propertyItems = value.PropertyItems;
                }
                ImgSrc = value;
                SetSizeMode();
                imgEditor.Zoom = 100;
                imgEditor.Image = ImgSrc;
                imgEditor.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Normal;

                Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.Undo, HistoryUndo.Count > 1 ) );
                Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.Redo, HistoryRedo.Count > 0 ) );
            }
        }

        //private Stack<Image> HistoryUndo = new Stack<Image>(10);
        //private Stack<Image> HistoryRedo = new Stack<Image>(10);
        private CircularStack<Image> HistoryUndo = new CircularStack<Image>(10);
        private CircularStack<Image> HistoryRedo = new CircularStack<Image>(10);

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
            if ( propertyItems is PropertyItem[] )
            {
                foreach ( var pi in propertyItems )
                {
                    ImgDst.SetPropertyItem( pi );
                }
            }
            return ( ImgDst );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        public static void SetImage(Image image)
        {
            ImgSrc = image;
            propertyItems = image.PropertyItems;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetSizeMode(bool fit=false)
        {
            if (ImgSrc is Image)
            {
                RectangleF rectSel = imgEditor.SelectionRegion;

                if ( imgEditor.ClientSize.Width >= ImgSrc.Width && imgEditor.ClientSize.Height >= ImgSrc.Height )
                {
                    imgEditor.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Normal;
                }
                else
                {
                    imgEditor.SizeMode = Cyotek.Windows.Forms.ImageBoxSizeMode.Fit;
                    //float zoom = Math.Min(imgEditor.ClientSize.Width, imgEditor.ClientSize.Height) / Math.Min(ImgSrc.Width, ImgSrc.Height);
                    //imgEditor.Zoom = (int) Math.Ceiling( zoom * 100 );
                }
                imgEditor.SelectionRegion = rectSel;
            }
        }

        public ValueType Zoom( AddinCommand zoomMode)
        {
            switch(zoomMode)
            {
                case AddinCommand.ZoomIn:
                    imgEditor.ZoomIn();
                    break;
                case AddinCommand.ZoomOut:
                    imgEditor.ZoomOut();
                    break;
                case AddinCommand.ZoomRegion:
                    imgEditor.ZoomToRegion( imgEditor.SelectionRegion );
                    break;
                case AddinCommand.ZoomFit:
                    imgEditor.ZoomToFit();
                    break;
                case AddinCommand.Zoom100:
                    imgEditor.Zoom = 100;
                    break;
                case AddinCommand.ZoomLevel:
                    break;
            }
            return ( imgEditor.Zoom );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal object Undo()
        {
            var img = HistoryUndo.Count==1 ? HistoryUndo.Peek() : HistoryUndo.Pop();
            if(img is Image)
            {
                HistoryRedo.Push( imgEditor.Image );
                imgEditor.Image = img;
            }
            Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.Undo, HistoryUndo.Count > 1 ) );
            Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.Redo, HistoryRedo.Count > 0 ) );
            return ( true );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal object Redo()
        {
            var img = HistoryRedo.Pop();
            if ( img is Image )
            {
                HistoryUndo.Push( imgEditor.Image );
                imgEditor.Image = img;
            }
            Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.Undo, HistoryUndo.Count > 1 ) );
            Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.Redo, HistoryRedo.Count > 0 ) );
            return ( true );
        }

        internal void ClearHistory()
        {
            HistoryUndo.Clear();
            HistoryRedo.Clear();
            Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.Undo, HistoryUndo.Count > 1 ) );
            Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.Redo, HistoryRedo.Count > 0 ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditorForm_Load( object sender, EventArgs e )
        {
            //imgEditor.Image = ImgSrc;
            ImageData = ImgSrc;
            //SetSizeMode();

            //this.MinimizeBox = false;
            //this.MaximizeBox = false;
            //this.ControlBox = false;
            //this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgEditor_SizeChanged( object sender, EventArgs e )
        {
            //SetSizeMode();
        }

        private void imgEditor_ImageChanged( object sender, EventArgs e )
        {

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
            rect = Rectangle.Round( rect );

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgEditor_ZoomChanged( object sender, EventArgs e )
        {
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgEditor_Zoomed( object sender, Cyotek.Windows.Forms.ImageBoxZoomEventArgs e )
        {
            Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.ZoomLevel, imgEditor.Zoom ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal Rectangle GetImageSelection()
        {
            Rectangle result = new Rectangle(
                (int)Math.Round(imgEditor.SelectionRegion.X), 
                (int)Math.Round(imgEditor.SelectionRegion.Y),
                (int)Math.Round(imgEditor.SelectionRegion.Width), 
                (int)Math.Round(imgEditor.SelectionRegion.Height));
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selection"></param>
        internal void SetImageSelection(RectangleF selection)
        {
            imgEditor.SelectionRegion = selection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selection"></param>
        internal void SetImageSelection( Rectangle selection )
        {
            imgEditor.SelectionRegion = selection;
        }

    }
}
