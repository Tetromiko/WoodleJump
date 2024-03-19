using System;
using System.Threading.Tasks;
using Data;
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
        
        var itemDataBase = await assetProvider.LoadAssetAsync<ObjectDatabase>("ItemDatabase");
        
        var itemFactory = await CreateItemFactory(itemDataBase, assetProvider);

        var platformDataBase = await assetProvider.LoadAssetAsync<ObjectDatabase>("PlatformDatabase");
        
        var platformFactory = await CreatePlatformFactory(platformDataBase, assetProvider);

        var player = await CreatePlayer(assetProvider);

        var worldBuilder = await CreateWorldBuilder(assetProvider, platformFactory, itemFactory);

        cameraController.Initialize(player.transform);

        var cameraBox = CreateCameraBox();

        var deadZone = await CreateDeadZone(assetProvider, cameraBox);

        var ground = await CreateGround(assetProvider, cameraBox);

        await CreateWorldManager(assetProvider, player, cameraBox, worldBuilder, deadZone, ground);

        uiManager.HideLoadingScreen();
    }

    private static async Task<ItemFactory> CreateItemFactory(ObjectDatabase itemDataBase, AssetProvider assetProvider)
    {
        var itemFactory = new ItemFactory(itemDataBase, assetProvider);
        await itemFactory.Initialize();
        ServiceLocator.Register(itemFactory);
        return itemFactory;
    }

    private static async Task<PlatformFactory> CreatePlatformFactory(ObjectDatabase platformDataBase, AssetProvider assetProvider)
    {
        var platformFactory = new PlatformFactory(platformDataBase, assetProvider);
        await platformFactory.Initialize();
        ServiceLocator.Register(platformFactory);
        return platformFactory;
    }

    private static async Task<Player> CreatePlayer(AssetProvider assetProvider)
    {
        var playerPrefab = await assetProvider.LoadAssetAsync<GameObject>("Player");
        var player = Instantiate(playerPrefab).GetComponent<Player>();
        return player;
    }

    private static async Task<WorldBuilder> CreateWorldBuilder(AssetProvider assetProvider, PlatformFactory platformFactory,
        ItemFactory itemFactory)
    {
        var worldBuilderPrefab = await assetProvider.LoadAssetAsync<GameObject>("WorldBuilder");
        var worldBuilder = Instantiate(worldBuilderPrefab).GetComponent<WorldBuilder>();
        worldBuilder.Initialize(platformFactory, itemFactory);
        ServiceLocator.Register(worldBuilder);
        return worldBuilder;
    }

    private CameraBox CreateCameraBox()
    {
        var cam = cameraController.GetComponent<Camera>();
        var cameraBox = new CameraBox(cam, 1.2f);
        ServiceLocator.Register(cameraBox);
        return cameraBox;
    }

    private static async Task CreateWorldManager(AssetProvider assetProvider, Player player, CameraBox cameraBox,
        WorldBuilder worldBuilder, GameObject deadZone, GameObject ground)
    {
        var worldManagerPrefab = await assetProvider.LoadAssetAsync<GameObject>("WorldManager");
        var worldManager = Instantiate(worldManagerPrefab).GetComponent<WorldManager>();
        worldManager.Initialize(player.transform, cameraBox, worldBuilder, deadZone.transform, ground.transform);
        ServiceLocator.Register(worldManager);
    }

    private static async Task<GameObject> CreateDeadZone(AssetProvider assetProvider, CameraBox cameraBox)
    {
        var deadZonePrefab = await assetProvider.LoadAssetAsync<GameObject>("DeadZone");
        var deadZone = Instantiate(deadZonePrefab);
        deadZone.GetComponent<SpriteRenderer>().size = new Vector2(cameraBox.Size.x, 10);
        return deadZone;
    }

    private static async Task<GameObject> CreateGround(AssetProvider assetProvider, CameraBox cameraBox)
    {
        var groundPrefab = await assetProvider.LoadAssetAsync<GameObject>("Ground");
        var ground = Instantiate(groundPrefab);
        ground.GetComponent<SpriteRenderer>().size = new Vector2(cameraBox.Size.x, 10);
        ground.transform.GetChild(0).GetComponent<SpriteRenderer>().size = new Vector2(cameraBox.Size.x, 0.5f);
        ground.GetComponent<BoxCollider2D>().size = new Vector2(cameraBox.Size.x, 10);
        ground.transform.position = new Vector3(0, -cameraBox.Size.y/4);
        ground.GetComponent<BoxCollider2D>().offset = new Vector2(0, -5);
        return ground;
    }
}