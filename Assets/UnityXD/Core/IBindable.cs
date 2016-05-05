using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Assets.UnityXD.Core
{
    public interface IBindable
    {
        event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(PropertyChangedEventArgs e);
        
        void NotifyOfPropertyChange(string propertyName);

        void NotifyOfPropertyChange<T>(Expression<Func<T>> property);

        void NotifyOfPropertyChange<T>(T value, ref T field);

        void NotifyOfPropertyChange<T>(T value, Expression<Func<T>> property, ref T field);

        void NotifyOfPropertyChange<T>(T value, string propertyName, ref T field);
    }
}