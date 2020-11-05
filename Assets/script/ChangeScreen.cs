using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreen : MonoBehaviour
{
    int currentPage = 1;
    int laterPage;


    public GameObject camera;

    public void Awake()
    {
        NowPage();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("CPage"))
        {
            Vector2 playerPos = this.gameObject.transform.position;
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
        Vector2 endPos;

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
                camera.transform.position = new Vector3(middlePos.x, 13, -10);

                if (!nowStat)
                {
                    endPos = new Vector2(middlePos.x + 45.0f, this.transform.position.y);
                    StartCoroutine(MoveTo(GameObject.FindWithTag("Player"), endPos));
                }    
                break;

            case 2 :
                middlePos.x = 102;
                camera.transform.position = new Vector3(middlePos.x, 13, -10);

                if (nowStat)
                {
                    endPos = new Vector2(middlePos.x - 45.0f, this.transform.position.y);
                    StartCoroutine(MoveTo(GameObject.FindWithTag("Player"), endPos));
                }
                else
                {
                    endPos = new Vector2(middlePos.x + 45.0f, this.transform.position.y);
                    StartCoroutine(MoveTo(GameObject.FindWithTag("Player"), endPos));
                }
                break;
        }

    }

    IEnumerator MoveTo(GameObject a, Vector2 toPos)
    {
        float count = -1;
        Vector2 wasPos = a.transform.position;
        while(true)
        {
            count += Time.deltaTime;
            a.transform.position = Vector2.Lerp(wasPos, toPos, count);
            if(count >= 1)
            {
                a.transform.position = toPos;
                break;
            }
            yield return null;
        }
    }
}
