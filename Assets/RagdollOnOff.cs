using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnOff : MonoBehaviour
{
    
  
    void Start()
    {
        SetKinematic(true);
     
    }
    public void SetKinematic(bool isTurnedOn)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = isTurnedOn;
        }
    }
    
}
