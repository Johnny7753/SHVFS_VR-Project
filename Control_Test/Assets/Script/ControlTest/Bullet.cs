using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float bulletLifeTime;
    public Vector3 oriPos;
    public float bulletDamage;
    public float criticalDamage;
    public GameObject GameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>().gameObject;
        oriPos = transform.position;
    }

    private void FixedUpdate()
    {
        bulletDamage = GameManager.GetComponent<GameManager>().BulletDamage;
        criticalDamage = bulletDamage * 2;
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Vector3 direction = transform.position - oriPos;
        
        Destroy(this.gameObject, bulletLifeTime);
        
        float ength = (transform.position - oriPos).magnitude;
        RaycastHit hitinfo;
        bool isCollider = Physics.Raycast(oriPos, direction, out hitinfo , ength);
        if (isCollider)
        {
            if(hitinfo.collider.GetComponent<WeaknessComponent>() != null)
            {
                hitinfo.collider.GetComponentInParent<EnemyController>().HP -= criticalDamage;//critical damage
            }
            else if(hitinfo.collider.GetComponent<EnemyController>() != null)
            {
                hitinfo.collider.GetComponentInParent<EnemyController>().HP -= bulletDamage;
            }
            if(hitinfo.collider.GetComponent<Ground>() != null)
            {
                Destroy(gameObject);
            }
            if (hitinfo.collider.GetComponent<FireballComponent>() != null)
            {
                Destroy(hitinfo.collider.gameObject);
            }
        }
        oriPos = transform.position;
    }
}
