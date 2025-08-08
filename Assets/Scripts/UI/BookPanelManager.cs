using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookPanelManager : MonoBehaviour
{
    public CanvasGroup mainPanel;
    public CanvasGroup buttonsPanel;
    public GameObject bookPanel;

    [SerializeField] Button closeButton;
    [SerializeField] Button openButton;

    void Awake()
    {
        closeButton.onClick.AddListener(() => PopUpSwitch(false));
        openButton.onClick.AddListener(() => PopUpSwitch(true));
    }

    void Start()
    {
        MainPanelSwitch(false);
        mainPanel.alpha = 0f;
        
       // PopUpSwitch(true);       
    }

    public void PopUpSwitch(bool enable)
    {
        if (enable)
            StartCoroutine(FadeInPopup(0.5f));
        else
            StartCoroutine(FadeOutPopup(0.5f));
    }

    IEnumerator FadeInPopup(float duration)
    {
        MainPanelSwitch(true);        
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            mainPanel.alpha = Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }       
    }

    IEnumerator FadeOutPopup(float duration)
    {
        float elapsedTime = 0f;     
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            mainPanel.alpha = Mathf.Clamp01(1 - (elapsedTime / duration));
            yield return null;
        }
        MainPanelSwitch(false);
    }

    void MainPanelSwitch(bool enable)
    {
        mainPanel.gameObject.SetActive(enable);
        mainPanel.interactable = enable;
        mainPanel.blocksRaycasts = enable;
        bookPanel.SetActive(enable);
        buttonsPanel.interactable = !enable;
    }
}
