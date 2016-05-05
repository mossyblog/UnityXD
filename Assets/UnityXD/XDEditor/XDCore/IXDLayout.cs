using Assets.UnityXD.Core;
using Assets.UnityXD.XDEditor.XDControls;

namespace Assets.UnityXD.XDEditor.XDCore
{
    /// <summary>
    /// Layouts are widgets that can contain other child widgets.
    /// </summary>
    public interface IXDLayout : IXDWidget
    {
        IXDAnchorToolbar AnchorToolbar();

        /// <summary>
        /// Creates a new button and adds it to the layout.
        /// </summary>
        IXDButton Button();

        /// <summary>
        /// Creates a new label and adds it to the view.
        /// </summary>
        IXDLabel Label();

        /// <summary>
        /// Creates a new TextBox and adds it to the layout.
        /// </summary>
        IXDTextBox TextBox();

        /// <summary>
        /// Widget for choosing dates, similar do TextBox except with date validation built-in.
        /// </summary>
        IXDDateTimePicker DateTimePicker();

        /// <summary>
        /// Creates a new drop-down selection box and adds it to the layout.
        /// </summary>
        IXDComboBox ComboBox();

        /// <summary>
        /// Creates a new checkbox and adds it to the layout.
        /// </summary>
        IXDCheckbox Checkbox();

        /// <summary>
        /// Creates a Vector3 field with X, Y and Z entry boxes.
        /// </summary>
        IXDVector3Field Vector3Field();

        /// <summary>
        /// Creates a widget for editing layer masks.
        /// </summary>
        IXDLayerPicker LayerPicker();

        /// <summary>
        /// Inserts a space between other widgets.
        /// </summary>
        IXDLayout Spacer();

        IXDLayout Spacer(int atm);

        IXDLayout FlexiSpace();

        /// <summary>
        /// Creates a VerticalLayout and adds it to this layout.
        /// </summary>
        IXDLayout VerticalLayout();

        /// <summary>
        /// Creates a horizontal layout and adds it to this layout.
        /// </summary>
        IXDLayout HorizontalLayout();

        /// <summary>
        /// Whether or not to draw this layout and its sub-widgets (default is true).
        /// </summary>
        IPropertyBinding<bool, IXDLayout> Enabled { get; }

    }
}
