﻿using System;
using System.Security.Cryptography;
using Assets.UnityXD.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UnityXD.Styles
{
    [Serializable]
    public class Style : Bindable
    {
        [SerializeField]
        public ColorLibrary Background;

        [SerializeField]
        public Sprite Disabled;

        [SerializeField]
        public FontClass Font;

        [SerializeField]
        public ColorLibrary Foreground;

        [SerializeField]
        public Sprite Icon;

        [SerializeField]
        public RectOffset IconMargin;

        [SerializeField]
        public Sprite Invalid;

        [SerializeField]
        public Sprite Normal;

        [SerializeField]
        public Sprite Pressed;

        [SerializeField]
        public string StyleName;

        [SerializeField]
        public RectOffset Padding;

        [SerializeField]
        public RectOffset Margin;

        [SerializeField]
        public bool IsDefault;

        [SerializeField]
        public int Width = -1;

        [SerializeField]
        public int Height = -1;

        [SerializeField]
        public void Size(int amt)
        {
            Width = amt;
            Height = amt;
        }

        public override string ToString()
        {
            return StyleName;
        }
    }
}