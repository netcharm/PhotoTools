using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetCharm.Common;
using NetCharm.Image;
using NetCharm.Image.Addins;

namespace InternalFilters
{
    public partial class CropForm : Form
    {
        internal AddinHost host;
        internal IAddin addin;

        private Image thumb = null;

        private Dictionary<string, Size> aspectList = new Dictionary<string, Size>();

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

        ContentMaskMode opaqueMode = ContentMaskMode.Alpha;

        private CropMode cropMode = CropMode.AspectRatio;
        private SideType cropSide = (SideType.Top | SideType.Bottom | SideType.Left | SideType.Right);
        private string cropAspect = "3 x 2";
        private float cropAspectFactor = 1f;

        public ParamItem ParamCropMode
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "CropMode";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = cropMode.GetType();
                pi.Value = cropMode;
                return ( pi );
            }
            internal set { cropMode = (CropMode) Convert.ToInt32( value.Value ); }
        }
        public ParamItem ParamCropSide
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "CropSide";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = cropSide.GetType();
                pi.Value = cropSide;
                return ( pi );
            }
            internal set { cropSide = (SideType) value.Value; }
        }
        public ParamItem ParamCropAspect
        {
            get
            {
                ParamItem pi = new ParamItem();
                pi.Name = "CropAspect";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = cropAspect.GetType();
                pi.Value = cropAspect;
                return ( pi );
            }
            internal set { cropAspect = (string) value.Value; }
        }
        public ParamItem ParamCropRegion
        {
            get
            {
                selectionSrc = GetImageSelection();
                ParamItem pi = new ParamItem();
                pi.Name = "CropRegion";
                pi.DisplayName = AddinUtils._( addin, pi.Name );
                pi.Type = Rectangle.Round( selectionSrc ).GetType();
                pi.Value = Rectangle.Round( selectionSrc );
                return ( pi );
            }
            internal set { selectionSrc = (Rectangle) value.Value; }
        }

        internal void SetImageSelection( Rectangle rectangle )
        {
            selectionSrc = rectangle;
            selection = rectangle;
        }

        internal void SetImageSelection( RectangleF rectangleF )
        {
            selectionSrc = Rectangle.Round( rectangleF );
        }

        internal Rectangle GetImageSelection()
        {
            selection = imgPreview.SelectionRegion;
            selectionSrc = AddinUtils.RemapRegion( selection, thumb, addin.ImageData );
            return ( Rectangle.Round( selectionSrc ) );
        }

        internal RectangleF MakeAspectRegion( Size size, RectangleF region, float aspect, bool force=false )
        {
            RectangleF result = new RectangleF(region.X, region.Y, region.Width, region.Height);

            if ( force || region.Width == 0 || region.Height == 0 )
            {
                result.X = size.Width / 2.0f;
                result.Y = size.Height / 2.0f;
                result.Width = 0;
                result.Height = 0;
                float w = size.Width;
                float h = size.Height;
                if ( cropAspectFactor >= 1 && size.Width >= size.Height )
                {
                    w = h * cropAspectFactor;
                    if ( w > size.Width )
                    {
                        w = size.Width;
                        h = w / cropAspectFactor;
                    }
                }
                else if ( cropAspectFactor >= 1 && size.Width < size.Height )
                {
                    h = w / cropAspectFactor;
                    if ( h > size.Height )
                    {
                        h = size.Height;
                        w = h * cropAspectFactor;
                    }
                }
                else if ( cropAspectFactor < 1 && size.Width >= size.Height )
                {
                    w = h * cropAspectFactor;
                    if ( w > size.Width )
                    {
                        w = size.Width;
                        h = w / cropAspectFactor;
                    }
                }
                else
                {
                    h = w / cropAspectFactor;
                    if ( h > size.Height )
                    {
                        h = size.Height;
                        w = h * cropAspectFactor;
                    }
                }
                result.Inflate( w / 2.0f, h / 2.0f );
            }
            return ( result );
        }

        public CropForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ////
        /// </summary>
        /// <param name="filter"></param>
        public CropForm( IAddin filter )
        {
            this.addin = filter;
            InitializeComponent();
            toolTip.ToolTipTitle = addin.DisplayName;
            AddinUtils.Translate( addin, this, toolTip );

            aspectList.Clear();
            aspectList.Add( "1 x 1", new Size( 1, 1 ) );
            aspectList.Add( "2 x 1", new Size( 2, 1 ) );
            aspectList.Add( "3 x 1", new Size( 3, 1 ) );
            aspectList.Add( "3 x 2", new Size( 3, 2 ) );
            aspectList.Add( "4 x 3", new Size( 4, 3 ) );
            aspectList.Add( "16 x 9", new Size( 16, 9 ) );
            aspectList.Add( "16 x 10", new Size( 16, 10 ) );
            aspectList.Add( "1 x 2", new Size( 1, 2 ) );
            aspectList.Add( "1 x 3", new Size( 1, 3 ) );
            aspectList.Add( "2 x 3", new Size( 2, 3 ) );
            aspectList.Add( "3 x 4", new Size( 3, 4 ) );
            aspectList.Add( "9 x 16", new Size( 9, 16 ) );
            aspectList.Add( "10 x 16", new Size( 10, 16 ) );

            thumb = AddinUtils.CreateThumb( addin.ImageData, imgPreview.Size );
            imgPreview.Image = thumb;
        }

        private void CropForm_Load( object sender, EventArgs e )
        {
            cbAspect.Items.Clear();
            cbAspect.Items.AddRange( aspectList.Keys.ToArray() );
            cbAspect.SelectedIndex = 0;

            btnModeTrans.Enabled = AddinUtils.AlphaFormat.Contains( addin.ImageData.PixelFormat );

            if ( cropSide.HasFlag( SideType.Left ) )
                chkSideL.Checked = true;
            else
                chkSideL.Checked = false;
            if ( cropSide.HasFlag( SideType.Right ) )
                chkSideR.Checked = true;
            else
                chkSideR.Checked = false;
            if ( cropSide.HasFlag( SideType.Top ) )
                chkSideT.Checked = true;
            else
                chkSideT.Checked = false;
            if ( cropSide.HasFlag( SideType.Bottom ) )
                chkSideB.Checked = true;
            else
                chkSideB.Checked = false;

            if ( selection.Width > 0 && selection.Height > 0 )
            {
                btnModeSelection.PerformClick();
                imgPreview.SelectionRegion = AddinUtils.RemapRegion( selectionSrc, addin.ImageData, thumb );
            }                
            else
                btnModeAspect.PerformClick();
        }

        private void btnMode_Click( object sender, EventArgs e )
        {
            if ( sender == btnModeSelection )
            {
                grpCropSide.Enabled = false;
                grpCropAspect.Enabled = true;
                cropMode = CropMode.Selection;
            }
            else if ( sender == btnModeTrans || sender == btnModeTopLeft || sender == btnModeBottomRight )
            {
                grpCropSide.Enabled = true;
                grpCropAspect.Enabled = false;

                if ( ( sender as RadioButton ).Checked )
                {
                    if ( sender == btnModeTrans )
                    {
                        opaqueMode = ContentMaskMode.Alpha;
                        cropMode = CropMode.Opaque;
                    }
                    else if ( sender == btnModeTopLeft )
                    {
                        opaqueMode = ContentMaskMode.TopLeft;
                        cropMode = CropMode.TopLeft;
                    }
                    else if ( sender == btnModeBottomRight )
                    {
                        opaqueMode = ContentMaskMode.BottomRight;
                        cropMode = CropMode.BottomRight;
                    }

                    selectionSrc = AddinUtils.AdjustRegion( AddinUtils.GetContentBound( addin.ImageData, opaqueMode ), addin.ImageData, cropSide );
                    imgPreview.SelectionRegion = AddinUtils.RemapRegion( selectionSrc, addin.ImageData, thumb );
                }
            }
            else if ( sender == btnModeAspect )
            {
                grpCropSide.Enabled = false;
                grpCropAspect.Enabled = true;
                cropMode = CropMode.AspectRatio;
                selection = MakeAspectRegion( thumb.Size, selection, cropAspectFactor );
                imgPreview.SelectionRegion = selection;
            }
        }

        private void chkSide_Click( object sender, EventArgs e )
        {
            cropSide = SideType.None;

            if ( chkSideL.Checked )
            {
                cropSide |= SideType.Left;
            }
            if ( chkSideR.Checked )
            {
                cropSide |= SideType.Right;
            }
            if ( chkSideT.Checked )
            {
                cropSide |= SideType.Top;
            }
            if ( chkSideB.Checked )
            {
                cropSide |= SideType.Bottom;
            }

            selectionSrc = AddinUtils.AdjustRegion( AddinUtils.GetOpaqueBound( addin.ImageData, opaqueMode ), addin.ImageData, cropSide );
            imgPreview.SelectionRegion = AddinUtils.RemapRegion( selectionSrc, addin.ImageData, thumb );
        }

        private void cbAspect_SelectedIndexChanged( object sender, EventArgs e )
        {
            if(aspectList.ContainsKey( cbAspect.Text ) )
            {
                var size = aspectList[cbAspect.Text];
                edAspectW.Value = size.Width;
                edAspectH.Value = size.Height;
            }
        }

        private void edAspect_ValueChanged( object sender, EventArgs e )
        {
            cropAspectFactor = (float) Convert.ToDouble( edAspectW.Value / edAspectH.Value );
            if ( cropMode == CropMode.AspectRatio )
            {
                selection = MakeAspectRegion( thumb.Size, selection, cropAspectFactor, true );
            }
            imgPreview.SelectionRegion = selection;
        }

        private void imgPreview_MouseDown( object sender, MouseEventArgs e )
        {
            pO = imgPreview.PointToImage( e.X, e.Y );
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

        private void imgPreview_MouseUp( object sender, MouseEventArgs e )
        {
            if ( e.Clicks >= 1 && e.Button == MouseButtons.Right) imgPreview.SelectNone();

            mSelection = false;
            mMoveRegion = false;
            mResizeRegion = false;
            mCornerSide = false;
            mStart = false;
            mPos = CornerRegionType.None;
            imgPreview.Cursor = Cursors.Default;
        }

        private void imgPreview_MouseMove( object sender, MouseEventArgs e )
        {
            PointF pN = imgPreview.PointToImage( e.X, e.Y );
            if ( e.Button == MouseButtons.Left )
            {
                if ( mSelection )
                {
                    #region Mouse Selection
                    if(!mStart)
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

                    float min = 0;

                    if ( cropMode == CropMode.AspectRatio )
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
                                min = Math.Min( selection.Width, selection.Height );
                                if ( selection.Width > selection.Height )
                                    selection.Height = selection.Width / cropAspectFactor;
                                else
                                    selection.Width = selection.Height * cropAspectFactor;
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
#if DEBUG
                    string log = $"Region: X[{selection.X}], Y[{selection.Y}], W[{selection.Width}], H[{selection.Height}]";
                    host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.Log, log ) );
#endif
                    #endregion
                }
                imgPreview.SelectionRegion = selection;
                selectionCorner = new CornerRegion( selection );
            }
            else if(e.Button == MouseButtons.None)
            {
                if ( selection.Width > 0 && selection.Height > 0 )
                {
                    #region Detect Mouse Role
                    Cursor cur = Cursors.Default;
                    mCornerSide = AddinUtils.GetPosOfRegion( selection, pN, out mPos, out cur );
                    imgPreview.Cursor = cur;
                    #endregion
                }
                else
                {
                    mPos = CornerRegionType.None;
                    mCornerSide = false;
                    imgPreview.Cursor = Cursors.Default;
                }
            }
        }

        private void imgPreview_SelectionRegionChanged( object sender, EventArgs e )
        {
            if ( !mMoveRegion || !mSelection )
            {
                selection = imgPreview.SelectionRegion;
                selectionCorner = new CornerRegion( selection );
            }
            selectionSrc = AddinUtils.RemapRegion( imgPreview.SelectionRegion, thumb, addin.ImageData );
            toolTip.SetToolTip( imgPreview, $"X:{selectionSrc.X}, Y:{selectionSrc.Y}, W:{selectionSrc.Width}, H:{selectionSrc.Height}]" );
        }

    }
}
