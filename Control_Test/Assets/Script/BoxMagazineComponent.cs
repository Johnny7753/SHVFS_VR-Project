using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMagazineComponent : MonoBehaviour
{
    public int AmmoCount;
    public bool isEmpty = false;
    public bool isConnected = false;
    public bool isLeftHandIn = false;
    public bool isRightHandIn = false;
    public bool isHandled = false;
    public GameObject Gun;
    public GameObject LoadPoint;
    public GameObject LeftHand;
    public GameObject RightHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AmmoCount <= 0)
        {
            isEmpty = true;
        }
        if (isLeftHandIn)
        {
            if (Input.GetAxisRaw("LeftGrip") == 1)
            {
                isHandled = true;
                
            }
        }
        if (isRightHandIn)
        {
            if (Input.GetAxisRaw("RightGrip") >= 0.1f)
            {
                isHandled = true;
                isConnected = false;
                transform.SetParent(RightHand.transform);
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().useGravity = false;
            }
            
        }
        if (Input.GetAxisRaw("RightGrip") == 0 && isHandled)
        {
            transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            isHandled = false;
        }

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<RightHandComponent>() != null)
        {
            isRightHandIn= true;
        }
        if (collision.gameObject.GetComponent<Ground>() != null)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            //transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.GetComponent<BoxMagazineLoadPoint>() != null && isHandled == false)
        {
            transform.position = LoadPoint.transform.position;
            transform.rotation = LoadPoint.transform.rotation;
            transform.SetParent(Gun.transform);
            isConnected = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<RightHandComponent>() != null)
        {
            isRightHandIn = false;
        }
    }
}
