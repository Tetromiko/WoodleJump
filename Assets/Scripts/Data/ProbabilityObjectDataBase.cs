using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ProbabilityObjectDataBase", menuName = "DataBases/ProbabilityObjectDataBase", order = 0)]
public class ProbabilityObjectDatabase : ScriptableObject
{
    [SerializeField] private AssetLabelReference label;
    [SerializeField] private ProbabilityList probabilityObjectsData = new();
    
    private AssetProvider _assetProvider;

    public async Task Initialize(AssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
        var loadedAssets = await _assetProvider.LoadLabeledAssetsAsync<object>(label);

        if (loadedAssets.Count != probabilityObjectsData.Count)
        {
            probabilityObjectsData.Clear();
        
            foreach (var probabilityObject in loadedAssets)
            {
                probabilityObjectsData.Add((Object)probabilityObject);
            }
        }
    }
    
    public Object GetObject()
    {
        return probabilityObjectsData.Get();;
    }
}