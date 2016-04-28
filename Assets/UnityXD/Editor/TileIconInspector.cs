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
    [CustomEditor(typeof(TileIcon))]
    public class TileIconInspector : UIComponentInspector
    {
        protected TileIcon _tileIconRef;

        protected override void Initialize()
        {
            if (_tileIconRef == null)
            {
                _tileIconRef = target as TileIcon;
            }

            if (_labelRef == null)
            {
                _labelRef = _tileIconRef.LabelRef as Label;
            }

            base.Initialize();
        }

        protected override void CreateDesignControls()
        {
            base.CreateDesignControls();

            var placementList = new List<String>
            {
                XDVerticalAlignment.Top.ToString(),
                XDVerticalAlignment.Bottom.ToString()
            };

            using (new XDGUIPanel(false, groupStyle))
            {
                XDGUI.Create().Text("Icon").Size(64, 22, 92).ComboBox(ref _tileIconRef.CurrentIcon,  null, true);
                EditorGUILayout.Space();
                XDGUI.Create().Text("Placement").Size(64, 22, 64).ComboBox(ref _tileIconRef.IconPlacement, placementList, true);
            }


            XDGUIStyleInspector.Create(ref _labelRef)
                .FillColor()
                .BackFillColor()            
                .Heading("Background")    
                .Heading("Font Settings")
                .LabelText()
                .FontAlignment()
                .FontStyle();

        }
    }
}
