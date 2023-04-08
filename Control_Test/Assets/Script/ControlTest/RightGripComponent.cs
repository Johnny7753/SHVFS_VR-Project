using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class RightGripComponent : MonoBehaviour
{
    public bool isRightGripCaught = false;
    public GameObject rightHand;
    public GameObject rightGripHandmodel;
    public GameObject rightHandModel;
    public GameObject rightGripRange;
    private bool isEnter = false;
    // Start is called before the first frame update
    void Start()
    {
        rightGripHandmodel.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isRightGripCaught == true)
        {
            if (Input.GetAxisRaw("RightGrip") == 0)
            {
                rightHand.GetComponent<HandComponent>().holdingObj = null;
                rightHandModel.transform.localScale = new Vector3(-1, 1, 1);
                rightGripHandmodel.transform.localScale = new Vector3(0, 0, 0);
                isRightGripCaught = false;
            }
        }
        if (isEnter)
        {
            if (Input.GetAxisRaw("RightGrip") > 0f)
            {
                if (rightHand.GetComponent<HandComponent>().holdingObj == null)
                {
                    rightHand.GetComponent<HandComponent>().holdingObj = this.gameObject;
                    rightGripHandmodel.transform.localScale = new Vector3(26.31579f, 26.31579f, 26.31579f);
                    rightHandModel.transform.localScale = new Vector3(0, 0, 0);
                    isRightGripCaught = true;
                }
            }
        }
        if (Input.GetAxisRaw("RightGrip") > 0f)
        {
            if (rightGripRange.GetComponent<RightGripRange>().isEnterRightGripRange == false)
            {
                rightHand.GetComponent<HandComponent>().holdingObj = null;
                rightHandModel.transform.localScale = new Vector3(-1, 1, 1);
                rightGripHandmodel.transform.localScale = new Vector3(0, 0, 0);
                isRightGripCaught = false;
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<RightHandComponent>() != null)
        {
            isEnter = true; 
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<RightHandComponent>() != null)
        {
            isEnter = false;
        }
    }

}
