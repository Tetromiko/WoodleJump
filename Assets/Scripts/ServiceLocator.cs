using System;
using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, IService> Services = new Dictionary<Type, IService>();

    public static void Register<T>(T service) where T : IService
    {
        if(Services.ContainsKey(typeof(T)))
        {
            Debug.LogError("Already registered service of type " + typeof(T).ToString());
            return;
        }
        Services[typeof(T)] = service;
    }
    
    public static void Unregister<T>() where T : IService
    {
        if(!Services.ContainsKey(typeof(T)))
        {
            Debug.LogError("No service of type " + typeof(T).ToString() + " registered!");
            return;
        }
        Services.Remove(typeof(T));
    }

    public static T Get<T>() where T : IService
    {
        if(!Services.ContainsKey(typeof(T)))
        {
            Debug.LogError("No service of type " + typeof(T).ToString() + " registered!");
            return default(T);
        }
        return (T)Services[typeof(T)];
    }
}