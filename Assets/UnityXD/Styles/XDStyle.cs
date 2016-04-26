using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UI;

namespace UnityXD.Styles
{
    [Serializable]
	public class XDStyle
	{
		public XDSizes Size = XDSizes.M;
		public XDColors FrontFill = XDColors.Chrome;
		public XDColors BackFill = XDColors.ChromeLightest;
		public XDFontStyle FontStyle = new XDFontStyle();
	}
}