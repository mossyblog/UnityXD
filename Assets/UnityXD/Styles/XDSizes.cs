using System;

namespace UnityXD.Styles
{
    [Serializable]
    public enum XDSizes
    {
        XXX = 256,
        XXL = 128,
        XL = 96,
        L = 64,
        M = 48,
        S = 32,
        XS = 24,
        XXS = 16,
        XXXS = 8,
        Custom = 0
    }

    public static class XDSizesExtension {
     
        public static XDSizes Previous(this XDSizes size) {

            switch (size)
            {
                case XDSizes.XXX:
                    return XDSizes.XXL;

                case XDSizes.XXL:
                    return XDSizes.XL;
                case XDSizes.XL:
                    return XDSizes.L;
                case XDSizes.L:
                    return XDSizes.M;
                case XDSizes.M:
                    return XDSizes.S;
                case XDSizes.S:
                    return XDSizes.XS;
                case XDSizes.XS:
                    return XDSizes.XXS;
                case XDSizes.XXS:
                    return XDSizes.XXXS;
                case XDSizes.XXXS:
                    return XDSizes.XXXS;
                case XDSizes.Custom:
                    return XDSizes.Custom;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}