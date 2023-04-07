using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandComponent : MonoBehaviour
{
    public GameObject leftHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("LeftGrip") == 0)
        {
            this.GetComponent<HandComponent>().holdingObj = null;
        }
        transform.position = leftHand.transform.position;
        transform.rotation = leftHand.transform.rotation;
    }
}
