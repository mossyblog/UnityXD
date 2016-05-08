using Assets.UnityXD.Core;
using Assets.UnityXD.Styles;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDComponents
{
    public interface IXDAnchorToolbar : IXDWidget
    {
        IXDAnchorToolbar Size(int amt);
        IXDAnchorToolbar BindTo(ref BaseControl control);
    }
    internal class XDAnchorToolbar : XDWidget, IXDAnchorToolbar
    {
        private SpriteAlignment _currentAlignment;
        private BaseControl baseControl;
        private int _size = 24;

        internal XDAnchorToolbar(IXDLayout parent) : base(parent)
        {
        }

        public IXDAnchorToolbar BindTo(ref BaseControl control)
        {
            baseControl = control;

            var horizAlign = _currentHorizontalAlignment = control.CurrentAnchorAlignment.ToHorizontalAlignment();
            var vertAlign = _currentVerticalAlignment = control.CurrentAnchorAlignment.ToVerticalAlignment();
            var hstretch = control.IsHorizontalStretchEnabled;
            var vstretch = control.IsVeritcalStretchEnabled;

            parent.ToolBar()
               
                .ButtonSize(_size)
                .Decals(ColorLibrary.ChromeDark.ToColor(), ColorLibrary.Chrome.ToColor(0.5F))
                .Foreground(ColorLibrary.Accent.ToColor(), ColorLibrary.Chrome.ToColor(0.5F))
                .Background(ColorLibrary.ChromeDarkest.ToColor(), ColorLibrary.ChromeDarker.ToColor())

                .Button("HStretch", HstretchClicked, hstretch)
                .Button("VStretch", VstretchClicked, vstretch)
                .Space()
                .Button("HLeft", () => { HorizAlignClicked(XDHorizontalAlignment.Left); }, horizAlign == XDHorizontalAlignment.Left)
                .Button("HCenter", () => { HorizAlignClicked(XDHorizontalAlignment.Center); }, horizAlign == XDHorizontalAlignment.Center)
                .Button("HRight", () => { HorizAlignClicked(XDHorizontalAlignment.Right); }, horizAlign == XDHorizontalAlignment.Right)
                .Space()
                .Button("VTop", () => { VertAlignClicked(XDVerticalAlignment.Top); }, vertAlign == XDVerticalAlignment.Top)
                .Button("VCenter", () => { VertAlignClicked(XDVerticalAlignment.Center); }, vertAlign == XDVerticalAlignment.Center)
                .Button("VBottom", () => { VertAlignClicked(XDVerticalAlignment.Bottom); }, vertAlign == XDVerticalAlignment.Bottom);
            return this;

        }

        public IXDAnchorToolbar Size(int amt)
        {
            _size = amt;
            return this;
        }


        private void HstretchClicked()
        {
            baseControl.IsHorizontalStretchEnabled = !baseControl.IsHorizontalStretchEnabled;
        }

        private void VstretchClicked()
        {
            baseControl.IsVeritcalStretchEnabled = !baseControl.IsVeritcalStretchEnabled;
        }

        private XDHorizontalAlignment _currentHorizontalAlignment;
        private XDVerticalAlignment _currentVerticalAlignment;

        private void HorizAlignClicked(XDHorizontalAlignment align)
        {
            _currentHorizontalAlignment = align;
            baseControl.CurrentAnchorAlignment = StyleUtility.ToAlignment(_currentHorizontalAlignment, _currentVerticalAlignment);
        }

        private void VertAlignClicked(XDVerticalAlignment align)
        {
            _currentVerticalAlignment = align;
            baseControl.CurrentAnchorAlignment = StyleUtility.ToAlignment(_currentHorizontalAlignment, _currentVerticalAlignment);
        }

        public override void Render()
        {
           

        }


    }
}
