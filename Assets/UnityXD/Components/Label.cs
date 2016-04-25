using System;
using UnityEngine;
using UnityEngine.UI;
using UnityXD.Styles;

namespace UnityXD.Components
{
    [RequireComponent(typeof(Text))]
    [ExecuteInEditMode]
    public class Label : UIComponent
    {
        public XDFontStyle FontClassData = new XDFontStyle();
        public bool TruncateToFit;
        public bool AutoSize;
        public TextAnchor Alignment;
        public string Text;
        public override void Start()
        {
            base.Start();
            InvalidateDisplay();
        }

        public override void SetSize(int w, int h)
        {
            if (AutoSize)
            {
                w = (int)TextRef.preferredWidth;
                h = (int)TextRef.preferredHeight;
            }
            else {
                Math.Max((int)TextRef.preferredHeight, h);
            }
            base.SetSize(w, h);
        }

        public override void InvalidateDisplay()
        {
            base.InvalidateDisplay();

           

            ApplyElipseSuffix();
        }

        protected override void Measure()
        {
            Height = Math.Max(Height, (int)TextRef.preferredHeight);
        }
        
        protected override void CommitProperties()
        {
            if (AutoSize)
            {
                CurrentStyle.Size = XDSizes.Custom;
                IsHeightDependantOnWidth = false;
            }
          

            TextRef.text = Text;
            TextRef.font = FontClassData.Font;
            TextRef.fontSize = FontClassData.ActualFontSize;
            TextRef.color = CurrentStyle.FrontFill.ToColor();
            TextRef.alignment = Alignment;

            if (AutoSize)
            {
                TextRef.horizontalOverflow = IsHorizontalStretchEnabled ? HorizontalWrapMode.Wrap : HorizontalWrapMode.Overflow;
                TextRef.verticalOverflow = IsVeritcalStretchEnabled ? VerticalWrapMode.Truncate : VerticalWrapMode.Overflow;
            }
            else {
                TextRef.horizontalOverflow = HorizontalWrapMode.Wrap;
                TextRef.verticalOverflow = VerticalWrapMode.Truncate;
            }
            base.CommitProperties();
        }

        private void ApplyElipseSuffix()
        {

            string updatedText; ;
            Vector2 extents = TextRef.rectTransform.rect.size;
            TextGenerationSettings settings = TextRef.GetGenerationSettings(extents);
            TextRef.cachedTextGenerator.Populate(Text, settings);

            float scale = extents.x / TextRef.preferredWidth;


            if (scale < 1)
            {
                var amt = TextRef.cachedTextGenerator.characterCount - 3;
                if (amt > 0)
                {
                    updatedText = Text.Substring(0, amt);
                    updatedText += "...";
                }
                else
                {
                    updatedText = Text;
                }
            }
            else
            {
                updatedText = Text;
            }

            TextRef.text = TruncateToFit ? updatedText : Text;

        }

        public void ApplyTheme(XDFontStyle theme)
        {
            base.ApplyTheme(theme);
            FontClassData = theme;
        }

        public void OnRectTransformDimensionsChange()
        {
            // May need to truncate according to new dimensions.
            ApplyElipseSuffix();
        }
    }
}