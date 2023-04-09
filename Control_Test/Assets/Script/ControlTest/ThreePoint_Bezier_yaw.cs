using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreePoint_Bezier_yaw : MonoBehaviour
{
    [SerializeField]
    Transform p0, p1, p2, obj;
    [SerializeField, Range(0, 1)]
    public float slider;
    [SerializeField]
    GameObject rightHand;
    [SerializeField]
    GameObject leftHand;
    Vector3 v3;
    [SerializeField]
    GameObject LeftGrip;
    [SerializeField]
    GameObject RightGrip;
    // Start is called before the first frame update
    void Start()
    {
        v3 = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
       if(LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == true && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == true )
        {
            if (leftHand.transform.eulerAngles.y > 30 && leftHand.transform.eulerAngles.y < 150 && rightHand.transform.eulerAngles.y > 30 && rightHand.transform.eulerAngles.y < 150)
            {
                slider = (rightHand.transform.eulerAngles.y + leftHand.transform.eulerAngles.y) / 360;
            }
        }
        else if (LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == true && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == false )
        {
            if (leftHand.transform.eulerAngles.y+10 > 30 && leftHand.transform.eulerAngles.y+10 < 150)
            {
                slider = (leftHand.transform.eulerAngles.y+10) / 180;
            }
        }
        else if (RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == true && LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == false )
        {
            if (rightHand.transform.eulerAngles.y-10 > 30 && rightHand.transform.eulerAngles.y-10 <150 )
            {
                slider = (rightHand.transform.eulerAngles.y-10) / 180;
            }
        }
        obj.position = Bezier(slider);
    }

    Vector3 Bezier(float t)
    {
        float t1 = (1 - t) * (1 - t);
        float t2 = 2 * t * (1 - t);
        float t3 = t * t;
        v3 = t1 * p0.position + t2 * p1.position + t3 * p2.position;
        return v3;
    }
}
