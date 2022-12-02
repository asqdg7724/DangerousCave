using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3_Puzzle : MonoBehaviour
{
    public GameObject Drum;
    public GameObject Explosion;
    public GameObject Boxes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrumDestory();
    }

    void DrumDestory()
    {
        if (Drum.activeSelf == false)
        {
            Explosion.SetActive(true);
            Boxes.SetActive(false);
        }
    }
}
