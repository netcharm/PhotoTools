using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace NetCharm.Image.Addins
{
    public enum OpaqueMode
    {
        Alpha = 0,
        TopLeft = 1,
        BottomRight = 2
    }

    public enum CropMode
    {
        Selection = 0,
        Opaque = 1,
        TopLeft = 2,
        BottomRight = 3,
        AspectRatio = 4
    }

    public class ImageInfo
    {
        public List<PropertyItem> EXIF;
        public Dictionary<string, string> IPTC;
        public System.Windows.Media.Imaging.BitmapMetadata Meta;
    }

    /// <summary>
    /// 
    /// </summary>
    public class CommandPropertiesChangeEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public CommandPropertiesChangeEventArgs()
        {
            _cmd = AddinCommand.Unknown;
            _value = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="property"></param>
        public CommandPropertiesChangeEventArgs( AddinCommand command, ValueType property )
        {
            _cmd = command;
            _value = property;
        }

        public CommandPropertiesChangeEventArgs( AddinCommand command, object property )
        {
            _cmd = command;
            _value = property;
        }

        /// <summary>
        /// 
        /// </summary>
        private AddinCommand _cmd = AddinCommand.Unknown;
        public AddinCommand Command
        {
            get { return ( _cmd ); }
            private set { }
        }

        /// <summary>
        /// 
        /// </summary>
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
    public enum AddinType
    {
        Unknown = 0,
        App = 1,
        Action = 2,
        Editor = 3,
        Effect = 4,
        FormatIn = 5,
        FormatOut = 6,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum AddinCommand
    {
        Unknown = 0,

        About = 10,
        Commands = 20,
        Log = 30,

        Open = 100,
        Save = 101,
        SaveAs = 102,
        Close = 103,

        Copy = 201,
        Cut = 202,
        Paste = 203,
        Clear = 204,

        Zoom = 301,
        ZoomIn = 302,
        ZoomOut = 303,
        Zoom100 = 304,
        ZoomFit = 305,
        ZoomRegion = 306,
        ZoomLevel = 310,

        GetImage = 1001,
        SetImage = 1002,
        GetThumb = 1003,
        SetThumb = 1004,
        GetImageName = 1101,
        GetImageSize = 1102,
        GetImageInfo = 1103,
        GetImageColors = 1104,
        GetImageSelection = 1201,
        SetImageSelection = 1202,

        GetHost = 2001,
        SetHost = 2002,
        GetAddin = 2003,
        SetAddin = 2004,

        GetParams = 2101,
        SetParams = 2102,

        Show = 3001,
        Apply = 3002,
        ApplyAll = 3003,

        Reset = 4001,
    }
}
