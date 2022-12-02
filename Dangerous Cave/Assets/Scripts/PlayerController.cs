using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController2D controller;

    public float Speed = 30f;
    float horizontalMove = 0f;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    bool jump = false;
    bool Attack = false;
    bool isInvisible = false;
    bool onMovingPlatform = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * Speed;

        animator.SetFloat("MoveSpeed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("jumping");
            animator.SetTrigger("Jump");
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //this.GetComponent<BoxCollider2D>().enabled = false;
            //this.GetComponent<CircleCollider2D>().enabled = false;

            isInvisible = true;
            StartCoroutine(InvisibleTime());
        }

        else if (col.gameObject.tag == "movePlatform")
        {
            onMovingPlatform = true;
        }
    }

   void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "jwel")
        {
            Destroy(col.gameObject);
            GameManager.gm.updatejewlstake();
        }
        else if (col.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene("Stage2");
        }
    }

    public void OnLanding()
    {
        print("Landing.");
        animator.SetTrigger("Idle");
    }

    public void MeleeAttack()
    {
        if(Attack)
        {
            animator.SetTrigger("Attack");
            rb.velocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        MeleeAttack();
        jump = false;

        if (onMovingPlatform)

        ResetValues();
    }

    public void ResetValues()
    {
        Attack = false;
    }

    IEnumerator InvisibleTime()
    {

        int countTime = 0;

        animator.SetTrigger("Damage");

        while (countTime < 10)
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
}
