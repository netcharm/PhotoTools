using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetCharm.Image.Addins;

namespace InternalFilters
{
    public partial class CropForm : Form
    {
        private AddinHost host;
        private IAddin addin;

        private Image thumb = null;

        private Dictionary<string, Size> aspectList = new Dictionary<string, Size>();

        private RectangleF selection = new RectangleF();
        private RectangleF selectionSrc = new RectangleF();
        private bool mSelection;
        private bool mMoveRegion;
        private bool mResizeRegion;
        private bool mCornerSide;
        private PointF pO;
        private PointF pS;
        private PointF pL;
        private float corner = 8f;
        private ContentAlignment mPos = ContentAlignment.MiddleCenter;

        OpaqueMode opaqueMode = OpaqueMode.Alpha;

        private int cropMode = 4;
        private AnchorStyles cropSide = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
        private string cropAspect = "3 x 2";

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
            internal set { cropMode = (int) value.Value; }
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
            internal set { cropSide = (AnchorStyles) value.Value; }
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

            if ( cropSide.HasFlag( AnchorStyles.Left ) )
                chkSideL.Checked = true;
            else
                chkSideL.Checked = false;
            if ( cropSide.HasFlag( AnchorStyles.Right ) )
                chkSideR.Checked = true;
            else
                chkSideR.Checked = false;
            if ( cropSide.HasFlag( AnchorStyles.Top ) )
                chkSideT.Checked = true;
            else
                chkSideT.Checked = false;
            if ( cropSide.HasFlag( AnchorStyles.Bottom ) )
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
                cropMode = 0;
            }
            else if ( sender == btnModeTrans || sender == btnModeTopLeft || sender == btnModeBottomRight )
            {
                grpCropSide.Enabled = true;
                grpCropAspect.Enabled = false;

                if ( ( sender as RadioButton ).Checked )
                {
                    if ( sender == btnModeTrans )
                    {
                        opaqueMode = OpaqueMode.Alpha;
                        cropMode = 1;
                    }
                    else if ( sender == btnModeTopLeft )
                    {
                        opaqueMode = OpaqueMode.TopLeft;
                        cropMode = 2;
                    }
                    else if ( sender == btnModeBottomRight )
                    {
                        opaqueMode = OpaqueMode.BottomRight;
                        cropMode = 3;
                    }

                    selectionSrc = AddinUtils.AdjustRegion( AddinUtils.GetOpaqueBound( addin.ImageData, opaqueMode ), addin.ImageData, cropSide );
                    imgPreview.SelectionRegion = AddinUtils.RemapRegion( selectionSrc, addin.ImageData, thumb );
                }
            }
            else if ( sender == btnModeAspect )
            {
                grpCropSide.Enabled = false;
                grpCropAspect.Enabled = true;
                cropMode = 4;
            }
        }

        private void btnMode_CheckedChanged( object sender, EventArgs e )
        {
            //
        }

        private void chkSide_Click( object sender, EventArgs e )
        {
            cropSide = AnchorStyles.None;

            if ( chkSideL.Checked )
            {
                cropSide |= AnchorStyles.Left;
            }
            if ( chkSideR.Checked )
            {
                cropSide |= AnchorStyles.Right;
            }
            if ( chkSideT.Checked )
            {
                cropSide |= AnchorStyles.Top;
            }
            if ( chkSideB.Checked )
            {
                cropSide |= AnchorStyles.Bottom;
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
            //
        }

        private void imgPreview_Click( object sender, EventArgs e )
        {
            //
        }

        private void imgPreview_DoubleClick( object sender, EventArgs e )
        {
            imgPreview.SelectNone();
        }

        private void imgPreview_MouseDown( object sender, MouseEventArgs e )
        {
            pO = imgPreview.PointToImage( e.X, e.Y );
            pO.X = (float) Math.Round( pO.X );
            pO.Y = (float) Math.Round( pO.Y );

            if ( pO.X > selection.X + corner && pO.X < selection.X + selection.Width - corner &&
                pO.Y > selection.Y+ corner && pO.Y < selection.Y + selection.Height - corner )
            {
                mSelection = false;
                mMoveRegion = true;
                mResizeRegion = false;
                imgPreview.Cursor = Cursors.Hand;
                pS = new PointF( selection.X, selection.Y );
            }
            else if ( mCornerSide )
            {
                mSelection = false;
                mMoveRegion = false;
                mResizeRegion = true;
                pL = new PointF( pO.X, pO.Y );
            }
            else
            {
                mSelection = true;
                mMoveRegion = false;
                mResizeRegion = false;
                selection.X = pO.X;
                selection.Y = pO.Y;
            }
        }

        private void imgPreview_MouseUp( object sender, MouseEventArgs e )
        {
            mMoveRegion = false;
            mSelection = false;
            mResizeRegion = false;
            mCornerSide = false;
            mPos = 0;
            imgPreview.Cursor = Cursors.Default;
        }

        private void imgPreview_MouseMove( object sender, MouseEventArgs e )
        {
            PointF pN = imgPreview.PointToImage( e.X, e.Y );
            if ( mSelection )
            {
                #region Mouse Selection
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

                imgPreview.SelectionRegion = selection;
                #endregion
            }
            else if ( mMoveRegion )
            {
                #region Mouse Move SelectionRegion
                float dX = pN.X - pO.X;
                float dY = pN.Y - pO.Y;
                selection.X = pS.X + dX;
                selection.Y = pS.Y + dY;
                imgPreview.SelectionRegion = selection;
                #endregion
            }
            else if ( mResizeRegion )
            {
                #region Resize SelectionRegion

                float dX = pN.X - pL.X;
                float dY = pN.Y - pL.Y;
                pL.X = pN.X;
                pL.Y = pN.Y;

                switch(mPos)
                {
                    case ContentAlignment.TopLeft:
                        selection.X += dX;
                        selection.Width -= dX;
                        selection.Y += dY;
                        selection.Height -= dY;
                        break;
                    case ContentAlignment.TopCenter:
                        selection.Y += dY;
                        selection.Height -= dY;
                        break;
                    case ContentAlignment.TopRight:
                        selection.Width += dX;
                        selection.Y += dY;
                        selection.Height -= dY;
                        break;
                    case ContentAlignment.MiddleLeft:
                        selection.X += dX;
                        selection.Width -= dX;
                        break;
                    case ContentAlignment.MiddleCenter:
                        break;
                    case ContentAlignment.MiddleRight:
                        selection.Width += dX;
                        break;
                    case ContentAlignment.BottomLeft:
                        selection.X += dX;
                        selection.Width -= dX;
                        selection.Height += dY;
                        break;
                    case ContentAlignment.BottomCenter:
                        selection.Height += dY;
                        break;
                    case ContentAlignment.BottomRight:
                        selection.Width += dX;
                        selection.Height += dY;
                        break;
                }
                imgPreview.SelectionRegion = selection;

                #endregion
            }
            else if ( selection.Width > 0 && selection.Height > 0 )
            {
                Cursor cur = Cursors.Default;
                mCornerSide = AddinUtils.GetPosOfRegion( selection, pN, out mPos, out cur );
                if ( mCornerSide )
                {
                    imgPreview.Cursor = cur;
                }
                else
                {
                    if(mPos == ContentAlignment.MiddleCenter)
                        imgPreview.Cursor = Cursors.Hand;
                    else
                        imgPreview.Cursor = Cursors.Default;
                }
            }
            else
            {
                mCornerSide = false;
                imgPreview.Cursor = Cursors.Default;
            }

        }

        private void imgPreview_SelectionRegionChanged( object sender, EventArgs e )
        {
            if ( !mMoveRegion || !mSelection )
            {
                selection = imgPreview.SelectionRegion;
            }
        }

    }
}
