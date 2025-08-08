using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubblesSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] int maxSpawnInterval = 2;
    [SerializeField] ThermoManager thermoManager;

    void Start()
    {
       // StartCoroutine(Task1Timer());
        StartCoroutine(SpawnBubbles());     
    }

    IEnumerator SpawnBubbles()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("Nenhum ponto de spawn ou prefab configurado.");
            yield break;
        }

        while (!GameManager.Instance.IsMissionCompleted("Mission1"))
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);                                                  
            BubblesSpawnPoints pointsScript = spawnPoints[randomIndex].GetComponent<BubblesSpawnPoints>();          
            if (pointsScript.isEmpty)                                                                               
            {   
                Transform spawnPoint = spawnPoints[randomIndex].transform;
                GameObject bubble = Instantiate(pointsScript.GetBubbleType(), spawnPoint.position, Quaternion.identity, spawnPoint);
                
                pointsScript.bubble = bubble;           
                pointsScript.isEmpty = false;                                                                       
                thermoManager.IncreaseTemperature(pointsScript.bubbleTypeInt);                                         
            }
            int randomInterval = Random.Range(1, maxSpawnInterval);                                                 
            yield return new WaitForSeconds(randomInterval);
        }
        Debug.Log("task completada");
        yield break;
    }

    //IEnumerator Task1Timer()
    //{
    //    yield return new WaitForSeconds(30);
    //    GameManager.Instance.CompleteMission("Mission1");
    //}
}
