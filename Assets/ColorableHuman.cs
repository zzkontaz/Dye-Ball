using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorableHuman : MonoBehaviour
{
    public int maxColorCount;
    private bool canBeHit;
    public SkinnedMeshRenderer[] renderer;
    private GameManager manager;
    
    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
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
        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].material.SetColor("_Color",matColor);
        }
        gameObject.GetComponent<RagdollOnOff>().SetKinematic(false);
        manager.AddMoney(5);
        // Para kazanacağız
    }
}
