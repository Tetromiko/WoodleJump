using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetProvider : IService
{
    private readonly Dictionary<string, object> _loadedAssets = new ();

    public async Task<T> LoadAssetAsync<T>(string address)
    {
        if (_loadedAssets.TryGetValue(address, out var loaded))
        {
            return (T)loaded;
        }
        
        var operationHandle = Addressables.LoadAssetAsync<T>(address);
        await operationHandle.Task;
        
        if (operationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            var result = operationHandle.Result;
            _loadedAssets[address] = result;
            return result;
        }
        Debug.LogError($"Failed to load asset at address: {address}");
        throw new NullReferenceException($"Failed to load asset at address: {address}");
    }
    
    public async Task<IList<T>> LoadLabeledAssetsAsync<T>(AssetLabelReference label)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;
        var loadedAssets = new List<T>();
        
        foreach (var location in locations)
        {
            var operationHandle = Addressables.LoadAssetAsync<T>(location.PrimaryKey);
            await operationHandle.Task;

            if (operationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                loadedAssets.Add(operationHandle.Result);
            }
            else
            {
                Debug.LogError($"Failed to load asset at location: {location.PrimaryKey}");
            }
        }

        return loadedAssets;
    }

    public void ReleaseAsset(string address)
    {
        if (!_loadedAssets.TryGetValue(address, out var loaded)) return;
        Addressables.Release(loaded);
        _loadedAssets.Remove(address);
    }
    
    public void ReleaseAllAssets()
    {
        foreach (var loadedAsset in _loadedAssets)
        {
            Addressables.Release(loadedAsset.Value);
        }
        _loadedAssets.Clear();
    }
}
