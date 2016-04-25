using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityXD.Components;
using UnityXD.Editor;
using UnityXD.Styles;

namespace Assets.UnityXD.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Label))]

    public class LabelInspector : UIComponentInspector
    {
        protected Label _labelRef;
        protected TextAnchor m_textAlignment;
        protected bool m_autosized;
        protected bool m_truncate;
        protected XDFontSizes m_fontSize;
        protected XDFontStyle m_fontStyle;
        protected XDFontStyleNames m_fontStyleName;
        protected override void Initialize()
        {
            if (_labelRef == null)
            {
                _labelRef = target as Label;
            }

            base.Initialize();
        }

        protected override void CommitProperties()
        {
            base.CommitProperties();

            m_fontStyle = XDTheme.Instance.ResolveFontClass(m_fontStyleName, m_fontSize);
            m_fontStyle.FrontFill = m_style.FrontFill;
            m_fontStyle.Size = m_currentSize;

            XDGUIUtility.Bind(ref _labelRef.Text, ref m_text);
            XDGUIUtility.Bind(ref _labelRef.Alignment, ref m_textAlignment);
            XDGUIUtility.Bind(ref _labelRef.AutoSize, ref m_autosized);
            XDGUIUtility.Bind(ref _labelRef.TruncateToFit, ref m_truncate);
            XDGUIUtility.Bind(ref _labelRef.FontClassData, ref m_fontStyle);
            XDGUIUtility.Bind(ref _labelRef.FontClassData.FontSize, ref m_fontSize);

          

            if (GUI.changed)
            {
                _labelRef.ApplyTheme(m_fontStyle);
            }
        }

        protected override void CreateDesignControls()
        {
            base.CreateDesignControls();
            CreateDivider();

            var filterSizes = new List<String>();
            filterSizes.Add(XDFontSizes.L.ToString());
            filterSizes.Add(XDFontSizes.M.ToString());
            filterSizes.Add(XDFontSizes.S.ToString());

            XDGUIUtility.CreateHeading("Font Settings");
            using (new XDGUILayout(false, XDGUIStyles.Instance.Group))
            {
                XDGUIUtility.CreateTextField("Text", ref m_text, 128, true, true);
                XDGUIUtility.CreateEnumField("Alignment", ref m_textAlignment, (int)m_textAlignment, 128, null);
                XDGUIUtility.CreateSpacer(16);
                XDGUIUtility.CreateEnumField("Size", ref m_fontSize, (int)m_fontSize, 128, filterSizes.ToArray());
                XDGUIUtility.CreateEnumField("Style", ref m_fontStyleName, (int)m_fontStyleName, 128, null);
                XDGUIUtility.CreateSpacer(16);
                using (new XDGUILayout(true))
                {
                    XDGUIUtility.CreateCheckbox("AutoSize",ref m_autosized, XDGUISizes.Medium);
                    XDGUIUtility.CreateCheckbox("Truncate", ref m_truncate, XDGUISizes.Medium);
                }
            }
            

        }
    }
}
