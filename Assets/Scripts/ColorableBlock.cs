using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorableBlock : MonoBehaviour
{

    public int maxColorCount;
    private bool canBeHit;
   
    private MeshRenderer renderer;
    
    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        canBeHit = true;
    }

   public void GotHitByBall(Color matColor)
    {
        if (canBeHit)
        {
            maxColorCount--;
            if (maxColorCount == 0)
            {
            ObjectGotColored(matColor);
            }
        }
    }

   void ObjectGotColored(Color matColor)
   {
       canBeHit = false;
       renderer.material.SetColor("_Color",matColor);
   
       // Para kazanacağız
   }
    
}
