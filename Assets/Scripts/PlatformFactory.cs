using System.Threading.Tasks;
using Data;
using UnityEditor.Animations;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlatformFactory : FactoryBase
{
    public PlatformFactory(ProbabilityObjectDataBase objectDataBase, AssetProvider assetProvider) : base(objectDataBase, assetProvider)
    {
        
    }
    
    public override async Task Initialize()
    {
        ObjectBase = await AssetProvider.LoadAssetAsync<GameObject>("PlatformHolder");
    }

    public override GameObject Create(Vector2 position, Transform parent)
    {
         var objectBase = Object.Instantiate(ObjectBase, position, Quaternion.identity, parent);
         var platformData = (PlatformData)ObjectDataBase.GetObject();
         var platform = objectBase.transform.Find("Platform");
         platform.GetComponent<SpriteRenderer>().sprite = platformData.sprite;
         platform.GetComponent<Animator>().runtimeAnimatorController = platformData.animatorController;
         return objectBase;
    }

    public override void Dispose()
    {
        
    }
}