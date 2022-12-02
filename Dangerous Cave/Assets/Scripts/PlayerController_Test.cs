using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_Test : MonoBehaviour
{
    public static PlayerController_Test PT;
    public PlayerSoundList PlayerSfx;
    public BGMList BP;
    public CameraFollow CF;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    public float movementSpeed;

    int HP = 5;
    int MaxHP = 5;

    public Image[] Lifes;

    bool facingRight;
    bool isAlive;
    bool attack;
    bool pushedPlayer = false;
    bool isInvisible = false;

    [SerializeField]
    Transform[] groundPoints;

    [SerializeField]
    float groundRadius;

    [SerializeField]
    LayerMask whatIsGround;

    bool isGrounded;

    bool jump;

    public bool onMovingPlatform;

    [SerializeField]
    bool airControl;

    [SerializeField]
    public float jumpForce;

    public float pushdirection;

    bool haveDynamite;

    [SerializeField]
    GameObject DynamitePrefab;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        isInvisible = false;
        facingRight = true;
        haveDynamite = false;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        print("Ground Layer Index: " + animator.GetLayerIndex("Ground Layer"));
        print("Air Layer Index: " + animator.GetLayerIndex("Air Layer"));

    }

    void Update()
    {
        HandleInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive )
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                print("Testing Dead");
                Dead();
            }

            float horizontal = Input.GetAxis("Horizontal");

            isGrounded = IsGrounded();

            HandleMovement(horizontal);

            Flip(horizontal);

            HandleAttacks();

            HandleLayers();

            SpecialItem();

            ResetValues();
        }
    }

    void HandleMovement(float horizontal)
    {
        if (rb.velocity.y < 0)
        {
            animator.SetBool("Land", true);
        }

        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("MeeleAttack") && (!isGrounded || airControl))
        {
            rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
        }

        if (isGrounded && jump)
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0, jumpForce));
            animator.SetTrigger("Jump");
            PlayerSfx.PlayerSFX_SoundPlay(1);
        }

        animator.SetFloat("MoveSpeed", Mathf.Abs(horizontal));
    }

    void HandleAttacks()
    {
        if (attack)
        {
            PlayerSfx.PlayerSFX_SoundPlay(0);
            animator.SetTrigger("Attack");
            rb.velocity = Vector2.zero;

        }
    }


    void HandleInput()
    {
        if(Input.GetKeyDown(KeyCode.X) && isGrounded)
        {
            attack = true;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            jump = true;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if(haveDynamite)
            {
                PlayerSfx.PlayerSFX_SoundPlay(4);
                animator.SetTrigger("Throw");
                ThrowDynamite(0);
            }
        }
    }

    void SpecialItem()
    {
        if (GameManager.gm.Dynamite_UI.activeSelf == true)
        {
            haveDynamite = true;
        }

        else
        {
            haveDynamite = false;
        }
    }

    void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    void ResetValues()
    {
        attack = false;
        jump = false;
    }

    bool IsGrounded()
    {
        if (rb.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0 ; i < colliders.Length ; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        //print("setting animator.Land to false.");
                        animator.ResetTrigger("Jump");
                        animator.SetBool("Land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void HandleLayers()
    {
        if (!isGrounded)
        {
            animator.SetLayerWeight(0, 0);
            animator.SetLayerWeight(1, 1);

        }

        else
        {
            animator.SetLayerWeight(0, 1);
            animator.SetLayerWeight(1, 0);
   
        }
    }

    void ThrowDynamite(int value)
    {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(DynamitePrefab, transform.position, Quaternion.identity);
            tmp.GetComponent<Dynamite>().Initialize(Vector2.right);
        }

        else
        {
            GameObject tmp = (GameObject)Instantiate(DynamitePrefab, transform.position, Quaternion.identity);
            tmp.GetComponent<Dynamite>().Initialize(Vector2.left);
        }
    }

    public void UpdateLife(int newHp)
    {
        if (newHp != HP)
        {
            if (newHp < 0)
                newHp = 0;

            if (newHp > MaxHP)
                newHp = MaxHP;

            for (int i = 0; i < MaxHP; i++)
            {
                if (i < HP) //HP를 가지는 스프라이트 업데이트
                    Lifes[i].color = new Color(1,1,1,1);
                else
                    Lifes[i].color = new Color(1,1,1,0);
            }
        }

        if (HP == 0)
        {
            Dead();
        }
    }

    void pushPlayer(float vx, float vy)
    {
        //Push in Y axis.
        if (vy != 0.0f)
        {
            //rb.AddForce(new Vector2(0.0f,vy));
            jump = true;
        }

        //Push in X axis.
        if (vx != 0.0f)
        {
            //rb.AddForce(new Vector2(vx*direction, 0.0f));
            pushedPlayer = true;
        }

        rb.AddForce(new Vector2(vx * pushdirection, vy));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "jwel")
        {
            PlayerSfx.PlayerSFX_SoundPlay(2);
            Destroy(other.gameObject);
            GameManager.gm.updatejewlstake();
        }
        else if (other.gameObject.tag == "Attack_Item")
        {
            PlayerSfx.PlayerSFX_SoundPlay(5);
            Destroy(other.gameObject);
            GameManager.gm.updatedynamitetake();
        }

        else if (other.gameObject.tag == "Goal")
        {
            BP.BGM_Play(0);
            GameManager.gm.showGameResult();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy" && isInvisible == false)
        {
            UpdateLife(HP--);
            PlayerSfx.PlayerSFX_SoundPlay(3);
            pushPlayer(500f, 0f);
            isInvisible = true;
            StartCoroutine(InvisibleTime());
        }

        else if (col.gameObject.tag == "Spike" && isInvisible == false)
        {
            UpdateLife(HP--);
            PlayerSfx.PlayerSFX_SoundPlay(3);
            isInvisible = true;
            StartCoroutine(InvisibleTime());
        }

        else if (col.gameObject.tag == "movePlatform")
        {
            onMovingPlatform = true;
        }
    }

    IEnumerator InvisibleTime()
    {

        int countTime = 0;

        animator.SetTrigger("Damage");

        while (countTime < 16)
        {
            if (countTime % 2 == 0)
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            else
                spriteRenderer.color = new Color32(255, 255, 255, 180);

            yield return new WaitForSeconds(0.2f);

            countTime++;
        }

        spriteRenderer.color = new Color32(255, 255, 255, 255);

        isInvisible = false;

        yield return null;
    }

    void Dead ()
    {
        isAlive = false;

        animator.SetTrigger("Dead");

        BP.BGM_Play(1);

        CF.GetComponent<CameraFollow>().enabled = false;

        GameManager.gm.showGameOver();
    }
}
