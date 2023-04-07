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
        transform.position = rightHand.transform.position;
        transform.rotation = rightHand.transform.rotation;
    }
}
