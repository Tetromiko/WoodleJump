using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WorldManager : MonoBehaviour, IService
{
    private Player _player;
    private Camera _cam;
    private WorldBuilder _worldBuilder;
    
    private List<GameObject> _platforms = new ();
    
    public void Initialize(Player player, Camera cam, WorldBuilder worldBuilder)
    {
        _player = player;
        _cam = cam;
        _worldBuilder = worldBuilder;

        do
        {
            Vector2 position;
            if(_platforms.Count == 0) position = new Vector2(0, 0);
            else position = new Vector2(
                Random.Range(-5, 6) / 2f,
                Random.Range(0, 3) + _platforms[^1].transform.position.y
            );
            _platforms.Add(_worldBuilder.CreatePlatformItemPair(position));
        }while(_platforms[^1].transform.position.y < 5);
    }

    public void PlacePlatformAtRandomPosition(GameObject platform)
    {
        
        // Vector2 previousPosition;
        //
        // if (_platforms.Count == 0) previousPosition = _startPoint;
        // else previousPosition = _platforms[^1].transform.position;
        //
        // var positionX = Random.Range(-5, 6)/2f;
        // var positionY = Mathf.Sqrt(_platformDistance * _platformDistance - positionX * positionX) + previousPosition.y;
        //
        // var position = new Vector2(positionX, positionY);
        //
        // var platform = _worldBuilder.CreatePlatform(position, Random.Range(0, 2) == 1);
        // _platforms.Add(platform);
    }
}