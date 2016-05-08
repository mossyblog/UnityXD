using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Assets.UnityXD.Core;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{
    /// <summary>
    ///     Clickable push button widget.
    /// </summary>
    public interface IXDButton : IXDWidget
    {

        IEventBinding<IXDButton> Click { get; }

        IXDButton Clicked(Action action);

        IXDButton Decals(List<XDDecals> decals);
    }


    public class XDDecals
    {
        public string path;
        public Color tintColor { get; set; }
    }


    /// <summary>
    ///     Clickable push button widget.
    /// </summary>
    internal class XDButton : XDWidget, IXDButton
    {
        // Concrete property bindings
        private Action _clickAction;
        private bool _boolField;
        private List<XDDecals> decals;

        private EventBinding<IXDButton> clickEvent;
        public IEventBinding<IXDButton> Click { get { return clickEvent; } }

        internal XDButton(IXDLayout parent) : base(parent)
        {
            clickEvent = new EventBinding<IXDButton>(this);
        }

        public void Bind(object viewModel)
        {
            clickEvent.BindViewModel(viewModel);
        }

        public IXDButton Clicked(Action action)
        {
            _clickAction = action;
            return this;
        }

        public IXDButton Decals(List<XDDecals> value)
        {
            decals = value;

            return this;
        }

        public override void Render()
        {

            if (!OverrideStyle)
            {
                style = new GUIStyle(GUI.skin.button);
                style.alignment = TextAnchor.MiddleCenter;
            }

            if (GUILayout.Button(content.text, style, GetLayoutOptions().ToArray()))
            {
                clickEvent.Invoke();
                _clickAction.Invoke();
            }
            
            RenderDecals();
          
        }

        public virtual void RenderDecals()
        {
            var rect = GUILayoutUtility.GetLastRect();
            var currentColor = GUI.color;
            if (decals != null)
            {
                foreach (var decal in decals)
                {

                    var decalStyle = new GUIStyle();
                    if (Event.current.type != EventType.repaint)
                        return;

                    decalStyle.normal.background = Resources.Load<Sprite>(decal.path).texture;
                    GUI.color = decal.tintColor;
                    decalStyle.Draw(rect, GUIContent.none, false, false, false, false);
                    GUI.color = currentColor;
                }
            }
        }
    }

}