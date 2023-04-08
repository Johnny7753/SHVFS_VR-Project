using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BoxMagazineComponent : MonoBehaviour
{
    public bool isEmpty = false;
    public bool isConnected = false;
    public bool isrightHandIn = false;
    public bool isleftHandIn = false;
    public bool isHandled = false;
    public bool hasLoaded = true;
    public float timer = 0;
    public int BulletCapacity = 500;
    public GameObject Gun;
    public GameObject LoadPoint;
    public GameObject leftHand;
    public GameObject rightHand;
    public Transform leftHandHoldPoint;
    public Transform rightHandHoldPoint;

    // Start is called before the first frame update
    void Start()
    {
        Gun = FindObjectOfType<GunComponent>().gameObject;
        LoadPoint = FindObjectOfType<BoxMagazineLoadPoint>().gameObject;
        leftHand = FindObjectOfType<LeftHandComponent>().gameObject;
        rightHand = FindObjectOfType<RightHandComponent>().gameObject;
        leftHandHoldPoint = FindObjectOfType<LeftHandComponent>().gameObject.GetComponentInChildren<HoldPointComponent>().transform;
        rightHandHoldPoint = FindObjectOfType<RightHandComponent>().gameObject.GetComponentInChildren<HoldPointComponent>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 5)
        {
            Destroy(this.gameObject);
        }
        if (isleftHandIn)
        {
            if (Input.GetAxisRaw("LeftGrip") >= 0.1f)
            {
                if (leftHand.GetComponent<HandComponent>().holdingObj == null)
                {
                    timer = 0;
                    leftHand.GetComponent<HandComponent>().holdingObj = this.gameObject;
                    isHandled = true;
                    hasLoaded = true;
                    Gun.GetComponent<GunComponent>().loadingBoxMagazine = null;
                    Gun.GetComponent<GunComponent>().IsConnected = false;
                    transform.SetParent(leftHand.transform);
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GetComponent<Rigidbody>().useGravity = false;
                    transform.position = leftHandHoldPoint.position;
                    transform.rotation = leftHandHoldPoint.rotation;
                }
            }
            if (Input.GetAxisRaw("LeftGrip") == 0 && isHandled)
            {
                leftHand.GetComponent<HandComponent>().holdingObj = null;
                transform.parent = null;
                GetComponent<Rigidbody>().useGravity = true;
                isHandled = false;
            }
        }
        if (isrightHandIn)
        {
            if (Input.GetAxisRaw("RightGrip") >= 0.1f)
            {
                if (rightHand.GetComponent<HandComponent>().holdingObj == null)
                {
                    timer = 0;
                    rightHand.GetComponent<HandComponent>().holdingObj = this.gameObject;
                    isHandled = true;
                    hasLoaded = true;
                    Gun.GetComponent<GunComponent>().loadingBoxMagazine = null;
                    Gun.GetComponent<GunComponent>().IsConnected = false;
                    transform.SetParent(rightHand.transform);
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GetComponent<Rigidbody>().useGravity = false;
                    transform.position = rightHandHoldPoint.position;
                    transform.rotation = rightHandHoldPoint.rotation;
                }
            }
            if (Input.GetAxisRaw("RightGrip") == 0 && isHandled)
            {
                rightHand.GetComponent<HandComponent>().holdingObj = null;
                transform.parent = null;
                GetComponent<Rigidbody>().useGravity = true;
                isHandled = false;
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (isrightHandIn == false)
        {
            if (collision.gameObject.GetComponent<LeftHandComponent>() != null)
            {
                isleftHandIn = true;
            }
        }
        if (isleftHandIn == false)
        {
            if (collision.gameObject.GetComponent<RightHandComponent>() != null)
            {
                isrightHandIn = true;
            }
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
            if (Gun.GetComponent<GunComponent>().loadingBoxMagazine == null)
            {
                if (hasLoaded)
                {
                    Gun.GetComponent<GunComponent>().AmmoCount = BulletCapacity;
                    hasLoaded = false;
                }
                Gun.GetComponent<GunComponent>().loadingBoxMagazine = this.gameObject;
                transform.position = LoadPoint.transform.position;
                transform.rotation = LoadPoint.transform.rotation;
                transform.SetParent(Gun.transform);
                Gun.GetComponent<GunComponent>().IsConnected = true;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().useGravity = false;
            }
        }
        if (collision.gameObject.GetComponent<Ground>() != null)
        {
            timer += Time.deltaTime;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<LeftHandComponent>() != null)
        {
            isleftHandIn = false;
        }
        if (collision.gameObject.GetComponent<RightHandComponent>() != null)
        {
            isrightHandIn = false;
        }
    }
}
