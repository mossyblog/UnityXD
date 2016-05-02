using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityXD.Styles
{
    [Serializable]
    public class XDFontStyle
    {
        public XDFontSizes FontSize;
        public XDFontStyleNames StyleName;
        public FontData FontData = new FontData();

        public XDFontStyle()
        {            
        }


        public XDFontStyle (XDFontStyle data)
        {
            StyleName = data.StyleName;            
            FontSize = data.FontSize;
            FontData = data.FontData;

        }

    }
}