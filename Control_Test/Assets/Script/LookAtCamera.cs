using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public GameObject RightHand;
    public GameObject LeftHand;
    private Vector3 target;
    public SelectXYZ selectXYZ = SelectXYZ.None;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        target = new Vector3(-(RightHand.transform.position.x + LeftHand.transform.position.x) / 2, (RightHand.transform.position.y + LeftHand.transform.position.y) / 2, -(RightHand.transform.position.z + LeftHand.transform.position.z) / 2);
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

public enum SelectXYZ
{
    x,
    y,
    z,
    None
}
