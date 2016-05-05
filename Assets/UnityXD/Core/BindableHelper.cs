using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using Debug = UnityEngine.Debug;

namespace Assets.UnityXD.Core
{
    public static class BindableHelper
    {
        /// <summary>
        ///     Resolves a Property's name from a Lambda Expression passed in.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        internal static string GetPropertyName<T>(Expression<Func<T>> property)
        {
            var expression = (MemberExpression)property.Body;
            var propertyName = expression.Member.Name;

            Debug.AssertFormat(propertyName != null, "MonoBindable Property shouldn't be null!");
            return propertyName;
        }

        #region Notification Handlers

        /// <summary>
        ///     Notify's all other objects listening that a value has changed for nominated propertyName
        /// </summary>
        /// <param name="propertyName"></param>
        public static void NotifyOfPropertyChange(string propertyName, ref IBindable bindable)
        {
            bindable.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Notifies subscribers of the property change.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property expression.</param>
        public static void NotifyOfPropertyChange<T>(Expression<Func<T>> property, ref IBindable bindable)
        {
            var propertyName = GetPropertyName(property);
            NotifyOfPropertyChange(propertyName, ref bindable);
        }


        #endregion

        #region Setters

        /// <summary>
        ///     Sets the value of a property whilst automatically looking up its caller name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value1"></param>
        /// <param name="value"></param>
        /// <param name="text"></param>
 

        internal static void NotifyOfPropertyChange<T>(T value, ref T field, ref IBindable monoBindable)
        {
            var propertyName = new StackTrace().GetFrame(1).GetMethod().Name.Replace("set_", ""); // strips the set_ from name;
            Debug.Assert(!propertyName.Contains("ctor"), "The Stacktrace can't find the appropriate field name");
            NotifyOfPropertyChange(value, propertyName, ref field, ref monoBindable);
        }

        /// <summary>
        ///     Sets the value of a property based off an Expression (()=>FieldName)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="property"></param>
        public static  void NotifyOfPropertyChange<T>(T value, Expression<Func<T>> property, ref T field, ref IBindable bindable)
        {
            var propertyName = GetPropertyName(property);
            NotifyOfPropertyChange<T>(value, propertyName, ref field, ref bindable);
        }

        /// <summary>
        ///     Sets the value of a property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="name"></param>
        public static  void NotifyOfPropertyChange<T>(T value, string propertyName, ref T field, ref IBindable bindable)
        {
            Debug.Assert(propertyName != null, "name != null");
            field = value;
            NotifyOfPropertyChange(propertyName, ref bindable);
        }

        



        #endregion
    }
}