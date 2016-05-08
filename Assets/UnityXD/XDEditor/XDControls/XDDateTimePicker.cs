using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Assets.UnityXD.Core;
using Assets.UnityXD.XDEditor.XDCore;
using UnityEngine;

namespace Assets.UnityXD.XDEditor.XDControls
{
    /// <summary>
    /// Widget for entering a date and time.
    /// </summary>
    public interface IXDDateTimePicker : IXDWidget
    {
        /// <summary>
        /// Date and time currently being displayed in the widget.
        /// </summary>
        IPropertyBinding<DateTime, IXDDateTimePicker> Date { get; }
        
    }

    /// <summary>
    /// Widget for entering a date and time.
    /// </summary>
    internal class XDDateTimePicker : XDWidget, IXDDateTimePicker
    {
        private DateTime date;
        private string text;
        private bool textValid = true;
        private CultureInfo culture;

        private PropertyBinding<DateTime, IXDDateTimePicker> dateProperty;


        public IPropertyBinding<DateTime, IXDDateTimePicker> Date
        {
            get { return dateProperty; }
        }


        internal XDDateTimePicker(IXDLayout parent) : base(parent)
        {
            culture = CultureInfo.CreateSpecificCulture("en-AU");

            dateProperty = new PropertyBinding<DateTime, IXDDateTimePicker>(
                this,
                value =>
                {
                    this.date = value;
                    this.text = date.ToString(culture);
                }
                );


        }

        public override void Render()
        {
            var layoutOptions = new List<GUILayoutOption>();
            if (width >= 0)
            {
                layoutOptions.Add(GUILayout.Width(width));
            }
            if (height >= 0)
            {
                layoutOptions.Add(GUILayout.Height(height));
            }

            // Make the background of the widget red if the date is invalid.
            var savedColour = UnityEngine.GUI.backgroundColor;
            if (!textValid)
            {
                UnityEngine.GUI.backgroundColor = Color.red;
            }
            string newText = GUILayout.TextField(text, layoutOptions.ToArray());
            UnityEngine.GUI.backgroundColor = savedColour;

            // Update the date
            if (newText != text)
            {
                text = newText;

                textValid = DateTime.TryParse(text, culture, DateTimeStyles.None, out date);
                if (textValid)
                {
                    dateProperty.UpdateView(date);
                }
            }
        }


    }
}
