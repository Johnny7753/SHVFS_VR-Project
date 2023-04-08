using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightGripRange : MonoBehaviour
{
    public bool isEnterRightGripRange = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RightHandComponent>() != null)
        {
            isEnterRightGripRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<RightHandComponent>() != null)
        {
            isEnterRightGripRange = false;
        }
    }
}
