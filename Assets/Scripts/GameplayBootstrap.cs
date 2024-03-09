using System.Threading.Tasks;
using UnityEngine;

public class GameplayBootstrap : Bootstrap
{
    protected override async Task Initialize()
    {
        var assetProvider = ServiceLocator.Get<AssetProvider>();
        
        var itemDataBase = await assetProvider.LoadAssetAsync<ObjectDataBase>("ItemDataBase");
        await itemDataBase.Initialize(assetProvider);
        
        var itemFactory = new ItemFactory(assetProvider, itemDataBase);
        
        var platformDataBase = await assetProvider.LoadAssetAsync<ObjectDataBase>("PlatformDataBase");
        await platformDataBase.Initialize(assetProvider);
        
        var platformFactory = new PlatformFactory(assetProvider, platformDataBase);
    }
}