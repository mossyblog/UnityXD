﻿using System.Collections.Generic;
using Assets.UnityXD.Components;
using UnityEditor;
using UnityXD.Components;
using UnityXD.Editor;

namespace UnityXD.XDGUIEditor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof (SearchTab))]
    public class SearchTabInspector : UIComponentInspector
    {
        protected Label _counterRef;
        protected SearchTab _SearchTabRef;

        protected override void Initialize()
        {
            if (_SearchTabRef == null)
            {
                _SearchTabRef = target as SearchTab;
            }

            if (_counterRef == null)
            {
                _counterRef = _SearchTabRef.CounterRef;
            }

            if (_labelRef == null)
            {
                _labelRef = _SearchTabRef.LabelRef;
            }

            base.Initialize();
        }

        protected override void CreateDesignControls()
        {
            base.CreateDesignControls();

            var placementList = new List<string>
            {
                XDVerticalAlignment.Top.ToString(),
                XDVerticalAlignment.Bottom.ToString()
            };

            using (new XDGUIPanel(false, XDGUIStyles.Instance.Panel))
            {
                XDGUI.Create().Text("Icon").Size(64, 22, 92).ComboBox(ref _SearchTabRef.CurrentIcon, null, true);
                EditorGUILayout.Space();
                XDGUI.Create().Text("Icon Size").Size(64, 22, 92).ComboBox(ref _SearchTabRef.CurrentIconSize,  null, true);
            }


            if (_labelRef != null)
                XDGUIStyleInspector.Create(ref _labelRef)
                    .Heading("Labels")
                    .Label();

            XDGUI.Create()
                .Text("Counter")
                .Size(LabelMedium, FieldHeightSmall)
                .TextField(ref _SearchTabRef.Counter, true);


            XDGUI.Create().SwatchPicker(ref _SearchTabRef.PositiveColor, "Positive");            
            XDGUI.Create().SwatchPicker(ref _SearchTabRef.NegativeColor, "Negative");

            XDGUI.Create()
                .Text("Selected")
                .Size(LabelMedium, FieldHeightSmall,CheckBoxSize,CheckBoxSize)
                .CheckBox(ref _SearchTabRef.IsSelected, true);

            XDGUIStyleInspector.Create(ref _labelRef)
                .Heading("Font Settings")
                .FontSettings()
                .FontAlignment()
                .FontStyle();
        }


        protected override void CreateBindingControls()
        {
            base.CreateBindingControls();

            XDGUIBindingInspector.Create(ref _componentRef)
                .Heading("References")
                .References("Icon", ref _SearchTabRef.IconRef)
                .References("Label", ref _SearchTabRef.LabelRef)
                .References("Counter", ref _SearchTabRef.CounterRef)
                .Heading("States")
                .Aspect()
                .Sprite("Selected", ref _SearchTabRef.Selected, false)
                .Sprite("Normal", ref _SearchTabRef.Normal, false)
                .Sprite("Empty", ref _SearchTabRef.Empty, false)
                .Sprite("Disabled", ref _SearchTabRef.Disabled, false);
        }
    }
}