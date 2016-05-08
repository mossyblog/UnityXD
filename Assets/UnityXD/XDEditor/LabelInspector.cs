using System;
using System.Collections.Generic;
using System.Linq;
using Assets.UnityXD.Components;
using Assets.UnityXD.Core;
using Assets.UnityXD.Styles;
using Assets.UnityXD.XDEditor;
using Assets.UnityXD.XDEditor.XDControls;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.Editor
{
    [CustomEditor(typeof (Label))]
    public class LabelInspector : BaseInspector
    {
        private Label _labelRef;

        public void OnEnable()
        {
            _labelRef = target as Label;
            Undo.RecordObject(_labelRef,"LabelRef");
        }

        public override void OnTabLayout(XDGUI gui)
        {
            var options = ZuQAPI.Controller.Instance().Theme().FetchStyles<Label>().ToArray();
            gui.VerticalLayout(PanelDefault).TextBox().Field((s) => { _labelRef.Text = s; }, _labelRef.Text).Label("Text", LabelDefault).Width(172).End();
            gui.Spacer(8);

            gui
                .HorizontalLayout(PanelDefault)
                .Label().Content("Style").Style(LabelDefault).Width(64).End()
                .ComboBox()
                .Options(options)
                .Selected(() => _labelRef.CurrentStyle)
                .Bind(_labelRef);
        }

        public override void OnTabBinding(XDGUI gui)
        {
        }

        public override void OnCustomInspector()
        {

        }
    }

}