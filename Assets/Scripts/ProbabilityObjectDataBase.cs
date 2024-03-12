using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ProbabilityObjectDataBase", menuName = "DataBases/ProbabilityObjectDataBase", order = 0)]
public class ProbabilityObjectDataBase : ScriptableObject
{
    [SerializeField] private AssetLabelReference label;
    
    [SerializeField] private ProbabilityList probabilityObjects = new();
    private AssetProvider _assetProvider;

    public async Task Initialize(AssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
        var loadedAssets = await _assetProvider.LoadLabeledAssetsAsync<object>(label);

        if (loadedAssets.Count != probabilityObjects.Count)
        {
            probabilityObjects.Clear();
        
            foreach (var probabilityObject in loadedAssets)
            {
                probabilityObjects.Add((Object)probabilityObject);
            }
        }
    }
    
    public Object GetObject()
    {
        return probabilityObjects.Get();;
    }
}