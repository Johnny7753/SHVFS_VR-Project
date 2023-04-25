using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossAttack
{
    public AttackWay attackWay;
    public float timeInterval;
}
public enum AttackWay
{
    call,
    wave,
    fireball
};
public class BossController : MonoBehaviour
{


    [Header("Attributes")]
    public float HP=300;
    [SerializeField]
    private AttackWay attackway;

    [Header("Attack")]
    [SerializeField]
    private BossAttack[] attacklist;

    [Header("Boss Call")]
    [SerializeField]
    private float hornInterval=15f;
    [SerializeField]
    private int murlocNum = 6;
    [SerializeField]
    private int mmNum = 2;

    [Header("Shock Wave")]
    [SerializeField]
    private GameObject wavePrefab;

    [Header("Fireball")]
    [SerializeField]
    private GameObject fireballPrefab;
    [SerializeField]
    private int fireballNum;
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private float angleBetween;

    private Transform player;

    private int attackIndex = 0;
    private float timer;
    private bool isAttacking;

    private bool isDie;

    private Animator anim;

    private bool beginHorn;

    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        player = GameObject.Find("XR Origin").transform;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            HP = 0;
        }
        if(HP<=0&&!isDie)
        {
            isDie = true;
            anim.SetTrigger("IsDead");
        }

    }
    private void FixedUpdate()
    {
        if (!isAttacking && attackIndex < attacklist.Length)
        {
            attackway = attacklist[attackIndex].attackWay;
            isAttacking = true;
            timer = 0;
            switch (attackway)
            {
                case AttackWay.call:
                    CallEnemiesAnim();
                    break;
                case AttackWay.fireball:
                    ShootFireball();
                    break;
                case AttackWay.wave:
                    CallWave();
                    break;
            }
        }
        if (isAttacking)
        {
            timer += Time.deltaTime;
            if (timer >= attacklist[attackIndex].timeInterval)
            {
                attackIndex = (attackIndex + 1) % attacklist.Length;
                isAttacking = false;
            }
        }
        if (beginHorn)
        {
            timer += Time.deltaTime;
            if (timer > hornInterval)
            {
                timer = 0;
                isAttacking = true;
                beginHorn = false;
                CallEnemies();
            }
        }
    }

    /// <summary>
    /// shoot fireball
    /// </summary>
    private void ShootFireball()
    {
        audioManager.DragonAttack.Play();
        anim.SetTrigger("Fire");
        for (int i=0;i<fireballNum;i++)
        {
            //var offset = new Vector3(0,0,i*30f*((i%2)*2-1));
            var fireball= Instantiate(fireballPrefab,shootPoint.position,shootPoint.rotation);
            fireball.transform.RotateAround(player.position,Vector3.up,i*angleBetween * ((i % 2) * 2 - 1));
        } 
    }
    /// <summary>
    /// call shock wave
    /// </summary>
    private void CallWave()
    {
        audioManager.BossCallShockWave.Play();
        anim.SetTrigger("Wave");
        Instantiate(wavePrefab, shootPoint.position, shootPoint.rotation);
    }
    /// <summary>
    /// the function to call enemies
    /// </summary>
    private void CallEnemies()
    {
        audioManager.BossCallEnemy.Play();
        for(int i=0;i<murlocNum;i++)
        {
            EnemySystem.Instance.RefreshOneEnemy(EnemySystem.Instance.enemyPrefab[1]);
        }
        for (int i = 0; i < mmNum; i++)
        {
            EnemySystem.Instance.RefreshOneEnemy(EnemySystem.Instance.enemyPrefab[4]);
        }
    }
    private void CallEnemiesAnim()
    {        
        anim.SetTrigger("Call");
        beginHorn = true;
    }
}
