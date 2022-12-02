using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever_Script : MonoBehaviour
{
    Animator animator;

    public ObjectSoundList OL;
    public GameObject barrier;
    public float speed;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Attack_Col")
        {
            OL.ObjectSFX_SoundPlay(0);
            animator.SetTrigger("Move");
            barrier.transform.Translate(0, speed * Time.deltaTime, 0);
        }
    }
}
