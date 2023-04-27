using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierControl : MonoBehaviour
{
    public GameObject Lefthand;
    public GameObject RightHand;
    public bool isFront;
    public bool isTargetPoint;
    public float offset;
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
        if (RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == true && LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == true)
        {
            if (isTargetPoint)
            {
                if (isFront)
                {
                    transform.position = new Vector3((Lefthand.transform.position.x + RightHand.transform.position.x) / 2 + offset+2, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3((Lefthand.transform.position.x + RightHand.transform.position.x) / 2, transform.position.y, transform.position.z);
                }
            }
            else
            {
                if (isFront)
                {
                    transform.position = new Vector3((Lefthand.transform.position.x + RightHand.transform.position.x) / 2 + offset, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3((Lefthand.transform.position.x + RightHand.transform.position.x) / 2, transform.position.y, transform.position.z);
                }
            }
        }
        
    }
}
