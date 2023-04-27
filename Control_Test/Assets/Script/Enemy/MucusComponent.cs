using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MucusComponent : MonoBehaviour
{
    [Tooltip("x means the force on x axis, y means the force on y axis")]
    public Vector2 emissionForce;
    private Rigidbody rigid;
    public float FireballDamage;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        //give initial force
        rigid.AddForce(transform.forward * emissionForce.x + transform.up * emissionForce.y);
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Base>())
        {
            FindObjectOfType<Base>().GetComponent<Base>().BaseHp -= FireballDamage;
            //Debug.Log("attacking");
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            Destroy(gameObject);
        }
    }
}
