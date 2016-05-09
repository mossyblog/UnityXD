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
    [RequireComponent(typeof(Image))]
    public class Icon : BaseControl
    {
        
        [SerializeField]
        private IconLibrary _currentIcon;


        public IconLibrary CurrentIcon
        {
            get { return _currentIcon; }
            set { 
                NotifyOfPropertyChange(value, ()=>CurrentIcon, ref _currentIcon); 
            }
        }      


      

    

        #region implemented abstract members of BaseControl
     

        public override void InvalidateControl()
        {
            IsHorizontalStretchEnabled = true;
            IsVeritcalStretchEnabled = true;


            if (CurrentStyle != null)
            {
                ImageRef.color = CurrentStyle.Foreground == ColorLibrary.Uknown ? Color.white : CurrentStyle.Foreground.ToColor( );
                ImageRef.preserveAspect = true;

                if (CurrentStyle.IconMargin != null)
                {
                    Margin = CurrentStyle.IconMargin;
                }
            }


            if (_currentIcon != IconLibrary.Unknown)
            {
                ImageRef.sprite = _currentIcon.ResolveSprite( );
            }
            
        }



        internal override void RegisterBindings()
        {
           
        }

        public override void CacheComponentReferences()
        {
           
        }

        #endregion
    }
}
