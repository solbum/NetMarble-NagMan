using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class myButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public PlayerMovement_Start player;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("눌렸네");
        player.JumpPress();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.JumpCancel();
    }
}
