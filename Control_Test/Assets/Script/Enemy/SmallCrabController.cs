using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallCrabController : EnemyController
{
    [Header("SmallCrab Movement")]
    [Tooltip("the vector2 is a range")]
    [SerializeField]
    private Vector2 lateralOffset;
    [SerializeField]
    private Vector2 verticalOffset;
    [SerializeField]
    private float disToAttackPlayer=20f;
    [SerializeField]
    private float disToChangeTarget=10f;
    [SerializeField]
    private Vector2 actionArea;

    public GameObject Explosion;
    public GameObject Beetle;

    [Header("SmallCrab Attack")]
    [SerializeField]
    private float explodeRadius;

    private GameObject Audiomanager;
    //give player a hint that the crab is going to explode
    private MeshRenderer mat;
    private int direction;
    private float dieTimer;
    protected override void Start()
    {
        base.Start();
        InitializeEnemy();
        mat = GetComponentInChildren<MeshRenderer>();
        direction = Random.Range(-1f, 1f) > 0 ? 1 : -1;
        GetNextGoal();
        Audiomanager = FindObjectOfType<AudioManager>().gameObject;
        agent.updateRotation = false;
    }
    protected override void FixedUpdate()
    {
        transform.LookAt(player);
        //find next destination
        if (!beginAttack&& Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 5f)
        {
            agent.enabled=false;
            GetNextGoal();
        }
        //set player as destination
        //Debug.Log(Mathf.Abs(enemyTarget.x - transform.position.x));
        if (!beginAttack&&Mathf.Abs(enemyTarget.x-transform.position.x)<disToChangeTarget)
        {
            Debug.Log("change goal");
            goal = agent.destination = enemyTarget;
            beginAttack = true;
        }
        //explode state
        if (beginAttack&& Vector3.Distance(enemyTarget, transform.position)<disToAttackPlayer)
        {
            Debug.Log("attack");
            agent.enabled = false;
            //mat.material.color = Color.red;
            transform.LookAt(player);
            timer += Time.deltaTime;
            if (timer >= attackInterval)
            {
                timer = -100;
                //Debug.Log("explode");
                Explosion.SetActive(true);
                AudioSource.PlayClipAtPoint(Audiomanager.GetComponent<AudioManager>().smallcrabblast,this.transform.position);
                Beetle.SetActive(false);
                Invoke("waitToDie", 1f);
                
            }
        }
    }

    private void GetNextGoal()
    {
        Vector3 offset = new Vector3(-Random.Range(verticalOffset.x, verticalOffset.y), 0, direction * Random.Range(lateralOffset.x, lateralOffset.y));
        agent.enabled = true;
        goal =transform.position + offset;

        NavMeshHit hit;
        NavMesh.SamplePosition(goal, out hit, 10.0f, NavMesh.AllAreas);

        goal = new Vector3(hit.position.x, hit.position.y, hit.position.z < actionArea.x ? actionArea.x : hit.position.z);
        goal = new Vector3(hit.position.x, hit.position.y, hit.position.z > actionArea.y ? actionArea.y : hit.position.z);
        agent.destination = goal;

        direction = -direction;
    }

    public override void EnemyDie()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explodeRadius);
        if (hitColliders.Length > 0)
        {
            foreach (var hit in hitColliders)
            {
                if (hit.GetComponent<EnemyController>())
                {
                    hit.GetComponent<EnemyController>().HP -= enemyDamage;
                }
                else if (hit.GetComponent<Base>())
                {
                    hit.GetComponent<Base>().BaseHp -= enemyDamage;
                }
            }
        }

        base.EnemyDie();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if(beginAttack && Vector3.Distance(enemyTarget, transform.position) < disToAttackPlayer)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, explodeRadius);
        }
    }
    private void waitToDie()
    {
        EnemyDie();
    }
}
