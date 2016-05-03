using System;
using UnityEngine;
using UnityEngine.UI;
using UnityXD.Styles;

namespace UnityXD.Components
{
    [ExecuteInEditMode]
    [RequireComponent(typeof (Image))]
    [RequireComponent(typeof (Button))]
    [Serializable]
    public class TileIcon : UIComponent
    {
        private const string LabelRefName = "Label";
        private const string IconRefName = "Icon";
        private Icon _iconRef;
        private Label _labelRef;
       
        public XDIcons CurrentIcon = XDIcons.Cancel;
        public XDSizes CurrentIconSize;
        public XDVerticalAlignment IconPlacement = XDVerticalAlignment.Center;
        public Label LabelRef;
        public Icon IconRef;
        public XDColors Foo;

        protected int LabelSize;
        protected int IconSize;
        private int ChildrenPadding;

        protected override void ValidateHeirachy()
        {
            base.ValidateHeirachy();
            if (LabelRef != null) ApplyChildNaming(LabelRef.gameObject, LabelRefName);
            if (IconRef != null) ApplyChildNaming(IconRef.gameObject, IconRefName);
        }

        protected override void CommitProperties()
        {
            base.CommitProperties();

            if (LabelRef == null || IconRef == null)
            {
                return;
            }



            LabelRef.CurrentStyle.FrontFill = CurrentStyle.FrontFill;
            LabelRef.IsChildReadOnly = false;
            LabelRef.IgnoreParentPadding = false;

            IconRef.CurrentIcon = CurrentIcon;
            IconRef.IsChildReadOnly = false;
            IconRef.CurrentStyle.FrontFill = CurrentStyle.FrontFill;
            IconRef.IgnoreParentPadding = false;

            ImageRef.color = CurrentStyle.BackFill.ToColor();
        }

        protected override void UpdateLayout()
        {            
            base.UpdateLayout();
            if (!(LabelRef != null & IconRef != null))
            {
                return;
            }

            LabelRef.SetSize(Width, LabelSize, true);
            LabelRef.Alignment = TextAnchor.MiddleCenter;
            IconRef.SetSize(IconSize, IconSize,true);

            switch (IconPlacement)
            {
                case XDVerticalAlignment.Top:
                    LabelRef.Dock(SpriteAlignment.BottomCenter, false, false);
                    IconRef.Y = -LabelSize/2;
                    IconRef.Dock(SpriteAlignment.Center, false, false);
                    break;

                case XDVerticalAlignment.Center:
                default:                   
                    LabelRef.Dock(SpriteAlignment.TopCenter, false, false);
                    IconRef.Dock(SpriteAlignment.Center, false, false);
                    break;
            }

            IconRef.InvalidateDisplay();
            LabelRef.InvalidateDisplay();
        }

        protected override void Measure()
        {
            // Split by Thirds.    
            //IconYOffset = -((Height / 3) - (int)CurrentIconSize/2 - (Padding.top + Padding.bottom));
            LabelSize = CurrentStyle.FontStyle.FontData.maxSize/2;
            var scaleFactor = (Height/2) / 24;
            IconSize = 24 * scaleFactor; 
            base.Measure();
        }

        public void AutoBind() {               
            LabelRef = GetComponentInChildren<Label>();
            IconRef = GetComponentInChildren<Icon>();
        }

       
        public override void ApplyTheme(XDStyle xd)
        {       
            base.ApplyTheme(xd);

            if (CurrentStyle.StyleName == XDThemeStyleNames.Unknown)
            {
                ApplyDefaultTheme(XDThemeStyleNames.TileIcon);
            }

            if (LabelRef != null)
                LabelRef.ApplyDefaultTheme(XDThemeStyleNames.TileIcon);

            if(IconRef != null) 
                IconRef.ApplyTheme(CurrentStyle);
        }

        public void SetIcon(XDIcons icon)
        {
            CurrentIcon = icon;
            IconRef.SetIcon(icon);
        }
    }
}