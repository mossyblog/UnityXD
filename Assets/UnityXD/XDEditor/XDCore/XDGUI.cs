using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDCore
{
    /// <summary>
    ///     Base XDGUI class. Creates and keeps track of the _root of the widget stack, which can then be used to add new
    ///     widgets.
    /// </summary>
    public class XDGUI : XDLayout
    {
        public XDGUI() : base(null)
        {
        }

        /// <summary>
        ///     Updates the UI and processes events. Should be called in the unity Render function.
        /// </summary>
        public override void Render()
        {
            var pStyle = new GUIStyle();
            pStyle.normal.background = CreateColoredTexture(background);
            pStyle.padding = new RectOffset(0,0,0,0);
            pStyle.margin = new RectOffset(0,0,0,0);

            using (new GUILayout.VerticalScope(pStyle))
            {
                base.Render();
            }
        }
    }
}