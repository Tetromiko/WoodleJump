using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class GameplayBootstrap : Bootstrap
{
    [SerializeField] private UIManager uiManager;
    protected override async Task Initialize()
    {
        uiManager.ShowLoadingScreen();
        
        var assetProvider = ServiceLocator.Get<AssetProvider>();
        
        var itemDataBase = await assetProvider.LoadAssetAsync<ProbabilityObjectDataBase>("ItemDataBase");
        await itemDataBase.Initialize(assetProvider);
        
        var itemFactory = new ItemFactory(itemDataBase);
        ServiceLocator.Register(itemFactory);
        
        var platformDataBase = await assetProvider.LoadAssetAsync<ProbabilityObjectDataBase>("PlatformDataBase");
        await platformDataBase.Initialize(assetProvider);
        
        var platformFactory = new PlatformFactory(platformDataBase);
        ServiceLocator.Register(platformFactory);
        
        var playerPrefab = await assetProvider.LoadAssetAsync<GameObject>("Player");
        var player = Instantiate(playerPrefab).GetComponent<Player>();
        
        var worldBuilderPrefab = await assetProvider.LoadAssetAsync<GameObject>("WorldBuilder");
        var worldBuilder = Instantiate(worldBuilderPrefab).GetComponent<WorldBuilder>();
        worldBuilder.Initialize(platformFactory, itemFactory);
        ServiceLocator.Register(worldBuilder);
        
        var worldManagerPrefab = await assetProvider.LoadAssetAsync<GameObject>("WorldManager");
        var worldManager = Instantiate(worldManagerPrefab).GetComponent<WorldManager>();
        worldManager.Initialize(player, worldBuilder);
        ServiceLocator.Register(worldManager);
        
        uiManager.HideLoadingScreen();
    }
}