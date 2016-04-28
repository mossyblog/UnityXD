using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityXD.Components;

namespace UnityXD.XDGUIEditor
{
    public class XDGUIBaseInspector
    {
        protected UIComponent _componentRef;
        protected GUIStyle PanelStyle = XDGUIStyles.Instance.Panel;
        protected int FieldSmall = 32;
        protected int FieldMedium = 64;
        protected int FieldLarge = 92;
        protected int FieldXXL = 128;
        protected int LabelSmall = 48;
        protected int LabelMedium = 92;
        protected int LabelLarge = 128;
        protected int FieldHeightSmall = 22;
        protected int FieldHeightMedium = 24;
        protected int FieldHeightLarge = 48;
        protected int CheckBoxSize = 16;

    }
}
