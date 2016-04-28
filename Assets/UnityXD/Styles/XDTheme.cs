using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityXD.Styles
{
    [Serializable]
    public class XDTheme
    {
        private Dictionary<string,XDFontStyle> m_FontClasses = new Dictionary<string,XDFontStyle> ();
        private List<XDStyle> m_styles = new List<XDStyle> ();
        private static XDTheme _xdTheme;
        private Font m_font_regular;
        private Font m_font_light;
        private Font m_font_medium;
        private Font m_font_bold;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static XDTheme Instance { 

            get { 
				
                if (_xdTheme == null) {
                    _xdTheme = new XDTheme ();
                }
				
                return _xdTheme;
            } 
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="XDTheme"/> class.
        /// </summary>
        public XDTheme ()
        {
            InitializeDefaultStyles ();
        }

        /// <summary>
        /// Initializes the default styles.
        /// </summary>
        public void InitializeDefaultStyles ()
        {
			
            m_font_regular = (Font)Resources.Load ("Fonts/Roboto-Regular");
            m_font_light = (Font)Resources.Load ("Fonts/Roboto-Light");
            m_font_medium = (Font)Resources.Load ("Fonts/Roboto-Medium");
            m_font_bold = (Font)Resources.Load ("Fonts/Roboto-Bold");
	
            // CAPTION
            RegisterFontClass (XDFontStyleNames.Caption, XDFontSizes.S, m_font_regular);
            RegisterFontClass (XDFontStyleNames.Caption, XDFontSizes.M, m_font_light);
            RegisterFontClass (XDFontStyleNames.Caption, XDFontSizes.L, m_font_light);

            // BODY
            RegisterFontClass (XDFontStyleNames.Body, XDFontSizes.S, m_font_regular, XDFontSizes.XS);
            RegisterFontClass (XDFontStyleNames.Body, XDFontSizes.M, m_font_regular, XDFontSizes.S);
            RegisterFontClass (XDFontStyleNames.Body, XDFontSizes.L, m_font_regular, XDFontSizes.M);

            // LABEL
            RegisterFontClass (XDFontStyleNames.Label, XDFontSizes.S, m_font_regular, XDFontSizes.XS);
            RegisterFontClass (XDFontStyleNames.Label, XDFontSizes.M, m_font_bold, XDFontSizes.S);
            RegisterFontClass (XDFontStyleNames.Label, XDFontSizes.L, m_font_bold, XDFontSizes.M);

            // INPUT
            RegisterFontClass (XDFontStyleNames.Input, XDFontSizes.S, m_font_regular);
            RegisterFontClass (XDFontStyleNames.Input, XDFontSizes.M, m_font_light);
            RegisterFontClass (XDFontStyleNames.Input, XDFontSizes.L, m_font_light);

            // HEADING
            RegisterFontClass (XDFontStyleNames.Heading, XDFontSizes.S, m_font_regular, XDFontSizes.L);
            RegisterFontClass (XDFontStyleNames.Heading, XDFontSizes.M, m_font_light, XDFontSizes.XL);
            RegisterFontClass (XDFontStyleNames.Heading, XDFontSizes.L, m_font_light, XDFontSizes.XXL);

            RegisterFontClass (XDFontStyleNames.Title, XDFontSizes.M, m_font_bold, XDFontSizes.L);
            RegisterFontClass (XDFontStyleNames.SubTitle, XDFontSizes.M, m_font_bold);


        }

        /// <summary>
        /// Registers the font class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="fontSize">Font size.</param>
        /// <param name="font">Font.</param>
        private void RegisterFontClass (XDFontStyleNames name, XDFontSizes fontSize, Font font)
        {
            RegisterFontClass (name, fontSize, font, (int)fontSize);
        }


        /// <summary>
        /// Registers the font class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="fontSize">Font size.</param>
        /// <param name="font">Font.</param>
        /// <param name="actualFontSize">Actual font size.</param>
        private void RegisterFontClass (XDFontStyleNames name, XDFontSizes fontSize, Font font, XDFontSizes actualFontSize)
        {
            RegisterFontClass (name, fontSize, font, (int)actualFontSize);
        }

        /// <summary>
        /// Registers the font class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="fontSize">Font size.</param>
        /// <param name="font">Font.</param>
        /// <param name="actualFontSize">Actual font size.</param>
        private void RegisterFontClass (XDFontStyleNames name, XDFontSizes fontSize, Font font, int actualFontSize)
        {
            var fd = new XDFontStyle ();
            fd.StyleName = name;
            fd.FontSize = fontSize;
            fd.FontData = new FontData();
            fd.FontData.font = font;
            fd.FontData.fontSize = actualFontSize;
            RegisterFontClass (fd);
        }

        /// <summary>
        /// Registers the font class.
        /// </summary>
        /// <param name="fontClass">Font class.</param>
        public void RegisterFontClass (XDFontStyle fontClass)
        {
            var key = fontClass.StyleName.ToString () + fontClass.FontSize.ToString ();
            m_FontClasses [key] = fontClass;
        }

        public XDFontStyle ResolveFontClass (XDFontStyleNames styleName, XDFontSizes FontSize)
        {

            // The Intended ThemeFontClasses style should be paired with size.
            var key = styleName.ToString () + FontSize.ToString ();

            if (m_FontClasses.ContainsKey (key)) {
                return m_FontClasses [key];
            } else {
                RegisterFontClass (styleName, FontSize, m_font_regular);
                return m_FontClasses [key];
            }
				
        }

    }
}