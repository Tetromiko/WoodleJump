using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WorldBuilder : MonoBehaviour, IService
{
    private PlatformFactory _platformFactory;
    private ItemFactory _itemFactory;
    
    public void Initialize(PlatformFactory platformFactory, ItemFactory itemFactory)
    {
        _platformFactory = platformFactory;
        _itemFactory = itemFactory;
    }

    public GameObject CreatePlatform(Vector2 position, bool createItem)
    {
        var platformItemPair = _platformFactory.Create(position, null);

        if (!createItem) return platformItemPair;

        var parent = platformItemPair.transform.childCount>0 ? platformItemPair.transform.Find("Platform") : platformItemPair.transform;
        _itemFactory.Create(new Vector2(0, 0.5f), parent);
        return platformItemPair;
    }
}
