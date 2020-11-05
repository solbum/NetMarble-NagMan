using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer render;
    Vector3 movement;
    public Animator animator;

    public int jumpCount = 2; // 점프 횟수

    float timer;
    float waitingJump = 0.3f; // 점프 딜레이 시간
    public float movePower = 6f;
    public float jumpPower = 20f;


    public bool inputLeft = false;
    public bool inputRight = false;
    public bool inputJump = false;
    public bool isGrounded = false;
    bool isJumping = false; // 점프를 하는 가


    void Start()
    {
        jumpCount = 0;

        rigid = gameObject.GetComponent<Rigidbody2D>();
        render = gameObject.GetComponent<SpriteRenderer>();

        Button ui = GameObject.FindGameObjectWithTag("Managers").GetComponent<Button>();
        ui.Init();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Update()
    {
        if (isGrounded)
        {
            if (jumpCount > 0)
            {
                timer += Time.deltaTime;
                if (inputJump)
                {
                    if (timer > waitingJump)
                    {
                        timer = 0.0f;
                        isJumping = true;
                        jumpCount--;
                    }
                }
            }
        }

    }

    public void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if ((!inputRight && !inputLeft))
        {
            animator.SetBool("isMoving", false);
        }
        else if (inputLeft)
        {
            animator.SetBool("isMoving", true);
            moveVelocity = Vector3.left;
            render.flipX = true;
        }
        else if (inputRight)
        {
            animator.SetBool("isMoving", true);
            moveVelocity = Vector3.right;
            render.flipX = false;
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


        inputJump = false;
        isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            timer = waitingJump;
            isGrounded = true;
            jumpCount = 2;
        }
    }

}
