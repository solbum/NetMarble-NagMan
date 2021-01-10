using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPoint : MonoBehaviour
{
    GameObject line = null;
    Camera Camera;
    private GameObject target;


    private void Awake()
    {
        Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            target = GetClickedObject();
            Debug.Log("버튼을 누름");
            if (ObjectCheck())
            {
                line = this.transform.parent.gameObject;

                line.GetComponent<DrawingLine>().leftClick = true;

                this.gameObject.SetActive(false);
                Debug.Log("버튼 눌림");
            }
        }
    }

    private GameObject GetClickedObject()
    {
        GameObject target = null;

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
        }

        return target;
    }

    private bool ObjectCheck()
    {
        int i = 2;
        Vector2 newPosition = new Vector2(this.transform.position.x, this.transform.position.y);
            
        if(((target.transform.position.x >= newPosition.x -2 ) && (target.transform.position.y >= newPosition.y - 2)) 
            && ((target.transform.position.x <= newPosition.x + 2 ) && target.transform.position.y <= newPosition.y + 2 ))
        {
            return true;
        }
        return false;
    }
}