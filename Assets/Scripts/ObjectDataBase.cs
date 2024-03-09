using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ObjectDataBase", menuName = "DataBases/ObjectDataBase", order = 1)]
public class ObjectDataBase: ScriptableObject
{
    private readonly Dictionary<string, Object> _objects = new();

    private AssetProvider _assetProvider;
    [SerializeField] private AssetLabelReference label;

    public async Task Initialize(AssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
        var loadedAssets = await _assetProvider.LoadLabeledAssetsAsync<object>(label);
        
        Debug.Log($"{loadedAssets.Count}");
        
        foreach (var loadedAsset in loadedAssets)
        {
            var loadedAssetObject = (Object)loadedAsset;
            _objects.Add(loadedAssetObject.name, loadedAssetObject);
        }
    }

    public Object GetObject(string key)
    {
        Debug.Log($"{_objects.GetValueOrDefault(key).GetType()}");
        return _objects.GetValueOrDefault(key);
    }
}