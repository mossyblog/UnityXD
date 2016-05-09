using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Assets.UnityXD.XDEditor.XDControls;
using UnityEngine;

namespace Assets.UnityXD.Core
{

    /// <summary>
    /// Binds a widget's property to a property in the external view model.
    /// </summary>
    public interface IPropertyBinding<ValueT, WidgetT>
    {
        /// <summary>
        /// Configure the property to bind to later.
        /// </summary>
        WidgetT BindTo(string propertyName);

        /// <summary>
        /// Configure the property to bind to later.
        /// </summary>
        WidgetT BindTo(Expression<Func<ValueT>> propertyExpression);

        /// <summary>
        /// NotifyPropertyChanged the value of the property directly (only used in initial setup)
        /// </summary>
        WidgetT Value(ValueT propertyValue);
    }

    /// <summary>
    /// Binds a widget's property to a property in the external view model.
    /// </summary>
    internal class PropertyBinding<ValueT, WidgetT> : IPropertyBinding<ValueT, WidgetT>
    {
        // Used in fluent API so that BindTo and Value methods can return the parent widget and thus be chained together
        WidgetT parentWidget;
        Action<ValueT> onViewModelUpdated;

        private string _boundPropertyName;
        private object _viewModel;
        private PropertyInfo _boundProperty;

        public void BindViewModel(object newViewModel)
        {
            _viewModel = newViewModel;
            if (!String.IsNullOrEmpty(_boundPropertyName))
            {
                var viewModelType = newViewModel.GetType();
                _boundProperty = viewModelType.GetProperty(_boundPropertyName);
                if (_boundProperty == null)
                {
                    throw new ApplicationException(string.Format( "Expected property {0} not found on type {1} ({2}).", _boundPropertyName, viewModelType.Name, viewModelType ));
                }

                // Update the widget with the initial value from the bound property.
                var widgetValue = GetValueFromViewModel();
                UpdateWidget(widgetValue);

                // BindTo the property so that the widget gets updated when the view model changes
                var notifyPropertyChanged = _viewModel as INotifyPropertyChanged;
                if (notifyPropertyChanged != null)
                {
                    notifyPropertyChanged.PropertyChanged += viewModel_PropertyChanged;
                }
            }
        }

        void viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == _boundPropertyName)
            {
                var widgetValue = GetValueFromViewModel();
                UpdateWidget(widgetValue);
            }
        }

        /// <summary>
        /// Gets the value from the property in the bound view model.
        /// </summary>
        private ValueT GetValueFromViewModel()
        {
            if (_boundProperty == null)
            {
                return default(ValueT);
            }

            var viewModelValue = _boundProperty.GetValue(_viewModel, null);
            try
            {
                return (ValueT) viewModelValue;
            }
            catch (InvalidCastException)
            {
                //Logger.LogError(ex, "todo")
                Debug.LogError("Failed to cast view model value of type " + viewModelValue.GetType().Name + " to " +
                               typeof (ValueT).Name);
            }

            return default(ValueT);
        }

        /// <summary>
        /// Create the PropertyBinding with a reference to the widget using it and an action to be called when the external view model changes.
        /// </summary>
        internal PropertyBinding(WidgetT parentWidget, Action<ValueT> onViewModelUpdated)
        {
            this.parentWidget = parentWidget;
            this.onViewModelUpdated = onViewModelUpdated;
        }

        /// <summary>
        /// Update the parent widget when the value of the property is changed.
        /// </summary>
        internal void UpdateWidget(ValueT newValue)
        {
            onViewModelUpdated(newValue);
        }

        /// <summary>
        /// Updates the bound view model when the value is changed by the widget.
        /// </summary>
        internal void UpdateView(ValueT newValue)
        {
            if (_viewModel != null && _boundProperty != null)
            {
                _boundProperty.SetValue(_viewModel, newValue, null);
            }
        }

        /// <summary>
        /// Binds this PropertyBinding to an external property.
        /// </summary>
        public WidgetT BindTo(string propertyName)
        {
            _boundPropertyName = propertyName;
            return parentWidget;
        }


        /// <summary>
        /// Binds this PropertyBinding to an external property.
        /// </summary>
        public WidgetT BindTo(Expression<Func<ValueT>> propertyExpression)
        {
            return BindTo(GetPropertyName(propertyExpression));
        }

        /// <summary>
        /// Get the string name of a property.
        /// </summary>
        private static string GetPropertyName(Expression<Func<ValueT>> propertyExpression)
        {
            var expr = (MemberExpression) propertyExpression.Body;
            return expr.Member.Name;
        }

        /// <summary>
        /// Permanently set the value of this PropertyBinding
        /// </summary>
        public WidgetT Value(ValueT propertyValue)
        {
            UpdateWidget(propertyValue);

            return parentWidget;
        }

   

    }
}