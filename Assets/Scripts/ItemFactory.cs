using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ItemFactory : FactoryBase
{
    private readonly Dictionary<string, GameObject> _loadedPlatformPrefabs = new();

    public ItemFactory(AssetProvider assetProvider, ObjectDataBase objectDataBase) : base(assetProvider, objectDataBase)
    {
    }

    public override GameObject Create(string key, Vector2 position)
    {
        var itemPrefab = ObjectDataBase.GetObject(key);
        var item = Object.Instantiate((GameObject)itemPrefab, position, Quaternion.identity);
        return item;
    }

    public override void Dispose()
    {
        
    }
}