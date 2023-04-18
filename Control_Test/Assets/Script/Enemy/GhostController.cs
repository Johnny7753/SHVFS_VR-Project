using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : EnemyController
{
    [Header("SmallCrab Movement")]
    [Tooltip("the vector2 is a range")]
    [SerializeField]
    private Vector2 lateralOffset;
    [SerializeField]
    private Vector2 verticalOffset;
    [SerializeField]
    private float disToAttackPlayer = 20f;
    [SerializeField]
    private float disToChangeTarget = 10f;
    [SerializeField]
    private Vector2 actionArea;

    [Header("SmallCrab Invisible")]
    [SerializeField]
    private Vector2 invisibleInterval;
    [SerializeField]
    private float invisibleDuration;
    [SerializeField]
    private Material invisibleMat;

    private Material mat;
    private SkinnedMeshRenderer ghostRenderer;
    private int direction;
    private float invisbleTimer = 0;
    private float currentInvisibleInterval;
    private enum GhostState
    {
        invisible,
        visible
    };
    private GhostState state;
    private bool changeState;
    protected override void Start()
    {
        base.Start();
        InitializeEnemy();

        ghostRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        mat = ghostRenderer.material;

        direction = Random.Range(-1f, 1f) > 0 ? 1 : -1;
        GetNextGoal();

        agent.updateRotation = false;
       
        state = GhostState.visible;
        changeState = true;
    }

    protected override void FixedUpdate()
    {
        transform.LookAt(player);

        InvisbleStateChange();

        //find next destination
        if (!beginAttack && Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 5f)
        {
            agent.enabled = false;
            GetNextGoal();
        }
        //set player as destination
        //Debug.Log(Mathf.Abs(enemyTarget.x - transform.position.x));
        if (!beginAttack && Mathf.Abs(enemyTarget.x - transform.position.x) < disToChangeTarget)
        {
            Debug.Log("change goal");
            goal = agent.destination = enemyTarget;
            beginAttack = true;
        }
        //explode state
        if (Vector3.Distance(enemyTarget, transform.position) < disToAttackPlayer)
        {
            GroundEnemyAttack();
        }
    }

    private void GetNextGoal()
    {
        Vector3 offset = new Vector3(-Random.Range(verticalOffset.x, verticalOffset.y), 0, direction * Random.Range(lateralOffset.x, lateralOffset.y));
        //Ray ray1 = new Ray(transform.position + offset, -Vector3.up);
        //Ray ray2 = new Ray(transform.position + offset, Vector3.up);
        //RaycastHit hit1;
        //RaycastHit hit2;
        //Physics.Raycast(ray1, out hit1, 50, 1 << 7);
        //Physics.Raycast(ray2, out hit2, 50, 1 << 7);

        agent.enabled = true;
        goal = transform.position + offset;
        NavMeshHit hit;
        NavMesh.SamplePosition(goal, out hit, 10.0f, NavMesh.AllAreas);
        //+new Vector3(0,hit1.collider? hit1.point.y + 1 : hit2.point.y + 1, 0);
        //ensure the enemy only move in the action area
        goal = new Vector3(hit.position.x, hit.position.y, hit.position.z < actionArea.x ? actionArea.x : hit.position.z);
        goal = new Vector3(hit.position.x, hit.position.y, hit.position.z > actionArea.y ? actionArea.y : hit.position.z);
        agent.destination = goal;

        direction = -direction;
    }
    //this is for enemy to change state
    private void InvisbleStateChange()
    {
        if (state == GhostState.visible)
        {
            if (changeState)
            {
                changeState = false;
                ghostRenderer.material = mat;
                currentInvisibleInterval = Random.Range(invisibleInterval.x, invisibleInterval.y);
            }
            invisbleTimer += Time.deltaTime;
            if (invisbleTimer > currentInvisibleInterval)
            {
                invisbleTimer = 0;
                state = GhostState.invisible;
                changeState = true;
            }
        }
        else
        {
            if (changeState)
            {
                changeState = false;
                ghostRenderer.material = new Material(invisibleMat);
            }
            invisbleTimer += Time.deltaTime;
            if (invisbleTimer > invisibleDuration)
            {
                invisbleTimer = 0;
                state = GhostState.visible;
                changeState = true;
            }
        }
    }
}
