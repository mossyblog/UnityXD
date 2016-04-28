﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityXD.Components;
using UnityXD.Editor;
using UnityXD.Styles;
using UnityXD.XDGUIEditor;

namespace UnityXD.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Label))]

    public class LabelInspector : UIComponentInspector
    {
       
        protected override void Initialize()
        {            
            if (_labelRef == null)
            {
                _labelRef = target as Label;
            }
            base.Initialize();
        }

        protected override void CreateDesignControls()
        {

            base.CreateDesignControls();

            XDGUIStyleInspector.Create(ref _labelRef)
                .DisplayHeading()
                .DisplayText()
                .DisplayAlignment()
                .DisplayFontStyle();


        }
    }
}
