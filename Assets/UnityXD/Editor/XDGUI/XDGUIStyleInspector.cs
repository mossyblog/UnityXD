using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityXD.Components;
using UnityXD.Styles;
using UnityXD.XDGUIEditor;

namespace UnityXD.XDGUIEditor
{
    public class XDGUIStyleInspector 
    {
        public GUIStyle PanelStyle = XDGUIStyles.Instance.Panel;
        private int FieldHeight = 22;
        private int FieldWidth = 72;
        private int LabelWidth = 72;
        private Label _componentRef;

        public static XDGUIStyleInspector Create(ref Label componentRef)
        {
            return new XDGUIStyleInspector(ref componentRef);
        }

        public XDGUIStyleInspector(ref Label componentRef)
        {
            _componentRef = componentRef;
        }

        public XDGUIStyleInspector DisplayHeading(string label = "Font Settings")
        {
            EditorGUILayout.Space();

            XDGUI.Create().Label(label).Style(XDGUIStyles.Instance.Heading).RenderLabel();

            EditorGUILayout.Space();
            return this;
        }
      
        public XDGUIStyleInspector DisplayText()
        {
            XDGUI.Create().Label("Text").Size(LabelWidth, FieldHeight).RenderTextField(ref _componentRef.Text, true);

            XDGUI.Create()
                              .Label("AutoSize")
                              .Size(LabelWidth, FieldHeight, 16, 16)
                              .RenderCheckBox(ref _componentRef.AutoSize, true);

            EditorGUILayout.Space();

            XDGUI.Create()
                .Label("Truncate")
                .Size(LabelWidth, FieldHeight, 16, 16)
                .RenderCheckBox(ref _componentRef.TruncateToFit, true);
            EditorGUILayout.Space();
            return this;
        }

        public XDGUIStyleInspector DisplayAlignment()
        {

            XDGUI.Create()
                      .Label("Alignment")
                      .Size(LabelWidth, FieldHeight, 128)
                      .RenderEnumField(ref _componentRef.Alignment, null, true);
            EditorGUILayout.Space();
            return this;
        }

        public XDGUIStyleInspector DisplayFontStyle()
        {
            var filterSizes = new List<string>
            {
                XDFontSizes.L.ToString(),
                XDFontSizes.M.ToString(),
                XDFontSizes.S.ToString()
            };

            using (new XDGUIPanel(true))
            {
                XDGUI.Create()
                    .Label("Size")
                    .Size(LabelWidth, FieldHeight, 48)
                    .RenderEnumField(ref _componentRef.CurrentStyle.FontStyle.FontSize, filterSizes, true);

                XDGUI.Create()
                    .Label("Style")
                    .Size(LabelWidth/2, FieldHeight, 64)
                    .RenderEnumField(ref _componentRef.CurrentStyle.FontStyle.StyleName, null, true);
            }
            return this;
        }
    }
}
