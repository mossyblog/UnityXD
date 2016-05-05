using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDCore
{
    /// <summary>
    ///     Basic interface for all widgets.
    /// </summary>
    public interface IXDWidget
    {
        /// <summary>
        ///     Returns ths widget's parent layout.
        /// </summary>
        IXDLayout End();

        /// <summary>
        ///  Text displayed on mouse hover.
        /// </summary>
        IXDWidget Tooltip(string value);

        /// <summary>
        ///  Width of the widget in pixels. Default uses auto-layout.
        /// </summary>
        IXDWidget Width(int value);

        /// <summary>
        ///  Height of the widget in pixels. Default uses auto-layout.
        /// </summary>
        IXDWidget Height(int value);

        /// <summary>
        /// Enforces the GUI to use the internal style system.
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        IXDWidget Style(GUIStyle style);
    }
}