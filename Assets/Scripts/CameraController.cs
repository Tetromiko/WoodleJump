using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public float followSpeed;
    
    private Transform _followTarget;

    public void Initialize(Transform followTarget)
    {
        _followTarget = followTarget;
    }
    
    private void Update()
    {
        if(_followTarget == null) return;
        
        var targetPosition = new Vector3(0,_followTarget.position.y) + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
}
