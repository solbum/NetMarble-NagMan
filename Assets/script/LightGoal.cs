using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGoal : MonoBehaviour
{
    public GameObject player;

    public void ActiveArea()
    {
        player.SendMessage("LightComplete");
    }
}
