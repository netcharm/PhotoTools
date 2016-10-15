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

    public enum AddinMessage
    {
        Open = 0,
        Save = 1,
        SaveAs = 2,
        Close = 3,

        Copy = 4,
        Cut = 5,
        Paste = 6,
        Clear = 7,

        Zoom = 8,
        ZoomIn = 9,
        ZoomOut = 10,
        Zoom100 = 11,
        ZoomFit = 12,
        ZoomSelect = 13,
    }
}
