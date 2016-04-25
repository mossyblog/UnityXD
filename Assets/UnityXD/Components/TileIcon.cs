using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityXD.Components;
using UnityXD.Styles;

namespace Assets.UnityXD.Components
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    [Serializable]
    public class TileIcon : UIComponent
    {
        public XDIcons CurrentIcon = XDIcons.Cancel;
        private Label _labelRef;
        private Icon _iconRef;
        public XDVerticalAlignment IconPlacement;

        public Label LabelRef
        {
            get { return _labelRef != null ? _labelRef : (_labelRef = GetOrCreateChild<Label>(gameObject.name + "Label")); }
        }

        public Icon IconRef
        {
            get { return _iconRef != null ? _iconRef : (_iconRef = GetOrCreateChild<Icon>(gameObject.name + "Icon")); }
        }

        public override void InvalidateDisplay()
        {
            base.InvalidateDisplay();
        }

        protected override void CommitProperties()
        {           
            base.CommitProperties();
            LabelRef.CurrentStyle.FrontFill = CurrentStyle.FrontFill;

            LabelRef.Dock(SpriteAlignment.Center, true, true);
            if (IconPlacement == XDVerticalAlignment.Top)
            {
                LabelRef.Alignment = TextAnchor.LowerCenter;
                IconRef.Dock(SpriteAlignment.TopCenter, false, false);
            }
            else
            {                
                
                LabelRef.Alignment = TextAnchor.UpperCenter;
                IconRef.Dock(SpriteAlignment.BottomCenter, false, false);
            }
            IconRef.CurrentStyle.FrontFill = CurrentStyle.FrontFill;

            ImageRef.color = CurrentStyle.BackFill.ToColor();
        }

        protected override void Measure()
        {

            // Split by Thirds.
            
            var iconSize = ((Height/3) * 2) - 8;

            IconRef.CurrentStyle.Size = XDSizes.Custom;
            IconRef.SetSize(iconSize, iconSize);
            IconRef.SetMargin(new RectOffset(4,4,4,4));
            LabelRef.SetMargin(new RectOffset(4,4,4,4));
            base.Measure();
        }

        public void SetIcon(XDIcons icon)
        {
            CurrentIcon = icon;
            IconRef.SetIcon(icon);
        }
    }
}
