using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WorldManager : MonoBehaviour, IService
{
    public float upperBoundOffset = 5;
    public float lowerBoundOffset = -5;
    
    private Transform _player;
    private CameraBox _cameraBox;
    private WorldBuilder _worldBuilder;
    private Transform _deadZone;
    private Transform _ground;
    
    private float _upperBound;
    private float _lowerBound;
    private float _deadLine;
    
    private List<GameObject> _platforms = new ();
    private bool _isInitialized;

    public void Initialize(Transform player, CameraBox cameraBox, WorldBuilder worldBuilder,
        Transform deadZone, Transform ground)
    {
        _player = player;
        _worldBuilder = worldBuilder;
        _cameraBox = cameraBox;
        _deadZone = deadZone;
        _ground = ground;

        do
        {
            Vector2 position;
            
            if (_platforms.Count == 0)
            {
                position = new Vector2(0, 0);
            }
            else
            {
                position = new Vector2(
                    Random.Range(-5, 6) / 2f,
                    Random.Range(0, 3) + _platforms[^1].transform.position.y
                );
            }
            
            _platforms.Add(_worldBuilder.CreatePlatformItemPair(position));
        }
        while(_platforms[^1].transform.position.y < 5);
        
        _deadLine = _player.position.y + lowerBoundOffset;
        _deadZone.position = new Vector3(0, _deadLine);
        _isInitialized = true;
    }

    private void Update()
    {
        if(!_isInitialized) return;
        
        CalculateBounds();

        ProcessPlatforms();

        LimitPlayerHorizontalPosition();
    }

    private void LimitPlayerHorizontalPosition()
    {
        var playerPosition = _player.position;
        _player.position = new Vector3(
            Mathf.Clamp( playerPosition.x, -_cameraBox.Size.x/2, _cameraBox.Size.x/2),
            playerPosition.y
        );
    }

    private void ProcessPlatforms()
    {
        for (int i = _platforms.Count - 1; i >= 0; i--)
        {
            var platformPosition = _platforms[i].transform.position;
            
            _platforms[i].SetActive(_cameraBox.IsInside(platformPosition));

            if (platformPosition.y < _deadLine)
            {
                Destroy(_platforms[i]);
                _platforms.RemoveAt(i);
            }
        }

        while (_platforms[^1].transform.position.y < _upperBound)
        {
            Vector2 position = new Vector2(
                Random.Range(-5, 6) / 2f,
                Random.Range(0, 3) + _platforms[^1].transform.position.y
            );
            _platforms.Add(_worldBuilder.CreatePlatformItemPair(position));
        }
    }

    private void CalculateBounds()
    {
        var playerPosition = _player.position;
        
        _upperBound = _cameraBox.Position.y + _cameraBox.Size.x/2 + upperBoundOffset;
        _lowerBound = playerPosition.y - _cameraBox.Size.x/2 + lowerBoundOffset;

        if (_deadLine < playerPosition.y + lowerBoundOffset)
        {
            _deadLine = playerPosition.y + lowerBoundOffset;
            _deadZone.position = new Vector3(0, _deadLine);
        }
    }

    private void OnDrawGizmos()
    {
        if(!_isInitialized) return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(int.MinValue, _upperBound), new Vector3(int.MaxValue,_upperBound));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(int.MinValue, _lowerBound), new Vector3(int.MaxValue,_lowerBound));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(int.MinValue, _deadLine), new Vector3(int.MaxValue,_deadLine));
    }
}