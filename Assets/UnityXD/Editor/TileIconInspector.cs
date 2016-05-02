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
            new XDGUIInspector()
                .Icon(ref _tileIconRef.CurrentIcon, ref _tileIconRef.IconPlacement);

            if (_labelRef != null)
            {
                new XDGUIInspector()
                    .TextField(ref _tileIconRef.LabelRef.Text, "Label")
                    .Swatch("Foreground", ref _tileIconRef.CurrentStyle.FrontFill)
                    .Swatch("Background", ref _tileIconRef.CurrentStyle.BackFill);

            }
        }

        protected override void CreateBindingControls()
        {
            base.CreateBindingControls();

            new XDGUIInspector().Heading("Local References")
                .Bind<Icon>("Icon", ref _tileIconRef.IconRef)
                .Bind<Label>("Label", ref _tileIconRef.LabelRef);
        }
    }
}
