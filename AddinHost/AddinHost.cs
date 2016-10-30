using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mono.Addins;

[assembly: AddinRoot( "AddinHost", "1.0" )]
namespace NetCharm.Image.Addins
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxBitmap( @"AddIn.ico" )]
    //public class AddinHost : UserControl
    public class AddinHost : Component
    {
        #region Addin Host List Properties
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _apps = null;
        [Browsable( false )]
        public Dictionary<string, IAddin> Apps
        {
            get
            {
                if ( !( _apps is Dictionary<string, IAddin> ) )
                    _apps = new Dictionary<string, IAddin>();
                return _apps;
            }
            set { _apps = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _actions = null;
        [Browsable( false )]
        public Dictionary<string, IAddin> Actions
        {
            get
            {
                if ( !( _actions is Dictionary<string, IAddin> ) )
                    _actions = new Dictionary<string, IAddin>();
                return _actions;
            }
            set { _actions = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _editors = null;
        [Browsable( false )]
        public Dictionary<string, IAddin> Editors
        {
            get
            {
                if ( !( _editors is Dictionary<string, IAddin> ) )
                    _editors = new Dictionary<string, IAddin>();
                return _editors;
            }
            set { _editors = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _effects = null;
        [Browsable( false )]
        public Dictionary<string, IAddin> Effects
        {
            get
            {
                if ( !( _effects is Dictionary<string, IAddin> ) )
                    _effects = new Dictionary<string, IAddin>();
                return _effects;
            }
            set { _effects = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _formatins = null;
        [Browsable( false )]
        public Dictionary<string, IAddin> FormatIns
        {
            get
            {
                if ( !( _formatins is Dictionary<string, IAddin> ) )
                    _formatins = new Dictionary<string, IAddin>();
                return _formatins;
            }
            set { _formatins = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _formatouts = null;
        [Browsable( false )]
        public Dictionary<string, IAddin> FormatOuts
        {
            get
            {
                if( !(_formatouts is Dictionary<string, IAddin> ))
                    _formatouts = new Dictionary<string, IAddin>();
                return _formatouts;
            }
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
        [Browsable( true ), Category( "Addin Host" ), DefaultValue( @".\" )]
        public string AddinRootDir
        {
            get { return _rootdir; }
            set { _rootdir = value; }
        }
        private string _configdir = "";
        [Browsable( true ), Category( "Addin Host" ), DefaultValue( @".\" )]
        public string AddinConfigDir
        {
            get { return _configdir; }
            set { _configdir = value; }
        }
        private string _addindir = "";
        [Browsable( true ), Category( "Addin Host" ), DefaultValue( @".\" )]
        public string AddinStorageDir
        {
            get { return _addindir; }
            set { _addindir = value; }
        }
        private string _databasedir = "";
        [Browsable( true ), Category( "Addin Host" ), DefaultValue( @".\" )]
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

        private void InitAddinList()
        {
            if ( !( _apps is Dictionary<string, IAddin> ) )
                _apps = new Dictionary<string, IAddin>();
            else
                _apps.Clear();

            if ( !( _actions is Dictionary<string, IAddin> ) )
                _actions = new Dictionary<string, IAddin>();
            else
                _actions.Clear();

            if ( !( _editors is Dictionary<string, IAddin> ) )
                _editors = new Dictionary<string, IAddin>();
            else
                _editors.Clear();

            if ( !( _effects is Dictionary<string, IAddin> ) )
                _effects = new Dictionary<string, IAddin>();
            else
                _effects.Clear();

            if ( !( _formatins is Dictionary<string, IAddin> ) )
                _formatins = new Dictionary<string, IAddin>();
            else
                _formatins.Clear();

            if ( !( _formatouts is Dictionary<string, IAddin> ) )
                _formatouts = new Dictionary<string, IAddin>();
            else
                _formatouts.Clear();
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
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public AddinHost( string path = "" )
        {
            SetDir( path );
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

            InitAddinList();

            foreach ( IAddin addin in AddinManager.GetExtensionObjects<IAddin>( true ) )
            {
                addin.Host = this;
                switch ( addin.Type )
                {
                    case AddinType.App:
                        if ( _apps.ContainsKey( addin.Name ) )
                            _notloadedaddin.Add( $"{addin.GUID}_{addin.Name}", $" APP: {addin.Location}" );
                        else
                            _apps.Add( addin.Name, addin );
                        break;
                    case AddinType.Action:
                        if ( _actions.ContainsKey( addin.Name ) )
                            _notloadedaddin.Add( $"{addin.GUID}_{addin.Name}", $" Action: {addin.Location}" );
                        else
                            _actions.Add( addin.Name, addin );
                        break;
                    case AddinType.Editor:
                        if ( _editors.ContainsKey( addin.Name ) )
                            _notloadedaddin.Add( $"{addin.GUID}_{addin.Name}", $" Editor: {addin.Location}" );
                        else
                            _editors.Add( addin.Name, addin );
                        break;
                    case AddinType.Effect:
                        if ( _effects.ContainsKey( addin.Name ) )
                            _notloadedaddin.Add( $"{addin.GUID}_{addin.Name}", $" Effect: {addin.Location}" );
                        else
                            _effects.Add( addin.Name, addin );
                        break;
                    case AddinType.FormatIn:
                        if ( _formatins.ContainsKey( addin.Name ) )
                            _notloadedaddin.Add( $"{addin.GUID}_{addin.Name}", $" Format In: {addin.Location}" );
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
            // 
            // AddinHost
            // 
            //this.Name = "AddinHost";
            resources.ApplyResources( this, "$this" );
        }

    }
}


