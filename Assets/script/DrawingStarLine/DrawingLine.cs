using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingLine : MonoBehaviour
{
    int nowChild;
    public bool leftClick, rightClick;

    private void Awake()
    {
        this.transform.GetChild(2).gameObject.SetActive(false);
        leftClick = false;
        rightClick = false;
    }

    private void FixedUpdate()
    {
        if(leftClick && rightClick)
        {
            this.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

}
