using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UI;
using UnityXD.Components;
using UnityXD.Styles;
using UnityXD.Editor.Controls;

namespace UnityXD.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UIComponent))]
    public class UIComponentInspector : UnityEditor.Editor
    {
        protected UIComponent _componentRef;

        // LABEL SPECIFIC
        protected Label _labelRef;
        protected string m_text;
        protected TextAnchor m_textAlignment;
        protected bool m_autosized;
        protected bool m_truncate;
        protected XDFontSizes m_fontSize;
        protected XDFontStyleNames m_fontStyleName;

        protected bool m_isVisible;
        protected bool m_hidecomponents;

        // Inspector Show / Hide Switches.
        protected bool m_design_backfillEnabled;
        protected bool m_design_fillEnabled = true;
        protected bool m_design_labelEnabled = false;
        protected bool m_design_labelAlignment = true;
        protected bool m_layout_paddingEnabled = true;
        protected bool m_layout_sizelistEnabled = true;


        // Inspector Tabs.
        protected const string m_labelLayout = "Layout";
        protected const string m_labelDesign = "Design";
        protected const string m_labelBinding = "Binding";
        protected string[] m_tabs = { m_labelLayout, m_labelDesign, m_labelBinding };
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

        // Anchor Metrics.
        protected int m_width;
        protected int m_height;
        protected int m_x;
        protected int m_y;
        protected bool m_isWLinkedToH;
        protected XDSizes m_currentSize;
        protected RectOffset m_padding;
        protected RectOffset m_margin;
        protected XDStyle m_style;
        protected Rect m_bodyRect;

        protected GUIStyle groupStyle = XDGUIStyles.Instance.Group;
        protected int Col1 = 56;
        protected int Col2 = 56;
        protected int Col3 = 56;

        protected int fieldH = 22;

        public void OnEnable()
        {

            if (_componentRef == null)
                _componentRef = (UIComponent)target;

            if (_labelRef == null)
            {
                _labelRef = target as Label;
            }

            m_hidecomponents = EditorPrefs.GetBool("hidecomponents_" + _componentRef.GetInstanceID(), true);
            m_currentSelectedTab = EditorPrefs.GetString("currentTab_" + _componentRef.GetInstanceID(), m_currentSelectedTab);
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
            CommitProperties();
          
        }

        protected virtual void Initialize()
        {

            m_margin = _componentRef.Margin;
            m_padding = _componentRef.Padding;
            m_style = _componentRef.CurrentStyle;
            m_currentSize = _componentRef.CurrentStyle.Size;
            m_fontSize = _componentRef.CurrentStyle.FontStyle.FontSize;
            m_fontStyleName = _componentRef.CurrentStyle.FontStyle.StyleName;
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
            XDGUIUtility.Bind(ref _componentRef.CurrentStyle, ref m_style);
            XDGUIUtility.Bind(ref _componentRef.CurrentStyle.Size, ref m_currentSize);
            
            if (_labelRef != null)
            {				
                m_style.FontStyle = XDTheme.Instance.ResolveFontClass(m_fontStyleName, m_fontSize);
                XDGUIUtility.Bind(ref _labelRef.Text, ref m_text);
                XDGUIUtility.Bind(ref _labelRef.Alignment, ref m_textAlignment);
                XDGUIUtility.Bind(ref _labelRef.AutoSize, ref m_autosized);
                XDGUIUtility.Bind(ref _labelRef.TruncateToFit, ref m_truncate);
                XDGUIUtility.Bind(ref _labelRef.CurrentStyle.FontStyle.FontSize, ref m_fontSize);
                XDGUIUtility.Bind(ref _labelRef.CurrentStyle.FontStyle.StyleName, ref m_fontStyleName);
            }

            if (GUI.changed)
            {
                // Dock It.
                var align = XDThemeUtility.ToAlignment(m_horizAlignment, m_vertAlignment);

//				_componentRef.Dock (align, m_isAnchorHStretched, m_isAnchorVStretched);
                _componentRef.SetMargin(m_margin);
                _componentRef.SetPadding(m_padding);
                _componentRef.ApplyTheme(m_style);
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

                    pos.x = (EditorGUIUtility.currentViewWidth / 2) - 64;
                    XDGUIUtility.CreateSpritePreview(pos, sprite);

                }
            }
         

        }

        protected virtual void CreateTabBar()
        {
            XDGUIUtility.CreateTabBar(m_tabs, ref m_currentSelectedTab);

        }

        protected virtual void CreateLayoutControls()
        {
    
            XDGUI.Create(ref _componentRef).RenderToolBar();
            CreateDivider();

            // SWITCHES.
            var w_enabled = m_currentSize == XDSizes.Custom && !m_isAnchorHStretched;
            var h_enabled = m_currentSize == XDSizes.Custom && !m_isAnchorVStretched && !m_isWLinkedToH;
            var link_enabled = w_enabled && !m_isAnchorHStretched && !m_isAnchorVStretched;

            /*********************************************************************************
                SIZES.            
            *********************************************************************************/

            if (m_layout_sizelistEnabled)
            {
                using (new XDGUIPanel(false, groupStyle))
                {                    
                    XDGUI.Create().Label("Size").Size(64, fieldH, 64).RenderEnumField(ref m_currentSize, null, true);
                   
                }
            }

            using (new XDGUIPanel(true, groupStyle))
            {
               
                using (new XDGUIPanel(false, GUILayout.MaxWidth(64)))
                {
                    XDGUI.Create().Label("W").Size(16, 22, 48).RenderTextField(ref m_width, true, w_enabled);
                    EditorGUILayout.Space();
                    XDGUI.Create().Label("H").Size(16, 22, 48).RenderTextField(ref m_height, true, h_enabled);
                }   

                using (new XDGUIPanel(false, GUILayout.Width(24)))
                {
                    GUILayout.Space(8);
                    var isLinkSprite = Resources.Load<Sprite>("Icons/Editor/SizeLink_" + (m_isWLinkedToH ? "On" : "Off"));
                    XDGUI.Create().Sprite(isLinkSprite).Size(24, 48).Style(GUIStyle.none).RenderButton(ref m_isWLinkedToH, link_enabled);                   
                }

                GUILayout.FlexibleSpace();

                using (new XDGUIPanel(false))
                {
                    XDGUI.Create().Label("X").Size(16, 22, 48).RenderTextField(ref m_x, true);
                    EditorGUILayout.Space();
                    XDGUI.Create().Label("Y").Size(16, 22, 48).RenderTextField(ref m_y, true);
                }
            }
            EditorGUILayout.Space();

            if (m_layout_paddingEnabled)
            {
                
                /*********************************************************************************
                    Padding
                *********************************************************************************/
                XDGUI.Create().Label("Padding").Style(XDGUIStyles.Instance.Heading).RenderLabel();
                using (new XDGUIPanel(true, groupStyle))
                {
                    var left = m_padding.left;
                    var right = m_padding.right;
                    var top = m_padding.top;
                    var bot = m_padding.bottom;

                    XDGUI.Create().Label("Left").RenderTextField(ref left, false);
                    XDGUI.Create().Label("Right").RenderTextField(ref right, false);
                    XDGUI.Create().Label("Top").RenderTextField(ref top, false);
                    XDGUI.Create().Label("Bottom").RenderTextField(ref bot, false);

                    m_padding = new RectOffset(left, right, top, bot);
                }
                EditorGUILayout.Space();
            }

            /*********************************************************************************
                Margin
            *********************************************************************************/
			
            XDGUI.Create().Label("Margins").Style(XDGUIStyles.Instance.Heading).RenderLabel();
            using (new XDGUIPanel(true, groupStyle))
            {
                var left = m_margin.left;
                var right = m_margin.right;
                var top = m_margin.top;
                var bot = m_margin.bottom;
                XDGUI.Create().Label("Left").RenderTextField(ref left, false);
                XDGUI.Create().Label("Right").RenderTextField(ref right, false);
                XDGUI.Create().Label("Top").RenderTextField(ref top, false);
                XDGUI.Create().Label("Bottom").RenderTextField(ref bot, false);
                m_margin = new RectOffset(left, right, top, bot);
            }
        }

        protected virtual void CreateDesignControls()
        {
            XDGUI.Create().RenderSwatchPicker(ref m_style, m_design_fillEnabled, m_design_backfillEnabled);
        }

        protected virtual void CreateDesignLabelControls()
        {
            var filterSizes = new List<String>();
            filterSizes.Add(XDFontSizes.L.ToString());
            filterSizes.Add(XDFontSizes.M.ToString());
            filterSizes.Add(XDFontSizes.S.ToString());

            EditorGUILayout.Space();
            XDGUI.Create().Label("Font Settings").Style(XDGUIStyles.Instance.Heading).RenderLabel();
            EditorGUILayout.Space();

            using (new XDGUIPanel(false))
            {
                XDGUI.Create().Label("Text").Size(Col1, fieldH).RenderTextField(ref m_text, true);
                EditorGUILayout.Space();
                if (m_design_labelAlignment)
                    XDGUI.Create().Label("Alignment").Size(Col1, fieldH, 64).RenderEnumField(ref m_textAlignment, null, true);
                EditorGUILayout.Space();
                using (new GUILayout.HorizontalScope())
                {
                    XDGUI.Create().Label("Size").Size(Col1, fieldH, 48).RenderEnumField(ref m_fontSize, filterSizes, true);
                    XDGUI.Create().Label("Style").Size(Col2, fieldH, 64).RenderEnumField(ref m_fontStyleName, null, true);
                }
                EditorGUILayout.Space();
                using (new XDGUIPanel(false))
                {
                    XDGUI.Create().Label("AutoSize").Size(Col1, fieldH, 16, 16).RenderCheckBox(ref m_autosized, true);
                    EditorGUILayout.Space();

                    XDGUI.Create().Label("Truncate").Size(Col2, fieldH, 16, 16).RenderCheckBox(ref m_truncate, true);
                }
            }
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
	
