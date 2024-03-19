using UnityEngine;

public class CameraBox : IService
{
    private readonly Transform _cameraTransform;
    private readonly Vector2 _size;
    
    public Vector2 Position => _cameraTransform.position;
    public Vector2 Size => new Vector3(_size.x, _size.y);

    public CameraBox(Camera cam, float multiplier)
    {
        _cameraTransform = cam.transform;
        _size = new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize)*2*multiplier;
    }
    
    public bool IsInside(Vector2 position)
    {
        var cameraPosition = _cameraTransform.position;
        return position.x > cameraPosition.x -_size.x/2 && 
               position.x < cameraPosition.x + _size.x/2 && 
               position.y > cameraPosition.y -_size.y/2 && 
               position.y < cameraPosition.y + _size.y/2;
    }
}