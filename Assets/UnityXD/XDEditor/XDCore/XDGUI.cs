using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDCore
{
    /// <summary>
    ///     Base XDGUI class. Creates and keeps track of the _root of the widget stack, which can then be used to add new
    ///     widgets.
    /// </summary>
    internal class XDGUI : XDLayout
    {
        public XDGUI() : base(null)
        {
        }

        /// <summary>
        ///     Updates the UI and processes events. Should be called in the unity OnGUI function.
        /// </summary>
        public override void OnGUI()
        {
            var pStyle = new GUIStyle();
            pStyle.padding = new RectOffset(4, 4, 4, 4);
            pStyle.margin = new RectOffset(4, 4, 4, 4);
            GUILayout.BeginVertical(pStyle);
            base.OnGUI();
            GUILayout.EndVertical();
        }
    }
}