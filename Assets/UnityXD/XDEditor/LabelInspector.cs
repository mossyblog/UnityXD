using System.Linq;
using Assets.UnityXD.Components;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.Editor
{
    [CustomEditor(typeof (Label))]
    public class LabelInspector : UnityEditor.Editor
    {
        private Label _labelRef;

        public void OnEnable()
        {
            _labelRef = target as Label;
            Undo.RecordObject(_labelRef,"LabelRef");
        }

        [MenuItem("UnityXD/Label")]
        public static void Dummy()
        {
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            EditorGUILayout.Space();

            var gui = new XDGUI();
            var styles = ZuQAPI.Controller.Instance().Theme().FetchStyles<Label>();
            var labelW = 32;

            gui.VerticalLayout()
               
                .Spacer()
                .HorizontalLayout()
                    .TextBox()
                        .Label("W", labelW)
                        .FormValue(()=> _labelRef.Width)
                        .Width(labelW)
                    .End()
                    .Spacer()
                    .TextBox()                        
                        .Label("X", labelW)                        
                        .FormValue(()=> _labelRef.X)
                        .Width(labelW)
                    .End()
                    .Spacer()
                .End()
                .HorizontalLayout()
                    .TextBox()
                        .Label("H", labelW)
                        .FormValue(() => _labelRef.Height)
                        .Width(labelW)
                    .End()
                    .Spacer()
                    .TextBox()
                        .Label("Y", labelW)
                        .FormValue(() => _labelRef.Y)
                        .Width(labelW)
                    .End()
                    .Spacer()
                .End()
                .Checkbox()
                    .Text("Height Linked")
                    .Selected(() => _labelRef.IsHeightDependantOnWidth)
                .End()
                .VerticalLayout()                    
                    .AnchorToolbar()
                    .Selected(()=>_labelRef.CurrentAnchorAlignment)
                    .Stretch(()=> _labelRef.IsHorizontalStretchEnabled, ()=> _labelRef.IsVeritcalStretchEnabled)
                    .Size(24)                        
                .End()
                .Spacer(32)
                .ComboBox()
                    .Label.Value("Styles")
                    .Options(styles.ToArray())
                    .Selected(() => _labelRef.CurrentStyle)
                .End()
                .TextBox()
                    .Label("Text", labelW)
                     .FormValue(()=>_labelRef.Text)
                    .End()
            
                .FlexiSpace()
            .End();

            gui.BindViewModel(_labelRef);
            gui.OnGUI();

            if (GUI.changed)
            {
                _labelRef.InvalidateComponent();
            }
            EditorGUILayout.Space();
            EditorUtility.SetDirty(target);
        }
    }
}