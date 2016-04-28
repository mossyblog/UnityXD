using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityXD.Components;
using UnityXD.Styles;
using UnityXD.XDGUIEditor;

namespace UnityXD.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof (UIComponent))]
    public class UIComponentInspector : UnityEditor.Editor
    {
        // Inspector Tabs.
        protected const string m_labelLayout = "Layout";
        protected const string m_labelDesign = "Design";
        protected const string m_labelBinding = "Binding";
        protected UIComponent _componentRef;

        // LABEL SPECIFIC
        protected Label _labelRef;
        protected int Col1 = 56;
        protected int Col2 = 56;
        protected int Col3 = 56;

        protected int fieldH = 22;

        protected GUIStyle groupStyle = XDGUIStyles.Instance.Panel;
        protected bool m_autosized;
        protected Rect m_bodyRect;
        protected string m_currentSelectedTab = m_labelLayout;
        protected XDSizes m_currentSize;

        // Inspector Show / Hide Switches.
        protected bool m_design_backfillEnabled;
        protected bool m_design_fillEnabled = true;
        protected bool m_design_labelAlignment = true;
        protected bool m_design_labelEnabled = false;
        protected XDFontSizes m_fontSize;
        protected XDFontStyleNames m_fontStyleName;
        protected int m_height;
        protected bool m_hidecomponents;
        protected XDHorizontalAlignment m_horizAlignment;
        protected bool m_isAnchorBottomEnabled;
        protected bool m_isAnchorHStretched;

        // Anchor Alignment Config.
        protected bool m_isAnchorLeftEnabled;
        protected bool m_isAnchorMiddleHEnabled;
        protected bool m_isAnchorMiddleVEnabled;
        protected bool m_isAnchorRightEnabled;
        protected bool m_isAnchorTopEnabled;
        protected bool m_isAnchorVStretched;

        protected bool m_isVisible;
        protected bool m_isWLinkedToH;
        protected bool m_layout_paddingEnabled = true;
        protected bool m_layout_sizelistEnabled = true;
        protected RectOffset m_margin;
        protected RectOffset m_padding;
        protected XDStyle m_style;
        protected string[] m_tabs = {m_labelLayout, m_labelDesign, m_labelBinding};
        protected string m_text;
        protected TextAnchor m_textAlignment;
        protected bool m_truncate;
        protected XDVerticalAlignment m_vertAlignment;

        // Anchor Metrics.
        protected int m_width;
        protected int m_x;
        protected int m_y;

        public void OnEnable()
        {
            if (_componentRef == null)
                _componentRef = (UIComponent) target;

            if (_labelRef == null)
            {
                _labelRef = target as Label;
            }

            m_hidecomponents = EditorPrefs.GetBool("hidecomponents_" + _componentRef.GetInstanceID(), true);
            m_currentSelectedTab = EditorPrefs.GetString("currentTab_" + _componentRef.GetInstanceID(),
                m_currentSelectedTab);
        }

        public void OnDestroy()
        {
            EditorPrefs.SetBool("hidecomponents_" + _componentRef.GetInstanceID(), m_hidecomponents);
            EditorPrefs.SetString("currentTab_" + _componentRef.GetInstanceID(), m_currentSelectedTab);
        }

        public override void OnInspectorGUI()
        {
            Initialize();
            GeneratateInspector();
            //CommitProperties();
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void CommitProperties()
        {
            EditorUtility.SetDirty(target);
        }


        protected virtual void GeneratateInspector()
        {
            XDGUIStyles.Instance.InitStyles();

            var body = XDGUIUtility.CreateSpacer();

            if (body.width > 0)
                m_bodyRect = body;

            CreateTabBar();
            using (var rect = new XDGUIPanel(false, XDGUIStyles.Instance.Body))
            {
                if (!_componentRef.IsChildReadOnly)
                {
                    if (m_currentSelectedTab == m_labelLayout)
                        CreateLayoutControls();

                    if (m_currentSelectedTab == m_labelDesign)
                        CreateDesignControls();

                    if (m_currentSelectedTab == m_labelBinding)
                        CreateBindingControls();

                    GUILayout.FlexibleSpace();
                }
                else
                {
                    var sprite = Resources.Load<Sprite>("EditorChrome/ChildReadOnlyMode");
                    var pos = XDGUIUtility.CreateEmptyPlaceHolder(128, 128, false);

                    pos.x = EditorGUIUtility.currentViewWidth/2 - 64;
                    XDGUIUtility.CreateSpritePreview(pos, sprite);
                }
            }

            _componentRef.InvalidateDisplay();
        }

        protected virtual void CreateTabBar()
        {
            XDGUIUtility.CreateTabBar(m_tabs, ref m_currentSelectedTab);
        }

        protected virtual void CreateLayoutControls()
        {
            XDGUIAnchorInspector.Create(ref _componentRef)
                .AnchorToolBar()
                .Metrics()
                .Padding()
                .Margins();
        }

        protected virtual void CreateDesignControls()
        {           
                        
        }
        
        protected virtual void CreateBindingControls()
        {
        }

        protected virtual void CreateDivider()
        {
            XDGUIUtility.CreateDivider((int) m_bodyRect.width - (int) m_bodyRect.x);
        }
    }
}