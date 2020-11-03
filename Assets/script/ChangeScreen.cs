using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreen : MonoBehaviour
{
    int currentPage = 1;
    int laterPage;


    public GameObject player;

    public void Awake()
    {
        NowPage();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("CPage"))
        {
            Vector2 playerPos = player.gameObject.transform.position;
            Vector2 thatPos = coll.gameObject.transform.position;

            laterPage = currentPage;

            if (playerPos.x <= thatPos.x)
            {
                currentPage++;
            }
            else // thisPos.x > thatPos.x
            {
                currentPage--;
            }
        }

        NowPage();
    }

    void NowPage()
    {
        Vector2 middlePos;
        bool nowStat;
        if(laterPage <= currentPage)
        {
            nowStat = true;
        }
        else
        {
            nowStat = false;
        }
        switch(currentPage)
        {
            case 1 :
                middlePos.x = 5;
                this.transform.position = new Vector2(middlePos.x, 13);
                if (nowStat)
                {
                    player.transform.();
                }    
                break;
            case 2 :
                this.transform.position = new Vector2(102, 13);
                break;
        }

    }
}
