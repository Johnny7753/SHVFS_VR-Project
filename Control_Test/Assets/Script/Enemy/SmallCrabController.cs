using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCrabController : EnemyController
{
    [Header("SmallCrab Movement")]
    [Tooltip("the vector2 is a range")]
    [SerializeField]
    private Vector2 lateralOffset;
    [SerializeField]
    private Vector2 verticalOffset;
    [SerializeField]
    private float disToAttackPlayer=10f;
    [SerializeField]
    private float disToChangeTarget=10f;

    //give player a hint that the crab is going to explode
    private MeshRenderer mat;
    private int direction;
    protected override void Start()
    {
        base.Start();
        InitializeEnemy();
        mat = GetComponentInChildren<MeshRenderer>();
        GetNextGoal();

        agent.updateRotation = false;
        direction = Random.Range(-1f,1f)>0?1:-1;
    }
    protected override void FixedUpdate()
    {
        transform.LookAt(player);
        //look at player to attack
        if (!beginAttack&& Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 5f)
        {
            agent.enabled=false;
            GetNextGoal();
        }
        //Debug.Log(Mathf.Abs(enemyTarget.x - transform.position.x));
        if(!beginAttack&&Mathf.Abs(enemyTarget.x-transform.position.x)<20f)
        {
            Debug.Log("change goal");
            goal = agent.destination = enemyTarget;
            beginAttack = true;
        }
        if (beginAttack&& Vector3.Distance(enemyTarget, transform.position)<10f)
        {
            Debug.Log("attack");
            agent.enabled = false;
            mat.material.color = Color.red;
            transform.LookAt(player);
            timer += Time.deltaTime;
            if (timer > attackInterval)
            {
                timer = 0;
                Debug.Log("explode");
                EnemyDie();
            }
        }
    }

    private void GetNextGoal()
    {
        Vector3 offset = new Vector3(-Random.Range(verticalOffset.x, verticalOffset.y), 0, direction * Random.Range(lateralOffset.x, lateralOffset.y));
        agent.enabled = true;
        goal = agent.destination = transform.position + offset;
        Debug.Log(goal);
        direction = -direction;
    }
}
