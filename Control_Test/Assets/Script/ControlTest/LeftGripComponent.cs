using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LeftGripComponent : MonoBehaviour
{
    public bool isLeftGripCaught = false;
    public GameObject leftHand;
    private bool isEnter = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isLeftGripCaught == true)
        {
            if (Input.GetAxisRaw("LeftGrip") == 0)
            {
                leftHand.GetComponent<HandComponent>().holdingObj = null;
                isLeftGripCaught = false;
            }
        }
        if (isEnter)
        {
            if (Input.GetAxisRaw("LeftGrip") > 0f)
            {
                if(leftHand.GetComponent<HandComponent>().holdingObj == null)
                {
                    leftHand.GetComponent<HandComponent>().holdingObj = this.gameObject;
                    isLeftGripCaught = true;
                }
               
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<LeftHandComponent>() != null)
        {
            isEnter = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<LeftHandComponent>() != null)
        {
            isEnter = false;
        }
    }
}
