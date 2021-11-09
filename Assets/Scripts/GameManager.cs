using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int level;
    public bool canPlay;
    public Transform groundHolderTransform;
    public GameObject groundPart;
    public GameObject ballPrefab;
    public int maxBallCount;
    public int maxObstacleCount;
    public List<int> selectedGroundIndexes = new List<int>();
    

    private void Start()
    {
        CreatePath();
        CreateBalls();
    }

    
     void CreatePath()
    {
        int pathCount = UnityEngine.Random.Range(level + 15, level + 20);
        int nextPath = 0;
        
        for (int i = 0; i < pathCount; i++)
        {
            Vector3 truePathTransform = new Vector3(groundHolderTransform.localPosition.x,
                groundHolderTransform.localPosition.y,groundHolderTransform.localPosition.z + nextPath);
            
            GameObject path = Instantiate(groundPart, truePathTransform, Quaternion.identity,
                groundHolderTransform);
            nextPath += 10;
        }
    }

     void CreateBalls()
     {
         GameObject[] groundParts = GameObject.FindGameObjectsWithTag("Ground");

         for (int i = 0; i < maxBallCount; i++)
         {
             int index = UnityEngine.Random.Range(0, groundParts.Length);
             while (selectedGroundIndexes.Contains(index))
             {
                 index = UnityEngine.Random.Range(0, groundParts.Length);
             }
             selectedGroundIndexes.Add(index);

             int ballCountInOneGround = UnityEngine.Random.Range(3, 6);
             groundParts[index].GetComponent<GroundPartHandler>().SpawnBalls(ballPrefab,ballCountInOneGround);
         }
         
     }
}
