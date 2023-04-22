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
    public GameObject MonsterExplosion;
    public GameObject FireBallExplosion;
    //public GameObject StoneExplosion;
    //public GameObject sandExplosion;


    private GameObject leftHand;
    private GameObject rightHand;
    // Start is called before the first frame update
    void Start()
    {
        leftHand = GameObject.Find("LeftHand Controller");
        rightHand = GameObject.Find("RightHand Controller");
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
                Destroy(gameObject);
            }
            else if(hitinfo.collider.GetComponent<SmallCrabController>() != null)
            {
                speed = 0;
                leftHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                rightHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                hitinfo.collider.GetComponentInParent<EnemyController>().HP -= bulletDamage;
                MonsterExplosion.SetActive(true);
                Invoke("DestoryBullet", 1f);
            }
            else if (hitinfo.collider.GetComponent<CrabController>() != null)
            {
                speed = 0;
                leftHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                rightHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                hitinfo.collider.GetComponentInParent<EnemyController>().HP -= bulletDamage;
                MonsterExplosion.SetActive(true);
                Invoke("DestoryBullet", 1f);
            }
            else if (hitinfo.collider.GetComponent<FlyingDragonController>() != null)
            {
                speed = 0;
                leftHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                rightHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                hitinfo.collider.GetComponentInParent<EnemyController>().HP -= bulletDamage;
                MonsterExplosion.SetActive(true);
                Invoke("DestoryBullet", 1f);
            }
            else if (hitinfo.collider.GetComponent<MurlocController>() != null)
            {
                speed = 0;
                leftHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                rightHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                hitinfo.collider.GetComponentInParent<EnemyController>().HP -= bulletDamage;
                MonsterExplosion.SetActive(true);
                Invoke("DestoryBullet", 1f);
            }
            else if (hitinfo.collider.GetComponent<GhostController>() != null)
            {
                speed = 0;
                leftHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                rightHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                hitinfo.collider.GetComponentInParent<EnemyController>().HP -= bulletDamage;
                MonsterExplosion.SetActive(true);
                Invoke("DestoryBullet", 1f);
            }
            /*else if (hitinfo.collider.GetComponent<SmallCrabController>() != null)
            {
                speed = 0;
                leftHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                rightHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                hitinfo.collider.GetComponentInParent<EnemyController>().HP -= bulletDamage;
                MonsterExplosion.SetActive(true);
                Invoke("DestoryBullet", 1f);
            }*/
            if (hitinfo.collider.GetComponent<Ground>() != null)
            {
                Destroy(gameObject);
            }
            if (hitinfo.collider.GetComponent<Stone>() != null)
            {
                Destroy(gameObject);
            }
            if (hitinfo.collider.GetComponent<FireballComponent>() != null)
            {
                speed = 0;
                FireBallExplosion.SetActive(true);
                Destroy(hitinfo.collider.gameObject);
                Invoke("DestoryBullet", 1f);
            }
        }
        oriPos = transform.position;
    }
    public void DestoryBullet()
    {
        Destroy(gameObject);
    }
}
