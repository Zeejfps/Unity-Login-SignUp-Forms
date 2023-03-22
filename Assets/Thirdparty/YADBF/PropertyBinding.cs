using System;
using UnityEngine;

namespace YADBF
{
    public sealed class PropertyBinding<T>
    {
        public bool IsBound => Property != null;
        public string PropertyName { get; private set; }
        public ObservableProperty<T> Property { get; private set; }

        public void Bind(object model, string propertyName)
        {
            if (model == null)
            {
                Unbind();
                return;
            }

            var modelType = model.GetType();
            var propertyInfo = modelType.GetProperty(propertyName);
            if (propertyInfo == null)
                throw new Exception(
                    $"No property found with name '{propertyName}' on view model type '{modelType}'");

            var bindableProperty = propertyInfo.GetValue(model);
            if (bindableProperty == null)
                throw new Exception($"Bindable Property {propertyName} is null");
            
            Property = bindableProperty as ObservableProperty<T>;
            if (Property == null)
                throw new Exception($"{propertyInfo.PropertyType} does not derive from {typeof(ObservableProperty<T>)}");
            PropertyName = propertyName;
        }

        public void Unbind()
        {
            PropertyName = null;
            Property = null;
        }
    }
}