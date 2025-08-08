using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

public class PlayerInteractions : MonoBehaviour
{
   // [SerializeField] private Texture2D defaultCursor;    // Ícone padrão do mouse
 //   [SerializeField] private Texture2D cauldronCursor;   // Ícone para quando estiver sobre o caldeirão
   // [SerializeField] private Vector2 hotSpotDefault = Vector2.zero;
   // [SerializeField] private Vector2 hotSpot2 = Vector2.zero;

   // [SerializeField] LayerMask bookLayer;
    [SerializeField] LayerMask bubbleLayer;
    [SerializeField] LayerMask cauldronLayer;
    [SerializeField] LayerMask potionsInteractibleAreaLayer;

    [SerializeField] BookPanelManager bookPanelController;
    [SerializeField] ItensManager itensManager;

    [SerializeField] GameObject spoon;
   // private bool isMouseOverCauldron = false;

    private bool isMousePressed = false;   

    private void Start()
    {
       // Cursor.SetCursor(defaultCursor, hotSpotDefault, CursorMode.Auto);
       // if (spoon != null)
       //     spoon.SetActive(false);
    }

    public void OnClick(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            isMousePressed = true;
        else if (ctx.canceled)
            isMousePressed = false;
    }

    void Update()
    {
        if (isMousePressed)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            RaycastHit2D hitBubble = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, bubbleLayer);
            RaycastHit2D hitCauldron = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, cauldronLayer);
            RaycastHit2D hitPotionsInteractibleArea = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, potionsInteractibleAreaLayer);

            if (GameManager.Instance.isUsingItem == false)
            {
                if (hitCauldron.collider != null)
                {
                   // isMouseOverCauldron = true;
                    if (spoon != null)
                    {
                        spoon.SetActive(true);
                        spoon.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
                    }
                }
                //else
                //{
                // //   isMouseOverCauldron = false;
                //    if (spoon != null)
                //        spoon.SetActive(false);
                //}

                if (hitBubble.collider != null)
                {
                    Debug.Log("bubble");
                    BubblesSpawnPoints bubble = hitBubble.collider.GetComponentInParent<BubblesSpawnPoints>();
                    if (bubble != null)
                    {
                        bubble.DestroyBubble();
                    }
                }
            }
            else
            {
                if (hitPotionsInteractibleArea.collider != null)
                {
                    if (!GameManager.Instance.IsMissionCompleted("Mission1"))       //ainda n terminou missao 1
                    {
                        if(GameManager.Instance.activeItem.name == "Potion_SweetCristal")
                        {
                            //pitadas
                            Debug.Log("sweet");
                        }
                    }                    
                }
                else
                {
                    itensManager.ResetPotionInteraction();
                }
            }                     
        }
    }
}
