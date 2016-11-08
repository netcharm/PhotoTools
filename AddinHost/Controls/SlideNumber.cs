using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetCharm.Image.Addins.Controls
{
    [ToolboxBitmap( typeof( TrackBar ), "SlideNumber.bmp" )]
    [ToolboxItem( true )]
    public partial class SlideNumber : UserControl
    {
        [Category( "Behavior" )]
        public string Caption
        {
            get { return ( lblCaption.Text ); }
            set { lblCaption.Text = value; }
        }

        [Category( "Behavior" )]
        public string Unit
        {
            get { return ( lblUnit.Text ); }
            set { lblUnit.Text = value; }
        }

        [Category( "Behavior" )]
        public decimal Value
        {
            get { return ( edValue.Value ); }
            set
            {
                edValue.Value = value;
                slideValue.Value = Convert.ToInt32( (float) value * factor );
            }
        }

        [Category( "Behavior" )]
        public decimal Minimum
        {
            get { return ( edValue.Minimum ); }
            set
            {
                edValue.Minimum = value;
                slideValue.Minimum = Convert.ToInt32( (float) value * factor );
            }
        }

        [Category( "Behavior" )]
        public decimal Maximum
        {
            get { return ( edValue.Maximum ); }
            set
            {
                edValue.Maximum = value;
                slideValue.Maximum = Convert.ToInt32( (float) value * factor );
            }
        }

        [Category( "Behavior" )]
        public decimal Step
        {
            get { return ( edValue.Increment ); }
            set
            {
                edValue.Increment = value;
                slideValue.SmallChange = Convert.ToInt32( (float)value * factor );
                slideValue.TickFrequency = Convert.ToInt32( (float) value * factor );
            }
        }

        [Category( "Behavior" )]
        public int DecimalPlaces
        {
            get { return ( edValue.DecimalPlaces ); }
            set
            {
                edValue.DecimalPlaces = value;
                factor = Math.Pow( 10, value );
                slideValue.SmallChange = Convert.ToInt32( (float) edValue.Increment * factor );
                slideValue.TickFrequency = Convert.ToInt32( (float) edValue.Increment * factor );
                slideValue.Minimum = Convert.ToInt32( (float) edValue.Minimum * factor );
                slideValue.Maximum = Convert.ToInt32( (float) edValue.Maximum * factor );
                slideValue.Value = Convert.ToInt32( (float) edValue.Value * factor );
            }
        }
        private double factor = 1f;

        public event EventHandler ValueChanged;

        public SlideNumber()
        {
            InitializeComponent();

            //slideValue.DataBindings.Add( new Binding( "Maximum", edValue, "Maximum" ) );
            //slideValue.DataBindings.Add( new Binding( "Minimum", edValue, "Minimum" ) );
            //slideValue.DataBindings.Add( new Binding( "SmallChange", edValue, "Increment" ) );
            //slideValue.DataBindings.Add( new Binding( "TickFrequency", edValue, "Increment" ) );
            //slideValue.DataBindings.Add( new Binding( "Value", edValue, "Value" ) );

            //edValue.DataBindings.Add( new Binding( "Value", slideValue, "Value" ) );
        }

        private void ParamNumber_Load( object sender, EventArgs e )
        {
            edValue.ValueChanged += HandleValueChanged;
            slideValue.ValueChanged += HandleValueChanged;
        }

        private void HandleValueChanged( object sender, EventArgs e )
        {
            // we'll explain this in a minute
            this.OnValueChanged( EventArgs.Empty );
        }

        protected virtual void OnValueChanged( EventArgs e )
        {
            this.ValueChanged?.Invoke( this, e );
        }

        private void edValue_ValueChanged( object sender, EventArgs e )
        {
            slideValue.Value = Convert.ToInt32( (float) edValue.Value * factor );
        }

        private void slideValue_ValueChanged( object sender, EventArgs e )
        {
            edValue.Value = Convert.ToDecimal( slideValue.Value / factor );
        }
    }
}
