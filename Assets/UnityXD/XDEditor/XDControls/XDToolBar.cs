using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.Core;
using Assets.UnityXD.Styles;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{
    internal class XDToolBar : XDWidget, IXDToolBar
    {
        public string BackgroundPath = "EditorChrome/AnchorBkg";
        private List<XDWidget> buttons = new List<XDWidget>();
        private int totalWidth;
        private Color decal_selected;
        private Color decal_unselected;
        private Color foreground_selected;
        private Color foreground_unselected;
        private Color background_selected;
        private Color background_unselected;
        private int _buttonSize;

        public IXDToolBar Decals(Color selected, Color unselected)
        {
            decal_selected = selected;
            decal_unselected = unselected;
            return this;
        }

        public override IXDWidget Size(int amt)
        {
            _buttonSize = amt;
            return base.Size(amt);
        }

        public IXDToolBar ButtonSize(int amt)
        {
            _buttonSize = amt;
            return this;
        }

        public IXDToolBar Foreground(Color selected, Color unselected)
        {
            foreground_selected = selected;
            foreground_unselected = unselected;
            return this;
        }

        public IXDToolBar Background(Color selected, Color unselected)
        {
            background_selected = selected;
            background_unselected = unselected;
            return this;
        }

        public IXDToolBar Space(int amt = 8)
        {
            totalWidth += amt;
            buttons.Add(new XDSpacer(parent, amt));
            return this;
        }

        public XDToolBar(IXDLayout parent) : base(parent)
        {
        }

        public IXDToolBar Button(string prefix, Action clicked, bool selected)
        {
            var m = margin == null ? 4 : margin.left;
            var b = _buttonSize <=0 ? 24 : _buttonSize;

            totalWidth += b+m;

            var fg = selected ? foreground_selected : foreground_unselected;
            var bg = selected ? background_selected : background_unselected;
            var dec = selected ? decal_selected : decal_unselected;
            GenerateButton(prefix, fg, dec, bg, selected, clicked);

            return this;
        }

        public override void Render()
        {

            int offsetL = (int) (EditorGUIUtility.currentViewWidth/2) - (totalWidth / 2);
            var mR = margin == null ? 4 : margin.right;
            var pStyle = new GUIStyle();
            pStyle.margin = new RectOffset(offsetL, offsetL-mR, 0,0);
            pStyle.padding = new RectOffset(0, 0, 0, 0);
          
            using (new GUILayout.HorizontalScope(pStyle))
            {
                buttons.ForEach(e=>e.Render());
            }
        }

        private void GenerateButton(string prefix, Color foreground, Color decal, Color back, bool field, Action clicked)
        {
            var decals = new List<XDDecals>();
            decals.Add(new XDDecals() { path = String.Format(BackgroundPath), tintColor = back });
            decals.Add(new XDDecals() { path = String.Format("EditorChrome/{0}_Bars",prefix), tintColor = foreground });
            decals.Add(new XDDecals() { path = String.Format("EditorChrome/{0}_Lines", prefix), tintColor = decal });

            // TODO : Fix Button Size / margin so that it fits in with the timing. Atm Logic is duplicated.
            var _margin = (margin == null) ? 4 : margin.left;
            var _btnsize = (_buttonSize <= 0) ? 24 : _buttonSize;

            XDButton btn = (XDButton) new XDButton(parent).Decals(decals).Clicked(clicked).LoadResource(BackgroundPath).Size(_btnsize).Margin(_margin).Padding(0);
            buttons.Add((XDWidget)btn);
        }
    }

    public interface IXDToolBar : IXDWidget
    {
        IXDToolBar Button(string prefix, Action clicked, bool selected);
        IXDToolBar Space(int amt = 8);

        IXDToolBar Background(Color selected, Color unselected);
        IXDToolBar Decals(Color selected, Color unselected);
        IXDToolBar Foreground(Color selected, Color unselected);

        IXDToolBar ButtonSize(int amt);
    }
}
