using Assets.UnityXD.Core;
using Assets.UnityXD.Styles;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UnityXD.Components
{
    [ExecuteInEditMode]
    [AddComponentMenu("UnityXD / Label")]
    [RequireComponent(typeof (Text))]
    public class Label : BaseControl
    {

        public Text TextRef { get; set; }

        [SerializeField]
        private string _text;

        public string Text
        {
            get { return _text; }
            set { NotifyOfPropertyChange(value, ()=>Text, ref _text); }
        }

        public override void SetStyle(Style style)
        {
            base.SetStyle(style);
            CurrentStyle = style;
        }

        private void OnTextChanged()
        {
            Text = TextRef.text;          
        }

        public override void SetSize(int w, int h)
        {
            if (CurrentStyle != null)
            {
                if (CurrentStyle.Font.AutoSize)
                {
                    w = (int)TextRef.preferredWidth;
                }
            }
            base.SetSize(w, h);
        }

        public override void InvalidateControl()
        {
            if (CurrentStyle != null)
            {
                TextRef.font = CurrentStyle.Font.font;
                TextRef.resizeTextForBestFit = CurrentStyle.Font.bestFit;
                TextRef.resizeTextMinSize = CurrentStyle.Font.minSize;
                TextRef.resizeTextMaxSize = CurrentStyle.Font.maxSize;
                TextRef.color = CurrentStyle.Foreground.ToColor();
                TextRef.alignment = CurrentStyle.Font.alignment;
            }

            if (!TextRef.text.Equals(Text))
            {
                TextRef.text = Text;
            }

        }

        public override void CacheComponentReferences()
        {
            TextRef = GetComponent<Text>();
            TextRef.RegisterDirtyLayoutCallback(OnTextChanged);            
        }

      

        internal override void RegisterBindings()
        {
            
        }
    }
}