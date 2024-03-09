using System.Threading.Tasks;

public class GameBootstrap : Bootstrap
{
    public string startSceneName = "GameplayScene";
    private SceneManager _sceneManager;
    
    protected override async Task Initialize()
    {
        var assetProvider = new AssetProvider();
        ServiceLocator.Register(assetProvider);
        
        LoadStartScene();
    }
    
    private void LoadStartScene()
    {
        SceneManager.LoadScene(startSceneName);
    }
}