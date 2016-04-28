using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityXD.Components;
using UnityXD.Editor;
using UnityXD.Styles;
using UnityXD.Editor.Controls;

namespace UnityXD.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TileIcon))]
    public class TileIconInspector : UIComponentInspector
    {
        protected TileIcon _tileIconRef;
        protected XDIcons m_currentIcon;
        protected XDVerticalAlignment m_placement;
        protected override void Initialize()
        {
            m_design_backfillEnabled = true;
			m_design_labelAlignment = false;

            if (_tileIconRef == null)
            {
                _tileIconRef = target as TileIcon;
            }

            if (_labelRef == null)
            {
                _labelRef = _tileIconRef.LabelRef as Label;
            }

            base.Initialize();
        }

        protected override void CreateDesignControls()
        {
            base.CreateDesignControls();

            var placementList = new List<String>();
            placementList.Add(XDVerticalAlignment.Top.ToString());
            placementList.Add(XDVerticalAlignment.Bottom.ToString());

            using (new XDGUIPanel(false, groupStyle))
            {
                XDGUI.Create().Label("Icon").Size(64, 22, 92).RenderEnumField(ref m_currentIcon,  null, true);
                EditorGUILayout.Space();
                XDGUI.Create().Label("Placement").Size(64, 22, 64).RenderEnumField(ref m_placement, placementList, true);

            }
            CreateDesignLabelControls();

        }

        protected override void CommitProperties()

        {
            m_layout_paddingEnabled = false;
            
			XDGUIUtility.Bind(ref _tileIconRef.CurrentIcon, ref m_currentIcon);
            XDGUIUtility.Bind(ref _tileIconRef.CurrentStyle, ref m_style);
            XDGUIUtility.Bind(ref _tileIconRef.IconPlacement, ref m_placement);

            
			base.CommitProperties();


            if (GUI.changed)
            {
                _tileIconRef.SetIcon(m_currentIcon);
            }

           

        }
    }
}
