using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointYChange : MonoBehaviour
{
    [SerializeField]
    Transform p0, p1, p2;
    [SerializeField]
    GameObject rightHand;
    [SerializeField]
    GameObject leftHand;
    [SerializeField]
    GameObject LeftGrip;
    [SerializeField]
    GameObject RightGrip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == true && LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == false)
        {
            p0.position = new Vector3(p0.position.x, 1.6f - rightHand.transform.position.y, p0.position.z);
            p1.position = new Vector3(p1.position.x, 1.6f - rightHand.transform.position.y, p1.position.z);
            p2.position = new Vector3(p2.position.x, 1.6f - rightHand.transform.position.y, p2.position.z);
        }
        else if (LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == true && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == false)
        {
            p0.position = new Vector3(p0.position.x, 1.6f - leftHand.transform.position.y, p0.position.z);
            p1.position = new Vector3(p1.position.x, 1.6f - leftHand.transform.position.y, p1.position.z);
            p2.position = new Vector3(p2.position.x, 1.6f - leftHand.transform.position.y, p2.position.z);
        }
        else if (LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == true && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == true)
        {
            p0.position = new Vector3(p0.position.x, 1.6f - (leftHand.transform.position.y + rightHand.transform.position.y)/2, p0.position.z);
            p1.position = new Vector3(p1.position.x, 1.6f - (leftHand.transform.position.y + rightHand.transform.position.y)/2, p1.position.z);
            p2.position = new Vector3(p2.position.x, 1.6f - (leftHand.transform.position.y + rightHand.transform.position.y)/2, p2.position.z);
        }
    }
}
