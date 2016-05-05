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
        private PropertyBinding<string, Text> _textCompToVm;

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

        public override void InvalidateComponent()
        {
            if (CurrentStyle != null)
            {
                TextRef.font = CurrentStyle.Font.font;
                TextRef.resizeTextForBestFit = CurrentStyle.Font.bestFit;
                TextRef.resizeTextMinSize = CurrentStyle.Font.minSize;
                TextRef.resizeTextMaxSize = CurrentStyle.Font.maxSize;
                TextRef.color = CurrentStyle.Foreground.ToColor();
            }

            if (!TextRef.text.Equals(Text))
            {
                TextRef.text = Text;
            }

            InvalidateLayout();
        }

        public override void CacheComponentReferences()
        {
            TextRef = GetComponent<Text>();
            TextRef.RegisterDirtyLayoutCallback(OnTextChanged);            
        }

        public override void ValidateHeirachy()
        {
            base.ValidateHeirachy();
            
            // Help keep it tidy...
            if (!name.StartsWith("label", true, null))
            {
                name = "Label" + name;
            }
        }

        public void DoSomething()
        {
            Dock(SpriteAlignment.BottomCenter, true, true);
            SetStyle(ZuQAPI.Controller.Instance().Theme().FetchStyle<Label>("Default"));
        }

        internal override void RegisterBindings()
        {
            _textCompToVm = new PropertyBinding<string, Text>(TextRef, e => TextRef.text = e);
            _textCompToVm.BindTo(() => Text);
            _textCompToVm.BindViewModel(this);
        }
    }
}