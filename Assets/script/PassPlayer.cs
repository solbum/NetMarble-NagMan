using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassPlayer : MonoBehaviour
{
    GameObject player;
    GameObject passColl;

    Vector2 setPos;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        passColl = this.gameObject;
        setPos = new Vector2(0, -5.0f);
    }

    private void FixedUpdate()
    {
        if (passColl.transform.position.y - setPos.y > player.transform.position.y)
        {
            passColl.GetComponent<BoxCollider2D>().isTrigger = true;
            Debug.Log("꺼진다");
        }
        else if (passColl.transform.position.y - setPos.y <= player.transform.position.y)
        {
            passColl.GetComponent<BoxCollider2D>().isTrigger = false;
            Debug.Log("켜진다.");
        }
    }
}
