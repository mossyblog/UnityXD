using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityXD.Components;
using UnityXD.Styles;

namespace UnityXD.Editor
{
    [DisallowMultipleComponent]
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIComponent))]
    public class UIComponentInspector : UnityEditor.Editor
    {
        protected UIComponent _componentRef;
        protected Label _label;
        protected string m_text;
        protected bool m_isVisible;
        protected const string m_labelLayout = "Layout";
        protected const string m_labelDesign = "Design";
        protected const string m_labelBinding = "Binding";

        protected string[] m_tabs = {m_labelLayout, m_labelDesign, m_labelBinding};
        protected string m_currentSelectedTab = m_labelLayout;

        // Anchor Alignment Config.
        protected bool m_isAnchorLeftEnabled;
        protected bool m_isAnchorRightEnabled;
        protected bool m_isAnchorMiddleHEnabled;
        protected bool m_isAnchorMiddleVEnabled;
        protected bool m_isAnchorTopEnabled;
        protected bool m_isAnchorBottomEnabled;
        protected bool m_isAnchorHStretched;
        protected bool m_isAnchorVStretched;
        protected XDVerticalAlignment m_vertAlignment;
        protected XDHorizontalAlignment m_horizAlignment;

        protected int m_width;
        protected int m_height;
        protected int m_x;
        protected int m_y;
        protected bool m_isWLinkedToH;
        protected XDSizes m_currentSize;

        protected RectOffset m_padding;
        protected RectOffset m_margin;

        protected Rect m_bodyRect;

        public void OnEnable()
        {

        }

        public void OnDestroy()
        {
        }

        public override void OnInspectorGUI()
        {
            Initialize();
            GeneratateInspector();
            CommitProperties();
        }

        protected virtual void Initialize()
        {

            if (_componentRef == null)
                _componentRef = (UIComponent)target;

            // For some reason the Play/Stop causes the Styles to act weird.
            m_horizAlignment = _componentRef.CurrentAnchorAlignment.ToHorizontalAlignment();
            m_vertAlignment = _componentRef.CurrentAnchorAlignment.ToVerticalAlignment();

            m_isAnchorRightEnabled = XDHorizontalAlignment.Right == m_horizAlignment;
            m_isAnchorLeftEnabled = XDHorizontalAlignment.Left== m_horizAlignment;
            m_isAnchorMiddleHEnabled = XDHorizontalAlignment.Center == m_horizAlignment;
            m_isAnchorTopEnabled = XDVerticalAlignment.Top == m_vertAlignment;
            m_isAnchorMiddleVEnabled = XDVerticalAlignment.Center == m_vertAlignment;
            m_isAnchorBottomEnabled = XDVerticalAlignment.Bottom == m_vertAlignment;
            m_isAnchorHStretched = _componentRef.IsHorizontalStretchEnabled;
            m_isAnchorVStretched = _componentRef.IsVeritcalStretchEnabled;

            m_margin = _componentRef.Margin;
            m_padding = _componentRef.Padding;

        }


        protected virtual void CommitBindings()
        {
           
        }
   

        protected virtual void CommitProperties()
        {

            XDGUIUtility.Bind(ref _componentRef.Width, ref m_width);
            XDGUIUtility.Bind(ref _componentRef.Height, ref m_height);
            XDGUIUtility.Bind(ref _componentRef.X, ref m_x);
            XDGUIUtility.Bind(ref _componentRef.Y, ref m_y);
            XDGUIUtility.Bind(ref _componentRef.IsHeightDependantOnWidth, ref m_isWLinkedToH);
            XDGUIUtility.Bind(ref _componentRef.Padding, ref m_padding);
            XDGUIUtility.Bind(ref _componentRef.Margin, ref m_margin);
            XDGUIUtility.Bind(ref _componentRef.CurrentStyle.Size, ref m_currentSize);

            if (GUI.changed)
            {
                // Dock It.
                var align = XDThemeUtility.ToAlignment(m_horizAlignment, m_vertAlignment);
                _componentRef.Dock(align, m_isAnchorHStretched, m_isAnchorVStretched);
                _componentRef.SetMargin(m_margin);
                _componentRef.SetPadding(m_padding);
            }
            EditorUtility.SetDirty(target);
        }

        protected virtual void GeneratateInspector()
        {

            XDGUIStyles.Instance.InitStyles();

            var body = XDGUIUtility.CreateSpacer();

            if (body.width > 0)
                m_bodyRect = body;

            CreateTabBar();
            using (var rect = new XDGUILayout(false, XDGUIStyles.Instance.Body))
            {
                if (m_currentSelectedTab == m_labelLayout)
                    CreateLayoutControls();

                if (m_currentSelectedTab == m_labelDesign)
                    CreateDesignControls();

                if (m_currentSelectedTab == m_labelBinding)
                    CreateBindingControls();
            }
            XDGUIUtility.CreateSpacer();
        }

        protected virtual void CreateTabBar()
        {
            XDGUIUtility.CreateTabBar(m_tabs, ref m_currentSelectedTab);

        }

        protected virtual void CreateLayoutControls()
        {
            XDGUIUtility.CreateSpacer();
            CreateAnchorToolBar();     
            CreateDivider();

            var layoutStyle = new GUIStyle(GUIStyle.none);                        
            layoutStyle.alignment = TextAnchor.MiddleCenter;
            layoutStyle.padding = new RectOffset(4,4,4,4);

            // SWITCHES.
            var w_enabled = m_currentSize == XDSizes.Custom && !m_isAnchorHStretched;
            var h_enabled = m_currentSize == XDSizes.Custom && !m_isAnchorVStretched && !m_isWLinkedToH;
            var link_enabled = w_enabled && !m_isAnchorHStretched && !m_isAnchorVStretched;

            /*********************************************************************************
                SIZES.            
            *********************************************************************************/

            using (new XDGUILayout(false, layoutStyle))
            {
                XDGUIUtility.CreateEnumField("Size", ref m_currentSize, (int)m_currentSize, XDGUISizes.Medium, null);
            }
            using (new XDGUILayout(true, layoutStyle, GUILayout.Width(64)))
            {
               
                using (new XDGUILayout(false))
                {
                    XDGUIUtility.CreateTextField("W", ref m_width, XDGUISizes.Small, true, w_enabled);
                    XDGUIUtility.CreateTextField("H", ref m_height, XDGUISizes.Small, true, h_enabled);                    
                }   
                using (new XDGUILayout(false, GUILayout.Width(48)))
                {
                    GUILayout.Space(3);
                    var linkSprite = Resources.Load<Sprite>("Icons/Editor/SizeLink_" + (m_isWLinkedToH ? "On" : "Off"));
                    if (XDGUIUtility.CreateButton(linkSprite, 24, 48, GUIStyle.none, false, link_enabled))
                    {
                        m_isWLinkedToH = !m_isWLinkedToH;
                    }                    
                    
                }
                using (new XDGUILayout(false))
                {
                    XDGUIUtility.CreateTextField("X", ref m_x, XDGUISizes.Small, true);
                    XDGUIUtility.CreateTextField("Y", ref m_y, XDGUISizes.Small, true);
                }
            }

            /*********************************************************************************
               Padding
           *********************************************************************************/
            using (new XDGUILayout(false,layoutStyle))
            {
                XDGUIUtility.CreateHeading("Padding");
            }

            using (new XDGUILayout(true, layoutStyle))
            {
                var left = m_padding.left;
                var right = m_padding.right;
                var top = m_padding.top;
                var bot = m_padding.bottom;

                XDGUIUtility.CreateTextField("Left", ref left, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);
                XDGUIUtility.CreateTextField("Right", ref right, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);
                XDGUIUtility.CreateTextField("Top", ref top, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);
                XDGUIUtility.CreateTextField("Bottom", ref bot, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);

                m_padding = new RectOffset(left,right,top,bot);
            }

            /*********************************************************************************
                Margin
            *********************************************************************************/
            using (new XDGUILayout(false, layoutStyle))
            {
                XDGUIUtility.CreateHeading("Margins");
            }

            using (new XDGUILayout(true, layoutStyle))
            {
                var left = m_margin.left;
                var right = m_margin.right;
                var top = m_margin.top;
                var bot = m_margin.bottom;

                XDGUIUtility.CreateTextField("Left", ref left, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);
                XDGUIUtility.CreateTextField("Right", ref right, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);
                XDGUIUtility.CreateTextField("Top", ref top, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);
                XDGUIUtility.CreateTextField("Bottom", ref bot, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);

                m_margin = new RectOffset(left, right, top, bot);
            }
        }

        protected virtual void CreateAnchorToolBar()
        {
            var path = "icons/Editor/";
            var horizPrefix = "AlignHoriz";
            var vertPrefix = "AlignVert";
            var onState = "On";
            var offState = "Off";

            var leftName = "Left_" + (m_isAnchorLeftEnabled ? onState : offState);
            var rightName = "Right_" + (m_isAnchorRightEnabled ? onState : offState);
            var topName = "Top_" + (m_isAnchorTopEnabled ? onState : offState);
            var botName = "Bottom_" + (m_isAnchorBottomEnabled ? onState : offState);
            var midHName = "Middle_" + (m_isAnchorMiddleHEnabled ? onState : offState);
            var midVName = "Middle_" + (m_isAnchorMiddleVEnabled ? onState : offState);

            var leftSprite = Resources.Load<Sprite>(path + horizPrefix + leftName);
            var midHSprite = Resources.Load<Sprite>(path + horizPrefix + midHName);
            var rightSprite = Resources.Load<Sprite>(path + horizPrefix + rightName);
            
            var topSprite = Resources.Load<Sprite>(path + vertPrefix + topName);
            var midVSprite = Resources.Load<Sprite>(path + vertPrefix + midVName);
            var botSprite = Resources.Load<Sprite>(path + vertPrefix + botName);

            var horizStretchSprite = Resources.Load<Sprite>(path + "StretchHoriz_" + (m_isAnchorHStretched ? onState : offState));
            var vertStretchSprite = Resources.Load<Sprite>(path + "StretchVert_" + (m_isAnchorVStretched ? onState : offState));

            var style = new GUIStyle(GUIStyle.none);
            style.normal.background = XDGUIUtility.CreateColoredTexture(Color.white);
            style.alignment = TextAnchor.MiddleCenter;
            style.margin = new RectOffset(3,3,3,3);

            using (new XDGUILayout(true, style, GUILayout.Width(128)))
            {
                XDGUIUtility.CreateSpacer(16);
                if (XDGUIUtility.CreateButton(horizStretchSprite, 24, 24, style, false))
                {
                    m_isAnchorHStretched = !m_isAnchorHStretched;
                }
                if (XDGUIUtility.CreateButton(vertStretchSprite, 24, 24, style, false))
                {
                    m_isAnchorVStretched = !m_isAnchorVStretched;
                }
                XDGUIUtility.CreateSpacer(16);

                if (XDGUIUtility.CreateButton(leftSprite, 24, 24, style, false))
                    m_horizAlignment = XDHorizontalAlignment.Left;

                if (XDGUIUtility.CreateButton(midHSprite, 24, 24, style, false))
                    m_horizAlignment = XDHorizontalAlignment.Center;

                if (XDGUIUtility.CreateButton(rightSprite, 24, 24, style, false))
                    m_horizAlignment = XDHorizontalAlignment.Right;

                XDGUIUtility.CreateSpacer(16);

                if (XDGUIUtility.CreateButton(topSprite, 24, 24, style, false))
                    m_vertAlignment = XDVerticalAlignment.Top;

                if (XDGUIUtility.CreateButton(midVSprite, 24, 24, style, false))
                    m_vertAlignment = XDVerticalAlignment.Center;

                if (XDGUIUtility.CreateButton(botSprite, 24, 24, style, false))
                    m_vertAlignment = XDVerticalAlignment.Bottom;
            }

        }

        protected virtual void CreateDesignControls()
        {
        }

        protected virtual void CreateBindingControls()
        {
        }

        protected virtual void CreateDivider()
        {
            XDGUIUtility.CreateDivider((int)m_bodyRect.width - (int)m_bodyRect.x);
        }
    }
}
	
