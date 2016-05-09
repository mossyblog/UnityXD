using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.Core;
using Assets.UnityXD.Styles;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.XDEditor
{
    public abstract class BaseInspector : UnityEditor.Editor
    {
        private XDGUI CustomGUI;
        internal BaseControl baseControl;
        internal const string TabLabelLayout = "Layout";
        internal const string TabLabelBindings = "Bindings";
        internal string TabSelected = TabLabelLayout;
        internal const string instanceName = "currentTab_";

        protected GUIStyle LabelDefault;
        protected GUIStyle PanelDefault;

        public override void OnInspectorGUI()
        {


            if (baseControl == null)
            {
                baseControl = target as BaseControl;
            }
            TabSelected = EditorPrefs.GetString(instanceName + baseControl.GetInstanceID(), TabSelected);


            LabelDefault = new GUIStyle(GUI.skin.label);
            LabelDefault.normal.textColor = ColorLibrary.ChromeLightest.ToColor();
            LabelDefault.padding = new RectOffset(4,4,0,0);
            LabelDefault.alignment = TextAnchor.MiddleLeft;

            PanelDefault = new GUIStyle();
            PanelDefault.padding = new RectOffset(16,16,4,4);

            var tabBar = new XDGUI();
            tabBar.TabBar()
                .Selected(TabSelected)
                .Tab(TabLabelLayout, DisplayTabLayout)
                .Tab(TabLabelBindings, DisplayTabBindings)
                .OnSelected(OnTabSelected);

            
            tabBar.Render();


            var guiFinish = new XDGUI();
            guiFinish.Background(ColorLibrary.ChromeDarker.ToColor());
            guiFinish.FlexiSpace();
            guiFinish.Render();

            if (GUI.changed)
            {
                Debug.Assert(baseControl != null, "baseControl != null");
                baseControl.InvalidateComponent();
            }
            EditorUtility.SetDirty(target);
        }

        private void OnTabSelected(string tab)
        {
            TabSelected = tab;
            EditorPrefs.SetString(instanceName + baseControl.GetInstanceID(), TabSelected);
        }

        private void DisplayTabLayout()
        {

            var anchorGUI = new XDGUI();
            anchorGUI.Spacer(16);
            anchorGUI.Background(ColorLibrary.ChromeDarker.ToColor());
            anchorGUI.AnchorToolbar().BindTo(ref baseControl).Size(24).Background(Color.red);
            anchorGUI.Render();

            var metricsGUI = new XDGUI();
            metricsGUI.Background(ColorLibrary.ChromeDarker.ToColor());
            metricsGUI.LayoutMetrics().BindTo(ref baseControl);
            metricsGUI.Render();

            CustomGUI = new XDGUI();
            CustomGUI.Background(ColorLibrary.ChromeDarker.ToColor());
            OnTabLayout(CustomGUI);          
            CustomGUI.Render();


        }

        private void DisplayTabBindings()
        {
            OnTabBinding(CustomGUI);
        }


        public abstract void OnTabLayout(XDGUI gui);

        public abstract void OnTabBinding(XDGUI gui);

        public override bool UseDefaultMargins()
        {
            return false;
        }
    }

}
