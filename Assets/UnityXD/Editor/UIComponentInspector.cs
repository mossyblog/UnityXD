using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine.UI;
using UnityXD.Components;
using UnityXD.Styles;

namespace UnityXD.Editor
{
	[CanEditMultipleObjects]
	[CustomEditor (typeof(UIComponent))]
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

		public void OnEnable ()
		{
			if (_componentRef == null)
				_componentRef = (UIComponent)target;

			if (_labelRef == null) {
				_labelRef = target as Label;
			}

			m_hidecomponents = EditorPrefs.GetBool ("hidecomponents_" + _componentRef.GetInstanceID (), true);
			m_currentSelectedTab = EditorPrefs.GetString ("currentTab_" + _componentRef.GetInstanceID (), m_currentSelectedTab);
		}

		public void OnDestroy ()
		{
			EditorPrefs.SetBool ("hidecomponents_" + _componentRef.GetInstanceID (), m_hidecomponents);
			EditorPrefs.SetString ("currentTab_" + _componentRef.GetInstanceID (), m_currentSelectedTab);
		}

		public override void OnInspectorGUI ()
		{
			Initialize ();
			GeneratateInspector ();
			CommitProperties ();
		}

		protected virtual void Initialize ()
		{

			// For some reason the Play/Stop causes the Styles to act weird.
			m_horizAlignment = _componentRef.CurrentAnchorAlignment.ToHorizontalAlignment ();
			m_vertAlignment = _componentRef.CurrentAnchorAlignment.ToVerticalAlignment ();

			m_isAnchorRightEnabled = XDHorizontalAlignment.Right == m_horizAlignment;
			m_isAnchorLeftEnabled = XDHorizontalAlignment.Left == m_horizAlignment;
			m_isAnchorMiddleHEnabled = XDHorizontalAlignment.Center == m_horizAlignment;
			m_isAnchorTopEnabled = XDVerticalAlignment.Top == m_vertAlignment;
			m_isAnchorMiddleVEnabled = XDVerticalAlignment.Center == m_vertAlignment;
			m_isAnchorBottomEnabled = XDVerticalAlignment.Bottom == m_vertAlignment;
			m_isAnchorHStretched = _componentRef.IsHorizontalStretchEnabled;
			m_isAnchorVStretched = _componentRef.IsVeritcalStretchEnabled;

			m_margin = _componentRef.Margin;
			m_padding = _componentRef.Padding;
			m_style = _componentRef.CurrentStyle;
			m_currentSize = _componentRef.CurrentStyle.Size;
			m_fontSize = _componentRef.CurrentStyle.FontStyle.FontSize;
			m_fontStyleName = _componentRef.CurrentStyle.FontStyle.StyleName;
		}

		protected virtual void CommitProperties ()
		{

			XDGUIUtility.Bind (ref _componentRef.Width, ref m_width);
			XDGUIUtility.Bind (ref _componentRef.Height, ref m_height);
			XDGUIUtility.Bind (ref _componentRef.X, ref m_x);
			XDGUIUtility.Bind (ref _componentRef.Y, ref m_y);
			XDGUIUtility.Bind (ref _componentRef.IsHeightDependantOnWidth, ref m_isWLinkedToH);
			XDGUIUtility.Bind (ref _componentRef.Padding, ref m_padding);
			XDGUIUtility.Bind (ref _componentRef.Margin, ref m_margin);
			XDGUIUtility.Bind (ref _componentRef.CurrentStyle, ref m_style);
			XDGUIUtility.Bind (ref _componentRef.CurrentStyle.Size, ref m_currentSize);
            
			if (_labelRef != null) {
				
				m_style.FontStyle = XDTheme.Instance.ResolveFontClass (m_fontStyleName, m_fontSize);

				XDGUIUtility.Bind (ref _labelRef.Text, ref m_text);
				XDGUIUtility.Bind (ref _labelRef.Alignment, ref m_textAlignment);
				XDGUIUtility.Bind (ref _labelRef.AutoSize, ref m_autosized);
				XDGUIUtility.Bind (ref _labelRef.TruncateToFit, ref m_truncate);
				XDGUIUtility.Bind (ref _labelRef.CurrentStyle.FontStyle.FontSize, ref m_fontSize);
				XDGUIUtility.Bind (ref _labelRef.CurrentStyle.FontStyle.StyleName, ref m_fontStyleName);


			}

			if (GUI.changed) {
				// Dock It.
				var align = XDThemeUtility.ToAlignment (m_horizAlignment, m_vertAlignment);

				_componentRef.Dock (align, m_isAnchorHStretched, m_isAnchorVStretched);
				_componentRef.SetMargin (m_margin);
				_componentRef.SetPadding (m_padding);
				_componentRef.ApplyTheme (m_style);


			}
			EditorUtility.SetDirty (target);
		}

        

		protected virtual void GeneratateInspector ()
		{

			XDGUIStyles.Instance.InitStyles ();

			var body = XDGUIUtility.CreateSpacer ();

			if (body.width > 0)
				m_bodyRect = body;
			
			CreateTabBar ();		
			using (var rect = new XDGUILayout (false, XDGUIStyles.Instance.Body)) {
				if (!_componentRef.IsChildReadOnly) {
					
					if (m_currentSelectedTab == m_labelLayout)
						CreateLayoutControls ();

					if (m_currentSelectedTab == m_labelDesign)
						CreateDesignControls ();

					if (m_currentSelectedTab == m_labelBinding)
						CreateBindingControls ();
					
				} else {

					var sprite = Resources.Load<Sprite> ("EditorChrome/ChildReadOnlyMode");
					var pos = XDGUIUtility.CreateEmptyPlaceHolder (128, 128, false);

					pos.x = (EditorGUIUtility.currentViewWidth / 2) - 64;
					XDGUIUtility.CreateSpritePreview (pos, sprite);

				}
			}
			
			XDGUIUtility.CreateSpacer ();
		}

		protected virtual void CreateTabBar ()
		{
			XDGUIUtility.CreateTabBar (m_tabs, ref m_currentSelectedTab);

		}

		protected virtual void CreateLayoutControls ()
		{
			XDGUIUtility.CreateSpacer ();
			CreateAnchorToolBar ();     
			CreateDivider ();

			var groupStyle = XDGUIStyles.Instance.Group;
          

			// SWITCHES.
			var w_enabled = m_currentSize == XDSizes.Custom && !m_isAnchorHStretched;
			var h_enabled = m_currentSize == XDSizes.Custom && !m_isAnchorVStretched && !m_isWLinkedToH;
			var link_enabled = w_enabled && !m_isAnchorHStretched && !m_isAnchorVStretched;

			/*********************************************************************************
                SIZES.            
            *********************************************************************************/

			if (m_layout_sizelistEnabled) {
				using (new XDGUILayout (false, groupStyle)) {
					XDGUIUtility.CreateEnumField ("Size", ref m_currentSize, (int)m_currentSize, 128, null);
				}
			}

			using (new XDGUILayout (true, groupStyle, GUILayout.Width (64))) {
               
				using (new XDGUILayout (false)) {
					XDGUIUtility.CreateTextField ("W", ref m_width, XDGUISizes.Small, true, w_enabled);
					XDGUIUtility.CreateTextField ("H", ref m_height, XDGUISizes.Small, true, h_enabled);                    
				}   
				using (new XDGUILayout (false, GUILayout.Width (48))) {
					GUILayout.Space (3);
					var linkSprite = Resources.Load<Sprite> ("Icons/Editor/SizeLink_" + (m_isWLinkedToH ? "On" : "Off"));
					if (XDGUIUtility.CreateButton (linkSprite, 24, 48, GUIStyle.none, false, link_enabled)) {
						m_isWLinkedToH = !m_isWLinkedToH;
					}                    
                    
				}
				using (new XDGUILayout (false)) {
					XDGUIUtility.CreateTextField ("X", ref m_x, XDGUISizes.Small, true);
					XDGUIUtility.CreateTextField ("Y", ref m_y, XDGUISizes.Small, true);
				}
			}

			if (m_layout_paddingEnabled) {
				/*********************************************************************************
                    Padding
                *********************************************************************************/
				using (new XDGUILayout (false, groupStyle)) {
					XDGUIUtility.CreateHeading ("Padding");
				}
				using (new XDGUILayout (true, groupStyle)) {
					var left = m_padding.left;
					var right = m_padding.right;
					var top = m_padding.top;
					var bot = m_padding.bottom;

					XDGUIUtility.CreateTextField ("Left", ref left, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);
					XDGUIUtility.CreateTextField ("Right", ref right, XDGUISizes.Small, false, true,
						TextAnchor.LowerCenter);
					XDGUIUtility.CreateTextField ("Top", ref top, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);
					XDGUIUtility.CreateTextField ("Bottom", ref bot, XDGUISizes.Small, false, true,
						TextAnchor.LowerCenter);

					m_padding = new RectOffset (left, right, top, bot);
				}
			}

			/*********************************************************************************
                Margin
            *********************************************************************************/
			using (new XDGUILayout (false, groupStyle)) {
				XDGUIUtility.CreateHeading ("Margins");
			}

			using (new XDGUILayout (true, groupStyle)) {
				var left = m_margin.left;
				var right = m_margin.right;
				var top = m_margin.top;
				var bot = m_margin.bottom;

				XDGUIUtility.CreateTextField ("Left", ref left, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);
				XDGUIUtility.CreateTextField ("Right", ref right, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);
				XDGUIUtility.CreateTextField ("Top", ref top, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);
				XDGUIUtility.CreateTextField ("Bottom", ref bot, XDGUISizes.Small, false, true, TextAnchor.LowerCenter);

				m_margin = new RectOffset (left, right, top, bot);
			}
		}

		protected virtual void CreateAnchorToolBar ()
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

			var leftSprite = Resources.Load<Sprite> (path + horizPrefix + leftName);
			var midHSprite = Resources.Load<Sprite> (path + horizPrefix + midHName);
			var rightSprite = Resources.Load<Sprite> (path + horizPrefix + rightName);
            
			var topSprite = Resources.Load<Sprite> (path + vertPrefix + topName);
			var midVSprite = Resources.Load<Sprite> (path + vertPrefix + midVName);
			var botSprite = Resources.Load<Sprite> (path + vertPrefix + botName);

			var horizStretchSprite = Resources.Load<Sprite> (path + "StretchHoriz_" + (m_isAnchorHStretched ? onState : offState));
			var vertStretchSprite = Resources.Load<Sprite> (path + "StretchVert_" + (m_isAnchorVStretched ? onState : offState));

			var style = new GUIStyle (GUIStyle.none);
			style.normal.background = XDGUIUtility.CreateColoredTexture (Color.white);
			style.alignment = TextAnchor.MiddleCenter;
			style.margin = new RectOffset (3, 3, 3, 3);

			using (new XDGUILayout (true, style, GUILayout.Width (128))) {
				XDGUIUtility.CreateSpacer (16);
				if (XDGUIUtility.CreateButton (horizStretchSprite, 24, 24, style, false)) {
					m_isAnchorHStretched = !m_isAnchorHStretched;
				}
				if (XDGUIUtility.CreateButton (vertStretchSprite, 24, 24, style, false)) {
					m_isAnchorVStretched = !m_isAnchorVStretched;
				}
				XDGUIUtility.CreateSpacer (16);

				if (XDGUIUtility.CreateButton (leftSprite, 24, 24, style, false))
					m_horizAlignment = XDHorizontalAlignment.Left;

				if (XDGUIUtility.CreateButton (midHSprite, 24, 24, style, false))
					m_horizAlignment = XDHorizontalAlignment.Center;

				if (XDGUIUtility.CreateButton (rightSprite, 24, 24, style, false))
					m_horizAlignment = XDHorizontalAlignment.Right;

				XDGUIUtility.CreateSpacer (16);

				if (XDGUIUtility.CreateButton (topSprite, 24, 24, style, false))
					m_vertAlignment = XDVerticalAlignment.Top;

				if (XDGUIUtility.CreateButton (midVSprite, 24, 24, style, false))
					m_vertAlignment = XDVerticalAlignment.Center;

				if (XDGUIUtility.CreateButton (botSprite, 24, 24, style, false))
					m_vertAlignment = XDVerticalAlignment.Bottom;
			}

		}

		protected virtual void CreateDesignControls ()
		{
			var groupStyle = XDGUIStyles.Instance.Group;
          
			// Chrome Row
			var ChromeColors = Enum.GetNames (typeof(XDColors))
                .OrderBy (x => x)
                .Where (s => s.ToLower ().Contains (XDColorTypes.Chrome.ToString ().ToLower ()))
                .ToList ();

			var BrandColors = Enum.GetNames (typeof(XDColors))
                .OrderBy (x => x)
                .Where (s => s.ToLower ().Contains (XDColorTypes.Brand.ToString ().ToLower ()))
                .ToList ();

			var AccentColors = Enum.GetNames (typeof(XDColors))
                     .OrderBy (x => x)
                     .Where (s => s.ToLower ().Contains (XDColorTypes.Accent.ToString ().ToLower ()))
                     .ToList ();

			if (m_design_fillEnabled) {
				XDGUIUtility.CreateSpacer (8);
				XDGUIUtility.CreateHeading ("Fill Color");
				using (new XDGUILayout (true, groupStyle)) {
					// Preview Swatch.
					XDGUIUtility.CreateSwatch (m_style.FrontFill.ToColor (), 48, true);
					XDGUIUtility.CreateSpacer (16);
					using (new XDGUILayout (false)) {
						XDGUIUtility.CreateSwatchRow (AccentColors.ToArray (), 16, ref m_style.FrontFill);
						XDGUIUtility.CreateSwatchRow (BrandColors.ToArray (), 16, ref m_style.FrontFill);
						XDGUIUtility.CreateSwatchRow (ChromeColors.ToArray (), 16, ref m_style.FrontFill);
					}

				}

				XDGUIUtility.CreateSpacer (16);
			}

			if (m_design_backfillEnabled) {
				XDGUIUtility.CreateHeading ("Back Fill Color");
				using (new XDGUILayout (true, groupStyle)) {
					// Preview Swatch.
					XDGUIUtility.CreateSwatch (m_style.BackFill.ToColor (), 48, true);
					XDGUIUtility.CreateSpacer (16);
					using (new XDGUILayout (false)) {
						XDGUIUtility.CreateSwatchRow (AccentColors.ToArray (), 16, ref m_style.BackFill);
						XDGUIUtility.CreateSwatchRow (BrandColors.ToArray (), 16, ref m_style.BackFill);
						XDGUIUtility.CreateSwatchRow (ChromeColors.ToArray (), 16, ref m_style.BackFill);
					}

				}
			}
		}

		protected virtual void CreateDesignLabelControls ()
		{
			CreateDivider ();

			var filterSizes = new List<String> ();
			filterSizes.Add (XDFontSizes.L.ToString ());
			filterSizes.Add (XDFontSizes.M.ToString ());
			filterSizes.Add (XDFontSizes.S.ToString ());

			XDGUIUtility.CreateHeading ("Font Settings");
			using (new XDGUILayout (false, XDGUIStyles.Instance.Group)) {
				XDGUIUtility.CreateTextField ("Text", ref m_text, 128, true, true);
				if (m_design_labelAlignment)
					XDGUIUtility.CreateEnumField ("Alignment", ref m_textAlignment, (int)m_textAlignment, 128, null);
				
				XDGUIUtility.CreateSpacer (16);
				XDGUIUtility.CreateEnumField ("Size", ref m_fontSize, (int)m_fontSize, 128, filterSizes.ToArray ());
				XDGUIUtility.CreateEnumField ("Style", ref m_fontStyleName, (int)m_fontStyleName, 128, null);
				XDGUIUtility.CreateSpacer (16);
				using (new XDGUILayout (true)) {
					XDGUIUtility.CreateCheckbox ("AutoSize", ref m_autosized, XDGUISizes.Medium);
					XDGUIUtility.CreateCheckbox ("Truncate", ref m_truncate, XDGUISizes.Medium);
				}
			}
		}

		protected virtual void CreateBindingControls ()
		{
		}

		protected virtual void CreateDivider ()
		{
			XDGUIUtility.CreateDivider ((int)m_bodyRect.width - (int)m_bodyRect.x);
		}
	}
}
	
