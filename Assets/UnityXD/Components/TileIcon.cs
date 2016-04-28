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
        public int CurrentIconSize;
        public XDVerticalAlignment IconPlacement;

        public Label LabelRef;

        public Icon IconRef;

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
            LabelRef.Dock(SpriteAlignment.Center, true, true);
            LabelRef.IsChildReadOnly = true;

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

            IconRef.CurrentStyle.Size = XDSizes.Custom;
            IconRef.SetSize(CurrentIconSize, CurrentIconSize);
            IconRef.SetMargin(new RectOffset(4, 4, 0, 8));
            LabelRef.SetMargin(new RectOffset(4, 4, 4, 4));

            LabelRef.InvalidateDisplay();
            IconRef.InvalidateDisplay();
            
        }

        protected override void Measure()
        {
            // Split by Thirds.            
            CurrentIconSize = Height/3*2 - 8;
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