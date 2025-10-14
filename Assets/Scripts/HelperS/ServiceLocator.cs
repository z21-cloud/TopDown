using System.Collections.Generic;
using UnityEngine;

public static class ServiceLocator
{
    private static Dictionary<System.Type, object> services = new();

    public static void Register<T>(T service)
    {
        services[typeof(T)] = service;
    }

    public static T Get<T>()
    {
        return (T)services[typeof(T)];
    }
}
