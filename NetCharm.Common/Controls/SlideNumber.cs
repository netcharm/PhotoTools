using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetCharm.Common.Controls
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
                slideValue.Value = CalcSlideValue();

                if ( slideValue.Value < slideValue.Minimum ) slideValue.Value = slideValue.Minimum;
                if ( slideValue.Value > slideValue.Maximum ) slideValue.Value = slideValue.Maximum;
                if ( edValue.Value > edValue.Maximum ) edValue.Value = edValue.Maximum;
                if ( edValue.Value < edValue.Minimum ) edValue.Value = edValue.Minimum;
            }
        }

        [Category( "Behavior" )]
        public decimal Minimum
        {
            get { return ( edValue.Minimum ); }
            set
            {
                edValue.Minimum = value;
                slideValue.Value = CalcSlideValue();

                if ( slideValue.Value < slideValue.Minimum ) slideValue.Value = slideValue.Minimum;
                if ( slideValue.Value > slideValue.Maximum ) slideValue.Value = slideValue.Maximum;
                if ( edValue.Value > edValue.Maximum ) edValue.Value = edValue.Maximum;
                if ( edValue.Value < edValue.Minimum ) edValue.Value = edValue.Minimum;
            }
        }

        [Category( "Behavior" )]
        public decimal Maximum
        {
            get { return ( edValue.Maximum ); }
            set
            {
                edValue.Maximum = value;
                slideValue.Value = CalcSlideValue();
                if ( slideValue.Value < slideValue.Minimum ) slideValue.Value = slideValue.Minimum;
                if ( slideValue.Value > slideValue.Maximum ) slideValue.Value = slideValue.Maximum;
                if ( edValue.Value > edValue.Maximum ) edValue.Value = edValue.Maximum;
                if ( edValue.Value < edValue.Minimum ) edValue.Value = edValue.Minimum;
            }
        }

        [Category( "Behavior" )]
        public decimal Step
        {
            get { return ( edValue.Increment ); }
            set
            {
                edValue.Increment = value;
                slideValue.SmallChange = 1;
                slideValue.LargeChange = 5;
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
                //slideValue.SmallChange = Convert.ToInt32( (float) edValue.Increment * factor );
                //slideValue.LargeChange = Convert.ToInt32( (float) edValue.Increment * factor * 5 );
                //slideValue.Minimum = Convert.ToInt32( (float) edValue.Minimum * factor );
                //slideValue.Maximum = Convert.ToInt32( (float) edValue.Maximum * factor );
                slideValue.Value = CalcSlideValue();
            }
        }
        private double factor = 1f;

        public event EventHandler ValueChanged;

        protected internal float CalcSlideValue()
        {
            var v = Convert.ToDouble( edValue.Value - edValue.Minimum);
            var d = Convert.ToDouble( edValue.Maximum - edValue.Minimum ) / 100f;
            return ( (float) ( v / d ) );
        }

        protected internal Decimal CalcNumValue()
        {
            var v = slideValue.Value;
            var d = Convert.ToDouble( edValue.Maximum - edValue.Minimum ) / 100f;
            return ( Convert.ToDecimal( ( v * d + Convert.ToDouble( edValue.Minimum ) ) ) );
        }

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
            slideValue.Value = CalcSlideValue();
        }

        private void slideValue_ValueChanged( object sender, EventArgs e )
        {
            edValue.Value = CalcNumValue();
        }
    }
}
