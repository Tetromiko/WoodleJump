using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "ObjectDataBase", menuName = "DataBases/ObjectDataBase", order = 1)]
public class ObjectDataBase : ScriptableObject
{
    [SerializeField] protected AssetLabelReference label;
    
    private readonly Dictionary<string, Object> _objects = new();
    protected AssetProvider AssetProvider;

    public virtual async Task Initialize(AssetProvider assetProvider)
    {
        AssetProvider = assetProvider;
        var loadedAssets = await AssetProvider.LoadLabeledAssetsAsync<object>(label);
        
        foreach (var loadedAsset in loadedAssets)
        {
            var loadedAssetObject = (Object)loadedAsset;
            _objects.Add(loadedAssetObject.name, loadedAssetObject);
        }
    }

    public Object GetObject(string key)
    {
        return _objects.GetValueOrDefault(key);
    }
}