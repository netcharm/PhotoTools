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

    /// <summary>
    /// 
    /// </summary>
    public class AddinHost : UserControl
    {
        #region addin list properties
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _apps = new Dictionary<string, IAddin>();
        public Dictionary<string, IAddin> Apps
        {
            get { return _apps; }
            set { _apps = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _actions = new Dictionary<string, IAddin>();
        public Dictionary<string, IAddin> Actions
        {
            get { return _actions; }
            set { _actions = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _filters = new Dictionary<string, IAddin>();
        public Dictionary<string, IAddin> Filters
        {
            get { return _filters; }
            set { _filters = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _formatins = new Dictionary<string, IAddin>();
        public Dictionary<string, IAddin> FormatIns
        {
            get { return _formatins; }
            set { _formatins = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, IAddin> _formatouts = new Dictionary<string, IAddin>();
        public Dictionary<string, IAddin> FormatOuts
        {
            get { return _formatouts; }
            set { _formatouts = value; }
        }
        #endregion

        private string RootDir = "";

        private string ConfigDir = "";
        private string AddinDir = "";
        private string DatabaseDir = "";

        private IAddin _currentapp = null;
        public IAddin CurrentApp
        {
            get { return _currentapp; }
            set { _currentapp = value; }
        }
        private IAddin _currentaction = null;
        public IAddin CurrentAction
        {
            get { return _currentaction; }
            set { _currentaction = value; }
        }
        private IAddin _currentfilter = null;
        public IAddin CurrentFilter
        {
            get { return _currentfilter; }
            set { _currentfilter = value; }
        }

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

        private void SetDir(string path )
        {
            if ( string.IsNullOrEmpty( path ) )
            {
                RootDir = Path.GetDirectoryName( GetType().Module.FullyQualifiedName );
            }
            else
            {
                RootDir = Path.GetFullPath( path );
            }

            ConfigDir = RootDir;
            AddinDir = Path.Combine( RootDir, "addins" );
            DatabaseDir = Path.Combine( RootDir, ".addinsdb" );
        }

        public AddinHost( string path = "" )
        {
            SetDir( path );
        }

        public void Scan( string path = "" )
        {
            SetDir( path );

            AddinManager.Initialize( ConfigDir, AddinDir, DatabaseDir );
            AddinManager.Registry.Update();

            foreach ( IAddin addin in AddinManager.GetExtensionObjects<IAddin>( true ) )
            {
                addin.Host = this;
                switch ( addin.Type )
                {
                    case AddinType.App: _apps.Add( addin.Name, addin ); break;
                    case AddinType.Action: _actions.Add( addin.Name, addin ); break;
                    case AddinType.Filter: _filters.Add( addin.Name, addin ); break;
                    case AddinType.FormatIn: _formatins.Add( addin.Name, addin ); break;
                    case AddinType.FormatOut: _formatouts.Add( addin.Name, addin ); break;
                }
            }
        }

        public IAddin GetCurrentApp()
        {
            return(CurrentApp);
        }
    }
}


