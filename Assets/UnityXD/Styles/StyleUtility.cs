using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.UnityXD.Styles
{
    public static class StyleUtility
    {
        public static Sprite ResolveSprite(this IconLibrary lib)
        {
            var pathStub = "icons/";
            var iconName = lib.ToString();
            var iconSprite = Resources.Load(pathStub + iconName, typeof(Sprite)) as Sprite;
            return iconSprite;
        }

        /// <summary>
        ///     Converts SpriteAlignment to Vector2.
        /// </summary>
        /// <returns>The to vector.</returns>
        /// <param name="posEnum">Position enum.</param>
        public static Vector2 ToVector(this SpriteAlignment posEnum)
        {
            switch (posEnum)
            {
                case SpriteAlignment.TopLeft:
                    return new Vector2(0.0f, 1.0f);
                case SpriteAlignment.LeftCenter:
                    return new Vector2(0.0f, 0.5f);
                case SpriteAlignment.BottomLeft:
                    return new Vector2(0.0f, 0.0f);

                case SpriteAlignment.TopCenter:
                    return new Vector2(0.5f, 1.0f);

                case SpriteAlignment.Center:
                    return new Vector2(0.5f, 0.5f);

                case SpriteAlignment.BottomCenter:
                    return new Vector2(0.5f, 0.0f);


                case SpriteAlignment.TopRight:
                    return new Vector2(1.0f, 1.0f);
                case SpriteAlignment.RightCenter:
                    return new Vector2(1.0f, 0.5f);
                case SpriteAlignment.BottomRight:
                    return new Vector2(1.0f, 0.0f);
                default:
                    return new Vector2(0.5f, 0.5f);
            }
        }

        public static TextAnchor ToTextAnchor(this SpriteAlignment align)
        {
            switch (align)
            {
                case SpriteAlignment.Center:
                    return TextAnchor.MiddleCenter;

                case SpriteAlignment.TopLeft:
                    return TextAnchor.UpperLeft;

                case SpriteAlignment.TopCenter:
                    return TextAnchor.UpperCenter;

                case SpriteAlignment.TopRight:
                    return TextAnchor.UpperRight;

                case SpriteAlignment.LeftCenter:
                    return TextAnchor.MiddleLeft;

                case SpriteAlignment.RightCenter:
                    return TextAnchor.MiddleRight;

                case SpriteAlignment.BottomLeft:
                    return TextAnchor.LowerLeft;

                case SpriteAlignment.BottomCenter:
                    return TextAnchor.LowerCenter;

                case SpriteAlignment.BottomRight:
                    return TextAnchor.LowerRight;

                case SpriteAlignment.Custom:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static SpriteAlignment ToAlignment(this TextAnchor align)
        {
            switch (align)
            {
                case TextAnchor.UpperLeft:
                    return SpriteAlignment.TopLeft;

                case TextAnchor.UpperCenter:
                    return SpriteAlignment.TopCenter;
                case TextAnchor.UpperRight:
                    return SpriteAlignment.TopRight;

                case TextAnchor.MiddleLeft:
                    return SpriteAlignment.LeftCenter;

                case TextAnchor.MiddleCenter:
                    return SpriteAlignment.Center;

                case TextAnchor.MiddleRight:
                    return SpriteAlignment.RightCenter;

                case TextAnchor.LowerLeft:
                    return SpriteAlignment.BottomLeft;

                case TextAnchor.LowerCenter:
                    return SpriteAlignment.BottomCenter;

                case TextAnchor.LowerRight:
                    return SpriteAlignment.BottomRight;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        ///     Converts Vector2 to SpriteAlignment
        /// </summary>
        /// <returns>The to alignment.</returns>
        /// <param name="pos">Position.</param>
        public static SpriteAlignment ToAlignment(Vector2 pos)
        {
            foreach (SpriteAlignment posEnum in Enum.GetValues(typeof(SpriteAlignment)))
            {
                if (ToVector(posEnum).Equals(pos))
                {
                    return posEnum;
                }
            }

            return SpriteAlignment.Custom;
        }

        public static XDVerticalAlignment ToVerticalAlignment(this SpriteAlignment align)
        {
            if (Regex.IsMatch(align.ToString(), "top", RegexOptions.IgnoreCase))
            {
                return XDVerticalAlignment.Top;
            }
            if (Regex.IsMatch(align.ToString(), "bottom", RegexOptions.IgnoreCase))
            {
                return XDVerticalAlignment.Bottom;
            }
            return XDVerticalAlignment.Center;
        }

        public static XDHorizontalAlignment ToHorizontalAlignment(this SpriteAlignment align)
        {
            if (Regex.IsMatch(align.ToString(), "left", RegexOptions.IgnoreCase))
            {
                return XDHorizontalAlignment.Left;
            }
            if (Regex.IsMatch(align.ToString(), "right", RegexOptions.IgnoreCase))
            {
                return XDHorizontalAlignment.Right;
            }
            return XDHorizontalAlignment.Center;
        }

        public static SpriteAlignment ToAlignment(XDHorizontalAlignment h, XDVerticalAlignment v)
        {
            if (h == XDHorizontalAlignment.Center && v == XDVerticalAlignment.Center)
            {
                return SpriteAlignment.Center;
            }

            if (h == XDHorizontalAlignment.Stretch || v == XDVerticalAlignment.Stretch)
            {
                return SpriteAlignment.Custom;
            }


            foreach (SpriteAlignment posEnum in Enum.GetValues(typeof(SpriteAlignment)))
            {
                var str = v + h.ToString();
                var str_backwards = h + v.ToString();

                if (string.Equals(posEnum.ToString(), str.ToLower(), StringComparison.CurrentCultureIgnoreCase))
                {
                    return posEnum;
                }

                if (string.Equals(posEnum.ToString(), str_backwards.ToLower(), StringComparison.CurrentCultureIgnoreCase))
                {
                    return posEnum;
                }
            }

            return SpriteAlignment.Custom;
        }
    }

    public enum XDVerticalAlignment
    {
        Top,
        Center,
        Bottom,
        Stretch
    }

    public enum XDHorizontalAlignment
    {
        Left,
        Center,
        Right,
        Stretch
    }
}