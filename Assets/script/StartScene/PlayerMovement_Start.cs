using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement_Start : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer render;

    Vector3 movement;

    Vector2 playerPos;
    Vector2 thatPos;

    public LevelChanger levelChanger;
    public FixedJoystick fixedJoystick;

    private bool jumpPressed = false;

    public Animator anim;
    public GameObject camera;
    public GameObject walkSound;
    public GameObject bookCanvas;
    public SceneTrans sceneTrans;

    private AudioSource source;

    private bool isComplete = false;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping = false;

    private bool isOnGround = true;

    [SerializeField]
    private Transform feetPos;
    [SerializeField]
    private float checkRadius;
    [SerializeField]
    private LayerMask groundType;

    public float movePower = 1f; // 움직이는 힘
    public float jumpPower = 1f; // 점프하는 힘
    public bool isGrounded = false;
    public int jumpCount = 2; // 점프 횟수
    float timer;
    float waitingJump = 0.3f; // 점프 딜레이 시간

    int currentPage = 1;
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

        rigid = gameObject.GetComponent<Rigidbody2D>();
        render = gameObject.GetComponent<SpriteRenderer>();
        source = walkSound.GetComponent<AudioSource>();
        source.loop = false;
    }

    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundType);

        if (isOnGround)
            Debug.Log("땅에 닿았음");
        else
            Debug.Log("땅에 닿음X");

        // 지정해둔 땅에 있으면서 스페이스바를 누르고 있고, 발이 충분히 땅에 닿아 있으면 점프를 합니다.
        if (isOnGround && jumpPressed)
        {
            Debug.Log("jump start");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
            anim.SetTrigger("doJumping");
        }

    }
    public void JumpPress()
    {
        Debug.Log("눌렸곡");
        jumpPressed = true;
    }

    public void JumpCancel()
    {
        jumpPressed = false;
    }

    void FixedUpdate()
    {
        Move();
        /*Jump();*/
    }

    void Move()
    {

        if (!isMoveCam)
        {
            Vector3 moveVelocity = Vector3.zero;

            //if (Input.GetAxisRaw("Horizontal") == 0)
            if (fixedJoystick.GetXValue() == 0)
            {
                anim.SetBool("isMoving", false);
            }

            //if (Input.GetAxisRaw("Horizontal") < 0)
            if (fixedJoystick.GetXValue() < 0)
            {
                moveVelocity = Vector3.left;
                render.flipX = true;
                anim.SetBool("isMoving", true);
            }
            //else if (Input.GetAxisRaw("Horizontal") > 0)
            else if (fixedJoystick.GetXValue() > 0)
            {
                moveVelocity = Vector3.right;
                render.flipX = false;
                anim.SetBool("isMoving", true);
            }
            transform.position += moveVelocity * movePower * Time.deltaTime;
        }
    }
    public void LightComplete()
    {
        isComplete = true;
        return;
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

    private void OnTriggerEnter2D(Collider2D coll)
    {
        playerPos = this.gameObject.transform.position;
        thatPos = coll.gameObject.transform.position;

        if (coll.gameObject.CompareTag("CPage"))
        {
            bookCanvas.SetActive(true);
            Invoke("StartPage", 0.6f);
        }
        else if (coll.gameObject.CompareTag("NextLevel") && isComplete)
        {
            bookCanvas.SetActive(true);
            Invoke("StartPage", 0.6f);
            Invoke("NextLevel", 1.5f);
        }
    }

    private void NextLevel()
    {
        Debug.Log("Next Level!");
        SceneManager.LoadScene("Main Game");
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
                middlePos.x = 9.21f;
                camera.transform.position = new Vector3(middlePos.x, 2.54f, -10);

                if (!nowStat)
                {
                    endPos = new Vector2(19.0f, this.transform.position.y);
                    StartCoroutine(MoveTo(endPos));
                }
                break;

            case 2:
                middlePos.x = 35.75f;
                camera.transform.position = new Vector3(middlePos.x, 2.54f, -10);

                if(nowStat)
                {
                    endPos = new Vector2(25.0f, this.transform.position.y);
                    StartCoroutine(MoveTo(endPos));
                }
                break;

            case 3:
                middlePos.x = 62.54f;
                camera.transform.position = new Vector3(middlePos.x, 32.54f, -10);

                if(nowStat)
                {
                    endPos = new Vector2(64, 9.1f);
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
        bookCanvas.SetActive(false);
        isMoveCam = false;
    }

    public void isPlay()
    {
        source.Play();
    }
}
