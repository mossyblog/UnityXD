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
        private Dictionary<string,XDStyle> m_styles = new Dictionary<string,XDStyle> ();

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
            m_FontClasses.Clear();
            m_styles.Clear();
			
            m_font_regular = (Font)Resources.Load ("Fonts/Roboto-Regular");
            m_font_light = (Font)Resources.Load ("Fonts/Roboto-Light");
            m_font_medium = (Font)Resources.Load ("Fonts/Roboto-Medium");
            m_font_bold = (Font)Resources.Load ("Fonts/Roboto-Bold");
	
            // TITLE.
            var fd =  new FontData();
            fd.font = m_font_regular;
            fd.bestFit = true;
            fd.minSize = 18;
            fd.maxSize = 24;
            RegisterFontClass (XDFontStyleNames.Title, fd);

            // Subheading
           
           
            fd = new FontData();
            fd.font = m_font_regular;
            fd.bestFit = true;
            fd.minSize = 12;
            fd.maxSize = 24;
            RegisterFontClass (XDFontStyleNames.SubTitle, fd);

            // INPUT          
            fd = new FontData();
            fd.font = m_font_light;
            fd.bestFit = true;
            fd.minSize = 12;
            fd.maxSize = 24;

            RegisterFontClass (XDFontStyleNames.Input, fd);

            // Label (passive etc).
            fd = new FontData();
            fd.font = m_font_light;
            fd.bestFit = true;
            fd.minSize = 12;
            fd.maxSize = 24;
            RegisterFontClass (XDFontStyleNames.Button1, fd);


            // TILEICON         
            fd = new FontData();
            fd.font = m_font_bold;
            fd.bestFit = true;
            fd.minSize = 10;
            fd.maxSize = 24;
            RegisterFontClass (XDFontStyleNames.Button2, fd);


            var themeStyle = new XDStyle();
            themeStyle.Size = XDSizes.L;
            themeStyle.FontStyle = new XDFontStyle(XDFontStyleNames.Button2, fd);
            themeStyle.BackFill = XDColors.Brand;
            themeStyle.FrontFill = XDColors.ChromeLightest;
            themeStyle.Padding = new RectOffset(8, 8, 8, 8);
            themeStyle.StyleName = XDThemeStyleNames.TileIcon;
            RegisterThemeStyle(themeStyle);

            // Label (passive etc).      
            fd = new FontData();
            fd.font = m_font_regular;
            fd.bestFit = true;
            fd.minSize = 12;
            fd.maxSize = 24;
            RegisterFontClass (XDFontStyleNames.Body1, fd);

            // Headers, Form Prefixes, etc.          
            fd = new FontData();
            fd.font = m_font_bold;
            fd.bestFit = true;
            fd.minSize = 12;
            fd.maxSize = 24;
            RegisterFontClass (XDFontStyleNames.Body2, fd);



        }


        public XDStyle ResolveThemeStyle(XDThemeStyleNames name) {
            var key = name.ToString();
            if (m_styles.ContainsKey(key))
            {
                return m_styles[key];
            }            
            return null;
        }

        public XDFontStyle ResolveFontStyle(XDFontStyleNames name) {
            var key = name.ToString();
            if (m_FontClasses.ContainsKey(key))
            {
                return m_FontClasses[key];
            }            
            return null;
        }

        /// <summary>
        /// Registers the font class.
        /// </summary>
        /// <param name="fontClass">Font class.</param>
        public void RegisterFontClass (XDFontStyleNames name, FontData fd)
        {
            var fs = new XDFontStyle (name,fd);          
            var key = name.ToString();
            m_FontClasses [key] = fs;
        }

        public void RegisterThemeStyle (XDStyle style)
        {
            var key = style.StyleName.ToString();
            m_styles [key] = style;
        }


    }
}