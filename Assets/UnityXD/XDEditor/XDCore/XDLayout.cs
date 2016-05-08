using System.Collections.Generic;
using Assets.UnityXD.Core;
using Assets.UnityXD.XDEditor.XDComponents;
using Assets.UnityXD.XDEditor.XDControls;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDCore
{

    public interface IXDLayout : IXDWidget
    {

        // COMPONENTS
        IXDAnchorToolbar AnchorToolbar();
        IXDLayoutMetrics LayoutMetrics();


        // CONTROLS
        IXDTabBar TabBar();

        IXDButton Button();

        IXDLabel Label();

        IXDToolBar ToolBar();

        IXDTextBox TextBox();

        IXDDateTimePicker DateTimePicker();

        IXDComboBox ComboBox();

        IXDCheckbox Checkbox();

        IXDLayout Spacer();

        IXDLayout Spacer(int atm);

        IXDLayout FlexiSpace();

        IXDLayout VerticalLayout();
        IXDLayout VerticalLayout(GUIStyle style);

        IXDLayout HorizontalLayout(GUIStyle style);
        IXDLayout HorizontalLayout();

        IPropertyBinding<bool, IXDLayout> Enabled { get; }

    }

    /// <summary>
    /// Layouts are widgets that can contain other child widgets. All layouts should inherit from XDLayout.
    /// </summary>
    public abstract class XDLayout : XDWidget, IXDLayout
    {
        protected bool enabled = true;

        private PropertyBinding<bool, IXDLayout> enabledProperty;

        /// <summary>
        /// Whether or not to draw this layout and its sub-widgets (default is true).
        /// </summary>
        public IPropertyBinding<bool, IXDLayout> Enabled { get { return enabledProperty; } }

        public List<XDWidget> Children = new List<XDWidget>();

        protected XDLayout(IXDLayout parent) : base(parent)
        {
            enabledProperty = new PropertyBinding<bool, IXDLayout>(this,value => this.enabled = value);
        }

        public override void Render()
        {
            Children.ForEach(child => child.Render());
        }

        /// <summary>
        /// Creates a new button and adds it to the layout.
        /// </summary>
        public IXDAnchorToolbar AnchorToolbar()
        {
            var newtoolbar = new XDAnchorToolbar(this);
            Children.Add(newtoolbar);
            return newtoolbar;
        }
        public IXDTabBar TabBar()
        {
            var comp = new XDTabBar(this);
            Children.Add(comp);
            return comp;
        }

        public IXDLayoutMetrics LayoutMetrics()
        {
            var comp = new XDLayoutMetrics(this);
            Children.Add(comp);
            return comp;
        }

        /// <summary>
        /// Creates a new button and adds it to the layout.
        /// </summary>
        public IXDButton Button()
        {
            var newButton = new XDButton(this);
            Children.Add(newButton);
            return newButton;
        }

        public IXDToolBar ToolBar()
        {
            var newtoolbar = new XDToolBar(this);
            Children.Add(newtoolbar);
            return newtoolbar;
        }

        /// <summary>
        /// Creates a new label and adds it to the view.
        /// </summary>
        public IXDLabel Label()
        {
            var newLabel = new XDLabel(this);
            Children.Add(newLabel);
            return newLabel;
        }

        /// <summary>
        /// Creates a new TextBox and adds it to the layout.
        /// </summary>
        public IXDTextBox TextBox()
        {
            var newTextBox = new XDTextBox(this);
            Children.Add(newTextBox);
            return newTextBox;
        }

        /// <summary>
        /// Widget for choosing dates, similar do TextBox except with date validation built-in.
        /// </summary>
        public IXDDateTimePicker DateTimePicker()
        {
            var newDateTimePicker = new XDDateTimePicker(this);
            Children.Add(newDateTimePicker);
            return newDateTimePicker;
        }

        /// <summary>
        /// Creates a new drop-down selection box and adds it to the layout.
        /// </summary>
        public IXDComboBox ComboBox()
        {
            var newDropdownBox = new XDComboBox(this);
            Children.Add(newDropdownBox);
            return newDropdownBox;
        }

        /// <summary>
        /// Creates a new checkbox and adds it to the layout.
        /// </summary>
        public IXDCheckbox Checkbox()
        {
            var newCheckbox = new XDCheckbox(this);
            Children.Add(newCheckbox);
            return newCheckbox;
        }



        /// <summary>
        /// Inserts a space between other widgets.
        /// </summary>
        public IXDLayout Spacer()
        {
            var newSpacer = new XDSpacer(this, -1);
            Children.Add(newSpacer);
            return this;
        }

        /// <summary>
        /// Inserts a space between other widgets.
        /// </summary>
        public IXDLayout Spacer(int amt = -1)
        {
            var newSpacer = new XDSpacer(this, amt);
            Children.Add(newSpacer);
            return this;
        }

        /// <summary>
        /// Inserts a flexable space (aka filler) between other widgets.
        /// </summary>
        public IXDLayout FlexiSpace()
        {
            var newSpacer = new XDFlexiSpace(this);
            Children.Add(newSpacer);
            return this;
        }

        /// <summary>
        /// Creates a VerticalLayout and adds it to this layout.
        /// </summary>
        public IXDLayout VerticalLayout()
        {
            return VerticalLayout(null);
        }
        public IXDLayout VerticalLayout(GUIStyle pStyle)
        {
            var newLayout = new XDVerticalLayout(this);
            if (pStyle != null)
            {
                newLayout.Style(pStyle);
            }
            Children.Add(newLayout);
            return newLayout;
        }

        /// <summary>
        /// Creates a horizontal layout and adds it to this layout.
        /// </summary>
        public IXDLayout HorizontalLayout(GUIStyle pStyle)
        {

            
            var newLayout = new XDHorizontalLayout(this);
            if (pStyle != null)
            {
                newLayout.Style(pStyle);
            }
            Children.Add(newLayout);
            return newLayout;
        }

        public IXDLayout HorizontalLayout()
        {
            return HorizontalLayout(null);
        }
    }
}
