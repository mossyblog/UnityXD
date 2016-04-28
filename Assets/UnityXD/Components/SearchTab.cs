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

        private const string CounterRefName = "Counter";

        public Label CounterRef;
        private int _counterTransformSize = 24;

        protected override void ValidateHeirachy()
        {
            base.ValidateHeirachy();
            if (CounterRef != null)
                ApplyChildNaming(CounterRef.gameObject, CounterRefName);
        }

        protected override void Measure()
        {
            base.Measure();


        }

        protected override void CommitProperties()
        {
            base.CommitProperties();
            if (CounterRef == null || LabelRef == null)
                return;

            IconPlacement = XDVerticalAlignment.Bottom;
            ImageRef.color = Color.clear;

            CounterRef.IsChildReadOnly = true;
            CounterRef.Alignment = TextAnchor.MiddleCenter;
            CounterRef.Text = Counter.ToString();
            CounterRef.CurrentStyle.FontStyle.StyleName = XDFontStyleNames.Label;
            CounterRef.CurrentStyle.FontStyle.FontSize = XDFontSizes.S;
            CounterRef.CurrentStyle.FrontFill = LabelRef.CurrentStyle.FrontFill;
            CounterRef.IgnoreParentPadding = true;
            CounterRef.CurrentStyle.Size = XDSizes.Custom;
            CounterRef.SetSize(24, 24);                        
            CounterRef.Dock(SpriteAlignment.BottomRight,false,false);

            
        }

        protected override void UpdateLayout()
        {
            base.UpdateLayout();

            if (CounterRef == null)
                return;

            IconRef.SetSize(24,24);
            IconRef.Dock(SpriteAlignment.Center,true,true);
            IconRef.SetMargin(new RectOffset(20, 20, 20, 20));

            if (Counter == 0)
            {
                
            }

            CounterRef.InvalidateDisplay();
            


        }
    }
}
