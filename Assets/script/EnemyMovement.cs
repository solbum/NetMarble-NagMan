using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    public float movingSpeed = 0.1f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 3);
    }

    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove * movingSpeed, rigid.velocity.y);
        Debug.Log("움직인다!");
        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + 0.4f*nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, 2*Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Ground"));
        if(rayHit.collider == null)
        {
            Debug.Log("위험!");
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", 3);
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2); // Range(a,b)일 경우 a<=X<b 이다.

        Invoke("Think", 3);
    }
}
