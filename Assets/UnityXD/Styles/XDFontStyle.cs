using System;
using UnityEngine;

namespace UnityXD.Styles
{
    [Serializable]
    public class XDFontStyle : XDStyle
    {
        public XDFontSizes FontSize;
        public XDFontStyleNames StyleName;
        public Font Font;
        public int ActualFontSize;
        public bool IsManuallyGenerated;

        public XDFontStyle ()
        {			
        }

        public XDFontStyle (XDFontStyle data)
        {
            StyleName = data.StyleName;
            Size = data.Size;
            FontSize = data.FontSize;
            Font = data.Font;
            ActualFontSize = data.ActualFontSize;		
        }

    }
}