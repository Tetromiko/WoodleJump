using UnityEngine;
using Object = UnityEngine.Object;

public class ItemFactory : FactoryBase
{
    public ItemFactory(ProbabilityObjectDataBase objectDataBase) : base(objectDataBase)
    {
        
    }

    public override GameObject Create(string key)
    {
        return Create(key, Vector2.zero);
    }

    public override GameObject Create(string key, Vector2 position)
    {
        return Create(key, position, null);
    }

    public override GameObject Create(string key, Vector2 position, Transform parent)
    {
        var itemPrefab = (GameObject)ObjectDataBase.GetObject();
        var item = Object.Instantiate(itemPrefab, parent);
        item.transform.localPosition = position;
        return item;
    }

    public override void Dispose()
    {
        
    }
}