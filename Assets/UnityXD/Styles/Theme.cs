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
        public Dictionary<string, List<Style>> StyleRegistry = new Dictionary<string, List<Style>>();

        public Theme()
        {
            LoadThemes();
        }

        public void LoadThemes()
        {
            LoadFontStyles();
            LoadStyles();
        }

        private void LoadStyles()
        {
            // Default TileIcon.
            var style = new Style()
            {
                Font = FetchFontClass("Button2"),
                Background = ColorLibrary.Brand,
                Foreground = ColorLibrary.ChromeLightest,
                Padding = new RectOffset(8,8,8,8),
                IsDefault = true,
                StyleName = "TileIconDefault"        
            };
            AddStyle<TileIcon>(style);


            // Default Body Style.
            style = new Style()
            {
                Font = FetchFontClass("Body1"),
                Foreground = ColorLibrary.ChromeDarkest,
                IsDefault = true,
                StyleName = "Default"
            };
            AddStyle<Label>(style);

            style = new Style()
            {
                Font = FetchFontClass("Title"),
                Foreground = ColorLibrary.Accent,
                StyleName = "Title"
            };
            AddStyle<Label>(style);

        }

        private void LoadFontStyles()
        {
            // TODO: Offload this to some sort of external Resource Dictionary moment.

            var fontRegular = (Font) Resources.Load("Fonts/Roboto-Regular");
            var fontLight = (Font) Resources.Load("Fonts/Roboto-Light");
            var fontMedium = (Font) Resources.Load("Fonts/Roboto-Medium");
            var fontBold = (Font) Resources.Load("Fonts/Roboto-Bold");

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
            AddFontClass(fc);

            fc = new FontClass
            {
                StyleName = "SubHeading",
                font = fontRegular,
                bestFit = true,
                fontSize = 18,
                minSize = 18,
                maxSize = 24
            };
            AddFontClass(fc);

            fc = new FontClass
            {
                StyleName = "Input",
                font = fontLight,
                bestFit = true,
                fontSize = 12,
                minSize = 12,
                maxSize = 24
            };
            AddFontClass(fc);


            fc = new FontClass
            {
                StyleName = "Button1",
                font = fontLight,
                bestFit = true,
                fontSize = 10,
                minSize = 10,
                maxSize = 24
            };
            AddFontClass(fc);

            fc = new FontClass
            {
                StyleName = "Button2",
                font = fontBold,
                bestFit = true,
                fontSize = 10,
                minSize = 10,
                maxSize = 24
            };
            AddFontClass(fc);


            fc = new FontClass
            {
                StyleName = "Body1",
                font = fontRegular,
                bestFit = true,
                fontSize = 12,
                minSize = 12,
                maxSize = 24
            };
            AddFontClass(fc);

            fc = new FontClass
            {
                StyleName = "Body2",
                font = fontBold,
                bestFit = true,
                fontSize = 12,
                minSize = 12,
                maxSize = 24
            };
            AddFontClass(fc);
        }

        #region Style Resolvers

        public List<Style> FetchStyles<T>()
        {
            var targetType = typeof(T).Name;
            return StyleRegistry[targetType];
        }

        /// <summary>
        ///     Registers a Style against the master theme library.
        /// </summary>
        /// <param name="style"></param>
        public void AddStyle<T>(Style style)
        {
            Debug.AssertFormat(style.StyleName != null, "Styles should be named...");

            if (style.StyleName == string.Empty)
                return;

            var targetType = typeof (T).Name;

            // If there's no Style, then add some.
            if (!StyleRegistry.ContainsKey(targetType))
            {
                StyleRegistry[targetType] = new List<Style>();
            }

            // If Styles already exist, nuke em...to make way for this new one.
            StyleRegistry[targetType].RemoveAll(
                e => e.StyleName.Equals(style.StyleName, StringComparison.InvariantCultureIgnoreCase));
            StyleRegistry[targetType].Add(style);
        }

        public Style FetchStyle<T>(string styleName)
        {
            var targetType = typeof (T).Name;
            if (!StyleRegistry.ContainsKey(targetType))
            {
                return null;
            }

            // Search based on Name?
            var styleFound = StyleRegistry[targetType].FirstOrDefault(e => e.StyleName.Equals(styleName, StringComparison.InvariantCultureIgnoreCase));

            // If nothing found, try and return ones marked as "default" for that type (if there's more than one,
            // than the list ordering applies in picking which one wins.
            if (styleFound == null)
                StyleRegistry[targetType].FirstOrDefault(e => e.IsDefault);

            return styleFound;
        }

        #endregion

        #region FontClass Resolvers

        public void AddFontClass(FontClass fd)
        {
            Debug.AssertFormat(fd.StyleName != null, "Styles should be named...");

            if (fd.StyleName == string.Empty)
                return;

            FontClassRegistry[fd.StyleName] = fd;
        }

        public FontClass FetchFontClass(string styleName)
        {
            FontClass result;
            FontClassRegistry.TryGetValue(styleName, out result);
            return result;
        }

        #endregion
    }
}