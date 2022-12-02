using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

    Rigidbody2D rb;

    public float Horizontal_force = 1.5f;
    public float Vertical_force = 0f;

    public float Loop_moveTime = 5.7f;

    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(moveLoop(new Vector2(Horizontal_force, Vertical_force), Loop_moveTime));
    }

    IEnumerator moveLoop(Vector2 forces, float moveTime)
    {
        while (true)
        {
            yield return movePlatform(forces, moveTime);
            yield return movePlatform(forces * -1f, moveTime);
        }
    }

    IEnumerator movePlatform(Vector2 forces, float moveTime)
    {
        float t = 0;

        while (t < moveTime)
        {
            t += Time.fixedDeltaTime;

            rb.velocity = forces;

            yield return new WaitForFixedUpdate();
        }

        rb.velocity = Vector2.zero;
    }
}

// 수정에 참고할 링크 : https://www.youtube.com/watch?v=8aSzWGKiDAM
