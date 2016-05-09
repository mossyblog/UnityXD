using System;
using UnityEditor;
using Assets.UnityXD.Components;
using Assets.UnityXD.XDEditor;
using Assets.UnityXD.XDEditor.XDCore;
using Assets.UnityXD.Styles;
using UnityEngine;

namespace Assets.UnityXD.Editor
{
    [CustomEditor(typeof (TileIcon))]
    public class TileIconInspector : BaseInspector
    {
        private TileIcon TileIconRef;

        public void OnEnable()
        {
            TileIconRef = target as TileIcon;
            Undo.RecordObject(TileIconRef,"LabelRef");
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
                    .Selected( () => TileIconRef.CurrentStyle )
                    .Bind( TileIconRef );
            }

            gui.VerticalLayout(PanelDefault)
                .TextBox().Field((s) => { TileIconRef.CurrentLabel = s; }, TileIconRef.CurrentLabel)
                .Label("Text", 64, LabelDefault)
                .End();

            gui.HorizontalLayout( PanelDefault )
                .Label( ).Content( "Icon" ).Style( LabelDefault ).Width( 64 ).End( )
                .ComboBox( )
                .Options( Enum.GetNames( typeof(IconLibrary) ) )
                .Selected( () => TileIconRef.CurrentIcon)
                .Bind( TileIconRef )
                .End( );


            gui.RectOffset( ).Field( ()=> TileIconRef.Margin, TileIconRef).Content("Margin");

            //Debug.LogFormat("Rect {0}", TileIconRef.Margin.left);

            
        }
                                    
        public override void OnTabBinding(XDGUI gui)
        {
            
        }
        #endregion
       
    }
}

