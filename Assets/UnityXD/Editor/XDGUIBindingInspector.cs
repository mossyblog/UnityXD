using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityXD.Components;
using UnityXD.XDGUIEditor;
using Object = System.Object;

namespace UnityXD.XDGUIEditor
{
    public class XDGUIBindingInspector : XDGUIBaseInspector
    {
        #region Constructors
        public static XDGUIBindingInspector Create(ref UIComponent componentRef)
        {
            return new XDGUIBindingInspector(ref componentRef);
        }
        public XDGUIBindingInspector(ref UIComponent componentRef)
        {
            _componentRef = componentRef;
        }
        #endregion

        public XDGUIBindingInspector Heading(string label)
        {
            EditorGUILayout.Space();

            XDGUI.Create().Text(label).Style(XDGUIStyles.Instance.Heading).Label();

            EditorGUILayout.Space();
            return this;
        }

        public XDGUIBindingInspector References<T>(string label, ref T field)
        {
            var arg = field as UIComponent;

                XDGUI.Create()
                    .Text(label)
                    .Size(LabelMedium, FieldHeightSmall)
                    .XDField<T>(ref arg, true);
            field = (T)(Object)arg;
            return this;
        }

        public XDGUIBindingInspector BackgroundSprite(string label)
        {
            return Sprite(label, ref _componentRef.BackgroundSprite);
        }

        public XDGUIBindingInspector Sprite(string label, ref Sprite spritefield, bool showPreview = true)
        {
            XDGUI.Create()
                .Text(label)
                .Size(LabelMedium, FieldHeightSmall)
                .SpriteField(ref spritefield, true, showPreview);
            return this;
        }

        public XDGUIBindingInspector Aspect()
        {
            EditorGUILayout.Space();
            XDGUI.Create()
                .Text("Pres Aspect")
                .Size(LabelMedium, FieldHeightSmall, CheckBoxSize, CheckBoxSize)
                .CheckBox(ref _componentRef.PreserveAspect, false);
            return this;
        }
    }
}
