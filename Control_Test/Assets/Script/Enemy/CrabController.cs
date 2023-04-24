using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrabController : EnemyController
{
   //public Animator animator;

    protected override void Start()
    {
        base.Start();
        MonsterAudio = FindObjectOfType<AudioManager>().BulletHitBigCrab;
        InitializeEnemy();
        goal = agent.destination = enemyTarget;
        animator = GetComponentInChildren<Animator>();
        agent.updateRotation = false;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!beginAttack&& Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 5f)
        {
            transform.LookAt(player);
            goal = agent.destination = transform.position;
            beginAttack = true;
            timer = attackInterval;
          //  animator.SetBool("Isattack",true);                     //attack animation                                        by Hardy  4/21
        }
        GroundEnemyAttack();
    }
}
