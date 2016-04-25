using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityXD.Components;
using UnityXD.Editor;
using UnityXD.Styles;

namespace Assets.UnityXD.Editor
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

        protected override void CommitProperties()
        {
            base.CommitProperties();
            CommitLabelProperties();
        }

        protected override void CreateDesignControls()
        {

            base.CreateDesignControls();
            CreateDesignLabelControls();
        }
    }
}
