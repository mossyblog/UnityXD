using System.Collections.Generic;
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
        ///     Text displayed on mouse hover.
        /// </summary>
        IXDWidget Tooltip(string value);

        /// <summary>
        ///     Width of the widget in pixels. Default uses auto-layout.
        /// </summary>
        IXDWidget Width(int value);

        /// <summary>
        ///     Height of the widget in pixels. Default uses auto-layout.
        /// </summary>
        IXDWidget Height(int value);

        /// <summary>
        ///     Enforces the GUI to use the internal style system.
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        IXDWidget Style(GUIStyle style);

        IXDWidget Content(string value);

        IXDWidget Content(Sprite value);

        IXDWidget Content(string value, Sprite sprite);

        List<GUILayoutOption> GetLayoutOptions();

        IXDWidget Background(Color color);

        IXDWidget Foreground(Color color);

        IXDWidget Padding(int amt);

        IXDWidget Padding(int left, int right, int top, int bottom );

        IXDWidget Margin(int amt);

        IXDWidget Margin(int left, int right, int top, int bottom);

        IXDWidget Size(int amt);

        IXDWidget LoadResource(string path);

    }
}

