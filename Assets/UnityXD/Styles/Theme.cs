using System;
using System.Collections.Generic;
using System.Linq;
using Assets.UnityXD.Components;
using Assets.UnityXD.Core;
using UnityEngine;

namespace Assets.UnityXD.Styles
{
    [Serializable]
    public class Theme : Bindable
    {
        public Dictionary<string, FontClass> FontClassRegistry = new Dictionary<string, FontClass>();
        public Dictionary<string, Style> StyleRegistry = new Dictionary<string, Style>();

        public Theme()
        {
            LoadThemes( );
        }

        public void LoadThemes()
        {
            LoadFontStyles( );
            LoadStyles( );
        }

        private void LoadStyles()
        {
            // Default TileIcon.
            var style = new Style();
            style.Font = FetchFontClass( "Button2" );
            style.Background = ColorLibrary.Brand;
            style.Foreground = ColorLibrary.ChromeLightest;
            style.Padding = new RectOffset( 8, 8, 8, 8 );
            style.IsDefault = true;
            style.Size(64);
            style.StyleName = "TileIconDefault";
            AddStyle( style );

            style = new Style();
            style.Font = FetchFontClass( "Button2" );
            style.Background = ColorLibrary.Brand;
            style.Foreground = ColorLibrary.ChromeLightest;
            style.Padding = new RectOffset( 3, 3,3, 3 );
            style.Margin = new RectOffset(2,2,2,2);
            style.IconMargin = new RectOffset(8, 8, 8, 8);
            style.IsDefault = true;
            style.Size(64);
            style.StyleName = "SearchButton";
            AddStyle( style );

            // Default Body Style.
            style = new Style();
            style.Font = FetchFontClass( "Body1" );
            style.Font.alignment = TextAnchor.MiddleCenter;
            style.Font.AutoSize = true;
            style.Foreground = ColorLibrary.ChromeDarkest;
            style.IsDefault = true;
            style.StyleName = "Default";
            AddStyle( style );

            style = new Style();
            style.Font = FetchFontClass( "Title" );
            style.Foreground = ColorLibrary.Accent;
            style.StyleName = "Title";
            AddStyle( style );

            style = new Style();
            style.Size( 32 );
            style.Foreground = ColorLibrary.Chrome;
            style.StyleName = "Other";
            AddStyle(style);

        }

        private void LoadFontStyles()
        {
            // TODO: Offload this to some sort of external Resource Dictionary moment.

            var fontRegular = (Font)Resources.Load( "Fonts/Roboto-Regular" );
            var fontLight = (Font)Resources.Load( "Fonts/Roboto-Light" );
            var fontMedium = (Font)Resources.Load( "Fonts/Roboto-Medium" );
            var fontBold = (Font)Resources.Load( "Fonts/Roboto-Bold" );

            // TITLE.
            var fc = new FontClass
            {
                StyleName = "Title",
                font = fontRegular,
                bestFit = true,
                fontSize = 18,
                minSize = 18,
                maxSize = 24,                
            };
            AddFontClass( fc );

            fc = new FontClass
            {
                StyleName = "SubHeading",
                font = fontRegular,
                bestFit = true,
                fontSize = 18,
                minSize = 18,
                maxSize = 24
            };
            AddFontClass( fc );

            fc = new FontClass
            {
                StyleName = "Input",
                font = fontLight,
                bestFit = true,
                fontSize = 12,
                minSize = 12,
                maxSize = 24
            };
            AddFontClass( fc );


            fc = new FontClass
            {
                StyleName = "Button1",
                font = fontLight,
                bestFit = true,
                fontSize = 10,
                minSize = 10,
                maxSize = 24
            };
            AddFontClass( fc );

            fc = new FontClass
            {
                StyleName = "Button2",
                font = fontBold,
                bestFit = true,
                fontSize = 10,
                minSize = 10,
                maxSize = 24
            };
            AddFontClass( fc );


            fc = new FontClass
            {
                StyleName = "Body1",
                font = fontRegular,
                bestFit = true,
                fontSize = 12,
                minSize = 12,
                maxSize = 24
            };
            AddFontClass( fc );

            fc = new FontClass
            {
                StyleName = "Body2",
                font = fontBold,
                bestFit = true,
                fontSize = 12,
                minSize = 12,
                maxSize = 24
            };
            AddFontClass( fc );
        }

        #region Style Resolvers

        public List<Style> FetchStyles()
        {
            return StyleRegistry.Values.ToList( );
        }

        /// <summary>
        ///     Registers a Style against the master theme library.
        /// </summary>
        /// <param name="style"></param>
        public void AddStyle(Style style)
        {
            Debug.AssertFormat( style.StyleName != null, "Styles should be named..." );

            if (style.StyleName == string.Empty)
                return;

            var key = style.StyleName;
            StyleRegistry[key] = style;
        }

        public Style FetchStyle(string styleName)
        {
            var targetType = styleName;
            if (!StyleRegistry.ContainsKey( targetType ))
            {
                return null;
            }                                  
            return StyleRegistry[styleName];
        }

        #endregion

        #region FontClass Resolvers

        public void AddFontClass(FontClass fd)
        {
            Debug.AssertFormat( fd.StyleName != null, "Styles should be named..." );

            if (fd.StyleName == string.Empty)
                return;

            FontClassRegistry[fd.StyleName] = fd;
        }

        public FontClass FetchFontClass(string styleName)
        {
            FontClass result;
            FontClassRegistry.TryGetValue( styleName, out result );
            return result;
        }

        #endregion
    }
}