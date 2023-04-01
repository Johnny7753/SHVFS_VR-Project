using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreePoint_Bezier : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        v3 = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        slider = (rightHand.transform.eulerAngles.y + leftHand.transform.eulerAngles.y) / 360;
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(slider);
            Debug.Log((rightHand.transform.eulerAngles.y));
            Debug.Log((rightHand.transform.rotation.y));
        }


        if (slider < 0.3)
        {
            slider = 0.3f;
        }
        if (slider > 0.7)
        {
            slider = 0.7f;
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
