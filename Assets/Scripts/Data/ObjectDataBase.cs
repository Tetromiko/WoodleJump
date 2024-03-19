using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Data
{ 
    [CreateAssetMenu(fileName = "ObjectDataBase", menuName = "DataBases/ObjectDataBase", order = 1)]
    public sealed class ObjectDatabase : ScriptableObject
    {
        [SerializeField] private List<ObjectData> objects = new();
        

        public void SetData(List<ObjectData> data)
        {
            objects = data;
        }
        
        public Object GetObject(int index)
        {
            return objects[index];
        }
        
        public Object GetRandomObject()
        {
            return objects[Random.Range(0, objects.Count)];
        }
        
        public Object GetRandomObjectBasedOnChance()
        {
            var total = 0;
            foreach (var obj in objects)
            {
                total += obj.spawnChance;
            }
            var random = Random.Range(0, total);
            foreach (var obj in objects)
            {
                if (random < obj.spawnChance)
                {
                    return obj;
                }
                random -= obj.spawnChance;
            }
            return null;
        }

        public void Clear()
        {
            objects.Clear();
        }

        public async Task AddDataAsync(ObjectData data)
        {
            await Task.Run(() =>
            {
                objects.Add(data);
            });
        }
    }
}