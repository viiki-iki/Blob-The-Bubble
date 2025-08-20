using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SocialPlatforms;

public class PlayerInteractions : MonoBehaviour
{
    // [SerializeField] private Texture2D defaultCursor;    // Ícone padrão do mouse
    //   [SerializeField] private Texture2D cauldronCursor;   // Ícone para quando estiver sobre o caldeirão
    // [SerializeField] private Vector2 hotSpotDefault = Vector2.zero;
    // [SerializeField] private Vector2 hotSpot2 = Vector2.zero;

    // [SerializeField] LayerMask bookLayer;
    [SerializeField] LayerMask cauldronLayer;
    [SerializeField] LayerMask bubbleLayer;

    [SerializeField] LayerMask spoonLayer;      //p movimento        
    [SerializeField] GameObject spoon;
    [SerializeField] GameObject spoonTip;
    // private SpoonTip tip;
    private Collider2D spoonTipCollider;   // p interações
    //private Vector2 spoonTipPos;

    //[SerializeField] LayerMask potionsInteractibleAreaLayer;

    // [SerializeField] BookPanelManager bookPanelController;
    //  [SerializeField] ItensManager itensManager;


   // public int teste;
    private bool isHoldingSpoon = false;
   // private bool isMouseOverCauldron = false;

    private bool isMousePressed = false;   

    private void Start()
    {
       // Cursor.SetCursor(defaultCursor, hotSpotDefault, CursorMode.Auto);
       // if (spoon != null)
       //     spoon.SetActive(false);

        //tip = spoon.GetComponentInChildren<SpoonTip>();
        spoonTipCollider = spoonTip.GetComponent<Collider2D>();
      //  spoonTipPos = spoonTip.transform.position;
    }

    bool IsTipInsideCauldron()
    {
        return Physics2D.OverlapPoint(spoonTipCollider.bounds.center, cauldronLayer) == null;
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
         //   RaycastHit2D hitBubble = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, bubbleLayer);
        //    RaycastHit2D hitCauldron = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, cauldronLayer);  //clique na area interativa interna 
            RaycastHit2D usingSpoon = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, spoonLayer); // clique na spoon


            //   RaycastHit2D hitPotionsInteractibleArea = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, potionsInteractibleAreaLayer);

            //  if (GameManager.Instance.isUsingItem == false)
            //  {
            // if (hitCauldron.collider != null)           

            if (usingSpoon.collider != null && usingSpoon.collider.gameObject == spoon.gameObject)
            {
                isHoldingSpoon = true;
            }
            if (isHoldingSpoon)
            {                                              
                Vector2 direction = worldPosition - (Vector2)spoon.transform.position;
                float distance = direction.magnitude;
                RaycastHit2D[] hits = new RaycastHit2D[1];
                int hitCount = spoonTipCollider.Cast(direction.normalized, hits, distance, true);
            //    teste = hitCount;

               // Collider2D hitCauldron = Physics2D.OverlapPoint(spoonTipPos, cauldronLayer);


                if (hitCount == 0)      // Sem colisão 
                {
                    spoon.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
                }
                else    // colisão 
                {
                    var hitLayer = hits[0].collider.gameObject.layer;
                    if (((1 << hitLayer) & cauldronLayer) == 0 && ((1 << hitLayer) & bubbleLayer) == 0)
                    {
                        spoon.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);                       
                    }
                    else
                    {
                        if(((1 << hitLayer) & cauldronLayer) != 0)
                        {
                            Debug.Log("BORDA");
                            isHoldingSpoon = false;
                            spoon.transform.position = spoon.transform.position;

                            if(((1 << hitLayer) & bubbleLayer) != 0)
                            {
                                //Debug.Log("bubble");
                                BubblesSpawnPoints bubble = hits[0].collider.GetComponentInParent<BubblesSpawnPoints>();
                                if (bubble != null)
                                {
                                    bubble.DestroyBubble();
                                }
                            }

                        }
                        else
                        {
                            spoon.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);

                            if (((1 << hitLayer) & bubbleLayer) != 0)
                            {
                                //Debug.Log("bubble");
                                BubblesSpawnPoints bubble = hits[0].collider.GetComponentInParent<BubblesSpawnPoints>();
                                if (bubble != null)
                                {
                                    bubble.DestroyBubble();
                                }
                            }

                        }
                                               
                    }

                }
                    
                    
                  //  if(!tip.Bubble())
                  //  {
                  //      spoon.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
                  //  }
                  //  else
                  //  {
                  //     
                  //   //   tip.SetCauldron(false);
                  //  }
                    
                    
                    
              //  }


                


                //spoon.transform.position = Vector3.Lerp(spoon.transform.position, targetPos, Time.deltaTime * 20f);
                // spoon.MovePosition(Vector3.Lerp(spoon.transform.position, targetPos, Time.deltaTime * 20f));

                // spoon.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);                   
            }


            //}

            //   if (hitBubble.collider != null)
            //   {
            //      Debug.Log("bubble");
            //      BubblesSpawnPoints bubble = hitBubble.collider.GetComponentInParent<BubblesSpawnPoints>();
            //      if (bubble != null)
            //      {
            //          bubble.DestroyBubble();
            //      }
            //   }
            //  }
            //   else
            //   {
            //  if (hitPotionsInteractibleArea.collider != null)
            //  {
            //      if (!GameManager.Instance.IsMissionCompleted("Mission1"))       //ainda n terminou missao 1
            //      {
            //          if(GameManager.Instance.activeItem.name == "Potion_SweetCristal")
            //          {
            //              //pitadas
            //              Debug.Log("sweet");
            //          }
            //      }                    
            //  }
            //  else
            //  {
            //      itensManager.ResetPotionInteraction();
            //  }
            //  }                     
        }
        else
        {
            isHoldingSpoon = false;
        }
    }
}
