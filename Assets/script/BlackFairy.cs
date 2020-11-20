using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackFairy : MonoBehaviour
{
    public int dir = 1;

    public Vector2 nowDir;
    public Vector2 isPos;

    ReflectRays[] laser;

    private void Awake()
    {
        laser = transform.GetComponentsInChildren<ReflectRays>();
    }

    public void FixedUpdate()
    {
        isPos = this.transform.position;
        switch (dir)
        {
            case 1:
                nowDir = transform.right;
                isPos.x = this.transform.position.x + 2;
                break;
            case 2:
                nowDir = -transform.up;
                isPos.y = this.transform.position.y - 2;
                break;
            case 3:
                nowDir = -transform.right;
                isPos.x = this.transform.position.x - 2;
                break;
            case 4:
                nowDir = transform.up;
                isPos.y = this.transform.position.y + 2;
                break;
        }

        laser[0].ShotRay(isPos, nowDir, 100);
    }


    public void OnMouseDown()
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

    public void ChangeChildActive()
    {
        laser[0].gameObject.SetActive(true);
    }
}
