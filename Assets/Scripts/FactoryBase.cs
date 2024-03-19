using System;
using System.Threading.Tasks;
using Data;
using UnityEngine;

public abstract class FactoryBase : IService, IDisposable
{
    protected readonly ObjectDatabase ObjectDatabase;
    protected readonly AssetProvider AssetProvider;

    protected GameObject ObjectBase;
    
    protected FactoryBase(ObjectDatabase objectDatabase, AssetProvider assetProvider)
    {
        ObjectDatabase = objectDatabase;
        AssetProvider = assetProvider;
    }

    public abstract Task Initialize();
    
    public abstract GameObject Create(Vector2 position, Transform parent);

    public abstract void Dispose();
}