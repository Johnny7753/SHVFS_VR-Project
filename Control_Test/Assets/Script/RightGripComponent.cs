using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class RightGripComponent : MonoBehaviour
{
    public bool isRightGripCaught = false;
    private bool isEnter = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("RightGrip") == 0)
        {
            isRightGripCaught = false;
        }
        if (isEnter)
        {
            if (Input.GetAxisRaw("RightGrip") > 0f)
            {
                isRightGripCaught = true;
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
