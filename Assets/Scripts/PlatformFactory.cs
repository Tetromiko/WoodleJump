using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlatformFactory : FactoryBase
{
    private readonly Dictionary<string, GameObject> _loadedPlatformPrefabs = new();

    public PlatformFactory(AssetProvider assetProvider, ObjectDataBase objectDataBase) : base(assetProvider, objectDataBase)
    {
    }
    
    public override GameObject Create(string key, Vector2 position)
    {
        var platformPrefab = ObjectDataBase.GetObject(key);
        var platform = Object.Instantiate((GameObject)platformPrefab, position, Quaternion.identity);
        return platform;
    }

    public override void Dispose()
    {
        
    }
}