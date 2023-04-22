using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed;
    public float bulletLifeTime;
    public Vector3 oriPos;
    public float RocketDamage;
    public float criticalDamage;
    public GameObject GameManager;
    public GameObject ExplosionVFX;

    [SerializeField]
    private float explodeRadius;


    // Start is called before the first frame update
    void Start()
    {
        GameManager = FindObjectOfType<GameManager>().gameObject;
        oriPos = transform.position;
    }

    private void FixedUpdate()
    {
        RocketDamage = GameManager.GetComponent<GameManager>().oriRocketDamage;
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
            if(hitinfo.collider.GetComponent<EnemyController>() != null)
            {
                speed = 0;
                Collider[] hitColliders = Physics.OverlapSphere(hitinfo.transform.position, explodeRadius);
                if (hitColliders.Length > 0)
                {
                    foreach (var hit in hitColliders)
                    {
                        if (hit.GetComponent<EnemyController>())
                        {
                            hit.GetComponent<EnemyController>().HP -= RocketDamage;
                        }
                        else if (hit.GetComponent<FireballComponent>() != null)
                        {
                            Destroy(hit.gameObject);
                        }
                    }
                }
                //Instantiate(ExplosionVFX, this.transform);
                ExplosionVFX.SetActive(true);
                Invoke("Explotion", 1f);
            }
            if(hitinfo.collider.GetComponent<Ground>() != null)
            {
                speed = 0;
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, explodeRadius);
                if (hitColliders.Length > 0)
                {
                    foreach (var hit in hitColliders)
                    {
                        if (hit.GetComponent<EnemyController>())
                        {
                            hit.GetComponent<EnemyController>().HP -= RocketDamage;
                        }
                        else if (hit.GetComponent<FireballComponent>() != null)
                        {
                            Destroy(hit.gameObject);
                        }
                    }
                }
                
                //Instantiate(ExplosionVFX, this.transform);
                ExplosionVFX.SetActive(true);
                Invoke("Explotion", 1f);
            }
            if (hitinfo.collider.GetComponent<FireballComponent>() != null)
            {
                speed = 0;
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, explodeRadius);
                if (hitColliders.Length > 0)
                {
                    foreach (var hit in hitColliders)
                    {
                        if (hit.GetComponent<EnemyController>())
                        {
                            hit.GetComponent<EnemyController>().HP -= RocketDamage;
                        }
                        else if (hit.GetComponent<FireballComponent>() != null)
                        {
                            Destroy(hit.gameObject);
                        }
                    }
                }
                //Instantiate(ExplosionVFX, this.transform);
                ExplosionVFX.SetActive(true);
                Invoke("Explotion", 1f);
            }
        }
        oriPos = transform.position;
    }
    public void Explotion()
    {
        Destroy(gameObject);
    }
}
