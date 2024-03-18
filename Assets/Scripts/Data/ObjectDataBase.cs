using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Data
{ 
    [CreateAssetMenu(fileName = "ObjectDataBase", menuName = "DataBases/ObjectDataBase", order = 1)]
    public sealed class ObjectDatabase : ScriptableObject
    {
        [SerializeField] private List<ObjectData> objects = new();
        public Object GetObject(int index)
        {
            return objects[index];
        }

        public void SetData(List<ObjectData> data)
        {
            objects = data;
        }
    }
}