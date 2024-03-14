using UnityEngine;
using Object = UnityEngine.Object;

public class PlatformFactory : FactoryBase
{
    public PlatformFactory(ProbabilityObjectDataBase objectDataBase) : base(objectDataBase)
    {
        
    }

    public override GameObject Create()
    {
        return Create(Vector2.zero);
    }

    public override GameObject Create(Vector2 position)
    {
        return Create(position, null);
    }

    public override GameObject Create(Vector2 position, Transform parent)
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