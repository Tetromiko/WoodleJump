using UnityEngine;

public class WorldBuilder : MonoBehaviour, IService
{
    private PlatformFactory _platformFactory;
    private ItemFactory _itemFactory;
    
    [SerializeField]
    [Range(0f,1f)]
    private float itemSpawnChance;
    
    public void Initialize(PlatformFactory platformFactory, ItemFactory itemFactory)
    {
        _platformFactory = platformFactory;
        _itemFactory = itemFactory;
    }
    
    public GameObject CreatePlatformItemPair(Vector2 position)
    {
        var platform =  _platformFactory.Create(position, null);
        if (Random.Range(0f, 1f) < itemSpawnChance)
        {
            var item = _itemFactory.Create(position, platform.transform.Find("Platform"));
            item.transform.localPosition = new Vector3(0, 0.4f, 0);
        }
        return platform;
    }
}