using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreen : MonoBehaviour
{
    int currentPage = 1;
    int laterPage;

    public GameObject player;
    Camera camera;

    public void Awake()
    {
        NowPage();
        camera = GetComponent<Camera>();
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
                camera.transform.position = new Vector3(-28.0f, 18.5f, -10);
                
                camera.rect = new Rect(0.0f, 0.0f, 0.7f, 1.0f);

                if (!nowStat)
                {
                    endPos = new Vector2(20.0f, 33.0f);
                    StartCoroutine(MoveTo(endPos));
                }    
                break;

            case 2 :
                camera.transform.position = new Vector3(80, -5, -10);
                camera.rect = new Rect(0.0f, 0.0f, 0.69f, 1.0f);

                if (nowStat)
                {
                    endPos = new Vector2(12.0f, 33.0f);
                    StartCoroutine(MoveTo(endPos));
                }
                else
                {
                    endPos = new Vector2(148.0f, 23.0f);
                    StartCoroutine(MoveTo(endPos));
                }
                break;

            case 3:
                camera.transform.position = new Vector3(188.0f, 23, -10);
                camera.rect = new Rect(0.0f, 0.0f, 0.75f, 1.0f);

                if (nowStat)
                {
                    endPos = new Vector2(156.0f, 23.0f);
                    StartCoroutine(MoveTo(endPos));
                }
                else
                {
                    
                }
                break;
        }

    }

    IEnumerator MoveTo(Vector2 toPos)
    {
        float count = -1;
        Vector2 wasPos = player.transform.position;
        while(true)
        {
            count += Time.deltaTime;
            player.transform.position = Vector2.Lerp(wasPos, toPos, count);
            if(count >= 2)
            {
                player.transform.position = toPos;
                break;
            }
            yield return null;
        }
    }
}
