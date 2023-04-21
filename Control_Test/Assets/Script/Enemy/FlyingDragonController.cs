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
    //public Animator dragonanimator;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
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
            animator.SetBool("IsAttack",true);
            Invoke("FireBall",0.7f);
        }
    }

    public void FireBall()
    {
        Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }
}
