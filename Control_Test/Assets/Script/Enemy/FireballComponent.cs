using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballComponent : MonoBehaviour
{
    public float emissionForce;
    private Rigidbody rigid;
    public float FireballDamage;
    private void Awake()
    {
        rigid=GetComponent<Rigidbody>();
        //give initial force
        rigid.AddForce((transform.forward+transform.up)*emissionForce);
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
