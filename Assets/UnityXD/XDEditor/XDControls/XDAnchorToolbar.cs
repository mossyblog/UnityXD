using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Assets.UnityXD.Core;
using Assets.UnityXD.Styles;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEditor.Sprites;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{
    public interface IXDAnchorToolbar : IXDWidget
    {
        IXDAnchorToolbar Selected(Expression<Func<SpriteAlignment>> alignExpression);
        IXDAnchorToolbar Size(int amt);
        IXDAnchorToolbar Stretch(Expression<Func<bool>> hExpression, Expression<Func<bool>> vExpression);
    }
    internal class XDAnchorToolbar : XDWidget, IXDAnchorToolbar
    {
        private SpriteAlignment _currentAlignment;
        private bool _isHorizStretched;
        private bool _isVertStretched;

        private readonly PropertyBinding<SpriteAlignment, IXDAnchorToolbar> AnchorAlignmentProperty;
        private readonly PropertyBinding<bool, IXDAnchorToolbar> HorizontalStretchProperty;
        private readonly PropertyBinding<bool, IXDAnchorToolbar> VerticalStretchProperty;

        private int _size = 24;

        internal XDAnchorToolbar(IXDLayout parent) : base(parent)
        {
            AnchorAlignmentProperty = new PropertyBinding<SpriteAlignment, IXDAnchorToolbar>(this, value => _currentAlignment = value);
            HorizontalStretchProperty = new PropertyBinding<bool, IXDAnchorToolbar>(this, e=> _isHorizStretched = e);
            VerticalStretchProperty = new PropertyBinding<bool, IXDAnchorToolbar>(this, e => _isVertStretched = e);
        }

        public IXDAnchorToolbar Stretch(Expression<Func<bool>> hExpression, Expression<Func<bool>> vExpression)
        {
            HorizontalStretchProperty.BindTo(hExpression);
            VerticalStretchProperty.BindTo(vExpression);
            return this;
        }

        public IXDAnchorToolbar Selected(Expression<Func<SpriteAlignment>> alignExpression)
        {
            return AnchorAlignmentProperty.BindTo(alignExpression);
        }

        public IXDAnchorToolbar Size(int amt)
        {
            _size = amt;
            return this;
        }

        public override void OnGUI()
        {
            var horizAlignment = _currentAlignment.ToHorizontalAlignment();
            var vertAlignment = _currentAlignment.ToVerticalAlignment();

            var isAnchorRightEnabled = XDHorizontalAlignment.Right == horizAlignment;
            var isAnchorLeftEnabled = XDHorizontalAlignment.Left == horizAlignment;
            var isAnchorMiddleHEnabled = XDHorizontalAlignment.Center == horizAlignment;
            var isAnchorTopEnabled = XDVerticalAlignment.Top == vertAlignment;
            var isAnchorMiddleVEnabled = XDVerticalAlignment.Center == vertAlignment;
            var isAnchorBottomEnabled = XDVerticalAlignment.Bottom == vertAlignment;

            const string path = "icons/Editor/";
            const string horizPrefix = "AlignHoriz";
            const string vertPrefix = "AlignVert";
            const string onState = "On";
            const string offState = "Off";

            var leftName = "Left_" + (isAnchorLeftEnabled ? onState : offState);
            var rightName = "Right_" + (isAnchorRightEnabled ? onState : offState);
            var topName = "Top_" + (isAnchorTopEnabled ? onState : offState);
            var botName = "Bottom_" + (isAnchorBottomEnabled ? onState : offState);
            var midHName = "Middle_" + (isAnchorMiddleHEnabled ? onState : offState);
            var midVName = "Middle_" + (isAnchorMiddleVEnabled ? onState : offState);

            var left = FetchButtonTexture(path + horizPrefix + leftName);
            var midH = FetchButtonTexture(path + horizPrefix + midHName);
            var right = FetchButtonTexture(path + horizPrefix + rightName);
            var top = FetchButtonTexture(path + vertPrefix + topName);
            var midV = FetchButtonTexture(path + vertPrefix + midVName);
            var bot = FetchButtonTexture(path + vertPrefix + botName);

            var hStretch = FetchButtonTexture(path + "StretchHoriz_" + (_isHorizStretched ? onState : offState));
            var vStretch = FetchButtonTexture(path + "StretchVert_" + (_isVertStretched ? onState : offState));

            var style = new GUIStyle
            {
                normal =
                {
                    background = CreateColoredTexture(Color.white)
                },
                alignment = TextAnchor.MiddleCenter,
                padding = new RectOffset(4, 4, 4, 4),
                margin = new RectOffset(2,2,2,2)

            };



            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button(hStretch, style, GUILayout.Width(_size), GUILayout.Height(_size)))
                {
                    _isHorizStretched = !_isHorizStretched;
                }

                if (GUILayout.Button(vStretch, style, GUILayout.Width(_size), GUILayout.Height(_size)))
                {
                    _isVertStretched = !_isVertStretched;
                }

                GUILayout.Space(16);

                if (GUILayout.Button(left, style, GUILayout.Width(_size), GUILayout.Height(_size)))
                {
                    horizAlignment = XDHorizontalAlignment.Left;
                }
                if (GUILayout.Button(midH, style, GUILayout.Width(_size), GUILayout.Height(_size)))
                {
                    horizAlignment = XDHorizontalAlignment.Center;
                }

                if (GUILayout.Button(right, style, GUILayout.Width(_size), GUILayout.Height(_size)))
                {
                    horizAlignment = XDHorizontalAlignment.Right;
                }

                GUILayout.Space(16);

                if (GUILayout.Button(top, style, GUILayout.Width(_size), GUILayout.Height(_size)))
                {
                    vertAlignment = XDVerticalAlignment.Top;
                }
                if (GUILayout.Button(midV, style, GUILayout.Width(_size), GUILayout.Height(_size)))
                {
                    vertAlignment = XDVerticalAlignment.Center;
                }

                if (GUILayout.Button(bot, style, GUILayout.Width(_size), GUILayout.Height(_size)))
                {
                    vertAlignment = XDVerticalAlignment.Bottom;
                }

            }



            // Notify the Host of the change.
            _currentAlignment = StyleUtility.ToAlignment(horizAlignment, vertAlignment);

            // Update the Bindings
            HorizontalStretchProperty.UpdateView(_isHorizStretched);
            VerticalStretchProperty.UpdateView(_isVertStretched);
            AnchorAlignmentProperty.UpdateView(_currentAlignment);

        }

        internal override void BindViewModel(object viewModel)
        {
            base.BindViewModel(viewModel);
            AnchorAlignmentProperty.BindViewModel(viewModel);
            VerticalStretchProperty.BindViewModel(viewModel);
            HorizontalStretchProperty.BindViewModel(viewModel);
        }

        private GUIContent FetchButtonTexture(string path)
        {
            return new GUIContent(SpriteUtility.GetSpriteTexture(Resources.Load<Sprite>(path),false));
        }
    }
}
