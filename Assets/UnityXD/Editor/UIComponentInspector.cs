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

            var horizAlign = _componentRef.CurrentAnchorAlignment.ToHorizontalAlignment();
            var vertAlign = _componentRef.CurrentAnchorAlignment.ToVerticalAlignment();

            m_text = _componentRef.Text;
            m_isVisible = _componentRef.IsVisible;

            m_isAnchorRightEnabled = XDHorizontalAlignment.Right == horizAlign;
            m_isAnchorLeftEnabled = XDHorizontalAlignment.Left== horizAlign;
            m_isAnchorMiddleHEnabled = XDHorizontalAlignment.Center == horizAlign;

            m_isAnchorTopEnabled = XDVerticalAlignment.Top == vertAlign;
            m_isAnchorMiddleVEnabled = XDVerticalAlignment.Center == vertAlign;
            m_isAnchorBottomEnabled = XDVerticalAlignment.Bottom == vertAlign;

            m_isAnchorHStretched = _componentRef.IsHorizontalStretchEnabled;
            m_isAnchorVStretched = _componentRef.IsVeritcalStretchEnabled;

        }

        

        protected virtual void CommitProperties()
        {
            if (GUI.changed && _componentRef != null)
            {
                _componentRef.Text = m_text;
                _componentRef.IsVisible = m_isVisible;
                _componentRef.Dock(XDThemeUtility.ToAlignment(m_horizAlignment, m_vertAlignment), m_isAnchorHStretched, m_isAnchorVStretched);

            }
            EditorUtility.SetDirty(target);
        }

        protected virtual void GeneratateInspector()
        {

            
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

            using (new XDGUILayout(true, style))
            {
                if (XDGUIUtility.CreateButton(horizStretchSprite, 24, 24, style, false))
                {
                    m_isAnchorHStretched = !m_isAnchorHStretched;
                }
                if (XDGUIUtility.CreateButton(vertStretchSprite, 24, 24, style, false))
                {
                    m_isAnchorVStretched = !m_isAnchorVStretched;
                }
                XDGUIUtility.CreateSpacer();
                m_horizAlignment = XDGUIUtility.CreateButton(leftSprite, 24, 24, style, false) ? XDHorizontalAlignment.Left : m_horizAlignment;
                m_horizAlignment = XDGUIUtility.CreateButton(midHSprite, 24, 24, style, false) ? XDHorizontalAlignment.Center : m_horizAlignment;
                m_horizAlignment = XDGUIUtility.CreateButton(rightSprite, 24, 24, style, false) ? XDHorizontalAlignment.Right : m_horizAlignment;
                XDGUIUtility.CreateSpacer();
                m_vertAlignment = XDGUIUtility.CreateButton(topSprite, 24, 24, style, false) ? XDVerticalAlignment.Top : m_vertAlignment;
                m_vertAlignment = XDGUIUtility.CreateButton(midVSprite, 24, 24, style, false) ? XDVerticalAlignment.Center : m_vertAlignment;
                m_vertAlignment = XDGUIUtility.CreateButton(botSprite, 24, 24, style, false) ? XDVerticalAlignment.Bottom : m_vertAlignment;

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
	
