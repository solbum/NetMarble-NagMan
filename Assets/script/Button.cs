using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    GameObject player;
    JoyStickMovement playerScript;

    public void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        JoyStickMovement playerScript = player.GetComponent<JoyStickMovement>();
    }

    public void LeftDown()
    {
        playerScript.inputLeft = true;
    }

    public void LeftUp()
    {
        playerScript.inputLeft = false;
    }

    public void RightDown()
    {
        playerScript.inputRight = true;
    }

    public void RightUp()
    {
        playerScript.inputRight = false;
    }

    public void JumpClick()
    {
        playerScript.inputJump = true;
    }

    public void InterecetionClick()
    {
        Debug.Log("상호작용!");
    }
}
