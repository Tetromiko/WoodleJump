using UnityEngine.AddressableAssets;

public class SceneManager : IService
{
    public static void LoadScene(string sceneName)
    {
        Addressables.LoadSceneAsync(sceneName);
    }
}
