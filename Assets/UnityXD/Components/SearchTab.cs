using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityXD.Components;
using UnityXD.Styles;

namespace Assets.UnityXD.Components
{
    public class SearchTab : TileIcon
    {
        public Sprite Empty;
        public Sprite Disabled;
        public Sprite Selected;
        public Sprite Normal;
        public int Counter;
        public bool IsSelected;
        private const string CounterRefName = "Counter";
        public Label CounterRef;
        public XDColors PositiveColor;
        public XDColors NegativeColor;

        protected override void ValidateHeirachy()
        {
            base.ValidateHeirachy();
            if (CounterRef != null)
                ApplyChildNaming(CounterRef.gameObject, CounterRefName);
        }

        protected override void Measure()
        {
            base.Measure();
            IconYOffset = -4;

        }

        protected override void CommitProperties()
        {
            base.CommitProperties();
            if (CounterRef == null || LabelRef == null)
                return;
            
            IconPlacement = XDVerticalAlignment.Center;
            ImageRef.color = Color.clear;
            ImageRef.sprite = Counter == 0 ? Empty : (IsSelected ? Selected : Normal);
            CounterRef.IsChildReadOnly = true;
            CounterRef.Alignment = TextAnchor.MiddleCenter;
            CounterRef.Text = Counter.ToString();
            CounterRef.CurrentStyle.FontStyle.StyleName = XDFontStyleNames.Label;
            CounterRef.CurrentStyle.FontStyle.FontSize = XDFontSizes.S;            
            CounterRef.IgnoreParentPadding = true;
            CounterRef.CurrentStyle.Size = XDSizes.Custom;
            CounterRef.CurrentStyle.FrontFill = Counter == 0 ? NegativeColor : (IsSelected ? XDColors.ChromeLightest : PositiveColor);

        }

        protected override void UpdateLayout()
        {
            base.UpdateLayout();

            if (CounterRef == null)
                return;
            ImageRef.color = Color.white;
            CounterRef.SetSize(24, 24);            
            CounterRef.Dock(SpriteAlignment.BottomRight, false, false);
            CounterRef.SetMargin(new RectOffset(0, 4, 4, 2));
            CounterRef.InvalidateDisplay();
        }
    }
}
