using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    public Color[] colors;
    private bool isStacked;
    private bool isFired;
    private PlayerHandler player;
    private Animator anim;
    public int ballOrder;
    private int colorIndex;


    private void Start()
    {
        InitColorToMaterial();
        InitComponents();
    }

    void InitComponents()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        anim = GetComponent<Animator>();
    }

    void InitColorToMaterial()
    {
      colorIndex  = UnityEngine.Random.Range(0, colors.Length);
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material.SetColor("_Color",colors[colorIndex]);
    }

    void PlayerStackedThisBall()
    {
        if (!isStacked && !isFired)
        {
            isStacked = true;
            player.AddBallToStackList(gameObject);
        }
    }

    public void PlayStackedBallAnimation()
    {
        anim.SetTrigger("Collected");
    }

    private void FixedUpdate()
    {
        if(isStacked)HandleStacked();
        if(isFired)HandleFired();
    }

    void HandleStacked()
    {
         
        
        float x = Mathf.Lerp(transform.position.x, player.StackTransform.position.x, Mathf.Abs(ballOrder - player.stackedBalls.Count -1 ) * 4f * Time.deltaTime);
       
        Vector3 pos = new Vector3(x,1,player.StackTransform.position.z + ballOrder + 1.5f);
        transform.position = pos;
    }

    void HandleFired()
    {
        transform.position += transform.forward * 30 * Time.deltaTime;
    }

    public void FireTheBall()
    {
        isFired = true;
        isStacked = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ball"))
        {
            PlayerStackedThisBall();
        }

        if (other.CompareTag("Obstacle"))
        {
            if(other.GetComponent<ColorableBlock>() != null)
            other.GetComponent<ColorableBlock>().GotHitByBall(colors[colorIndex]);
            
            if(other.GetComponent<ColorableHuman>() != null)
                other.GetComponent<ColorableHuman>().GotHitByBall(colors[colorIndex]);
            
            player.stackedBalls.Remove(gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Cannon"))
        {
            other.GetComponent<Cannon>().FireBall(gameObject);
            if (other.GetComponent<Cannon>().maxShotCount > 0)
            {
              //  player.stackedBalls.Remove(gameObject);
            }
        }

        if (other.CompareTag("Wall"))
        {
            player.stackedBalls.Remove(gameObject);
            Destroy(gameObject);
        }
        
    }
}
