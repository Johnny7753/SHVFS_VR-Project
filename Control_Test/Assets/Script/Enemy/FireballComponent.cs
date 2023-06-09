using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballComponent : MonoBehaviour
{
    [Tooltip("x means the force on x axis, y means the force on y axis")]
    public Vector2 emissionForce;
    private Rigidbody rigid;
    public float FireballDamage;
    public AudioManager audioManager;
    private Transform player;
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rigid=GetComponent<Rigidbody>();

        player = GameObject.Find("XR Origin").transform;//  by rebe 0419
        //give initial force
        var direction = (player.position - transform.position).normalized;//  by rebe 0419
        rigid.AddForce(direction * emissionForce.x+transform.up*emissionForce.y);
    }
    private void Update()
    {
        //Debug.Log(Mathf.Atan(rigid.velocity.y / rigid.velocity.x));
        transform.rotation=Quaternion.Euler(new Vector3(0, -Mathf.Rad2Deg*Mathf.Atan((player.position.z-transform.position.z)/(player.position.x - transform.position.x)), Mathf.Rad2Deg* Mathf.Atan(rigid.velocity.y /( rigid.velocity.x+0.0001f))));
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
                audioManager.FireBallHitPlayer.Play();
                FindObjectOfType<Base>().GetComponent<Base>().BaseHp -= FireballDamage;
            }
            // FindObjectOfType<Base>().GetComponent<Base>().BaseHp -= FireballDamage;                                                   Hardy changed, 4/13
            Destroy(gameObject);
        }
    }
}
