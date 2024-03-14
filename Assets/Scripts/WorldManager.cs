using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WorldManager : MonoBehaviour, IService
{
    private Player _player;
    private WorldBuilder _worldBuilder;
    private readonly Vector2 _startPoint = new( 0,-4);
    private readonly float _platformDistance = 3f;
    
    private List<GameObject> _platforms = new ();
    
    public void Initialize(Player player, WorldBuilder worldBuilder)
    {
        _player = player;
        _worldBuilder = worldBuilder;

        int i = 0;
        do
        {
            CreatePlatformPair();
            i++;
        }
        while (i < 10);
    }

    private void CreatePlatformPair()
    {
        Vector2 previousPosition;

        if (_platforms.Count == 0) previousPosition = _startPoint;
        else previousPosition = _platforms[^1].transform.position;

        var positionX = Mathf.Clamp(previousPosition.x + Random.Range(-2f, 2f), -3 , 3);
        var positionY = Mathf.Sqrt(_platformDistance * _platformDistance - positionX * positionX) + previousPosition.y;
        
        var position = new Vector2(positionX, positionY);
        
        Debug.Log(position + " " + previousPosition + " " + _platformDistance + " " + Vector2.Distance(position, previousPosition));
        
        var platform = _worldBuilder.CreatePlatform(position, Random.Range(0, 2) == 1);
        _platforms.Add(platform);
    }
}
