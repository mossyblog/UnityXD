using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.Components;
using UnityEditor;
using UnityEngine;
using UnityXD.Components;
using UnityXD.Editor;
using UnityXD.Styles;

namespace Assets.UnityXD.Editor
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

            using (new XDGUILayout(false, XDGUIStyles.Instance.Group))
            {
                XDGUIUtility.CreateEnumField("Icon", ref m_currentIcon, (int)m_currentIcon, 128, null);
                XDGUIUtility.CreateEnumField("Placement", ref m_placement, (int)m_placement, 128, placementList.ToArray());
            }
            CreateDesignLabelControls();

        }

        protected override void CommitProperties()

        {
            m_layout_paddingEnabled = false;
            XDGUIUtility.Bind(ref _tileIconRef.CurrentIcon, ref m_currentIcon);
            XDGUIUtility.Bind(ref _tileIconRef.CurrentStyle, ref m_style);
            XDGUIUtility.Bind(ref _tileIconRef.IconPlacement, ref m_placement);

            CommitLabelProperties();
            base.CommitProperties();

            if (GUI.changed)
            {
                _tileIconRef.SetIcon(m_currentIcon);
            }
           

        }
    }
}
