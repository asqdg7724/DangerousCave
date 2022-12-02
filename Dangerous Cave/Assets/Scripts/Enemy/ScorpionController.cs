using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionController : MonoBehaviour
{
    public float speed;

    Animator animator;

    public bool MoveRight;

    // Update is called once per frame
    void Update()
    {
        animator = GetComponent<Animator>();

        if (MoveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(0.2f, 0.2f);
        }

        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-0.2f, 0.2f);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("turn"))
        {
            if(MoveRight)
            {
                MoveRight = false;
            }

            else
            {
                MoveRight = true;
            }
        }

        else if (col.gameObject.tag == "Attack_Col")
        {
            StartCoroutine(Dead());
        }
    }

    IEnumerator Dead()
    {
        animator.SetTrigger("Dead");

        yield return new WaitForSeconds(0.8f);

        DestroyObject(this.gameObject);
    }
}
