using System;

namespace UnityXD.Styles
{
    [Serializable]
    public enum XDColors
    {
        Uknown = 0,

        // BRAND
        BrandLightest = 0xDFEEE6,
        BrandLighter = 0xC0DCCE,
        BrandLight = 0xA1CBB4,
        Brand = 0x7ABA9A,
        BrandDark = 0x5D9D7D,
        BrandDarker = 0x3B7156,
        BrandDarkest = 0x2C513F,

        // CHROME
        ChromeLightest = 0xFFFFFF,
        ChromeLighter = 0xF2F2F2,
        ChromeLight = 0xDADADA,
        Chrome = 0x79797A,
        ChromeDark = 0x58595B,
        ChromeDarker = 0x474747,
        ChromeDarkest = 0x1B1B1B,

        // ACCENT COLOURS
        AccentLightest = 0xBFEAF8,
        AccentLighter = 0x77DDFF,
        AccentLight = 0x1CCEF7,
        Accent = 0x09ACEF,
        AccentDark = 0x1986B2,
        AccentDarker = 0x25627B,
        AccentDarkest = 0x204959,
    }
}