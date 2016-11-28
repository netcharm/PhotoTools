using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Cyotek.Windows.Forms;

namespace NetCharm.Common.Controls
{
    public partial class ImageActions : UserControl
    {
        public event EventHandler<ImageBoxZoomEventArgs> Zoomed;
        public event EventHandler ZoomOut;
        public event EventHandler ZoomIn;
        public event EventHandler ZoomChanged;
        public event EventHandler ZoomLevelsChanged;
        public event EventHandler ViewOriginalDown;
        public event EventHandler ViewOriginalUp;

        private int zoom = 100;
        public int Zoom
        {
            get
            {
                if ( imgbox is Cyotek.Windows.Forms.ImageBox )
                    zoom = imgbox.Zoom;
                return ( zoom );
            }
            set
            {
                zoom = value;
                if ( imgbox is Cyotek.Windows.Forms.ImageBox )
                    imgbox.Zoom = zoom;
                tscbZoomLevels.Text = $"{zoom}%";
            }
        }

        private Cyotek.Windows.Forms.ImageBox imgbox;
        public Cyotek.Windows.Forms.ImageBox ImageBox
        {
            get { return ( imgbox ); }
            set
            {
                imgbox = value;
                if ( imgbox is Cyotek.Windows.Forms.ImageBox )
                {
                    imgsrc = imgbox.Image;

                    zoomlevels = imgbox.ZoomLevels;

                    tscbZoomLevels.Items.Clear();
                    cmsZoomLevel.Items.Clear();
                    cmsZoomLevel.Items.Add( $"Fit" );
                    cmsZoomLevel.Items.Add( "-" );
                    foreach ( var zl in zoomlevels )
                    {
                        cmsZoomLevel.Items.Add( $"{zl}%" );
                        tscbZoomLevels.Items.Add( $"{zl}%" );
                    }
                }
            }
        }

        private ZoomLevelCollection zoomlevels = new ZoomLevelCollection();
        public ZoomLevelCollection ZoomLevels
        {
            get
            {
                if ( imgbox is Cyotek.Windows.Forms.ImageBox )
                    zoomlevels = imgbox.ZoomLevels;
                return ( zoomlevels );
            }
            set
            {
                zoomlevels = value;
                if ( imgbox is Cyotek.Windows.Forms.ImageBox )
                    imgbox.ZoomLevels = zoomlevels;

                tscbZoomLevels.Items.Clear();
                cmsZoomLevel.Items.Clear();
                cmsZoomLevel.Items.Add( $"Fit" );
                cmsZoomLevel.Items[0].Tag = -1;
                cmsZoomLevel.Items.Add( "-" );
                foreach ( var zl in zoomlevels )
                {
                    cmsZoomLevel.Items.Add( $"{zl}%" );
                    cmsZoomLevel.Items[cmsZoomLevel.Items.Count-1].Tag = zl;
                    tscbZoomLevels.Items.Add( $"{zl}%" );
                }
            }
        }

        private System.Drawing.Image imgBackup = null;
        private System.Drawing.Image imgsrc = null;
        public System.Drawing.Image Source
        {
            get { return ( imgsrc ); }
            set { imgsrc = value; }
        }

        public ImageActions()
        {
            InitializeComponent();
        }

        private void ImageActions_Load( object sender, EventArgs e )
        {
            if ( imgbox is Cyotek.Windows.Forms.ImageBox )
            {
                if ( this.Zoomed is EventHandler<ImageBoxZoomEventArgs> )
                    imgbox.Zoomed += new System.EventHandler<ImageBoxZoomEventArgs>( Zoomed );
                imgbox.Zoomed += new System.EventHandler<ImageBoxZoomEventArgs>( ImageBox_Zoomed );

                if ( this.ZoomChanged is EventHandler )
                    imgbox.ZoomChanged += new System.EventHandler( this.ZoomChanged );
                if ( this.ZoomLevelsChanged is EventHandler )
                    imgbox.ZoomLevelsChanged += new System.EventHandler( this.ZoomLevelsChanged );
            }
        }

        private void btnZoomOut_Click( object sender, EventArgs e )
        {
            if ( imgbox is Cyotek.Windows.Forms.ImageBox )
            {
                imgbox.ZoomOut();
                tscbZoomLevels.Text = $"{imgbox.Zoom}%";
            }
            else
                this.ZoomOut?.Invoke( this, e );
        }

        private void btnZoomIn_Click( object sender, EventArgs e )
        {
            if ( imgbox is Cyotek.Windows.Forms.ImageBox )
            {
                imgbox.ZoomIn();
                tscbZoomLevels.Text = $"{imgbox.Zoom}%";
            }
            else
                this.ZoomIn?.Invoke( this, e );
        }

        private void cmsZoomLevel_ItemClicked( object sender, ToolStripItemClickedEventArgs e )
        {
            //
            if( e.ClickedItem.Tag is int)
            {
                zoom = (int) e.ClickedItem.Tag;
                if ( imgbox is Cyotek.Windows.Forms.ImageBox )
                {
                    if ( zoom <= 0 )
                    {
                        imgbox.ZoomToFit();
                        zoom = imgbox.Zoom;
                    }
                    else
                    {
                        imgbox.Zoom = zoom;
                    }
                }
                this.ZoomChanged?.Invoke( this, new EventArgs() );
            }
            //MessageBox.Show( e.ClickedItem.Text );
        }

        private void tsbtnOriginal_MouseDown( object sender, MouseEventArgs e )
        {
            if ( imgbox is Cyotek.Windows.Forms.ImageBox )
            {
                this.imgBackup = imgbox.Image;
                imgbox.Image = imgsrc;
            }
            else
                this.ViewOriginalDown?.Invoke( this, new EventArgs() );
        }

        private void tsbtnOriginal_MouseUp( object sender, MouseEventArgs e )
        {
            if ( imgbox is Cyotek.Windows.Forms.ImageBox )
            {
                imgbox.Image = this.imgBackup;
            }
            else
                this.ViewOriginalUp?.Invoke( this, new EventArgs() );
        }

        private void tscbZoomLevels_TextChanged( object sender, EventArgs e )
        {
            try
            {
                zoom = Convert.ToInt32( tscbZoomLevels.Text.Replace( "%", "" ) );
            }
            catch ( Exception )
            {
            }
            if ( imgbox is Cyotek.Windows.Forms.ImageBox )
            {
                imgbox.Zoom = zoom;
                tscbZoomLevels.Text = $"{imgbox.Zoom}%";
            }
            else
                this.ZoomChanged?.Invoke( this, e );
        }

        private void tscbZoomLevels_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {
                zoom = Convert.ToInt32( tscbZoomLevels.Text.Replace( "%", "" ) );
            }
            catch ( Exception )
            {
            }
            if ( imgbox is Cyotek.Windows.Forms.ImageBox )
            {
                imgbox.Zoom = zoom;
                tscbZoomLevels.Text = $"{imgbox.Zoom}%";
            }
            else
                this.ZoomChanged?.Invoke( this, e );
        }

        private void ImageBox_Zoomed(object sender, ImageBoxZoomEventArgs e)
        {
            zoom = e.NewZoom;
            tscbZoomLevels.Text = $"{imgbox.Zoom}%";
        }
    }
}
