using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireThermoManager : MonoBehaviour
{
    private bool fireState = false;
    
    [SerializeField] Slider temperatureSlider;      
    [SerializeField] int maxBubbles = 10;                     
    private int currentBubbles = 0;

    [SerializeField] GameObject[] spawnPoints;
    [SerializeField] int maxSpawnInterval = 2;

    void Start()
    {
        if (temperatureSlider != null)
        {
            temperatureSlider.minValue = 0;
            temperatureSlider.maxValue = maxBubbles;
            temperatureSlider.value = 0;                                    // Inicia no valor mínimo
        }
        // StartCoroutine(Task1Timer());
        //  
    }

    public void SwitchFire()
    {
        fireState = !fireState;

        if (true)
        {
            StartCoroutine(SpawnBubbles());
        }
        else
        {

        }
            

        // on/off feedback visual

    }

    public void IncreaseTemperature(int type)
    {
        if (temperatureSlider != null && currentBubbles < maxBubbles)
        {
            if(type == 0)                                                   //small bubble
                currentBubbles++;
            else if (type == 1)                                             //big bubble
                currentBubbles += 2;

            // Certifica-se de que o valor não ultrapasse o máximo permitido
            if (currentBubbles > maxBubbles)
                currentBubbles = maxBubbles;

            temperatureSlider.value = currentBubbles;                       // Atualiza o slider
        }
    }

    public void DecreaseTemperature(int type)
    {
        if (temperatureSlider != null && currentBubbles > 0)
        {
            if (type == 0)                                                  
                currentBubbles--;
            else if (type == 1)                                          
                currentBubbles -= 2;

            // Certifica-se de que o valor não seja menor que zero
            if (currentBubbles < 0)
                currentBubbles = 0;

            temperatureSlider.value = currentBubbles; // Atualiza o slider
        }
    }

    public void ResetTemperature()
    {
        currentBubbles = 0;
        if (temperatureSlider != null)
            temperatureSlider.value = currentBubbles;
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
                IncreaseTemperature(pointsScript.bubbleTypeInt);
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
