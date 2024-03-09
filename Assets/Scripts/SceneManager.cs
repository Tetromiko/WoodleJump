using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

public class SceneManager : IService
{
    public static void LoadScene(string sceneName)
    {
        Addressables.LoadSceneAsync(sceneName);
    }
}
