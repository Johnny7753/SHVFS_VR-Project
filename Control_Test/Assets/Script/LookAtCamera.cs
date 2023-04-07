using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LookAtCamera : MonoBehaviour
{
    public GameObject RightHand;
    public GameObject LeftHand;
    private Vector3 target;
    public SelectXYZ selectXYZ = SelectXYZ.None;
    public GameObject LeftGrip;
    public GameObject RightGrip;
    public GameObject TargetPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == true && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == false)
        {
            target = TargetPoint.transform.position;
            Vector3 vector3 = target - transform.position;
            switch (selectXYZ)
            {
                case SelectXYZ.x:
                    vector3.y = vector3.z = 0.0f;
                    break;
                case SelectXYZ.y:
                    vector3.x = vector3.z = 0.0f;
                    break;
                case SelectXYZ.z:
                    vector3.x = vector3.y = 0.0f;
                    break;
            }
            transform.LookAt(target + vector3);
        }

        else if (RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == true && LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == false)
        {
            target = TargetPoint.transform.position;
            Vector3 vector3 = target - transform.position;
            switch (selectXYZ)
            {
                case SelectXYZ.x:
                    vector3.y = vector3.z = 0.0f;
                    break;
                case SelectXYZ.y:
                    vector3.x = vector3.z = 0.0f;
                    break;
                case SelectXYZ.z:
                    vector3.x = vector3.y = 0.0f;
                    break;
            }
            transform.LookAt(target + vector3);
        }
        else if (LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == true && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == true)
        {
            target = TargetPoint.transform.position;
            Vector3 vector3 = target - transform.position;
            switch (selectXYZ)
            {
                case SelectXYZ.x:
                    vector3.y = vector3.z = 0.0f;
                    break;
                case SelectXYZ.y:
                    vector3.x = vector3.z = 0.0f;
                    break;
                case SelectXYZ.z:
                    vector3.x = vector3.y = 0.0f;
                    break;
            }
            transform.LookAt(target + vector3);
        }
    }
            
}

public enum SelectXYZ
{
    x,
    y,
    z,
    None
}
