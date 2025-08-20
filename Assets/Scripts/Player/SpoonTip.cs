using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpoonTip : MonoBehaviour
{
    [SerializeField] LayerMask bubbleLayer;
    private bool bubble = false;

    public bool Bubble() {return bubble;}

    public void SetBubble(bool value)
    {
        bubble = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & bubbleLayer) != 0)
        {        
           // bubble = true;
            Debug.Log("Colidiu com bolha!");
        }
    }

    //  private void OnTriggerExit2D(Collider2D other)
    //  {
    //     // if (((1 << other.gameObject.layer) & bubbleLayer) != 0)
    //     // {
    //     //     bubble = false;
    //     // }
    //  }
}
