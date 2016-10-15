using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCharm.Image.Addins
{
    /// <summary>
    /// 
    /// </summary>
    public enum AddinType
    {
        Unknown = 0,
        App = 1,
        Action = 2,
        Filter = 3,
        FormatIn = 4,
        FormatOut = 5,
    }

    public enum AddinCommand
    {
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
        ZoomSelect = 306,
        ZoomLevel = 310,

        GetImage = 1001,
        SetImage = 1002,
        GetThumb = 1003,
        SetThumb = 1004,

        GetHost = 2001,
        SetHost = 2002,
        GetAddin = 2003,
        SetAddin = 2004,

        Show = 3001,
        Apply = 3002,
    }
}
