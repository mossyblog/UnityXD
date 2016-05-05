using UnityEngine;

namespace Assets.UnityXD.Styles
{
    public static class ColorLibraryExtensions
    {

        public static Color ToColor(this ColorLibrary color, float alpha = 1f)
        {
            var hex = (int)color;
            var R = (byte)((hex >> 16) & 0xFF);
            var G = (byte)((hex >> 8) & 0xFF);
            var B = (byte)(hex & 0xFF);
            var result = (Color)new Color32(R, G, B, 255);
            result.a = alpha;
            return result;
        }

    }
}