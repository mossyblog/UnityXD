using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

namespace Assets.UnityXD.Core
{
    public abstract class MonoBindable : MonoBehaviour, INotifyPropertyChanged, IBindable
    {
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyOfPropertyChange(string propertyName)
        {
            IBindable monoBindable = this;
            BindableHelper.NotifyOfPropertyChange(propertyName, ref monoBindable);
        }

        public void NotifyOfPropertyChange<T>(Expression<Func<T>> property)
        {
            IBindable monoBindable = this;
            BindableHelper.NotifyOfPropertyChange<T>(property, ref monoBindable);
        }

        public void NotifyOfPropertyChange<T>(T value, ref T field)
        {
            IBindable monoBindable = this;
            BindableHelper.NotifyOfPropertyChange<T>(value, ref field, ref monoBindable);
        }

        public void NotifyOfPropertyChange<T>(T value, Expression<Func<T>> property, ref T field)
        {
            IBindable monoBindable = this;
            BindableHelper.NotifyOfPropertyChange<T>(value, property, ref field, ref monoBindable);
        }

        public void NotifyOfPropertyChange<T>(T value, string propertyName, ref T field)
        {
            IBindable monoBindable = this;
            BindableHelper.NotifyOfPropertyChange<T>(value, propertyName, ref field, ref monoBindable);

        }
    }
}