using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{
    /// <summary>
    /// Inserts a space between other widgets.
    /// </summary>
    interface IXDSpacer : IXDWidget
    {

    }

    /// <summary>
    /// Inserts a space between other widgets.
    /// </summary>
    internal class XDSpacer : XDWidget, IXDSpacer
    {
        private int _amt;

        internal XDSpacer(IXDLayout parent, int amt) : base(parent)
        {
            _amt = amt;
        }

        public override void OnGUI()
        {
            if (_amt > 0)
            {
                GUILayout.Space(_amt);
               
            }
            else
            {
                EditorGUILayout.Space();
            }
            
        }

        internal override void BindViewModel(object viewModel)
        {

        }
    }
}
