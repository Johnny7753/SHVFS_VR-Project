using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandComponent : MonoBehaviour
{
    public GameObject rightHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("RightGrip") == 0)
        {
            this.GetComponent<HandComponent>().holdingObj = null;
        }
        transform.position = rightHand.transform.position;
        transform.rotation = rightHand.transform.rotation;
    }
}
