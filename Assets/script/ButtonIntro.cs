using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIntro : MonoBehaviour
{
    Animator animator;

    public int ButtonNum;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        Debug.Log("0");
        if (Input.GetKey(KeyCode.K))
        {
            if (coll.gameObject.tag == "Player")
            {
                if (this.gameObject.tag == "FirstButton")
                {
                    Debug.Log("1");
                    ButtonNum = 1;
                }
                else if (this.gameObject.tag == "SecondButton")
                {
                    Debug.Log("2");
                    ButtonNum = 2;
                }
                else if (this.gameObject.tag == "ThirdButton")
                {
                    Debug.Log("3");
                    ButtonNum = 3;
                }
            }
        }
    }
}
