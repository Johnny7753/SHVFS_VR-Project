using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDragonController : EnemyController
{    
    [Header("Enemy Shoot")]
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    protected Transform shootPoint;

    protected override void Start()
    {
        base.Start();
        InitializeEnemy();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Attack();
    }

    private void Attack()
    {
        timer += Time.deltaTime;
        if(timer>=attackInterval)
        {
            timer = 0;
            Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        }
    }
}
