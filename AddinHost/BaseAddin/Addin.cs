using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using Accord.Imaging;
using Accord.Imaging.Filters;
using Mono.Addins;
using NGettext;
using NGettext.WinForm;

namespace NetCharm.Image.Addins
{
    /// <summary>
    /// 
    /// </summary>
    public class ParamItem : object
    {
        /// <summary>
        /// 
        /// </summary>
        private string _name = null;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayName = null;
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private Type _type = null;
        public Type Type
        {
            get { return _type; }
            set { _type = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private object _value = null;
        public Object Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [TypeExtensionPoint]
    public interface IAddin
    {
        AddinHost Host { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Guid GUID { get; }
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 
        /// </summary>
        string DisplayName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string GroupName { get; }
        /// <summary>
        /// 
        /// </summary>
        string DisplayGroupName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string Location { get; }
        /// <summary>
        /// 
        /// </summary>
        string Domain { get; }
        /// <summary>
        /// 
        /// </summary>
        AddinType Type { get; }
        /// <summary>
        /// 
        /// </summary>
        System.Drawing.Image LargeIcon { get; }
        /// <summary>
        /// 
        /// </summary>
        System.Drawing.Image SmallIcon { get; }        
       
        /// <summary>
        /// 
        /// </summary>
        string Author { get; }
        /// <summary>
        /// 
        /// </summary>
        string Copyright { get; }
        /// <summary>
        /// 
        /// </summary>
        string Version { get; }
        /// <summary>
        /// 
        /// </summary>
        System.Drawing.Image ImageData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        List<IAddin> Filters { get; }
        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, ParamItem> Params { get; }
        /// <summary>
        /// 
        /// </summary>
        void Show();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        void Show(Form parent, bool setup);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        System.Drawing.Image Apply( System.Drawing.Image image );

        /// <summary>
        /// 
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// 
        /// </summary>
        bool SupportMultiFile { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="result"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        bool Command( AddinCommand cmd, out object result, params object[] args );
    }

    /// <summary>
    /// 
    /// </summary>
    public class BaseAddinEffect : IAddin, IFilter, IInPlaceFilter, IInPlacePartialFilter, IFilterInformation
    {
        #region Private object
        private static ICatalog catalog = null;

        private FileVersionInfo fv = null;
        //private Form fm = null;

        protected internal System.Drawing.Image ImgSrc = null;
        protected internal System.Drawing.Image ImgDst = null;

        protected internal Form Parent = null;
        #endregion

        #region IAddin Implementation

        #region IAddin public object
        /// <summary>
        /// 
        /// </summary>
        public virtual AddinType Type
        {
            get { return AddinType.Effect; }
        }
        /// <summary>
        /// 
        /// </summary>
        private AddinHost _host = null;
        public AddinHost Host
        {
            get { return _host; }
            set { _host = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Location
        {
            get
            {
                return ( GetType().Module.FullyQualifiedName );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Domain
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                return ( Path.GetFileNameWithoutExtension( fv.InternalName ) );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual Guid GUID
        {
            get { return ( GetType().GUID ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Author
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                return ( fv.CompanyName );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Copyright
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                return ( fv.LegalCopyright );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Version
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                return ( fv.FileVersion );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name
        {
            get { throw new NotImplementedException(); }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string DisplayName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string GroupName
        {
            get { return ( string.Empty ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string DisplayGroupName
        {
            get { return ( _( GroupName ) ); }
            set { throw new NotImplementedException(); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _description = "It's a Addin";
        public virtual string Description
        {
            get { return ( _( _description ) ); }
            set { _description = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Drawing.Image LargeIcon
        {
            get
            {
                if ( Type == AddinType.App )
                    return ( Properties.Resources.Application_32x );
                else if ( Type == AddinType.Action )
                    return ( Properties.Resources.Action_32x );
                else if ( Type == AddinType.Effect )
                    return ( Properties.Resources.Effect_32x );
                else
                    return ( Properties.Resources.AddIn_32x );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Drawing.Image SmallIcon
        {
            get
            {
                if ( Type == AddinType.App )
                    return ( Properties.Resources.Application_16x );
                else if ( Type == AddinType.Action )
                    return ( Properties.Resources.Action_16x );
                else if ( Type == AddinType.Effect )
                    return ( Properties.Resources.Effect_16x );
                else
                    return ( Properties.Resources.AddIn_16x );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual System.Drawing.Image ImageData
        {
            get
            {
                if ( ImgDst is System.Drawing.Image ) return ( ImgDst );
                else return ( ImgSrc );
            }
            set { ImgSrc = value; ImgDst = value; }
            //set
            //{
            //    ImgDst = (System.Drawing.Image) value.Clone();
            //    ImgSrc = (System.Drawing.Image) value.Clone();
            //}
        }

        public virtual List<IAddin> Filters
        {
            get { return ( null ); }
        }
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, ParamItem> _params = new Dictionary<string, ParamItem>();
        public Dictionary<string, ParamItem> Params
        {
            get { return ( _params ); }
        }

        /// <summary>
        /// 
        /// </summary>
        protected internal bool _success = false;
        public virtual bool Success
        {
            get { return ( _success ); }
            protected internal set { _success = value; }
        }

        #endregion

        #region I18N private routines
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ICatalog GetCatalog()
        {
            string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( Location ) ), "locale" );
            I18N i10n = new I18N( Domain, addinRoot );
            catalog = i10n.Catalog;
            return ( catalog );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        protected internal static string T( string t )
        {
            if ( catalog is ICatalog ) return ( catalog.GetString( t ) );
            return ( t );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        protected internal string _( string t )
        {
            if ( catalog == null )
                catalog = GetCatalog();

            if ( catalog == null )
                return ( I18N._( t ) );
            else
                return ( I18N._( catalog, t ) );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        protected internal void Translate( Form form, ToolTip tooltip = null, object[] extra = null )
        {
            string addinRoot = Path.Combine( Path.GetDirectoryName( Path.GetFullPath( Location ) ), "locale" );

            I18N i10n = new I18N( Domain, addinRoot, form, tooltip, extra );
            catalog = i10n.Catalog;
        }

        #endregion

        #region IAddin public routines
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kvlist"></param>
        public virtual void InitParams( Dictionary<string, object> kvlist )
        {
            Params.Clear();
            foreach ( var item in kvlist )
            {
                Params.Add( item.Key, new ParamItem() );
                Params[item.Key].Name = item.Key;
                Params[item.Key].DisplayName = AddinUtils._( this, item.Key );
                Params[item.Key].Type = item.Value.GetType();
                Params[item.Key].Value = item.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="img"></param>
        protected virtual void SetParams( Form form, System.Drawing.Image img = null )
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        protected virtual void GetParams( Form form )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Show()
        {
            MessageBox.Show( "Calling Show() method", "Title", MessageBoxButtons.OK );
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void Show( Form parent = null, bool setup=false )
        {
            MessageBox.Show( "Calling Show(parentForm) method", "Title", MessageBoxButtons.OK );
            //if ( fm == null )
            //{
            //    fm = new Form( this );
            //    Translate( fm );
            //    fm.Text = DisplayName;

            //    if ( Params.ContainsKey( "Width" ) )
            //        fm.SetWidth( Params["Width"] );
            //    else if ( ImgSrc != null )
            //        fm.SetWidth( ImgSrc.Width );

            //    if ( Params.ContainsKey( "Height" ) )
            //        fm.SetHeight( Params["Height"] );
            //    else if ( ImgSrc != null )
            //        fm.SetHeight( ImgSrc.Height );
            //}
            //if ( fm.ShowDialog() == DialogResult.OK )
            //{
            //    if ( Params.ContainsKey( "Width" ) )
            //        Params["Width"] = fm.GetWidth();
            //    else
            //        Params.Add( "Width", fm.GetWidth() );

            //    if ( Params.ContainsKey( "Height" ) )
            //        Params["Height"] = fm.GetHeight();
            //    else
            //        Params.Add( "Height", fm.GetHeight() );

            //    ImgDst = Apply( ImgSrc );
            //}
            //else ImgDst = ImgSrc;
            //if ( fm != null )
            //{
            //    fm.Dispose();
            //    fm = null;
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public virtual System.Drawing.Image Apply( System.Drawing.Image image )
        {
            return ( image );
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _supportMultiFile = false;
        public virtual bool SupportMultiFile
        {
            get { return ( _supportMultiFile ); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public virtual void Open( string filename )
        {
            //
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filenames"></param>
        public virtual void Open( string[] filenames )
        {
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="result"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual bool Command( AddinCommand cmd, out object result, params object[] args )
        {
            result = null;
            switch ( cmd )
            {
                case AddinCommand.InitParams:
                    if ( args.Length > 0 )
                    {
                        InitParams( args[0] as Dictionary<string, object> );
                    }
                    break;
                case AddinCommand.GetParams:
                    result = Params;
                    break;
                case AddinCommand.SetParams:
                    if ( args.Length > 0 && args[0] is Dictionary<string, ParamItem> )
                    {
                        Params.Clear();
                        foreach (var kv in args[0] as Dictionary<string, ParamItem> )
                        {
                            Params[kv.Key] = kv.Value;
                        }                        
                    }
                    break;
            }
            return ( true );
        }

        #endregion IAddin public routines

        #endregion IAddin Implementation

        #region IFilter Implementation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public virtual Bitmap Apply( Bitmap image )
        {
            return ( Apply( image as System.Drawing.Image ) as Bitmap );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageData"></param>
        /// <returns></returns>
        public virtual Bitmap Apply( BitmapData imageData )
        {
            return ( Apply( UnmanagedImage.FromManagedImage( imageData ) ).ToManagedImage() );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public virtual UnmanagedImage Apply( UnmanagedImage image )
        {
            return ( UnmanagedImage.FromManagedImage( Apply( image.ToManagedImage() ) ) );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="destinationImage"></param>
        public virtual void Apply( UnmanagedImage sourceImage, UnmanagedImage destinationImage )
        {
            destinationImage = Apply( sourceImage );
        }
        #endregion

        #region IApplyInPlace Implementation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        public virtual void ApplyInPlace( Bitmap image )
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageData"></param>
        public virtual void ApplyInPlace( BitmapData imageData )
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        public virtual void ApplyInPlace( UnmanagedImage image )
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IInPlacePartialFilter Implementation
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="rect"></param>
        public virtual void ApplyInPlace( Bitmap image, Rectangle rect )
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageData"></param>
        /// <param name="rect"></param>
        public virtual void ApplyInPlace( BitmapData imageData, Rectangle rect )
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="rect"></param>
        public virtual void ApplyInPlace( UnmanagedImage image, Rectangle rect )
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IFilterInformation Implementation
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<PixelFormat, PixelFormat> _formatTranslations = new Dictionary<PixelFormat, PixelFormat>();
        public virtual Dictionary<PixelFormat, PixelFormat> FormatTranslations
        {
            get
            {
                if ( !( _formatTranslations is Dictionary<PixelFormat, PixelFormat> ) )
                {
                    _formatTranslations = new Dictionary<PixelFormat, PixelFormat>();
                }
                if ( _formatTranslations.Count == 0 )
                {
                    _formatTranslations.Add( PixelFormat.Format8bppIndexed, PixelFormat.Format8bppIndexed );
                    _formatTranslations.Add( PixelFormat.Format24bppRgb, PixelFormat.Format24bppRgb );
                    _formatTranslations.Add( PixelFormat.Format32bppArgb, PixelFormat.Format32bppArgb );
                }
                return ( _formatTranslations );
            }
        }

        #endregion

    }
}
