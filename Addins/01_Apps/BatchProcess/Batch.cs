using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Addins;
using NetCharm.Image.Addins;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using NetCharm.Image;

namespace BatchProcess
{
    [Serializable]
    public class BatchOption
    {
        public SaveOption SaveOption = new SaveOption();
        public Dictionary<string, Dictionary<string, ParamItem>> FilterParams = new Dictionary<string, Dictionary<string, ParamItem>>();
    }

    [Extension]
    [Serializable]
    public class Batch : IAddin
    {
        private FileVersionInfo fv = null;
        private BatchProcessForm fm = null;
        private Image img = null;
        private SaveOption option = new SaveOption();

        protected internal Form ParentForm = null;

        #region Properties Override
        /// <summary>
        /// 
        /// </summary>
        public AddinType Type
        {
            get
            {
                return AddinType.App;
            }
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
        public Guid GUID
        {
            get { return ( GetType().GUID ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Author
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
        public string Copyright
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
        public string Version
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
        public string Name
        {
            get
            {
                return ( "Batch" );
                //if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                //return ( fv.InternalName );
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayName = AddinUtils.T("Batch");
        public string DisplayName
        {
            get { return ( AddinUtils._( this, _displayName) ); }
            set { _displayName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CategoryName
        {
            get { return ( "Application" ); }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _displayGroupName = AddinUtils.T("Application");
        public string DisplayCategoryName
        {
            get { return ( AddinUtils._( this, _displayGroupName ) ); }
            set { _displayGroupName = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _description = AddinUtils.T("Batch Image Process");
        public string Description
        {
            get
            {
                if ( fv == null ) fv = FileVersionInfo.GetVersionInfo( Location );
                if ( string.IsNullOrEmpty( _description ) ) _description = fv.FileDescription;
                return ( AddinUtils._( this, _description) );
            }
            set { _description = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Image LargeIcon
        {
            get { return ( Properties.Resources.Batch_32x ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public Image SmallIcon
        {
            get { return ( Properties.Resources.Batch_16x ); }
        }
        /// <summary>
        /// 
        /// </summary>
        public Image ImageData
        {
            get
            {
                if ( fm is Form ) img = fm.ImageData;
                return ( img );
            }
            set
            {
                img = value;
                if ( fm is Form ) fm.ImageData = img;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private List<IAddin> _filters = new List<IAddin>();
        public List<IAddin> Filters
        {
            get { return ( _filters ); }
            internal set { _filters = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _enabled = true;
        public bool Enabled { get { return ( _enabled ); } set { _enabled = value; } }

        /// <summary>
        /// 
        /// </summary>
        private bool _visible = true;
        public bool Visible { get { return ( _visible ); } }

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, ParamItem> _params = new Dictionary<string, ParamItem>();
        public Dictionary<string, ParamItem> Params
        {
            get { return ( _params ); }
            set { _params = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _success = true;
        public bool Success
        {
            get { return ( _success ); }
        }
        #endregion

        #region Methods Override
        /// <summary>
        /// 
        /// </summary>
        public DialogResult Show()
        {
            //MessageBox.Show( "Calling Show() method", "Title", MessageBoxButtons.OK );
            return ( Show( ParentForm ) );
        }
        /// <summary>
        /// 
        /// </summary>
        public DialogResult Show( Form parent = null, bool setup = false )
        {
            ParentForm = parent;
            if ( fm == null )
            {
                fm = new BatchProcessForm( this );
                fm.Text = Description;
                fm.MdiParent = parent;
                fm.Size = parent.ClientSize;
                fm.WindowState = FormWindowState.Maximized;
                fm.Show();
            }
            else
            {
                fm.Activate();
                fm.Show();
            }
            return( DialogResult.OK );
        }

        /// <summary>
        /// 
        /// </summary>
        private bool _supportMultiFile = true;
        public bool SupportMultiFile
        {
            get { return ( _supportMultiFile ); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void Open( string filename )
        {
            if (fm is BatchProcessForm )
            {
                fm.AddFiles( filename );
                if ( Host.CurrentApp != this)
                {
                    Host.CurrentApp = this;
                    Host.CurrentApp.Show( ParentForm, false );
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filenames"></param>
        public void Open( string[] filenames )
        {
            if ( fm is BatchProcessForm )
            {
                fm.AddFiles( filenames );
                if ( Host.CurrentApp != this )
                {
                    Host.CurrentApp = this;
                    Host.CurrentApp.Show( ParentForm, false );
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public Image Apply( Image src )
        {
            var st = DateTime.Now.Ticks;
            MessageBox.Show( "Calling Apply() method", "Title", MessageBoxButtons.OK );
            //options = 

            #region Apply Filters
            foreach ( IAddin filter in Filters )
            {
                if ( filter.Enabled )
                {
                    //if ( options.FilterParams.ContainsKey( filter ) )
                    //{
                    //    filter.Params = options.FilterParams[filter];
                    //}
                    src = filter.Apply( src as Image ) as Bitmap;
                }
            }
            #endregion

            float tc = new TimeSpan( DateTime.Now.Ticks - st ).Seconds + new TimeSpan( DateTime.Now.Ticks - st ).Milliseconds / 1000f;
            Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.ApplyTiming, tc ) );
            return ( src );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool Apply( string file )
        {
            bool result = false;
            var st = DateTime.Now.Ticks;
            MessageBox.Show( "Calling Apply() method", "Title", MessageBoxButtons.OK );

            var src = AddinUtils.LoadImage(file);
            var dst = Apply(src);

            //var dstFile = file.Re
            //AddinUtils.SaveImage( dstFile, dst, option );

            float tc = new TimeSpan( DateTime.Now.Ticks - st ).Seconds + new TimeSpan( DateTime.Now.Ticks - st ).Milliseconds / 1000f;
            Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.ApplyTiming, tc ) );
            return ( result );
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public bool ApplyAll(string[] files)
        {
            bool result = false;
            var st = DateTime.Now.Ticks;

            MessageBox.Show( "Calling ApplyAll() method", "Title", MessageBoxButtons.OK );
            foreach(var f in files)
            {
                Apply( f );
            }
            float tc = new TimeSpan( DateTime.Now.Ticks - st ).Seconds + new TimeSpan( DateTime.Now.Ticks - st ).Milliseconds / 1000f;
            Host.OnCommandPropertiesChange( new CommandPropertiesChangeEventArgs( AddinCommand.ApplyTiming, tc ) );
            return ( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool Command( AddinCommand cmd, out object result, params object[] cmdArgs )
        {
            result = null;
            switch ( cmd )
            {
                case AddinCommand.Open:
                    if ( cmdArgs.Length > 0 && cmdArgs[0] is string )
                        Open( cmdArgs[0] as string );
                    else if ( cmdArgs.Length > 0 && cmdArgs[0] is string[] )
                        Open( cmdArgs[0] as string[] );
                    break;
                case AddinCommand.ZoomIn:
                case AddinCommand.ZoomOut:
                case AddinCommand.ZoomRegion:
                case AddinCommand.ZoomFit:
                case AddinCommand.Zoom100:
                case AddinCommand.ZoomLevel:
                    if ( fm is BatchProcessForm )
                        result = fm.Zoom( cmd );
                    break;
                case AddinCommand.GetImageSize:
                    if ( fm is BatchProcessForm && fm.ImageData is Image )
                        result = new Size( fm.ImageData.Width, fm.ImageData.Height );
                    break;
                case AddinCommand.GetImageColors:
                    if ( fm is BatchProcessForm && fm.ImageData is Image )
                        result = fm.ImageData.PixelFormat;
                    break;
                case AddinCommand.Apply:
                    if ( fm is BatchProcessForm )
                        Apply( fm.SelectedFile );
                    break;
                case AddinCommand.ApplyAll:
                    if( fm is BatchProcessForm )
                        ApplyAll( fm.SelectedFiles );
                    break;
                default:
                    break;
            }
            return ( true );
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void InitParams()
        {
            Dictionary<string, object> kv = new Dictionary<string, object>();
            kv.Add( "BatchOption", new BatchOption() );

            Params.Clear();
            foreach ( var item in kv )
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
        protected void GetParams( Form form )
        {
            if ( Params.Count == 0 ) InitParams();

            if ( form is Form && !form.IsDisposed )
            {
                var cfm = (form as BatchProcessForm);
                //Params["BatchOption"] = cfm.ParamOption;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="img"></param>
        protected void SetParams( Form form, System.Drawing.Image img = null )
        {
            if ( Params.Count == 0 ) InitParams();

            if ( form is Form && !form.IsDisposed )
            {
                var cfm = (form as BatchProcessForm);
                //cfm.ParamOption = Params["BatchOption"];
            }
        }
    }
}
