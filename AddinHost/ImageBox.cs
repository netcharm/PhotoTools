using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetCharm.Image.Addins
{
    public partial class ImageBox : UserControl
    {
        private RectangleF selection = new RectangleF();
        public RectangleF SelectionRegion
        {
            get
            {
                selection = Viewer.SelectionRegion;
                return ( selection );
            }
            set
            {
                selection = value;
                Viewer.SelectionRegion = value;
            }
        }

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

        public ImageBox()
        {
            InitializeComponent();
        }

        private void Viewer_DoubleClick( object sender, EventArgs e )
        {
            Viewer.SelectNone();
        }

        private void Viewer_MouseDown( object sender, MouseEventArgs e )
        {
            pO = Viewer.PointToImage( e.X, e.Y );
            pO.X = (float) Math.Round( pO.X );
            pO.Y = (float) Math.Round( pO.Y );

            if ( pO.X > selection.X + corner && pO.X < selection.X + selection.Width - corner &&
                pO.Y > selection.Y + corner && pO.Y < selection.Y + selection.Height - corner )
            {
                mSelection = false;
                mMoveRegion = true;
                mResizeRegion = false;
                Viewer.Cursor = Cursors.Hand;
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

        private void Viewer_MouseUp( object sender, MouseEventArgs e )
        {
            mMoveRegion = false;
            mSelection = false;
            mResizeRegion = false;
            mCornerSide = false;
            mPos = 0;
            Viewer.Cursor = Cursors.Default;
        }

        private void Viewer_MouseMove( object sender, MouseEventArgs e )
        {
            PointF pN = Viewer.PointToImage( e.X, e.Y );
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

                Viewer.SelectionRegion = selection;
                #endregion
            }
            else if ( mMoveRegion )
            {
                #region Mouse Move SelectionRegion
                float dX = pN.X - pO.X;
                float dY = pN.Y - pO.Y;
                selection.X = pS.X + dX;
                selection.Y = pS.Y + dY;
                Viewer.SelectionRegion = selection;
                #endregion
            }
            else if ( mResizeRegion )
            {
                #region Resize SelectionRegion

                float dX = pN.X - pL.X;
                float dY = pN.Y - pL.Y;
                pL.X = pN.X;
                pL.Y = pN.Y;

                switch ( mPos )
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
                Viewer.SelectionRegion = selection;

                #endregion
            }
            else if ( selection.Width > 0 && selection.Height > 0 )
            {
                Cursor cur = Cursors.Default;
                mCornerSide = AddinUtils.GetPosOfRegion( selection, pN, out mPos, out cur );
                if ( mCornerSide )
                {
                    Viewer.Cursor = cur;
                }
                else
                {
                    if ( mPos == ContentAlignment.MiddleCenter )
                        Viewer.Cursor = Cursors.Hand;
                    else
                        Viewer.Cursor = Cursors.Default;
                }
            }
            else
            {
                mCornerSide = false;
                Viewer.Cursor = Cursors.Default;
            }
        }

        private void Viewer_SelectionRegionChanged( object sender, EventArgs e )
        {
            if ( !mMoveRegion || !mSelection )
            {
                selection = Viewer.SelectionRegion;
            }
        }
    }
}
