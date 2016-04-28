﻿using System;
using UnityXD.Components;
using UnityEngine;
using UnityEngine.UI;
using UnityXD.Styles;

namespace UnityXD.Components
{
	[RequireComponent(typeof(Image))]
	public class Panel : UIComponent
	{
		protected override void CommitProperties ()
		{
			base.CommitProperties ();
			ImageRef.color = CurrentStyle.FrontFill.ToColor();
		}
		
	}
}
