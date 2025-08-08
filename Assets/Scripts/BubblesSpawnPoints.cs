using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesSpawnPoints : MonoBehaviour
{
    public bool isEmpty = true;
    [SerializeField] GameObject[] bubbleTypesPrefabs;
    [HideInInspector] public GameObject bubble;
    [HideInInspector] public int bubbleTypeInt;
    [SerializeField] ThermoManager thermoManager;

    public GameObject GetBubbleType()
    {
        int randomIndex = Random.Range(0, bubbleTypesPrefabs.Length);
        bubbleTypeInt = randomIndex;
        return bubbleTypesPrefabs[randomIndex];
    }

    public void DestroyBubble()
    {
        // Aqui você pode adicionar uma animação antes de destruir
        Destroy(bubble);
        isEmpty = true;
        thermoManager.DecreaseTemperature(bubbleTypeInt);
    }
}
