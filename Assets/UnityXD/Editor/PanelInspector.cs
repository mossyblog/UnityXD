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
	        new XDGUIInspector()
                .Swatch("Fill Color", ref _componentRef.CurrentStyle.FrontFill);
	    }

	    protected override void CreateBindingControls()
	    {
	        base.CreateBindingControls();
	        new XDGUIInspector()
                .Heading("Skins")
                .Sprite("Background", ref _componentRef.BackgroundSprite);
	    }
	}
}

