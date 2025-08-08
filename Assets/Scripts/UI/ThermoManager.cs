using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThermoManager : MonoBehaviour
{
    [SerializeField] Slider temperatureSlider;      
    [SerializeField] int maxBubbles = 10;                     
    private int currentBubbles = 0;

    void Start()
    {
        if (temperatureSlider != null)
        {
            temperatureSlider.minValue = 0;
            temperatureSlider.maxValue = maxBubbles;
            temperatureSlider.value = 0;                                    // Inicia no valor mínimo
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
}
