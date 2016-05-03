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
    [CustomEditor(typeof(Label))]

    public class LabelInspector : UIComponentInspector
    {
        protected Label _labelRef;
       
        protected override void Initialize()
        {            
            if (_labelRef == null)
            {
                _labelRef = target as Label;
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
                .Margin();
        }
        protected override void CreateDesignControls()
        {

            base.CreateDesignControls();

            new XDGUIInspector()
                .Swatch("Fill Color", ref _componentRef.CurrentStyle.FrontFill)
                .TextField(ref _labelRef.Text, "Label")
                .LabelAlignment(ref _labelRef.Alignment, "Alignment");

//            XDGUIStyleInspector.Create(ref _labelRef)
//                .FillColor()
//                .Label()
//                .Heading("Font Settings")
//                .FontSettings()
//                .FontAlignment()
//                .FontStyle();


        }
    }
}
