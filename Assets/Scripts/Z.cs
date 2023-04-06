using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class Z
{
    private static readonly Dictionary<Type, IFactory> s_TypeToFactoryDictionary = new();

    private static T New<T>()
    {
        var type = typeof(T);
        return (T)New(type);
    }
    
    private static object New(Type type)
    {
        var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
        var paramValues = new List<object>();
        foreach (var constructor in constructors)
        {
            var isValidConstructor = true;
            var parameters = constructor.GetParameters();
            foreach (var parameter in parameters)
            {
                var paramType = parameter.ParameterType;
                if (!s_TypeToFactoryDictionary.TryGetValue(paramType, out var factory))
                {
                    Debug.Log($"No Factory for: {paramType}");
                    isValidConstructor = false;
                    break;
                }
                
                paramValues.Add(factory.Create());
            }
            
            if (isValidConstructor)
                return Activator.CreateInstance(type, paramValues.ToArray());
            
            paramValues.Clear();
        }

        throw new Exception($"Could not instantiate object of type {type}");
    }

    public static void RegisterScoped<TInterface, TConcrete>() where TConcrete : TInterface
    {
        var interfaceType = typeof(TInterface);
        try
        {
            s_TypeToFactoryDictionary.Add(interfaceType, new ScopedFactory<TConcrete>());
        }
        catch (ArgumentException)
        {
            throw new Exception($"Singleton for {interfaceType} type already registered");
        }
    }

    public static void RegisterSingleton<TInterface, TConcrete>() where TConcrete : TInterface
    {
        var interfaceType = typeof(TInterface);
        try
        {
            s_TypeToFactoryDictionary.Add(interfaceType, new SingletonFactory<TConcrete>());
        }
        catch (ArgumentException)
        {
            throw new Exception($"Singleton for {interfaceType} type already registered");
        }
    }
    
    public static T Get<T>()
    {
        var type = typeof(T);
        if (s_TypeToFactoryDictionary.TryGetValue(type, out var factory))
            return (T)factory.Create();
        return default;
    }
    
    private sealed class SingletonFactory<T> : IFactory
    {
        private T m_Singleton;
    
        public object Create()
        {
            if (m_Singleton == null)
                m_Singleton = New<T>();
            return m_Singleton;
        }
    }
    
    private sealed class ScopedFactory<T> : IFactory
    {
        public object Create()
        {
            return New<T>();
        }
    }
}

internal interface IFactory
{
    object Create();
}