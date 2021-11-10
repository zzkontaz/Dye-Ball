using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int level;
    private int money;
    public GameObject[] levelPrefab;
    public Transform mapSpawnPos;
    public Transform playerSpawnPos;
    private UIHandler ui;
    private PlayerHandler player;
    private CameraFollower camera;
    
    
    
    /*
    public Transform groundHolderTransform;
    public GameObject groundPart;
    public GameObject ballPrefab;
    public int maxBallCount;
    public int maxObstacleCount;
    public List<int> selectedGroundIndexes = new List<int>();
    */
    

    private void Start()
    {
      //  CreatePath();
      //  CreateBalls();
      ui = FindObjectOfType<UIHandler>();
      player = FindObjectOfType<PlayerHandler>();
      camera = FindObjectOfType<CameraFollower>();
      ui.Splash();
      ui.UpdateLevelText(level);
      ui.UpdateMoneyText(money);
      CreateMap();
    }

    public void CreateMap()
    {
        if (GameObject.FindGameObjectWithTag("Map"))
        {
            Destroy(GameObject.FindGameObjectWithTag("Map"));
        }
        
        int i = UnityEngine.Random.Range(0, levelPrefab.Length);
        GameObject map = Instantiate(levelPrefab[i].gameObject, mapSpawnPos.position, Quaternion.identity);

    }


    public void StartTheGame()
    {
        ui.InGame();
        player.StartPlayer();
       
    }
    
    public void SetGame()
    {
        ui.Splash();
        camera.gameObject.transform.position = playerSpawnPos.position;
        camera.SetPlayer();
        player.transform.position = playerSpawnPos.position;
        CreateMap();
    }
    
    public void MissionComplete()
    {
        ui.LevelPass();
        player.canMove = false;
        LevelUp();
    }

    public void MissionFailed()
    {
        ui.LevelFail(); 
        player.canMove = false;
    }

    public void AddMoney(int value)
    {
        money += value;
        ui.UpdateMoneyText(money);
    }

    public void LevelUp()
    {
        level++;
        ui.UpdateLevelText(level);
    }

    public void GameFinish()
    {
        camera.SetFinishLine(GameObject.FindGameObjectWithTag("CameraFinish").gameObject.transform);
        Invoke("MissionComplete",1);
    }
    
     /*void CreatePath()
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
         
     }*/
}
