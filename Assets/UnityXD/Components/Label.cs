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
            //ApplyElipseSuffix();
        }

        protected override void Measure()
        {
            if (CurrentStyle.FontStyle != null)
            {
                Height = Math.Max(Height, CurrentStyle.FontStyle.FontData.minSize);
            }
        }
        
        protected override void CommitProperties()
        {
            if (AutoSize)
            {
                CurrentStyle.Size = XDSizes.Custom;
                IsHeightDependantOnWidth = false;
            }

            //XDTheme.Instance.InitializeDefaultStyles();

            TextRef.text = Text;

            if (CurrentStyle.FontStyle != null)
            {
                TextRef.font = CurrentStyle.FontStyle.FontData.font;
                TextRef.resizeTextForBestFit = CurrentStyle.FontStyle.FontData.bestFit;
                TextRef.resizeTextMinSize = CurrentStyle.FontStyle.FontData.minSize;
                TextRef.resizeTextMaxSize = CurrentStyle.FontStyle.FontData.maxSize;
            }
            else
            {
                TextRef.resizeTextForBestFit = false;
            }
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
       
        public void OnRectTransformDimensionsChange()
        {
            // May need to truncate according to new dimensions.
            //ApplyElipseSuffix();
        }
    }
}