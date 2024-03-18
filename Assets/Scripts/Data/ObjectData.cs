using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public abstract class ObjectData : ScriptableObject
    {
        public Sprite sprite;
        public int spawnChance;
    }
}