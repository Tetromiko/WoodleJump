using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WorldBuilder : MonoBehaviour, IService
{
    private AssetProvider _assetProvider;
    private PlatformFactory _platformFactory;
    private ItemFactory _itemFactory;

    private List<GameObject> _platforms = new ();
    
    public void Initialize(AssetProvider assetProvider, PlatformFactory platformFactory, ItemFactory itemFactory)
    {
        _assetProvider = assetProvider;
        _platformFactory = platformFactory;
        _itemFactory = itemFactory;
    }

    public void Create(Vector2 position)
    {
        var platformItemPair = new GameObject("PlatformItemPair");
        platformItemPair.transform.position = position;
        _platformFactory.Create("PlatformBase", new Vector2(0, 0), platformItemPair.transform);
        _itemFactory.Create("ItemBase", new Vector2(0, 0.5f), platformItemPair.transform);
    }
}
