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
        protected Label _labelRef;

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

        protected override void CreateLayoutControls()
        {
            base.CreateLayoutControls();
            new XDGUIInspector()
                .Context(ref _componentRef)
                .AnchorToolbar()
                .SizeAndPositioning()
                .Sizing("Size", ref _componentRef.CurrentStyle.Size)
                .Margin()
                .Padding();
        }

        protected override void CreateDesignControls()
        {
            base.CreateDesignControls();

            var labelText = _labelRef == null ? String.Empty : _labelRef.Text;
            var resetTheme = false;
            var autobind = false;

            new XDGUIInspector()
                .Icon(ref _tileIconRef.CurrentIcon, ref _tileIconRef.IconPlacement)
                .TextField(ref labelText, "Label")
                .Swatch("Foreground", ref _tileIconRef.CurrentStyle.FrontFill)
                .Swatch("Background", ref _tileIconRef.CurrentStyle.BackFill)
                .Button("Reset Style", ref resetTheme, XDHorizontalAlignment.Left)
                .Button("AutoBind", ref autobind, XDHorizontalAlignment.Left);

            if (resetTheme)
            {
                _tileIconRef.ResetTheme();
            }

            if (autobind)
            {
                _tileIconRef.AutoBind();
            }

            if (_labelRef != null)
            {
                _labelRef.Text = labelText;
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
