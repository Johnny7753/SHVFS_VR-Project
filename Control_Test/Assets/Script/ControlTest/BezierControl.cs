using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierControl : MonoBehaviour
{
    public GameObject Lefthand;
    public GameObject RightHand;
    public bool isFront;
    public bool isRight;
    public bool isLeft;
    public bool isTargetPoint;
    public float xoffset;
    public float zoffset;
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
                    transform.position = new Vector3((Lefthand.transform.position.x + RightHand.transform.position.x) / 2 + xoffset + 4, transform.position.y,  (Lefthand.transform.position.z + RightHand.transform.position.z) / 2);
                }
                else if(isLeft)
                {
                    transform.position = new Vector3((Lefthand.transform.position.x + RightHand.transform.position.x) / 2 + xoffset, transform.position.y,  (Lefthand.transform.position.z + RightHand.transform.position.z) / 2 +2 + zoffset);
                }
                else if (isRight)
                {
                    transform.position = new Vector3((Lefthand.transform.position.x + RightHand.transform.position.x) / 2 + xoffset, transform.position.y, (Lefthand.transform.position.z + RightHand.transform.position.z) / 2 -2 + zoffset);
                }
            }
            else
            {
                if (isFront)
                {
                    transform.position = new Vector3((Lefthand.transform.position.x + RightHand.transform.position.x) / 2 + xoffset+2, transform.position.y,  (Lefthand.transform.position.z + RightHand.transform.position.z) / 2);
                }
                else if(isLeft)
                {
                    transform.position = new Vector3((Lefthand.transform.position.x + RightHand.transform.position.x) / 2 + xoffset, transform.position.y,  (Lefthand.transform.position.z + RightHand.transform.position.z) / 2 +1 + zoffset);
                }
                else if (isRight)
                {
                    transform.position = new Vector3((Lefthand.transform.position.x + RightHand.transform.position.x) / 2 + xoffset, transform.position.y, (Lefthand.transform.position.z + RightHand.transform.position.z) / 2 -1 + zoffset);
                }
            }
        }
        
    }
}
