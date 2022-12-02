using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    float xMax;

    [SerializeField]
    float yMax;

    [SerializeField]
    float xMin;

    [SerializeField]
    float yMin;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax),
            Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
    }
}
