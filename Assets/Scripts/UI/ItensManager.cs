using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ItensManager : MonoBehaviour
{
    [SerializeField] Button[] potionsButtons;
    [SerializeField] GameObject[] potionsSpritesPrefabs;
    private GameObject activePotionSprite = null;

    [SerializeField] Transform parent;
    [SerializeField] GameObject cauldronArea;

    void Awake()
    {
        for (int i = 0; i < potionsButtons.Length; i++)
        {
            int index = i;
            potionsButtons[i].onClick.AddListener(() => OnPotionButtonClick(index));
        }            
    }

    void Start()
    {
        cauldronArea.SetActive(false);
    }

    private void OnPotionButtonClick(int id)
    {
        if (GameManager.Instance.isUsingItem == false)
        {
            if (potionsButtons[id] != null)
            {
                potionsButtons[id].gameObject.SetActive(false);
                cauldronArea.SetActive(true);           
            }                        
            if (potionsSpritesPrefabs[id] != null)
            {
                activePotionSprite = Instantiate(potionsSpritesPrefabs[id], parent);
                GameManager.Instance.isUsingItem = true;
                GameManager.Instance.activeItem = activePotionSprite;
                GameManager.Instance.lastButtonClicked = potionsButtons[id];
            }             
        }               
    }

    private void Update()
    {
        if (activePotionSprite != null)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            activePotionSprite.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);

            if (cauldronArea.GetComponent<Collider2D>().OverlapPoint(activePotionSprite.transform.position))
            {
                Debug.Log("Mouse está sobre o alvo!");
                // Adicione aqui o que você deseja fazer quando o mouse estiver dentro do collider
            }
            else
            {

            }
        }
    }

    public void ResetPotionInteraction()
    {
        Destroy(activePotionSprite);
        GameManager.Instance.isUsingItem = false;                               //nao esta mais usando um item
        GameManager.Instance.activeItem = null;                                 
        GameManager.Instance.lastButtonClicked.gameObject.SetActive(true);      //ligar botao de novo
        GameManager.Instance.lastButtonClicked = null;  
        Debug.Log("reset de poção");
    }
}
