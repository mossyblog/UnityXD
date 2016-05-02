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

        protected int IconYOffset;
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
            LabelRef.IsChildReadOnly = true;
            LabelRef.IgnoreParentPadding = false;

            IconRef.CurrentIcon = CurrentIcon;
            IconRef.IsChildReadOnly = true;
            IconRef.CurrentStyle.FrontFill = CurrentStyle.FrontFill;
            ImageRef.color = CurrentStyle.BackFill.ToColor();



        }

        protected override void UpdateLayout()
        {
            ApplyTheme(CurrentStyle);

            base.UpdateLayout();

            if (!(LabelRef != null & IconRef != null))
            {
                return;
            }
            switch (IconPlacement)
            {
                case XDVerticalAlignment.Top:
                    LabelRef.Alignment = TextAnchor.LowerCenter;
                    IconRef.Dock(SpriteAlignment.TopCenter, false, false);
                    break;
                case XDVerticalAlignment.Center:
                    LabelRef.Alignment = TextAnchor.UpperCenter;

                    IconRef.Dock(SpriteAlignment.Center, false, false);
                    break;
                default:
                    LabelRef.Alignment = TextAnchor.UpperCenter;
                    IconRef.Dock(SpriteAlignment.BottomCenter, false, false);
                    break;
            }

            IconRef.CurrentStyle.Size = XDSizes.Custom;
            IconRef.SetSize(CurrentIconSize);           
            IconRef.Y = IconYOffset;


            LabelRef.Dock(SpriteAlignment.Center, true, true);

            LabelRef.InvalidateDisplay();
            IconRef.InvalidateDisplay();
            
        }

        protected override void Measure()
        {
            // Split by Thirds.    
            IconYOffset = -((Height / 3) - (int)CurrentIconSize/2 - (Padding.top + Padding.bottom));

            base.Measure();
        }

        public override void ApplyTheme(XDStyle xd)
        {
            base.ApplyTheme(xd);
            if (LabelRef != null)
                LabelRef.ApplyTheme(CurrentStyle);

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