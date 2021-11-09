using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public Transform lookAt;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void FixedUpdate ()
    {
        var pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, -2, 2);
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
        transform.LookAt(lookAt);
    }

}

