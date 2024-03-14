using System;
using UnityEngine;

public abstract class FactoryBase : IService, IDisposable
{
    protected readonly ProbabilityObjectDataBase ObjectDataBase;
    
    protected FactoryBase(ProbabilityObjectDataBase objectDataBase)
    {
        ObjectDataBase = objectDataBase;
    }

    public abstract GameObject Create();
    
    public abstract GameObject Create(Vector2 position);
    
    public abstract GameObject Create(Vector2 position, Transform parent);

    public abstract void Dispose();
}