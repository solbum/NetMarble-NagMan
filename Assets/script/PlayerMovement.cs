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
    public GameObject camera;

    public float movePower = 1f; // 움직이는 힘
    public float jumpPower = 1f; // 점프하는 힘
    bool isJumping = false; // 점프를 하는 가
    public bool isGrounded = false; 
    public int jumpCount = 2; // 점프 횟수
    float timer;
    float waitingJump = 0.3f; // 점프 딜레이 시간

    int currentPage = 1;
    int laterPage;
    bool isMoveCam;

    public void Awake()
    {
        NowPage();
    }

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
        if (!isMoveCam)
        { 
            Vector3 moveVelocity = Vector3.zero;

            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                anim.SetBool("isMoving", false);
            }

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                moveVelocity = Vector3.left;
                render.flipX = true;
                anim.SetBool("isMoving", true);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                moveVelocity = Vector3.right;
                render.flipX = false;
                anim.SetBool("isMoving", true);
            }
            transform.position += moveVelocity * movePower * Time.deltaTime;
        }
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
        Vector2 middlePos;
        Vector2 endPos;

        bool nowStat;

        if (laterPage <= currentPage)
        {
            nowStat = true;
        }
        else
        {
            nowStat = false;
        }
        switch (currentPage)
        {
            case 1:
                middlePos.x = 5;
                camera.transform.position = new Vector3(middlePos.x, 13, -10);

                if (!nowStat)
                {
                    endPos = new Vector2(middlePos.x + 40.0f, this.transform.position.y);
                    StartCoroutine(MoveTo(endPos));
                }
                break;

            case 2:
                middlePos.x = 102;
                camera.transform.position = new Vector3(middlePos.x, 13, -10);

                if (nowStat)
                {
                    endPos = new Vector2(middlePos.x - 40.0f, this.transform.position.y);
                    StartCoroutine(MoveTo(endPos));
                }
                else
                {
                    endPos = new Vector2(middlePos.x + 40.0f, this.transform.position.y);
                    StartCoroutine(MoveTo(endPos));
                }
                break;
        }

    }

    IEnumerator MoveTo(Vector2 toPos)
    {
        Debug.Log("코루틴 시작");
        isMoveCam = true;
        float count = 0.05f;
        Vector2 wasPos = this.transform.position;
        while (wasPos != toPos)
        {
            Debug.Log("움직이기 시작");
            count += Time.deltaTime;
            this.transform.position = Vector2.Lerp(wasPos, toPos, count);
            if (count >= 1)
            {
                this.transform.position = toPos;
                break;
            }
            yield return null;
        }
        isMoveCam = false;
        Debug.Log("움직일 수 있음");
    }
}
