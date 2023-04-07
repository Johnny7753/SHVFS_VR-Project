using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExampleClass : MonoBehaviour
{

    void Update()
    {
        if(Input.GetAxisRaw("RightTrigger") ==1)
        {
            Debug.Log("RightTrigger");
        }
        if (Input.GetAxisRaw("LeftTrigger") == 1)
        {
            Debug.Log("LeftTrigger");
        }
        if (Input.GetAxisRaw("RightGrip") == 1)
        {
            Debug.Log("RightGrip");
        }
        if (Input.GetAxisRaw("LeftGrip") == 1)
        {
            Debug.Log("LeftGrip");
        }
    }
}
