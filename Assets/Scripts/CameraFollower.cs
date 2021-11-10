using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform targetPlayer,targetFinishLine;
    public Transform lookAtPlayer;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public bool isPlayerFollow;


    public void SetFinishLine(Transform transform)
    {
        targetFinishLine.transform.position = transform.transform.position;
        isPlayerFollow = false;
    }

    public void SetPlayer()
    {
        isPlayerFollow = true;
    }
    
    void FixedUpdate ()
    {
       
        var pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, -2, 2);
        if (isPlayerFollow)
        {
            Vector3 desiredPosition = targetPlayer.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(lookAtPlayer);
        }
        else
        {
            Vector3 desiredPosition = targetFinishLine.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(targetFinishLine);
        }
     
      
        
     
    }

}

