using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public abstract class FactoryBase : IService, IDisposable
{
    protected readonly AssetProvider AssetProvider;
    protected readonly ObjectDataBase ObjectDataBase;
    
    protected FactoryBase(AssetProvider assetProvider, ObjectDataBase objectDataBase)
    {
        AssetProvider = assetProvider;
        ObjectDataBase = objectDataBase;
        
    }

    public abstract GameObject Create(string key, Vector2 position);

    public abstract void Dispose();
}