using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireThermoManager : MonoBehaviour
{
    [Header("fire")]
    public bool fireState = false;
    [SerializeField] Button fireButton;
    [SerializeField] GameObject fireSprite;

    [Header ("thermometro")]
    [SerializeField] Slider temperatureSlider;

    [Header("heat bubbles")]
    [SerializeField] int maxBubbles = 10;                     
    private int currentBubbles = 0;
    [SerializeField] GameObject[] spawnPointsBase;      //apenas pegar posição e capacidade; nao mexer em isempty
    [SerializeField] int maxSpawnInterval;
    List<BubblesSpawnPoints> emptyPoints = new List<BubblesSpawnPoints>();

    public float missiontimerteste;

    public int emptynn;

    private void Awake()
    {
        fireButton.onClick.AddListener(SwitchFire);
    }

    void Start()
    {       
        if (temperatureSlider != null)
        {
            temperatureSlider.minValue = 0;
            temperatureSlider.maxValue = maxBubbles;
            temperatureSlider.value = 0;                                    // Inicia no valor mínimo
        }
        emptyPoints.Capacity = spawnPointsBase.Length;
        ResetCauldron();        
        StartCoroutine(Task1Timer());                       //teste
    }

    void ResetCauldron()        
    {        
        emptyPoints.Clear();
        foreach (var point in spawnPointsBase)      //vai verificar 1x
        {
            BubblesSpawnPoints script = point.GetComponent<BubblesSpawnPoints>();
           // script.isEmpty = true;           
            emptyPoints.Add(script);                        
        }
    }

    private void SwitchFire()
    {
        fireState = !fireState;

        if (fireState)
        {           
            StartCoroutine(HeatingUp());
            Debug.Log("fogo acesso");
            fireSprite.SetActive(true); //anim
        }
        else
        {          
            //StartCoroutine(CoolingDown());
            fireSprite.SetActive(false);
            Debug.Log("fogo desligado");    //anim
        }
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

    IEnumerator Task1Timer()
    {
        GameManager.Instance.ResetMissionsForTesting();
        Debug.Log("missao 1 começou");
        yield return new WaitForSeconds(missiontimerteste);
        
        GameManager.Instance.CompleteMission("Mission1");
        Debug.Log("missao 1 terminou caraio");
    }

    IEnumerator HeatingUp()    
    {
        if (spawnPointsBase.Length == 0 || emptyPoints.Capacity == 0)
        {
            Debug.LogWarning("Nenhum ponto de spawn ou prefab configurado.");
            yield break;
        }

        yield return new WaitForSeconds(5);       

        while (!GameManager.Instance.IsMissionCompleted("Mission1"))
        {             
            if(emptyPoints.Count == 0)
            {               
                ResetCauldron();
                Debug.Log("0");
                break;
            }                                          
            int randomIndex = Random.Range(0, emptyPoints.Count);
            BubblesSpawnPoints pointsScript = emptyPoints[randomIndex];

          //  if(pointsScript.isEmpty)
          //  {
                Transform spawnPoint = pointsScript.transform;
                GameObject bubble = Instantiate(pointsScript.GetBubbleType(), spawnPoint.position, Quaternion.identity, spawnPoint);

                pointsScript.bubble = bubble;
              //  pointsScript.isEmpty = false;
                emptyPoints.Remove(pointsScript);
          //  }
                  IncreaseTemperature(pointsScript.bubbleTypeInt);
                 
            int randomInterval = Random.Range(1, maxSpawnInterval);                
            yield return new WaitForSeconds(randomInterval);           
        }
        Debug.Log("task completada");
        yield break;
    }

    public IEnumerator CoolingDown()
    {
        yield return new WaitForSeconds(5);
        yield break;
    }  
}
