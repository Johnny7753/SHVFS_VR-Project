using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float bulletLifeTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Destroy(this.gameObject, bulletLifeTime);
        Vector3 oriPos = transform.position;
        Vector3 direction = transform.position - oriPos;
        float ength = (transform.position - oriPos).magnitude;
        RaycastHit hitinfo;
        bool isCollider = Physics.Raycast(oriPos, direction, out hitinfo , ength);
        if (isCollider)
        {
            //hit.collider.getcomponent<>();
        }
    }
}
