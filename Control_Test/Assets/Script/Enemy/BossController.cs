using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private enum AttackWay
    {
        call,
        wave,
        fireball
    };

    [Header("Attributes")]
    public float HP=300;
    [SerializeField]
    private AttackWay attackway;

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


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("XR Origin").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CallEnemies();
        }
    }

    /// <summary>
    /// shoot fireball
    /// </summary>
    private void ShootFireball()
    {
        for(int i=0;i<fireballNum;i++)
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
        Instantiate(wavePrefab, shootPoint.position, shootPoint.rotation);
    }
    /// <summary>
    /// the function to call enemies
    /// </summary>
    private void CallEnemies()
    {
        for(int i=0;i<murlocNum;i++)
        {
            EnemySystem.Instance.RefreshOneEnemy(EnemySystem.Instance.enemyPrefab[1]);
        }
        for (int i = 0; i < mmNum; i++)
        {
            EnemySystem.Instance.RefreshOneEnemy(EnemySystem.Instance.enemyPrefab[4]);
        }
    }
}
