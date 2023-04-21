using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrabController : EnemyController
{
   public Animator animator;

    protected override void Start()
    {
        base.Start();
        InitializeEnemy();
        goal = agent.destination = enemyTarget;
        animator.SetBool("Isattack", false);
        agent.updateRotation = false;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 5f)
        {
            transform.LookAt(player);
            goal = agent.destination = transform.position;
            beginAttack = true;
            animator.SetBool("Isattack",true);                     //attack animation                                        by Hardy  4/21
        }
        GroundEnemyAttack();
    }
}
