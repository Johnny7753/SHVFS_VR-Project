using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurlocMotherController : EnemyController
{
    [Header("MM Attack")]
    [SerializeField]
    private Vector2 moveDuration;
    [SerializeField]
    private float attackDuration;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    protected Transform shootPoint;

    private enum MMState
    {
        attack,
        move,

     };
     [SerializeField]
    private MMState state;

    private float currentMoveDuration;

    private float mmTimer;
    private bool changeState;


    protected override void Start()
    {
        base.Start();
        InitializeEnemy();
        goal = agent.destination = enemyTarget;

        state = MMState.move;
        changeState = true;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        mmTimer += Time.deltaTime;
        if(state==MMState.move)
        {
            if(changeState)
            {
                changeState = false;
                currentMoveDuration = Random.Range(moveDuration.x,moveDuration.y);
                agent.enabled = true;
                goal = agent.destination = enemyTarget;

            }
            if(mmTimer>currentMoveDuration)
            {
                mmTimer = 0;
                state = MMState.attack;
                changeState = true;
            }
        }
        else
        {
            if (changeState)
            {
                changeState = false;
                agent.enabled = false;
            }
            //attack 
            Attack();
            if (mmTimer > attackDuration)
            {
                mmTimer = 0;
                state = MMState.move;
                changeState = true;
            }
        }

        if (Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 50f)
        {
            transform.LookAt(player);
            agent.enabled = false;
        }
    }
    private void Attack()
    {
        timer += Time.deltaTime;
        if (timer >= attackInterval)
        {
            timer = 0;
            Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        }
    }
}
