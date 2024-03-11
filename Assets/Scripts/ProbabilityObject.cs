using System;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

[Serializable]
public class ProbabilityObject
{
    [SerializeField] private int weight;
    [SerializeField] private Object obj;
    
    public int Weight => weight;
    public Object Object => obj;
    
    public ProbabilityObject(int weight, Object obj)
    {
        this.weight = weight;
        this.obj = obj;
    }
}