using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class FactoryBase : IService, IDisposable
{
    protected readonly ProbabilityObjectDatabase ObjectDatabase;
    protected readonly AssetProvider AssetProvider;

    protected GameObject ObjectBase;
    
    protected FactoryBase(ProbabilityObjectDatabase objectDatabase, AssetProvider assetProvider)
    {
        ObjectDatabase = objectDatabase;
        AssetProvider = assetProvider;
    }

    public abstract Task Initialize();
    
    public abstract GameObject Create(Vector2 position, Transform parent);

    public abstract void Dispose();
}