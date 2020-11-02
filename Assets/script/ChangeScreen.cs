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
            Vector2 thisPos = this.gameObject.transform.position;
            Vector2 thatPos = coll.gameObject.transform.position;

            laterPage = currentPage;

            if (thisPos.x <= thatPos.x)
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

        switch(currentPage)
        {
            case 1 :
                if
                this.transform.position = new Vector2(5, 13);
                break;
            case 2 :
                this.transform.position = new Vector2(102, 13);
                break;
        }

    }
}
