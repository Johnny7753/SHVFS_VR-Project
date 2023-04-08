using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftGripRange : MonoBehaviour
{
    public bool isEnterLeftGripRange = false;
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
        if (other.GetComponent<LeftHandComponent>() != null)
        {
            isEnterLeftGripRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LeftHandComponent>() != null)
        {
            isEnterLeftGripRange = false;
        }
    }
}
