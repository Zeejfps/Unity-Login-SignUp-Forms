using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class Z
{
    private static readonly Dictionary<Type, IFactory> s_TypeToFactoryDictionary = new();

    public static T New<T>()
    {
        var type = typeof(T);
        return (T)New(type);
    }
    
    public static object New(Type type)
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

    public static void RegisterFactory<T>(Func<T> factory)
    {
        var type = typeof(T);
        try
        {
            s_TypeToFactoryDictionary.Add(type, new FuncFactory<T>(factory));
        }
        catch (ArgumentException)
        {
            throw new Exception($"Factory for {type} type already registered");
        }
    }

    public static void RegisterSingleton<T>(T instance)
    {
        var type = typeof(T);
        try
        {
            s_TypeToFactoryDictionary.Add(type, new FuncFactory<T>(() => instance));
        }
        catch (ArgumentException e)
        {
            throw new Exception($"Singleton for {type} type already registered");
        }
    }

    public static void UnregisterSingleton<T>()
    {
        var type = typeof(T);
        s_TypeToFactoryDictionary.Remove(type);
    }

    public static T Locate<T>()
    {
        var type = typeof(T);
        if (s_TypeToFactoryDictionary.TryGetValue(type, out var factory))
            return (T)factory.Create();
        return default;
    }
}

internal sealed class FuncFactory<T> : IFactory
{
    private readonly Func<T> m_Func;

    public FuncFactory(Func<T> func)
    {
        m_Func = func;
    }

    public object Create()
    {
        return m_Func.Invoke();
    }
}

interface IFactory
{
    object Create();
}