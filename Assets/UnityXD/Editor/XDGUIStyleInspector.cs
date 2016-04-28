using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityXD.Components;
using UnityXD.Styles;

namespace UnityXD.XDGUIEditor
{
    public class XDGUIStyleInspector : XDGUIBaseInspector
    {
        private readonly Label _labelRef;
        private readonly UIComponent _componentRef;

        #region Constructors
        public XDGUIStyleInspector(ref Label labelRef)
        {
            _labelRef = labelRef;
            _componentRef = labelRef;
        }

        public XDGUIStyleInspector(ref UIComponent componentRef) 
        {
            _labelRef = componentRef.GetComponent<Label>();
            _componentRef = componentRef;
        }
        #endregion

        #region Creationists
        public static XDGUIStyleInspector Create(ref Label componentRef)
        {
            return new XDGUIStyleInspector(ref componentRef);
        }

        public static XDGUIStyleInspector Create(ref UIComponent componentRef)
        {
            return new XDGUIStyleInspector(ref componentRef);
        }
        #endregion

        public XDGUIStyleInspector Heading(string label)
        {
            EditorGUILayout.Space();

            XDGUI.Create().Text(label).Style(XDGUIStyles.Instance.Heading).Label();

            EditorGUILayout.Space();
            return this;
        }
        #region Core Styles
        public XDGUIStyleInspector FillColor()
        {
            XDGUI.Create().SwatchPicker(ref _componentRef.CurrentStyle.FrontFill, "FrontFill Fill");
            return this;
        }
        public XDGUIStyleInspector BackFillColor()
        {
            XDGUI.Create().SwatchPicker(ref _componentRef.CurrentStyle.BackFill, "Back Fill");
            return this;
        }

        public XDGUIStyleInspector BackgroundSprite(string label)
        {
            return Sprite(label, ref  _componentRef.BackgroundSprite);
        }

        public XDGUIStyleInspector Sprite(string label, ref Sprite spritefield)
        {
            XDGUI.Create()
                .Text(label)
                .Size(LabelMedium, FieldHeightSmall)
                .SpriteField(ref spritefield, true);

            EditorGUILayout.Space();
            XDGUI.Create()
                .Text("Aspect")
                .Size(LabelMedium, FieldHeightSmall, CheckBoxSize, CheckBoxSize)
                .CheckBox(ref _componentRef.PreserveAspect, false);

            return this;
        }
        #endregion

        #region Label Styling
        public XDGUIStyleInspector LabelText()
        {
            if (_labelRef == null)
                return this;

            XDGUI.Create()
                .Text("Text")
                .Size(LabelMedium, FieldHeightSmall)
                .TextField(ref _labelRef.Text, true);

            XDGUI.Create()
                .Text("AutoSize")
                .Size(LabelMedium, FieldHeightSmall, CheckBoxSize, CheckBoxSize)
                .CheckBox(ref _labelRef.AutoSize, true);

            EditorGUILayout.Space();

            XDGUI.Create()
                .Text("Truncate")
                .Size(LabelMedium, FieldHeightSmall, CheckBoxSize, CheckBoxSize)
                .CheckBox(ref _labelRef.TruncateToFit, true);
            EditorGUILayout.Space();
            return this;
        }

        public XDGUIStyleInspector FontAlignment()
        {
            if (_labelRef == null)
                return this;

            XDGUI.Create()
                .Text("Font Align")
                .Size(LabelMedium, FieldHeightSmall, FieldXXL)
                .ComboBox(ref _labelRef.Alignment, null, true);
            EditorGUILayout.Space();
            return this;
        }

        public XDGUIStyleInspector FontStyle()
        {
            if (_labelRef == null)
                return this;

            var filterSizes = new List<string>
            {
                XDFontSizes.L.ToString(),
                XDFontSizes.M.ToString(),
                XDFontSizes.S.ToString()
            };

            using (new XDGUIPanel(true))
            {
                XDGUI.Create()
                    .Text("Size")
                    .Size(LabelMedium, FieldHeightSmall, FieldSmall)
                    .ComboBox(ref _labelRef.CurrentStyle.FontStyle.FontSize, filterSizes, true);

                XDGUI.Create()
                    .Text("Style")
                    .Size(LabelSmall, FieldHeightSmall, FieldMedium)
                    .ComboBox(ref _labelRef.CurrentStyle.FontStyle.StyleName, null, true);
            }
            return this;
        }
        #endregion    

     
    }
}