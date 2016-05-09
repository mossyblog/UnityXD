using System;
using System.Linq;
using Assets.UnityXD.Components;
using Assets.UnityXD.Styles;
using Assets.UnityXD.XDEditor;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.Editor
{
    [CustomEditor(typeof (Icon))]

    public class IconInspector : BaseInspector
    {
        private Icon IconRef;

        public void OnEnable()
        {
            IconRef = target as Icon;
            Undo.RecordObject(IconRef,"LabelRef");
        }


        #region implemented abstract members of BaseInspector

        public override void OnTabLayout(XDGUI gui)
        {
            var options = ZuQAPI.Controller.Instance( ).Theme( ).FetchStyles( );

            if (options != null)
            {
                gui
                .HorizontalLayout( PanelDefault )
                .Label( ).Content( "Style" ).Style( LabelDefault ).Width( 64 ).End( )
                .ComboBox( )
                .Options( options.ToArray( ) )
                .Selected( () => IconRef.CurrentStyle )
                .Bind( IconRef );
            }

            gui.HorizontalLayout( PanelDefault )
                .Label( ).Content( "Icon" ).Style( LabelDefault ).Width( 64 ).End( )
                .ComboBox( )
                .Options( Enum.GetNames( typeof(IconLibrary) ) )
                .Selected( () => IconRef.CurrentIcon)
                .Bind( IconRef )
                .End( );
        }

        private void UpdateSelected(IconLibrary icon) {           
        }

        public override void OnTabBinding(XDGUI gui)
        {            
        }


        #endregion
    }
}

