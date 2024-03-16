using System.Threading.Tasks;
using Data;
using UnityEngine;
using Object = UnityEngine.Object;

public class ItemFactory : FactoryBase
{
    public ItemFactory(ProbabilityObjectDataBase objectDataBase, AssetProvider assetProvider) : base(objectDataBase, assetProvider)
    {
        
    }

    public override async Task Initialize()
    {
        ObjectBase = await AssetProvider.LoadAssetAsync<GameObject>("ItemHolder");
    }
    
    public override GameObject Create(Vector2 position, Transform parent)
    {
        var objectBase = Object.Instantiate(ObjectBase, position, Quaternion.identity, parent);
        var itemData = (ItemData)ObjectDataBase.GetObject();
        var item = objectBase.transform.Find("Item");
        item.GetComponent<SpriteRenderer>().sprite = itemData.sprite;
        return objectBase;
    }

    public override void Dispose()
    {
        
    }
}