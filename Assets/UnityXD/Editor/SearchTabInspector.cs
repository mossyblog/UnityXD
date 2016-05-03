using System.Collections.Generic;
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
        protected Label _labelRef;

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

            new XDGUIInspector()
                .TextField(ref _labelRef.Text, "Label")
                .TextField(ref _SearchTabRef.Counter, "Counter")
                .Icon(ref _SearchTabRef.CurrentIcon, ref _SearchTabRef.IconPlacement)
                .CheckBox("Selected", ref _SearchTabRef.IsSelected)            
                .Swatch("Normal", ref _SearchTabRef.PositiveColor)
                .Swatch("Empty", ref _SearchTabRef.NegativeColor);
        }

        protected override void CreateLayoutControls()
        {
            base.CreateLayoutControls();

            new XDGUIInspector()
                .Context(ref _componentRef)
                .AnchorToolbar()
                .SizeAndPositioning()
                .Sizing("Size", ref _componentRef.CurrentStyle.Size);
        }

        protected override void CreateBindingControls()
        {
            base.CreateBindingControls();
            new XDGUIInspector()
                .Heading("Skins")
                .Sprite("Selected", ref _SearchTabRef.Selected)
                .Sprite("Normal", ref _SearchTabRef.Normal)
                .Sprite("Empty", ref _SearchTabRef.Empty)
                .Sprite("Disabled", ref _SearchTabRef.Disabled)
                .Heading("Local References")
                .Bind<Icon>("Icon", ref _SearchTabRef.IconRef)
                .Bind<Label>("Label", ref _SearchTabRef.LabelRef)
                .Bind<Label>("Counter", ref _SearchTabRef.CounterRef);
        }
    }
}