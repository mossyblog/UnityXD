using System;
using UnityEditor;
using UnityXD.Components;
using UnityXD.Editor;

namespace UnityXD.XDGUIEditor
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(Panel))]
	public class PanelInspector : UIComponentInspector
	{
	    protected override void Initialize()
	    {
	        _componentRef = target as Panel;
	        base.Initialize();
	    }

	    protected override void CreateDesignControls()
	    {
	        base.CreateDesignControls();
	        XDGUIStyleInspector.Create(ref _componentRef)
	            .FillColor();
	    }
	}
}

