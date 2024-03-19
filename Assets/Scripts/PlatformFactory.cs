using System.Threading.Tasks;
using Data;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlatformFactory : FactoryBase
{
    public PlatformFactory(ObjectDatabase objectDatabase, AssetProvider assetProvider) : base(objectDatabase, assetProvider)
    {
        
    }
    
    public override async Task Initialize()
    {
        ObjectBase = await AssetProvider.LoadAssetAsync<GameObject>("PlatformHolder");
    }

    public override GameObject Create(Vector2 position, Transform parent)
    {
         var objectBase = Object.Instantiate(ObjectBase, position, Quaternion.identity, parent);
         var platformData = (PlatformData)ObjectDatabase.GetRandomObjectBasedOnChance();
         var platform = objectBase.transform.Find("Platform");
         platform.GetComponent<SpriteRenderer>().sprite = platformData.sprite;
         return objectBase;
    }

    public override void Dispose()
    {
        
    }
}