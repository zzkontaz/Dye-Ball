using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPartHandler : MonoBehaviour
{
    private bool hasBalls;
    private bool hasCannon;
    private bool hasObstacle;
    public Transform[] ballSpawnTransforms;
    private List<int> selectedTransforms = new List<int>();


    public void SpawnBalls(GameObject ballPrefab,int maxBallCount)
    {
        for (int i = 0; i < maxBallCount; i++)
        {
            int index = UnityEngine.Random.Range(0, ballSpawnTransforms.Length);
            while (selectedTransforms.Contains(index))
            {
                index = UnityEngine.Random.Range(0, ballSpawnTransforms.Length);
            }
            selectedTransforms.Add(index);
            GameObject ball = Instantiate(ballPrefab, ballSpawnTransforms[index].position, Quaternion.identity);
        }
    }

    

}
