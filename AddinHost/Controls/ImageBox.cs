using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cyotek.Windows.Forms;

namespace NetCharm.Image.Addins
{
    [ToolboxBitmap(typeof( Cyotek.Windows.Forms.ImageBox ), "ImageBox.bmp")]
    [ToolboxItem(true)]
    public partial class ImageBox : UserControl
    {
        #region Private variables for ImageBox
        private CornerRegion selectionCorner;
        private RectangleF selection = new RectangleF();
        private RectangleF selectionSrc = new RectangleF();
        private bool mSelection =false;
        private bool mMoveRegion = false;
        private bool mResizeRegion = false;
        private bool mCornerSide = false;
        private bool mStart = false;
        private PointF pO;
        private PointF pS;
        private PointF pL;

        private CornerRegionType mPos = CornerRegionType.None;

        //OpaqueMode opaqueMode = OpaqueMode.Alpha;

        private CropMode cropMode = CropMode.AspectRatio;
        private float cropAspectFactor = 1f;

        #endregion

        #region Properties of ImageBox
        /// <summary>
        /// 
        /// </summary>
        public System.Drawing.Image Image
        {
            get { return ( Viewer.Image ); }
            set { Viewer.Image = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Color SelectionColor
        {
            get { return ( Viewer.SelectionColor ); }
            set { Viewer.SelectionColor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public RectangleF SelectionRegion
        {
            get { return ( Viewer.SelectionRegion ); }
            set { Viewer.SelectionRegion = value; }
        }
        private bool _selectionKeepAspectRatio = false;
        public bool SelectionKeepAspect
        {
            get { return ( _selectionKeepAspectRatio ); }
            set { _selectionKeepAspectRatio = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public ImageBoxSizeMode SizeMode
        {
            get { return ( Viewer.SizeMode ); }
            set { Viewer.SizeMode = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Zoom
        {
            get { return ( Viewer.Zoom ); }
            set { Viewer.Zoom = value; }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public ImageBox()
        {
            InitializeComponent();
            Viewer.Size = ClientSize;
            Viewer.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Point PointToImage(Point p)
        {
            return ( Viewer.PointToImage( p ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Viewer_DoubleClick( object sender, EventArgs e )
        {
            Viewer.SelectNone();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Viewer_MouseDown( object sender, MouseEventArgs e )
        {
            pO = Viewer.PointToImage( e.X, e.Y );
            pO.X = (float) Math.Round( pO.X );
            pO.Y = (float) Math.Round( pO.Y );

            if ( mPos == CornerRegionType.MiddleCenter )
            {
                mSelection = false;
                mMoveRegion = true;
                mResizeRegion = false;
                pS = new PointF( selection.X, selection.Y );
            }
            else if ( mCornerSide )
            {
                mSelection = false;
                mMoveRegion = false;
                mResizeRegion = true;
                pL = new PointF( pO.X, pO.Y );
                pS = new PointF( selection.Right, selection.Bottom );
            }
            else
            {
                mSelection = true;
                mMoveRegion = false;
                mResizeRegion = false;
                mStart = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Viewer_MouseUp( object sender, MouseEventArgs e )
        {
            if ( e.Clicks >= 1 && e.Button == MouseButtons.Right ) Viewer.SelectNone();

            mSelection = false;
            mMoveRegion = false;
            mResizeRegion = false;
            mCornerSide = false;
            mStart = false;
            mPos = CornerRegionType.None;
            Viewer.Cursor = Cursors.Default;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Viewer_MouseMove( object sender, MouseEventArgs e )
        {
            PointF pN = Viewer.PointToImage( e.X, e.Y );
            if ( e.Button == MouseButtons.Left )
            {
                if ( mSelection )
                {
                    #region Mouse Selection
                    if ( !mStart )
                    {
                        selection.X = pO.X;
                        selection.Y = pO.Y;
                        mStart = true;
                    }

                    float dX = pN.X - pO.X;
                    float dY = pN.Y - pO.Y;
                    if ( dX < 0 )
                    {
                        selection.X = pN.X;
                        selection.Width = -dX;
                    }
                    else
                        selection.Width = dX;

                    if ( dY < 0 )
                    {
                        selection.Y = pN.Y;
                        selection.Height = -dY;
                    }
                    else
                        selection.Height = dY;

                    #endregion
                }
                else if ( mMoveRegion )
                {
                    #region Mouse Move SelectionRegion
                    float dX = pN.X - pO.X;
                    float dY = pN.Y - pO.Y;
                    selection.X = pS.X + dX;
                    selection.Y = pS.Y + dY;
                    #endregion
                }
                else if ( mResizeRegion )
                {
                    #region Resize SelectionRegion

                    float dX = pN.X - pL.X;
                    float dY = pN.Y - pL.Y;
                    pL.X = pN.X;
                    pL.Y = pN.Y;

                    if ( _selectionKeepAspectRatio )
                    {
                        #region Keep Aspect Ration Resize SelectionRegion

                        switch ( mPos )
                        {
                            case CornerRegionType.TopLeft:
                                selection.X += dX;
                                selection.Y += dY;
                                selection.Width -= dX;
                                selection.Height -= dY;
                                break;
                            case CornerRegionType.TopCenter:
                                selection.Y += dY;
                                selection.Height -= dY;
                                selection.Inflate( ( selection.Height * cropAspectFactor - selection.Width ) / 2.0f, 0 );
                                break;
                            case CornerRegionType.TopRight:
                                selection.Y += dY;
                                selection.Width += dX;
                                selection.Height -= dY;
                                break;
                            case CornerRegionType.MiddleLeft:
                                selection.X += dX;
                                selection.Width -= dX;
                                selection.Inflate( 0, ( selection.Width / cropAspectFactor - selection.Height ) / 2.0f );
                                break;
                            case CornerRegionType.MiddleCenter:
                                break;
                            case CornerRegionType.MiddleRight:
                                selection.Width += dX;
                                selection.Inflate( 0, ( selection.Width / cropAspectFactor - selection.Height ) / 2.0f );
                                break;
                            case CornerRegionType.BottomLeft:
                                selection.X += dX;
                                selection.Width -= dX;
                                selection.Height += dY;
                                break;
                            case CornerRegionType.BottomCenter:
                                selection.Height += dY;
                                selection.Inflate( ( selection.Height * cropAspectFactor - selection.Width ) / 2.0f, 0 );
                                break;
                            case CornerRegionType.BottomRight:
                                selection.Width += dX;
                                selection.Height += dY;
                                break;
                        }
                        #endregion
                    }
                    else
                    {
                        #region Normal Resize Selection Region
                        switch ( mPos )
                        {
                            case CornerRegionType.TopLeft:
                                selection.X += dX;
                                selection.Width -= dX;
                                selection.Y += dY;
                                selection.Height -= dY;
                                break;
                            case CornerRegionType.TopCenter:
                                selection.Y += dY;
                                selection.Height -= dY;
                                break;
                            case CornerRegionType.TopRight:
                                selection.Width += dX;
                                selection.Y += dY;
                                selection.Height -= dY;
                                break;
                            case CornerRegionType.MiddleLeft:
                                selection.X += dX;
                                selection.Width -= dX;
                                break;
                            case CornerRegionType.MiddleCenter:
                                break;
                            case CornerRegionType.MiddleRight:
                                selection.Width += dX;
                                break;
                            case CornerRegionType.BottomLeft:
                                selection.X += dX;
                                selection.Width -= dX;
                                selection.Height += dY;
                                break;
                            case CornerRegionType.BottomCenter:
                                selection.Height += dY;
                                break;
                            case CornerRegionType.BottomRight:
                                selection.Width += dX;
                                selection.Height += dY;
                                break;
                        }
                        #endregion
                    }
                    #endregion
                }
                Viewer.SelectionRegion = selection;
                selectionCorner = new CornerRegion( selection );
            }
            else if ( e.Button == MouseButtons.None )
            {
                if ( selection.Width > 0 && selection.Height > 0 )
                {
                    #region Detect Mouse Role
                    Cursor cur = Cursors.Default;
                    mCornerSide = AddinUtils.GetPosOfRegion( selection, pN, out mPos, out cur );
                    Viewer.Cursor = cur;
                    #endregion
                }
                else
                {
                    mPos = CornerRegionType.None;
                    mCornerSide = false;
                    Viewer.Cursor = Cursors.Default;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Viewer_SelectionRegionChanged( object sender, EventArgs e )
        {
            if ( !mMoveRegion || !mSelection )
            {
                selection = Viewer.SelectionRegion;
                selectionCorner = new CornerRegion( selection );
            }
        }
    }
}
