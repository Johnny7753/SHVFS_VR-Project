using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurlocController : EnemyController
{
    #region parameters
    [Header("Murloc Movement")]
    [Tooltip("the time range for finding next standing point")]
    [SerializeField]
    protected Vector2 waitSeconds = new Vector2(5, 10);
    [SerializeField]
    [Tooltip("The enemy will hide less if the value increases")]
    [Range(0, 10)]
    private float momentum = 3;//the enemy momentum
    [SerializeField]
    [Tooltip("this is the circle for enemy to find hidder")]
    private float checkHiddenPointRadius;

    private bool isHidden;//to check if player is hidden

    #endregion

    protected override void Start()
    {
        MonsterAudio = FindObjectOfType<AudioManager>().BulletHitMurloc;
        animator = GetComponentInChildren<Animator>();
        base.Start();
        InitializeEnemy();
        goal = agent.destination = enemyTarget;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.LookAt(player);
        if (!isHidden)
            CheckHiddenPoint();

        if (!beginAttack&&goal == enemyTarget && Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 5)
        {
            agent.enabled = false;
            beginAttack = true;
            timer = attackInterval;
        }
        GroundEnemyAttack();
    }
    //find hidden point while moving
    private void CheckHiddenPoint()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkHiddenPointRadius,1<<8);
        if(hitColliders.Length>0)
        {
            foreach (var hit in hitColliders)
            {
                if(hit.transform.position.x<transform.position.x-10 && hit.GetComponent<EnemyHidenPoint>().isTaken==false)
                {
                    Debug.Log("hide");
                    isHidden = true;
                    goal = agent.destination = hit.transform.position;
                    pointsTaken = hit.GetComponent<EnemyHidenPoint>();
                    pointsTaken.isTaken = true;
                    Invoke("ReTargetPlayer", Random.Range(waitSeconds.x, waitSeconds.y));
                    break;
                }
            }
        }
    }

    //show check sphere
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, checkHiddenPointRadius);
    }

    private void ReTargetPlayer()
    {
        isHidden = false;
        pointsTaken.isTaken = false;
        pointsTaken = null;
        goal = agent.destination = enemyTarget;
    }

}
