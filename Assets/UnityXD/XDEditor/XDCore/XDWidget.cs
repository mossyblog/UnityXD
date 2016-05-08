using System.Collections.Generic;
using Assets.UnityXD.Core;
using UnityEditor.Sprites;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDCore
{
    /// <summary>
    ///     Abstract class that all other widgets must implement.
    /// </summary>
    public abstract class XDWidget : IXDWidget
    {
        protected readonly IXDLayout parent;
        protected GUIContent content = new GUIContent();
        protected int height = -1;
        protected GUIStyle style = new GUIStyle();
        protected string tooltip = string.Empty;
        protected int width = -1;
        protected RectOffset margin;
        protected RectOffset padding;
        protected Color foreground;
        protected Color background;
        protected bool OverrideStyle;

        protected XDWidget(IXDLayout parent)
        {
            this.parent = parent;
        }

        public IXDWidget Height(int value)
        {
            height = value;
            return this;
        }

        public IXDWidget Style(GUIStyle style)
        {
            this.style = style;
            OverrideStyle = true;
            return this;
        }

        public IXDWidget Content(string value)
        {
            content.text = value;
            return this;
        }

        public IXDWidget Content(Sprite value)
        {
            content.image = SpriteUtility.GetSpriteTexture(value, false);
            return this;
        }

        public IXDWidget Content(string value, Sprite sprite)
        {
            content.text = value;
            content.image = SpriteUtility.GetSpriteTexture(sprite, false);
            return this;
        }


        public List<GUILayoutOption> GetLayoutOptions()
        {
            var layoutOptions = new List<GUILayoutOption>();

            
            if (height > 0)
                layoutOptions.Add(GUILayout.Height(height));

            if (width > 0)
                layoutOptions.Add(GUILayout.Width(width));

            return layoutOptions;
        }

        public IXDLayout End()
        {
            return parent;
        }

        public IXDWidget Tooltip(string value)
        {
            content.tooltip = value;
            tooltip = value;
            return this;
        }

        public IXDWidget Width(int value)
        {
            width = value;
            return this;
        }

        public IXDWidget Background(Color color)
        {
            background = color;
            if (style != null) style.normal.background = CreateColoredTexture(color);
            return this;
        }

        public IXDWidget Foreground(Color color)
        {
            foreground = color;
            if (style != null) style.normal.textColor = color;
            return this;
        }

        public IXDWidget Padding(int amt)
        {
            return Padding(amt,amt,amt,amt);
        }

        public IXDWidget Padding(int left, int right, int top, int bottom)
        {            
            padding = new RectOffset(left, right, top, bottom);
            if (style != null) style.padding = padding;
            return this;

        }

        public IXDWidget Margin(int amt)
        {
            return Margin(amt, amt, amt, amt);
        }

        public IXDWidget Margin(int left, int right, int top, int bottom)
        {
            margin = new RectOffset(left, right, top, bottom);
            if (style != null) style.margin = margin;
            return this;
        }

        public virtual IXDWidget Size(int amt)
        {
            width = amt;
            height = amt;
            return this;
        }

        public IXDWidget LoadResource(string path)
        {
            Sprite sprite = Resources.Load<Sprite>(path);

            if (sprite == null)
                Debug.LogErrorFormat("Cannot find {0}" , path);
            else
                Content(sprite);
            return this;
        }


        public Texture2D CreateColoredTexture(Color col)
        {
            var width = 10;
            var height = 10;
            var pix = new Color[width*height];

            for (var i = 0; i < pix.Length; i++)
                pix[i] = col;

            var result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }

        public abstract void Render();
    }
}