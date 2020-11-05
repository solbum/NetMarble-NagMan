using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use_Useable : MonoBehaviour
{ 
    public Vector3 playerPosition;
    public Transform target;

    public bool CheckPicking = false;
    

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (coll.gameObject.tag == "Player")
            {
                //if()
                CheckPicking = !CheckPicking;
                Debug.Log("플레이어");
                StartCoroutine(Coroutine());
            }
        }
    }
    private void Updata()
    {
        target = GameObject.Find("Player").transform;

        if(CheckPicking)
        {
            target.Translate(Vector3.right * 2 * Time.deltaTime);
            Debug.Log("따라간다");
        }
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
