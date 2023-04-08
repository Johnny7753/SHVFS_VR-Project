using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LeftGripComponent : MonoBehaviour
{
    public bool isLeftGripCaught = false;
    public GameObject leftHand;
    public GameObject leftGripHandmodel;
    public GameObject leftHandmodel;
    public GameObject leftGripRange;
    private bool isEnter = false;
    // Start is called before the first frame update
    void Start()
    {
        leftGripHandmodel.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isLeftGripCaught == true)
        {
            if (Input.GetAxisRaw("LeftGrip") == 0)
            {
                leftHand.GetComponent<HandComponent>().holdingObj = null;
                leftGripHandmodel.transform.localScale = new Vector3(0, 0, 0);
                leftHandmodel.transform.localScale = new Vector3(1,1,1);
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
                    leftGripHandmodel.transform.localScale = new Vector3(-26.31579f, 26.31579f, 26.31579f);
                    leftHandmodel.transform.localScale = new Vector3(0, 0, 0);
                    isLeftGripCaught = true;
                }
               
            }
        }
        if(Input.GetAxisRaw("LeftGrip") > 0f)
        {
            if (leftGripRange.GetComponent<LeftGripRange>().isEnterLeftGripRange == false)
            {
                leftHand.GetComponent<HandComponent>().holdingObj = null;
                leftHandmodel.transform.localScale = new Vector3(-1, 1, 1);
                leftGripHandmodel.transform.localScale = new Vector3(0, 0, 0);
                isLeftGripCaught = false;
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
