using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UI;
using UnityEngine;

namespace UnityXD.Styles
{
    [Serializable]
	public class XDStyle
	{
        public XDSizes Size;
        public XDColors FrontFill;
        public XDColors BackFill;
        public XDFontStyle FontStyle;
        public RectOffset Padding;
        public RectOffset Margin;
        public XDThemeStyleNames StyleName;

        public XDStyle() {
            
        }


        public XDStyle(XDStyle inboundStyle) {

            if (inboundStyle.StyleName == XDThemeStyleNames.Unknown)
                return;
            
            Size = inboundStyle.Size;
            FrontFill = inboundStyle.FrontFill;
            BackFill = inboundStyle.BackFill;
            FontStyle = inboundStyle.FontStyle;
            Margin = inboundStyle.Margin;
            Padding = inboundStyle.Padding;
            StyleName = inboundStyle.StyleName;

        }
	}
}