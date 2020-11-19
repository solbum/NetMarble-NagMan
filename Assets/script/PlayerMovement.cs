using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer render;

    Vector3 movement;

    Vector2 playerPos;
    Vector2 thatPos;

    public Animator anim;
    public GameObject camera;
    public GameObject walkSound;
    public GameObject bookCanvas;
    public SceneTrans sceneTrans;

    private AudioSource source;

    public float movePower = 1f; // 움직이는 힘
    public float jumpPower = 1f; // 점프하는 힘
    bool isJumping = false; // 점프를 하는 가
    public bool isGrounded = false; 
    public int jumpCount = 2; // 점프 횟수
    float timer;
    float waitingJump = 0.3f; // 점프 딜레이 시간

    public int currentPage = 1;
    int laterPage;
    bool isMoveCam;

    public void Awake()
    {
        bookCanvas.SetActive(false);
        NowPage();
    }

    void Start()
    {
        jumpCount = 0;

        rigid = gameObject.GetComponent<Rigidbody2D> ();
        render = gameObject.GetComponent<SpriteRenderer> ();
        source = walkSound.GetComponent<AudioSource>();
        source.loop = false;
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
                    isJumping = true;
                    anim.SetTrigger("doJumping");
                    if (timer > waitingJump)
                    {
                        timer = 0.0f;
                        
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
        playerPos = this.gameObject.transform.position;
        thatPos = coll.gameObject.transform.position;

        if (coll.gameObject.CompareTag("CPage"))
        {
            bookCanvas.SetActive(true);
            Invoke("StartPage", 0.6f);
        }
    }

    private void StartPage()
    {
        laterPage = currentPage;

        if (playerPos.x <= thatPos.x)
        {
            sceneTrans.NextScene();
            currentPage++;
        }
        else // thisPos.x > thatPos.x
        {
            sceneTrans.PrevScene();
            currentPage--;
        }
        NowPage();
    }

    void NowPage()
    {
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
                camera.transform.position = new Vector3(-28.0f, 18.5f, -10);
                //camera.rect = new Rect(0.0f, 0.0f, 0.7f, 1.0f);

                if (!nowStat)
                {
                    endPos = new Vector2(20.0f, 33.0f);
                    StartCoroutine(MoveTo(endPos));
                }
                break;

            case 2:
                camera.transform.position = new Vector3(80, -5, -10);
                //camera.rect = new Rect(0.0f, 0.0f, 0.69f, 1.0f);

                if (nowStat)
                {
                    endPos = new Vector2(12.0f, 33.0f);
                    StartCoroutine(MoveTo(endPos));
                }
                else
                {
                    endPos = new Vector2(148.0f, 23.0f);
                    StartCoroutine(MoveTo(endPos));
                }
                break;

            case 3:
                camera.transform.position = new Vector3(188.0f, 23, -10);
                //camera.rect = new Rect(0.0f, 0.0f, 0.75f, 1.0f);

                if (nowStat)
                {
                    endPos = new Vector2(156.0f, 23.0f);
                    StartCoroutine(MoveTo(endPos));
                }
                else
                {

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
            count += Time.deltaTime;
            this.transform.position = Vector2.Lerp(wasPos, toPos, count);
            if (count >= 1)
            {
                this.transform.position = toPos;
                break;
            }
            yield return null;
        }
        Debug.Log("사라진다.");
        Invoke("DisappearBook", 1.0f);
        isMoveCam = false;
    }

    private void DisappearBook()
    {
        bookCanvas.SetActive(false);
    }    
    public void isPlay()
    {
        source.Play();
    }
}
