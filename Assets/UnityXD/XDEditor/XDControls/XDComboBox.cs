﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Assets.UnityXD.Core;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEditor;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{
    /// <summary>
    /// Drop-down selection field.
    /// </summary>
    public interface IXDComboBox : IXDWidget
    {
        /// <summary>
        /// Currently selected item.
        /// </summary>
        IPropertyBinding<object, IXDComboBox> SelectedItem { get; }

        /// <summary>
        /// List of items to display. Names are generated by calling ToString on each item.
        /// </summary>
        IPropertyBinding<object[], IXDComboBox> Items { get; }

        /// <summary>
        /// Optional label to display to the left of the widget.
        /// </summary>
        IPropertyBinding<string, IXDComboBox> Label { get; }

        IXDComboBox Selected(Expression<Func<object>> propertyExpression);
        IXDComboBox Options(object[] items);
    }

    /// <summary>
    /// Drop-down selection field.
    /// </summary>
    internal class XDComboBox : XDWidget, IXDComboBox
    {
        private int selectedIndex;
        private object selectedItem;
        private object[] items;
        private string label;

        private PropertyBinding<object, IXDComboBox> selectedItemProperty;
        private PropertyBinding<object[], IXDComboBox> itemsProperty;
        private PropertyBinding<string, IXDComboBox> labelProperty;

        public IXDComboBox Options(object[] items)
        {            
            return itemsProperty.Value(items);
        }

        public IXDComboBox Selected(Expression<Func<object>> propertyExpression)
        {
            return selectedItemProperty.BindTo(propertyExpression);
        }


        public IPropertyBinding<object, IXDComboBox> SelectedItem { get { return selectedItemProperty; } }
        public IPropertyBinding<object[], IXDComboBox> Items { get { return itemsProperty; } }
        public IPropertyBinding<string, IXDComboBox> Label { get { return labelProperty; } }

        internal XDComboBox(IXDLayout parent) : base(parent)
        {
            itemsProperty = new PropertyBinding<object[], IXDComboBox>(this,value => this.items = value);

            selectedItemProperty = new PropertyBinding<object, IXDComboBox>(this, value =>
            {
                selectedItem = value.ToString();

                if (items != null)
                {
                    selectedIndex = Array.IndexOf(items.Select(i=>i.ToString()).ToArray(), selectedItem);                    
                }
            });

            labelProperty = new PropertyBinding<string, IXDComboBox>(this,value => this.label = value);
        }

        public override void OnGUI()
        {
            var itemStrings = items != null ? items.Select(i => i.ToString()).ToArray() : new string[] { };
            var guiContent = itemStrings.Select(m => new GUIContent(m, tooltip)).ToArray();
            int newIndex;

            if (!string.IsNullOrEmpty(label))
            {
                newIndex = EditorGUILayout.Popup(new GUIContent(label), selectedIndex, guiContent);
                
            }
            else
            {
                newIndex = EditorGUILayout.Popup(selectedIndex, guiContent);
            }
                

            if (newIndex != selectedIndex)
            {
                selectedIndex = newIndex;
                selectedItem = items[selectedIndex];
                selectedItemProperty.UpdateView(selectedItem);
            }
        }

        internal override void BindViewModel(object viewModel)
        {
            base.BindViewModel(viewModel);
            selectedItemProperty.BindViewModel(viewModel);
            itemsProperty.BindViewModel(viewModel);
            labelProperty.BindViewModel(viewModel);
        }
    }
}
