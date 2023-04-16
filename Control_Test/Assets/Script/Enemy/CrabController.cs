using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabController : EnemyController
{
    protected override void Start()
    {
        base.Start();
        InitializeEnemy();
        goal = agent.destination = enemyTarget;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 100f)
        {
            transform.LookAt(player);
            goal = agent.destination = transform.position;
            beginAttack = true;
        }
        GroundEnemyAttack();
    }
}
