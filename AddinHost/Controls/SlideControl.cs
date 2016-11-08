using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace NetCharm.Image.Addins.Controls
{
    //[DefaultValue( "Value" ), DefaultEvent( "ValueChanged" ), ToolboxBitmap( typeof( TrackBar ) )]
    [ToolboxBitmap( typeof( TrackBar ) )]
    public partial class SlideControl : System.Windows.Forms.TrackBar
    {
        private int numberTicks = 10;
        private Rectangle trackRectangle = new Rectangle();
        private Rectangle ticksRectangle = new Rectangle();
        private Rectangle thumbRectangle = new Rectangle();
        private int currentTickPosition = 0;
        private float tickSpace = 0;
        private bool thumbClicked = false;
        private TrackBarThumbState thumbState = TrackBarThumbState.Normal;

        public SlideControl()
        {
            InitializeComponent();

            SetupTrackBar();
        }

        // Calculate the sizes of the bar, thumb, and ticks rectangle.
        private void SetupTrackBar()
        {
            if ( !TrackBarRenderer.IsSupported )
                return;

            using ( Graphics g = this.CreateGraphics() )
            {
                // Calculate the size of the track bar.
                trackRectangle.X = ClientRectangle.X + 2;
                trackRectangle.Y = ClientRectangle.Y + 28;
                trackRectangle.Width = ClientRectangle.Width - 4;
                trackRectangle.Height = 4;

                // Calculate the size of the rectangle in which to 
                // draw the ticks.
                ticksRectangle.X = trackRectangle.X + 4;
                ticksRectangle.Y = trackRectangle.Y - 8;
                ticksRectangle.Width = trackRectangle.Width - 8;
                ticksRectangle.Height = 4;

                tickSpace = ( (float) ticksRectangle.Width - 1 ) / ( (float) numberTicks - 1 );

                // Calculate the size of the thumb.
                thumbRectangle.Size = TrackBarRenderer.GetTopPointingThumbSize( g, TrackBarThumbState.Normal );

                thumbRectangle.X = CurrentTickXCoordinate();
                thumbRectangle.Y = trackRectangle.Y - 8;
            }
        }

        private int CurrentTickXCoordinate()
        {
            if ( tickSpace == 0 )
            {
                return 0;
            }
            else
            {
                return ( (int) Math.Round( tickSpace ) *
                    currentTickPosition );
            }
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            // If there is an image and it has a location, 
            // paint it when the Form is repainted.
            base.OnPaint( e );

            if ( !TrackBarRenderer.IsSupported )
            {
                this.Parent.Text = "CustomTrackBar Disabled";
                return;
            }

            this.Parent.Text = "CustomTrackBar Enabled";
            TrackBarRenderer.DrawHorizontalTrack( e.Graphics, trackRectangle );
            TrackBarRenderer.DrawTopPointingThumb( e.Graphics, thumbRectangle, thumbState );
            TrackBarRenderer.DrawHorizontalTicks( e.Graphics, ticksRectangle, numberTicks, EdgeStyle.Raised );

            //using ( var g = e.Graphics )
            //{
            //    g.SmoothingMode = SmoothingMode.AntiAlias;
            //    g.TextRenderingHint = TextRenderingHint.AntiAlias;

            //    Rectangle rect = e.ClipRectangle;
            //    float cY = rect.Height / 2.0f - rect.Top;
            //    Pen pen = new Pen(Color.AliceBlue, 6);
            //    Brush brush = new SolidBrush( Color.AliceBlue );
            //    g.DrawLine( pen, rect.Left, cY - 3, rect.Right, cY - 3 );
            //    g.DrawRectangle( Pens.AliceBlue, 0, cY - 3, rect.Width, 6 );
            //    g.FillRectangle( brush, rect );
            //}
            e.Graphics.FillRectangle( new SolidBrush( Color.AliceBlue ), e.ClipRectangle );
            e.Graphics.DrawString( Text, Font, new SolidBrush( ForeColor ), ClientRectangle );
        }

        protected override void OnPaintBackground( PaintEventArgs e )
        {
            // If there is an image and it has a location, 
            // paint it when the Form is repainted.
            base.OnPaintBackground( e );
            e.Graphics.FillRectangle( new SolidBrush( Color.AliceBlue ), e.ClipRectangle );
            //using ( var g = e.Graphics )
            //{
            //    g.FillRectangle( new SolidBrush( Color.AliceBlue ), e.ClipRectangle );
            //}
        }
    }
}
