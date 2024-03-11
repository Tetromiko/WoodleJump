using UnityEngine;
using Object = UnityEngine.Object;

public class PlatformFactory : FactoryBase
{
    public PlatformFactory(ProbabilityObjectDataBase objectDataBase) : base(objectDataBase)
    {
        
    }

    public override GameObject Create(string key)
    {
        return Create(key, Vector2.zero);
    }

    public override GameObject Create(string key, Vector2 position)
    {
        return Create(key, position, null);
    }

    public override GameObject Create(string key, Vector2 position, Transform parent)
    {
        var platformPrefab = (GameObject)ObjectDataBase.GetObject();
        var platform = Object.Instantiate(platformPrefab, parent);
        platform.transform.localPosition = position;
        return platform;
    }

    public override void Dispose()
    {
        
    }
}