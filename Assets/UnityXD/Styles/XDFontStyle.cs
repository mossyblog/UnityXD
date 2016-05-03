using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityXD.Styles
{
    [Serializable]
    public class XDFontStyle
    {
        public XDFontStyleNames StyleName;
        public FontData FontData;
      
        public XDFontStyle (XDFontStyleNames name, FontData data)
        {
            StyleName = name;
            FontData = data;
        }

        public XDFontStyle(XDFontStyle inbound) {
            StyleName = inbound.StyleName;
            FontData = inbound.FontData;
        }
    }
}