using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class RightGripComponent : MonoBehaviour
{
    public bool isRightGripCaught = false;
    public GameObject rightHand;
    private bool isEnter = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isRightGripCaught == true)
        {
            if (Input.GetAxisRaw("RightGrip") == 0)
            {
                rightHand.GetComponent<HandComponent>().holdingObj = null;
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
                    isRightGripCaught = true;
                }
                
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
