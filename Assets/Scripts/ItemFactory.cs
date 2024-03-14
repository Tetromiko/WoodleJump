using UnityEngine;
using Object = UnityEngine.Object;

public class ItemFactory : FactoryBase
{
    public ItemFactory(ProbabilityObjectDataBase objectDataBase) : base(objectDataBase)
    {
        
    }

    public override GameObject Create()
    {
        return Create(Vector2.zero);
    }

    public override GameObject Create(Vector2 position)
    {
        return Create(position, null);
    }

    public override GameObject Create(Vector2 position, Transform parent)
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