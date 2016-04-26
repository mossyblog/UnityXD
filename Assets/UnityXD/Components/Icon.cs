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
    [Serializable]
    public class Icon : UIComponent
    {
        public XDIcons CurrentIcon = XDIcons.Cancel;
        public void SetIcon(XDIcons icon)
        {
            CurrentIcon = icon;
            InvalidateDisplay();
        }

        protected override void Measure()
        {
            Height = Width;
            IsHeightDependantOnWidth = true;
            base.Measure();
        }

        public override void InvalidateDisplay()
        {
            var sprite = CurrentIcon.ResolveSprite();

            if (sprite != null & ImageRef != null)
            {
                ImageRef.sprite = sprite;
                ImageRef.color = CurrentStyle.FrontFill.ToColor();
                ImageRef.preserveAspect = true;
            }
            base.InvalidateDisplay();
        }
    }
}
