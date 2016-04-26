using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityXD.Components;
using UnityXD.Styles;

namespace UnityXD.Components
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
		public int CurrentIconSize;

		private const string labelRefName = "Label";
		private const string iconRefName = "Icon";

        public Label LabelRef
        {
			get { return _labelRef != null ? _labelRef : (_labelRef = GetOrCreateChild<Label>(labelRefName)); }
        }

        public Icon IconRef
        {
			get { return _iconRef != null ? _iconRef : (_iconRef = GetOrCreateChild<Icon>(iconRefName)); }
        }

        public override void InvalidateDisplay()
        {
            base.InvalidateDisplay();
        }


		protected override void ValidateHeirachy ()
		{
			base.ValidateHeirachy ();
			ApplyChildNaming (LabelRef.gameObject, labelRefName);
			ApplyChildNaming (IconRef.gameObject, iconRefName);
		}

        protected override void CommitProperties()
        {           
            base.CommitProperties();
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
            
			IconRef.IsChildReadOnly = true;
			IconRef.CurrentStyle.FrontFill = CurrentStyle.FrontFill;
            ImageRef.color = CurrentStyle.BackFill.ToColor();

        }

		protected override void UpdateLayout ()
		{
			ApplyTheme (CurrentStyle);

			base.UpdateLayout ();
			IconRef.CurrentStyle.Size = XDSizes.Custom;
			IconRef.SetSize(CurrentIconSize, CurrentIconSize);
			IconRef.SetMargin(new RectOffset(4,4,4,4));
			LabelRef.SetMargin(new RectOffset(4,4,4,4));
		}
        protected override void Measure()
        {
            // Split by Thirds.            
			CurrentIconSize = ((Height/3) * 2) - 8;
            base.Measure();
        }

		public override void ApplyTheme (XDStyle xd)
		{
			base.ApplyTheme (xd);
			LabelRef.ApplyTheme (CurrentStyle);
		}


        public void SetIcon(XDIcons icon)
        {
            CurrentIcon = icon;
            IconRef.SetIcon(icon);
        }


    }
}
