using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityXD.Components;
using UnityXD.Editor;
using UnityXD.Styles;
using UnityXD.XDGUIEditor;

namespace UnityXD.XDGUIEditor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Icon))]
    public class IconInspector : UIComponentInspector
    {
        protected Icon _iconRef;

        protected override void Initialize()
        {
            if (_iconRef == null)
            {
                _iconRef = target as Icon;
            }

            base.Initialize();
        }

        protected override void CreateDesignControls()
        {
            base.CreateDesignControls();
            using (new XDGUIPanel(false, XDGUIStyles.Instance.Panel))
            {
                XDGUI.Create().Text("Icon").Size(64, 22, 92).ComboBox(ref _iconRef.CurrentIcon,  null, true);

                XDGUIStyleInspector.Create(ref _componentRef)
                    .FillColor();
            }
        }

    }
}
