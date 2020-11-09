using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFairy : MonoBehaviour
{
    public int dir = 1;
    
    public bool isShut;

    Vector2 nowDir;
    Vector2 isPos;

    ReflectRays[] laser;

    private IEnumerator coroutine;

    private void Awake()
    {
        laser = transform.GetComponentsInChildren<ReflectRays>();
    }

    public void FixedUpdate()
    {
        isPos = new Vector2(transform.position.x, transform.position.y);
        switch (dir)
        {
            case 1:
                nowDir = transform.right;
                break;
            case 2:
                nowDir = -transform.up;
                break;
            case 3:
                nowDir = -transform.right;
                break;
            case 4:
                nowDir = transform.up;
                break;
        }

        laser[0].ShotRay(isPos, nowDir.normalized, 1000);
        //StartCoroutine(ShotChildRay());
    }

    IEnumerator ShotChildRay()
    {
        if (isShut)
        {
            laser[0].ShotRay(isPos, nowDir.normalized, 1000);
        }
        else
            yield return null;
           
    }

    void OnMouseDown()
    {
        if(dir < 4 && dir >0)
        {
            dir++;
        }
        else
        {
            dir = 1;
        }
    }

}
