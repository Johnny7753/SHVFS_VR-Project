using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LeftGripComponent : MonoBehaviour
{
    public bool isLeftGripCaught = false;
    private bool isEnter = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("LeftGrip") == 0)
        {
            isLeftGripCaught = false;
        }
        if (isEnter)
        {
            if (Input.GetAxisRaw("LeftGrip") > 0f)
            {
                isLeftGripCaught = true;
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
