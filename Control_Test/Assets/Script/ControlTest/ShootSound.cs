using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootSound : MonoBehaviour
{
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("destory", 0.2f);
    }
    void destory()
    {
        Destroy(gameObject);
    }
}
