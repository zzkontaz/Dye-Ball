using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    private TextMesh text;
    public int maxShotCount;
    private Transform shotPoint;

    private void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMesh>();
        shotPoint = transform.GetChild(1).GetComponent<Transform>();
        text.text = maxShotCount.ToString();

    }

   public void FireBall(GameObject ballObj)
    {
        if (maxShotCount > 0)
        {
            maxShotCount--;
            text.text = maxShotCount.ToString();
            ballObj.transform.position = shotPoint.position;
            ballObj.GetComponent<BallHandler>().FireTheBall();
        }
        else
        {
            text.text = "";
        }
    }
}
