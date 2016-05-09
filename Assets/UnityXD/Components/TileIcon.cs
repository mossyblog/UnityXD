using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.Core;
using UnityEngine;
using UnityEngine.UI;
using Assets.UnityXD.Styles;

namespace Assets.UnityXD.Components
{
    [RequireComponent( typeof(Image))]
    public class TileIcon : BaseControl
    {
        private Label LabelRef;
        private Icon IconRef;

        [SerializeField]
        private IconLibrary _currentIcon;

        [SerializeField]
        private string _currentLabel = String.Empty;

        private PropertyBinding<IconLibrary, TileIcon> iconProperty;
        private PropertyBinding<string, TileIcon> labelProperty;

        public IconLibrary CurrentIcon
        {
            get { return _currentIcon; }
            set { NotifyOfPropertyChange( value, () => CurrentIcon, ref _currentIcon ); }
        }


        public string CurrentLabel
        {
            get { return _currentLabel; }
            set { NotifyOfPropertyChange( value, () => CurrentLabel, ref _currentLabel ); }
        }

        public override void Start()
        {
            base.Start( );
        }

        public override void InvalidateControl()
        {
            if (IconRef != null)
            {
                IconRef.SetStyle( CurrentStyle );
                IconRef.CurrentIcon = CurrentIcon;
                IconRef.InvalidateComponent( );
            }

            if (LabelRef != null)
            {
                LabelRef.SetStyle( CurrentStyle );
                LabelRef.Text = CurrentLabel;
                LabelRef.InvalidateComponent();
            }

            if (CurrentStyle != null)
            {
                ImageRef.color = CurrentStyle.Background.ToColor( );
            }
                         
        }



        public override void CacheComponentReferences()
        {           
            LabelRef = gameObject.GetComponentInChildren<Label>( );
            IconRef = gameObject.GetComponentInChildren<Icon>( );

        }

        internal override void RegisterBindings()
        {
        }

      
    }
}
