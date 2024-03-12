using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour, IService
{
    private Player _player;
    private WorldBuilder _worldBuilder;
    
    public void Initialize(Player player, WorldBuilder worldBuilder)
    {
        _player = player;
        _worldBuilder = worldBuilder;
    }
}
