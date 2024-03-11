using System.Threading.Tasks;
using UnityEngine;

public class GameplayBootstrap : Bootstrap
{
    public WorldBuilder worldBuilder;
    protected override async Task Initialize()
    {
        var assetProvider = ServiceLocator.Get<AssetProvider>();
        
        var itemDataBase = await assetProvider.LoadAssetAsync<ProbabilityObjectDataBase>("ItemDataBase");
        await itemDataBase.Initialize(assetProvider);
        
        var itemFactory = new ItemFactory(itemDataBase);
        ServiceLocator.Register(itemFactory);
        
        var platformDataBase = await assetProvider.LoadAssetAsync<ProbabilityObjectDataBase>("PlatformDataBase");
        await platformDataBase.Initialize(assetProvider);
        
        var platformFactory = new PlatformFactory(platformDataBase);
        ServiceLocator.Register(platformFactory);
        
        worldBuilder.Initialize(assetProvider, platformFactory, itemFactory);
        ServiceLocator.Register(worldBuilder);

        for (int i = 0; i < 20; i++)
        {
            worldBuilder.Create(new Vector2(0, i*3));
        }
    }
    
}