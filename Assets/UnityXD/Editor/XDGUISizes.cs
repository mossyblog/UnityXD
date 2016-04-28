using UnityEngine;

namespace UnityXD.Editor
{
    public enum XDGUISizes
    {
        Small = 24,
        Medium = 40,
        Large = 48
    }

    public static class XDGUISizeExtensions {

        public static Vector2 ForButton(this XDGUISizes size) {

            switch (size)
            {
                case XDGUISizes.Small:
                    return new Vector2(48,24);
                case XDGUISizes.Medium:                    
                    return new Vector2(64,24);
                case XDGUISizes.Large:
                    return new Vector2(92,24);
                default:
                    throw new System.ArgumentOutOfRangeException();
            }
        }
    }
}