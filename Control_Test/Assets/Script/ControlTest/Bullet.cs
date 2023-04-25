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
    public GameObject BrokeImpackWave;
    public GameObject GunAudio;

    public int VFXHitCount;
    private GameObject Audiomanager;
    //public GameObject StoneExplosion;
    //public GameObject sandExplosion;

    public float shaketime;
    public float amp;

    private GameObject leftHand;
    private GameObject rightHand;
    // Start is called before the first frame update
    void Start()
    {
        GunAudio = FindObjectOfType<GunAudio>().gameObject;
        Audiomanager = FindObjectOfType<AudioManager>().gameObject;
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
        if (GunAudio.GetComponent<GunAudio>().hitCount >= VFXHitCount)
        {
            MonsterExplosion.SetActive(true);
            GunAudio.GetComponent<GunAudio>().hitCount-=VFXHitCount;
        }

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
                leftHand.GetComponent<VibrateManager>().VibrateController(amp, shaketime);
                rightHand.GetComponent<VibrateManager>().VibrateController(amp, shaketime);
                hitinfo.collider.GetComponentInParent<EnemyController>().HP -= bulletDamage;
                //Audiomanager.GetComponent<AudioManager>().BulletHitSmallCrab.Play();
                // AudioSource.PlayClipAtPoint(Audiomanager.GetComponent<AudioManager>().BulletHitSmallCrab, this.transform.position);
                GunAudio.GetComponent<GunAudio>().hitCount++;
                Invoke("DestoryBullet", 1f);
            }
            else if (hitinfo.collider.GetComponent<CrabController>() != null)
            {
                speed = 0;
                leftHand.GetComponent<VibrateManager>().VibrateController(amp, shaketime);
                rightHand.GetComponent<VibrateManager>().VibrateController(amp, shaketime);
                hitinfo.collider.GetComponentInParent<EnemyController>().HP -= bulletDamage;
                //Audiomanager.GetComponent<AudioManager>().BulletHitBigCrab.Play();
                //AudioSource.PlayClipAtPoint(Audiomanager.GetComponent<AudioManager>().BulletHitBigCrab, this.transform.position);
                GunAudio.GetComponent<GunAudio>().hitCount++;
                Invoke("DestoryBullet", 1f);
            }
            else if (hitinfo.collider.GetComponent<FlyingDragonController>() != null)
            {
                speed = 0;
                leftHand.GetComponent<VibrateManager>().VibrateController(amp, shaketime);
                rightHand.GetComponent<VibrateManager>().VibrateController(amp, shaketime);
                hitinfo.collider.GetComponentInParent<EnemyController>().HP -= bulletDamage;
                //Audiomanager.GetComponent<AudioManager>().BulletHitDragon.Play();
                // AudioSource.PlayClipAtPoint(Audiomanager.GetComponent<AudioManager>().BulletHitDragon, this.transform.position);
                GunAudio.GetComponent<GunAudio>().hitCount++;
                Invoke("DestoryBullet", 1f);
            }
            else if (hitinfo.collider.GetComponent<MurlocController>() != null)
            {
                speed = 0;
                leftHand.GetComponent<VibrateManager>().VibrateController(amp, shaketime);
                rightHand.GetComponent<VibrateManager>().VibrateController(amp, shaketime);
                hitinfo.collider.GetComponentInParent<EnemyController>().HP -= bulletDamage;
                //Audiomanager.GetComponent<AudioManager>().BulletHitMurloc.Play();
                //AudioSource.PlayClipAtPoint(Audiomanager.GetComponent<AudioManager>().BulletHitMurloc, this.transform.position);
                GunAudio.GetComponent<GunAudio>().hitCount++;
                Invoke("DestoryBullet", 1f);
            }
            else if (hitinfo.collider.GetComponent<GhostController>() != null)
            {
                speed = 0;
                leftHand.GetComponent<VibrateManager>().VibrateController(amp, shaketime);
                rightHand.GetComponent<VibrateManager>().VibrateController(amp, shaketime);
                hitinfo.collider.GetComponentInParent<EnemyController>().HP -= bulletDamage;
                //Audiomanager.GetComponent<AudioManager>().BulletHitGhost.Play();
                //AudioSource.PlayClipAtPoint(Audiomanager.GetComponent<AudioManager>().BulletHitGhost, this.transform.position);
                GunAudio.GetComponent<GunAudio>().hitCount++;
                Invoke("DestoryBullet", 1f);
            }
            else if (hitinfo.collider.GetComponent<BossController>() != null)
            {
                speed = 0;
                leftHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                rightHand.GetComponent<VibrateManager>().VibrateController(0.8f, 5);
                hitinfo.collider.GetComponentInParent<BossController>().HP -= bulletDamage;
                GunAudio.GetComponent<GunAudio>().hitCount++;
                Invoke("DestoryBullet", 1f);
            }
            if (hitinfo.collider.GetComponent<Ground>() != null)
            {
                Audiomanager.GetComponent<AudioManager>().BulletHitSand.Play();
                //AudioSource.PlayClipAtPoint(Audiomanager.GetComponent<AudioManager>().BulletHitSand, this.transform.position);
                Invoke("DestoryBullet", 1f);
            }
            if (hitinfo.collider.GetComponent<Stone>() != null)
            {
                Audiomanager.GetComponent<AudioManager>().BulletHitStone.Play();
                //AudioSource.PlayClipAtPoint(Audiomanager.GetComponent<AudioManager>().BulletHitStone, this.transform.position);
                Invoke("DestoryBullet", 1f);
            }
            if (hitinfo.collider.GetComponent<FireballComponent>() != null)
            {
                speed = 0;
                FireBallExplosion.SetActive(true);
                Destroy(hitinfo.collider.gameObject);
                Invoke("DestoryBullet", 1f);
            }
            if (hitinfo.collider.GetComponent<AirDrop>() != null)
            {
                speed = 0;
                FindObjectOfType<SpawnBoxMagazine_Rocket>().gameObject.GetComponent<SpawnBoxMagazine>().Spawn();
                Destroy(hitinfo.collider.gameObject);
                Invoke("DestoryBullet", 1f);
            }
            if(hitinfo.collider.GetComponent<ShockWaveComponent>() != null)
            {
                speed = 0;
                Audiomanager.GetComponent<AudioManager>().BrokeImpackWave.Play();
                BrokeImpackWave.SetActive(true);
                Destroy(hitinfo.collider.gameObject);
                Invoke("DestoryBullet", 1.5f);
            }
        }
        oriPos = transform.position;
    }
    public void DestoryBullet()
    {
        Destroy(gameObject);
    }
}
