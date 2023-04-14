using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballComponent : MonoBehaviour
{
    [Tooltip("x means the force on x axis, y means the force on y axis")]
    public Vector2 emissionForce;
    private Rigidbody rigid;
    public float FireballDamage;
    private void Awake()
    {
        rigid=GetComponent<Rigidbody>();
        //give initial force
        rigid.AddForce(transform.forward*emissionForce.x+transform.up*emissionForce.y);
    }
    private void Update()
    {
        RaycastHit hitinfo;
        Physics.Raycast(transform.position, Vector3.down, out hitinfo ,2.1f);
        if (hitinfo.collider == null) return;
        else if(hitinfo.collider.GetComponent<Ground>() != null)
        {
            Destroy(gameObject);
        }
       
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(!collision.gameObject.GetComponent<EnemyController>()&&!collision.gameObject.GetComponent<FireballComponent>())
        {
            if (collision.gameObject.GetComponent<Base>())
            {
                FindObjectOfType<Base>().GetComponent<Base>().BaseHp -= FireballDamage;
                Debug.Log("attacking");
            }
            // FindObjectOfType<Base>().GetComponent<Base>().BaseHp -= FireballDamage;                                                   Hardy changed, 4/13
            Destroy(gameObject);


        }

        
    }
   
}
