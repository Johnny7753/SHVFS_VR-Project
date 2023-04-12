using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballComponent : MonoBehaviour
{
    public float emissionForce;
    private Rigidbody rigid;

    private void Awake()
    {
        rigid=GetComponent<Rigidbody>();
        //give initial force
        rigid.AddForce((transform.forward+transform.up)*emissionForce);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.gameObject.GetComponent<EnemyController>()&&!collision.gameObject.GetComponent<FireballComponent>())
        {
            Destroy(gameObject);
        }
    }
}
