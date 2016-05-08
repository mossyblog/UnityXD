using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.Styles;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{
    public interface IXDTabBar : IXDWidget
    {
        IXDTabBar Tab(string label, Action displayTab);

        IXDTabBar Selected(string label);
        IXDTabBar OnSelected(Action<String> onTabSelected);
    }

    public class XDTabBar : XDWidget, IXDTabBar
    {
        private Dictionary<string, Action> tabs;
        private string selected;
        private Action<String> tabSelectedEvent;

        public XDTabBar(IXDLayout parent) : base(parent)
        {
            tabs = new Dictionary<string, Action>();

            
        }

        public override void Render()
        {
       
            var tabStyle = new GUIStyle();
            tabStyle.normal.background = CreateColoredTexture(ColorLibrary.Chrome.ToColor());
            tabStyle.normal.textColor = ColorLibrary.ChromeLighter.ToColor();
            tabStyle.padding = new RectOffset(4,4,4,4);
            tabStyle.fixedHeight = 24;
            
            tabStyle.alignment = TextAnchor.MiddleCenter;
            tabStyle.margin = new RectOffset(4,4,4,0);

            var tabStyleSelected = new GUIStyle(tabStyle);
            tabStyleSelected.fontStyle = FontStyle.Bold;
            tabStyleSelected.normal.background = CreateColoredTexture(ColorLibrary.ChromeDarker.ToColor());

            using (new GUILayout.HorizontalScope())
            {
                foreach (var tab in tabs)
                {
                    var gui = new XDGUI();
                    gui.Button().Clicked(()=> { tabSelectedEvent.DynamicInvoke(tab.Key); }).Content(tab.Key.ToUpper()).Style(tab.Key == selected ? tabStyleSelected : tabStyle).End();
                    gui.Render();
                }
            }


            if (tabs.ContainsKey(selected))
            {
                tabs[selected].Invoke();
            }
        }

        public IXDTabBar Tab(string label, Action action)
        {
            tabs.Add(label,action);
            
            return this;
        }

        public IXDTabBar Selected(string label)
        {
            selected = label;
            
            return this;
        }

        public IXDTabBar OnSelected(Action<String> onTabSelected)
        {
            tabSelectedEvent = onTabSelected;
            return this;
        }
    }
}
