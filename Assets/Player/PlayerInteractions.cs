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
    [SerializeField] LayerMask bubbleLayer;
    [SerializeField] LayerMask cauldronLayer;
    [SerializeField] LayerMask spoonLayer;      //p movimento
    [SerializeField] Collider2D spoonTipCollider;   // p interações
    [SerializeField] EdgeCollider2D edgeTipCollider;

    //[SerializeField] LayerMask potionsInteractibleAreaLayer;

    // [SerializeField] BookPanelManager bookPanelController;
    //  [SerializeField] ItensManager itensManager;
       [SerializeField] GameObject spoon;

    public int teste;

    private bool isHoldingSpoon = false;
   // private bool isMouseOverCauldron = false;

    private bool isMousePressed = false;   

    private void Start()
    {
       // Cursor.SetCursor(defaultCursor, hotSpotDefault, CursorMode.Auto);
       // if (spoon != null)
       //     spoon.SetActive(false);
    }

    bool IsTipInsideCauldron()
    {
        // Testa a posição da ponta (centro do collider)

        return Physics2D.OverlapPoint(spoonTipCollider.bounds.center, cauldronLayer) != null;
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
            RaycastHit2D hitCauldron = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, cauldronLayer);  //clique na area interativa interna 
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
                // Cast do collider da pontinha na direção do movimento
                RaycastHit2D[] hits = new RaycastHit2D[1];
                int hitCount = spoonTipCollider.Cast(direction.normalized, hits, distance, true);

                teste = hitCount;

                if (hitCount == 0)
                {
                    // Sem colisão → move normalmente
                    spoon.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
                }
                else
                {
                    spoon.transform.position = spoon.transform.position;
                    isHoldingSpoon = false;
                }


                //if (hit.collider == null)
                //    {

                // if (IsTipInsideCauldron())
                // {
                //     // Sem colisão → move normalmente
                //     Vector3 targetPos = new Vector3(worldPosition.x, worldPosition.y, 0f);
                //     spoon.MovePosition(targetPos);
                // }
                // else
                // {
                //    // Com colisão → move até tocar a borda
                //     spoon.MovePosition(hit.point);
                // }

                //  }
                //else
                //  {
                // 
                // }



                //spoon.transform.position = Vector3.Lerp(spoon.transform.position, targetPos, Time.deltaTime * 20f);
                // spoon.MovePosition(Vector3.Lerp(spoon.transform.position, targetPos, Time.deltaTime * 20f));

                // spoon.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);                   
            }


            //}

            //   if (hitBubble.collider != null)
            //   {
            //       Debug.Log("bubble");
            //       BubblesSpawnPoints bubble = hitBubble.collider.GetComponentInParent<BubblesSpawnPoints>();
            //       if (bubble != null)
            //       {
            //           bubble.DestroyBubble();
            //       }
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
