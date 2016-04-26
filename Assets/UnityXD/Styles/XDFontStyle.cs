using System;
using UnityEngine;

namespace UnityXD.Styles
{
    [Serializable]
    public class XDFontStyle
    {
        public XDFontSizes FontSize;
        public XDFontStyleNames StyleName;
        public Font Font;
        public int ActualFontSize;

        public XDFontStyle ()
        {			
        }

        public XDFontStyle (XDFontStyle data)
        {
            StyleName = data.StyleName;            
            FontSize = data.FontSize;
            Font = data.Font;
            ActualFontSize = data.ActualFontSize;		
        }

    }
}