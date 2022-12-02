using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : MonoBehaviour
{
    public static MummyController Enemy;

    public float movementSpeed;

    Animator animator;
    Vector3 movement;

    int movementFlag = 0;

    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();

        StartCoroutine(ChangeMovement());
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (movementFlag == 1)
        {
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(-0.35f, 0.35f, -1);
        }

        else if (movementFlag == 2)
        {
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(0.35f, 0.35f, -1);
        }

        transform.position += moveVelocity * movementSpeed * Time.deltaTime;
    }

    IEnumerator ChangeMovement()
    {
        movementFlag = Random.Range(0, 3);

        if (movementFlag == 0)
        {
            animator.SetFloat("MoveSpeed", 0);
        }
        else
        {
            animator.SetFloat("MoveSpeed", 1);
        }

        yield return new WaitForSeconds(5f);

        StartCoroutine(ChangeMovement());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack_Col")
        {
            StartCoroutine(Dead());
        }
    }

    IEnumerator Dead ()
    {
        animator.SetTrigger("Dead");

        yield return new WaitForSeconds(0.8f);

        DestroyObject(this.gameObject);
    }
}
