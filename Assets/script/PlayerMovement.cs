using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer render;
    Vector3 movement;
    public Animator anim;

    public float movePower = 1f; // 움직이는 힘
    public float jumpPower = 1f; // 점프하는 힘
    bool isJumping = false; // 점프를 하는 가
    public bool isGrounded = false; 
    public int jumpCount = 2; // 점프 횟수
    float timer;
    float waitingJump = 0.3f; // 점프 딜레이 시간
    void Start()
    {
        jumpCount = 0;

        rigid = gameObject.GetComponent<Rigidbody2D> ();
        render = gameObject.GetComponent<SpriteRenderer> ();
    }

    void Update()
    {
        if (isGrounded)
        {
            if (jumpCount > 0)
            {
                timer += Time.deltaTime;
                if (Input.GetButtonDown("Jump"))
                {
                    if(timer > waitingJump)
                    {
                        timer = 0.0f;
                        isJumping = true;
                        jumpCount--;
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if(Input.GetAxisRaw("Horizontal") == 0)
        {
            anim.SetBool("isMoving", false);
        }
    
        if(Input.GetAxisRaw ("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            render.flipX = true;
            anim.SetBool("isMoving", true);
        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            render.flipX = false;
            anim.SetBool("isMoving", true);
        }
        transform.position += moveVelocity * movePower * Time.deltaTime;
    }

    void Jump()
    {
        if (!isJumping)
            return;

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            timer = waitingJump;
            isGrounded = true;
            jumpCount = 2;
        }
    }
}
