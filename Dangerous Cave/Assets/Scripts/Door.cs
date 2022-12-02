using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Drum;
    public GameObject Explo;
    public GameObject LockDoor;
    public GameObject UnlockDoor;

    bool isExplode;

    // Start is called before the first frame update
    void Start()
    {
        isExplode = false;
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
            Explo.SetActive(true);
            LockDoor.SetActive(false);
            UnlockDoor.SetActive(true);
        }
     }
}
