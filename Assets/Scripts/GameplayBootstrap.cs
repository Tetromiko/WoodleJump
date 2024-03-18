using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class GameplayBootstrap : Bootstrap
{
    [SerializeField] 
    private UIManager uiManager;
    [SerializeField] 
    private CameraController cameraController;
    
    protected override async Task Initialize()
    {
        uiManager.ShowLoadingScreen();
        
        var assetProvider = ServiceLocator.Get<AssetProvider>();
        
        var itemDataBase = await assetProvider.LoadAssetAsync<ProbabilityObjectDatabase>("ItemDataBase");
        await itemDataBase.Initialize(assetProvider);
        
        var itemFactory = new ItemFactory(itemDataBase, assetProvider);
        await itemFactory.Initialize();
        ServiceLocator.Register(itemFactory);
        
        var platformDataBase = await assetProvider.LoadAssetAsync<ProbabilityObjectDatabase>("PlatformDataBase");
        await platformDataBase.Initialize(assetProvider);
        
        var platformFactory = new PlatformFactory(platformDataBase, assetProvider);
        await platformFactory.Initialize();
        ServiceLocator.Register(platformFactory);
        
        var playerPrefab = await assetProvider.LoadAssetAsync<GameObject>("Player");
        var player = Instantiate(playerPrefab).GetComponent<Player>();

        var worldBuilderPrefab = await assetProvider.LoadAssetAsync<GameObject>("WorldBuilder");
        var worldBuilder = Instantiate(worldBuilderPrefab).GetComponent<WorldBuilder>();
        worldBuilder.Initialize(platformFactory, itemFactory);
        ServiceLocator.Register(worldBuilder);
        
        cameraController.Initialize(player.transform);

        var cam = cameraController.GetComponent<Camera>();
        
        var worldManagerPrefab = await assetProvider.LoadAssetAsync<GameObject>("WorldManager");
        var worldManager = Instantiate(worldManagerPrefab).GetComponent<WorldManager>();
        worldManager.Initialize(player, cam, worldBuilder);
        ServiceLocator.Register(worldManager);

        worldBuilder.CreatePlatformItemPair(Vector2.zero);
        
        uiManager.HideLoadingScreen();
    }
}