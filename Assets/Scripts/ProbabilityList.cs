using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[Serializable]
public class ProbabilityList : IEnumerable
{
    [SerializeField] private List<ProbabilityObject> probabilityObjects = new();
    private int _sumOfProbabilities;
    public int Count => probabilityObjects.Count;

    public void Add(Object obj) => Add(obj, 0);
    public void Add(Object obj, int probability)
    {
        ProbabilityObject probabilityObject = new ProbabilityObject(probability, obj);
        probabilityObjects.Add(probabilityObject);
        
    }
    public void Remove(Object obj)
    {
        var probabilityObject = probabilityObjects.Find(x => x.Object == obj);
        if (probabilityObject == null) return;
        probabilityObjects.Remove(probabilityObject);
        _sumOfProbabilities -= probabilityObject.Weight;
    }
    
    public Object Get()
    {
        _sumOfProbabilities = probabilityObjects.Sum(x => x.Weight);
        var randomValue = Random.Range(0, _sumOfProbabilities);
        
        foreach (var probabilityObject in probabilityObjects)
        {
            randomValue += probabilityObject.Weight;
            if (_sumOfProbabilities <= randomValue)
                return probabilityObject.Object;
        }

        return null;
    }
    
    public void Clear()
    {
        probabilityObjects.Clear();
        _sumOfProbabilities = 0;
    }

    public IEnumerator GetEnumerator()
    {
        return probabilityObjects.GetEnumerator();
    }
}