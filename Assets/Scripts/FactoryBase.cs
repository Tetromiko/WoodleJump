using System;
using UnityEngine;

public abstract class FactoryBase : IService, IDisposable
{
    protected readonly ProbabilityObjectDataBase ObjectDataBase;
    
    protected FactoryBase(ProbabilityObjectDataBase objectDataBase)
    {
        ObjectDataBase = objectDataBase;
    }

    public abstract GameObject Create(string key);
    
    public abstract GameObject Create(string key, Vector2 position);
    
    public abstract GameObject Create(string key, Vector2 position, Transform parent);

    public abstract void Dispose();
}