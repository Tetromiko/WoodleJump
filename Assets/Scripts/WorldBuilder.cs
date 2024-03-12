using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WorldBuilder : MonoBehaviour, IService
{
    private PlatformFactory _platformFactory;
    private ItemFactory _itemFactory;

    private List<GameObject> _platforms = new ();
    
    public void Initialize(PlatformFactory platformFactory, ItemFactory itemFactory)
    {
        _platformFactory = platformFactory;
        _itemFactory = itemFactory;
    }

    public void CreatePlatform(Vector2 position, bool createItem)
    {
        var platformItemPair = new GameObject("PlatformItemPair");
        platformItemPair.transform.position = position;
        _platformFactory.Create("PlatformBase", new Vector2(0, 0), platformItemPair.transform);

        if (!createItem) return;
        _itemFactory.Create("ItemBase", new Vector2(0, 0.5f), platformItemPair.transform);
    }
}
