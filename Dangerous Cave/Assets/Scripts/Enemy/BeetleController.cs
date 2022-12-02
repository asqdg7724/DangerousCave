using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleController : MonoBehaviour
{
    Animator animator;
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack_Col")
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
