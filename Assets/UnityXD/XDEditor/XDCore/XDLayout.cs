using System.Collections.Generic;
using Assets.UnityXD.Core;
using Assets.UnityXD.XDEditor.XDControls;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDCore
{
    /// <summary>
    /// Layouts are widgets that can contain other child widgets. All layouts should inherit from XDLayout.
    /// </summary>
    internal abstract class XDLayout : XDWidget, IXDLayout
    {
        protected bool enabled = true;

        private PropertyBinding<bool, IXDLayout> enabledProperty;

        /// <summary>
        /// Whether or not to draw this layout and its sub-widgets (default is true).
        /// </summary>
        public IPropertyBinding<bool, IXDLayout> Enabled { get { return enabledProperty; } }

        private List<XDWidget> children = new List<XDWidget>();

        protected XDLayout(IXDLayout parent) : base(parent)
        {
            enabledProperty = new PropertyBinding<bool, IXDLayout>(this,value => this.enabled = value);
        }

        public override void OnGUI()
        {
            children.ForEach(child => child.OnGUI());
        }

        internal override void BindViewModel(object viewModel)
        {
            enabledProperty.BindViewModel(viewModel);
            children.ForEach(child => child.BindViewModel(viewModel));
        }

        /// <summary>
        /// Creates a new button and adds it to the layout.
        /// </summary>
        public IXDAnchorToolbar AnchorToolbar()
        {
            var newtoolbar = new XDAnchorToolbar(this);
            children.Add(newtoolbar);
            return newtoolbar;
        }

        /// <summary>
        /// Creates a new button and adds it to the layout.
        /// </summary>
        public IXDButton Button()
        {
            var newButton = new XDButton(this);
            children.Add(newButton);
            return newButton;
        }

        /// <summary>
        /// Creates a new label and adds it to the view.
        /// </summary>
        public IXDLabel Label()
        {
            var newLabel = new XDLabel(this);
            children.Add(newLabel);
            return newLabel;
        }

        /// <summary>
        /// Creates a new TextBox and adds it to the layout.
        /// </summary>
        public IXDTextBox TextBox()
        {
            var newTextBox = new XDTextBox(this);
            children.Add(newTextBox);
            return newTextBox;
        }

        /// <summary>
        /// Widget for choosing dates, similar do TextBox except with date validation built-in.
        /// </summary>
        public IXDDateTimePicker DateTimePicker()
        {
            var newDateTimePicker = new XDDateTimePicker(this);
            children.Add(newDateTimePicker);
            return newDateTimePicker;
        }

        /// <summary>
        /// Creates a new drop-down selection box and adds it to the layout.
        /// </summary>
        public IXDComboBox ComboBox()
        {
            var newDropdownBox = new XDComboBox(this);
            children.Add(newDropdownBox);
            return newDropdownBox;
        }

        /// <summary>
        /// Creates a new checkbox and adds it to the layout.
        /// </summary>
        public IXDCheckbox Checkbox()
        {
            var newCheckbox = new XDCheckbox(this);
            children.Add(newCheckbox);
            return newCheckbox;
        }

        /// <summary>
        /// Creates a Vector3 field with X, Y and Z entry boxes.
        /// </summary>
        public IXDVector3Field Vector3Field()
        {
            var newVector3Field = new XDVector3Field(this);
            children.Add(newVector3Field);
            return newVector3Field;
        }

        /// <summary>
        /// Creates a widget for editing layer masks.
        /// </summary>
        public IXDLayerPicker LayerPicker()
        {
            var newLayerPicker = new XDLayerPicker(this);
            children.Add(newLayerPicker);
            return newLayerPicker;
        }

        /// <summary>
        /// Inserts a space between other widgets.
        /// </summary>
        public IXDLayout Spacer()
        {
            var newSpacer = new XDSpacer(this, -1);
            children.Add(newSpacer);
            return this;
        }

        /// <summary>
        /// Inserts a space between other widgets.
        /// </summary>
        public IXDLayout Spacer(int amt = -1)
        {
            var newSpacer = new XDSpacer(this, amt);
            children.Add(newSpacer);
            return this;
        }

        /// <summary>
        /// Inserts a flexable space (aka filler) between other widgets.
        /// </summary>
        public IXDLayout FlexiSpace()
        {
            var newSpacer = new XDFlexiSpace(this);
            children.Add(newSpacer);
            return this;
        }

        /// <summary>
        /// Creates a VerticalLayout and adds it to this layout.
        /// </summary>
        public IXDLayout VerticalLayout()
        {
            var newLayout = new XDVerticalLayout(this);
            children.Add(newLayout);
            return newLayout;
        }

        /// <summary>
        /// Creates a horizontal layout and adds it to this layout.
        /// </summary>
        public IXDLayout HorizontalLayout()
        {
         

            var newLayout = new HorizontalLayout(this);
            children.Add(newLayout);
            return newLayout;
        }

    }
}
