using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Resources;
using Mono.Addins;
using System.Diagnostics;
using System.Windows.Forms;

[assembly: AddinRoot( "AddinHost", "1.0" )]
namespace NetCharm.Image.Addins
{

    public class CommandPropertiesChangeEventArgs : EventArgs
    {
        public CommandPropertiesChangeEventArgs()
        {
            _cmd = AddinCommand.Unknown;
            _value = null;
        }
        public CommandPropertiesChangeEventArgs( AddinCommand command, ValueType property )
        {
            _cmd = command;
            _value = property;
        }
        
        private AddinCommand _cmd = AddinCommand.Unknown;
        public AddinCommand Command
        {
            get { return ( _cmd ); }
            private set { }
        }
        
        private object _value = null;
        public object Property
        {
            get { return ( _value ); }
            private set { }
        } // readonly
    }

    /// <summary>
    /// 
    /// </summary>
    [ToolboxBitmap( @"AddIn.ico" )]
    public class AddinHost : UserControl
    {
        #region Addin Host List Properties
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _apps = new Dictionary<string, IAddin>();
        [Browsable( false )]
        public Dictionary<string, IAddin> Apps
        {
            get { return _apps; }
            set { _apps = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _actions = new Dictionary<string, IAddin>();
        [Browsable( false )]
        public Dictionary<string, IAddin> Actions
        {
            get { return _actions; }
            set { _actions = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _filters = new Dictionary<string, IAddin>();
        [Browsable( false )]
        public Dictionary<string, IAddin> Filters
        {
            get { return _filters; }
            set { _filters = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _formatins = new Dictionary<string, IAddin>();
        [Browsable( false )]
        public Dictionary<string, IAddin> FormatIns
        {
            get { return _formatins; }
            set { _formatins = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _formatouts = new Dictionary<string, IAddin>();
        [Browsable( false )]
        public Dictionary<string, IAddin> FormatOuts
        {
            get { return _formatouts; }
            set { _formatouts = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, string> _notloadedaddin = new Dictionary<string, string>();
        [Browsable( false )]
        public Dictionary<string, string> NotLoadedAddin
        {
            get { return _notloadedaddin; }
            set { _notloadedaddin = value; }
        }
        #endregion

        #region Addin Host Properties
        /// <summary>
        /// 
        /// </summary>
        private string _rootdir = "";
        [Browsable( true ), Category( "Addin Host" ), DefaultValue( "" )]
        public string AddinRootDir
        {
            get { return _rootdir; }
            set { _rootdir = value; }
        }
        private string _configdir = "";
        [Browsable( true ), Category( "Addin Host" ), DefaultValue( "" )]
        public string AddinConfigDir
        {
            get { return _configdir; }
            set { _configdir = value; }
        }
        private string _addindir = "";
        [Browsable( true ), Category( "Addin Host" ), DefaultValue( "" )]
        public string AddinStorageDir
        {
            get { return _addindir; }
            set { _addindir = value; }
        }
        private string _databasedir = "";
        [Browsable( true ), Category( "Addin Host" ), DefaultValue( "" )]
        public string AddinDatabaseDir
        {
            get { return _databasedir; }
            set { _databasedir = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private IAddin _currentapp = null;
        [Browsable( false )]
        public IAddin CurrentApp
        {
            get { return _currentapp; }
            set { _currentapp = value; }
        }
        private IAddin _currentaction = null;
        [Browsable( false )]
        public IAddin CurrentAction
        {
            get { return _currentaction; }
            set { _currentaction = value; }
        }
        private IAddin _currentfilter = null;
        [Browsable( false )]
        public IAddin CurrentFilter
        {
            get { return _currentfilter; }
            set { _currentfilter = value; }
        }

        #endregion Addin Host Properties

        #region Addin Host Event
        public delegate void CommandPropertiesChangeHandle( object sender, CommandPropertiesChangeEventArgs e );
        public event CommandPropertiesChangeHandle CommandPropertiesChange;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnCommandPropertiesChange( CommandPropertiesChangeEventArgs e )
        {
            // Raise the event by using the () operator.
            CommandPropertiesChangeEventArgs enull = new CommandPropertiesChangeEventArgs(AddinCommand.Unknown, null);
            if ( e is CommandPropertiesChangeEventArgs )
                CommandPropertiesChange?.Invoke( this, e );
            else
                CommandPropertiesChange?.Invoke( this, enull );
        }

        #endregion Addin Host Event
        /// <summary>
        /// 
        /// </summary>
        private System.Drawing.Image _defaultLargeImage = Properties.Resources.AddIn_32x as Bitmap;
        public System.Drawing.Image LargeImage
        {
            get { return _defaultLargeImage; }
        }
        /// <summary>
        /// 
        /// </summary>
        private System.Drawing.Image _defaultSmallImage = Properties.Resources.AddIn_16x as Bitmap;
        public System.Drawing.Image SmallImage
        {
            get { return _defaultSmallImage; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void SetDir( string path )
        {
            if ( string.IsNullOrEmpty( path ) )
            {
                _rootdir = Path.GetDirectoryName( GetType().Module.FullyQualifiedName );
            }
            else
            {
                _rootdir = Path.GetFullPath( path );
            }

            _configdir = _rootdir;
            _addindir = Path.Combine( _rootdir, "addins" );
            _databasedir = Path.Combine( _rootdir, ".addinsdb" );
        }
        /// <summary>
        /// 
        /// </summary>
        public AddinHost()
        {
            SetDir( "" );
            Visible = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public AddinHost( string path = "" )
        {
            SetDir( path );
            Visible = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void Scan( string path = "" )
        {
            SetDir( path );

            AddinManager.Initialize( _configdir, _addindir, _databasedir );
            AddinManager.Registry.Update();

            _apps.Clear();
            _actions.Clear();
            _filters.Clear();

            foreach ( IAddin addin in AddinManager.GetExtensionObjects<IAddin>( true ) )
            {
                addin.Host = this;
                switch ( addin.Type )
                {
                    case AddinType.App:
                        if ( _apps.ContainsKey( addin.Name ) )
                            _notloadedaddin.Add( $"{addin.GUID}_{addin.Name}", $"Domain APP: {addin.Location}" );
                        else
                            _apps.Add( addin.Name, addin );
                        break;
                    case AddinType.Action:
                        if ( _actions.ContainsKey( addin.Name ) )
                            _notloadedaddin.Add( $"{addin.GUID}_{addin.Name}", $"Domain Action: {addin.Location}" );
                        else
                            _actions.Add( addin.Name, addin );
                        break;
                    case AddinType.Filter:
                        if ( _filters.ContainsKey( addin.Name ) )
                            _notloadedaddin.Add( $"{addin.GUID}_{addin.Name}", $"Domain Filter: {addin.Location}" );
                        else
                            _filters.Add( addin.Name, addin );
                        break;
                    case AddinType.FormatIn:
                        if ( _formatins.ContainsKey( addin.Name ) )
                            _notloadedaddin.Add( $"{addin.GUID}_{addin.Name}", $"Domain Format In: {addin.Location}" );
                        else
                            _formatins.Add( addin.Name, addin );
                        break;
                    case AddinType.FormatOut:
                        if ( _formatouts.ContainsKey( addin.Name ) )
                            _notloadedaddin.Add( $"{addin.GUID}_{addin.Name}", $"Domain Format Out: {addin.Location}" );
                        else
                            _formatouts.Add( addin.Name, addin );
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IAddin GetCurrentApp()
        {
            return ( CurrentApp );
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddinHost));
            this.SuspendLayout();
            // 
            // AddinHost
            // 
            this.Name = "AddinHost";
            resources.ApplyResources( this, "$this" );
            this.ResumeLayout( false );

        }

    }
}


