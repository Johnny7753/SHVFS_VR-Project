using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region parameters
    [Header("Attributes")]
    public float HP;
    public float Score;
    public float refreshInterval;
    
    [Header("Enemy Movement")]
    //[SerializeField]
    //protected Vector4 actionArea;
    [SerializeField]
    protected Vector4 bornArea;
    [SerializeField]
    [Tooltip("the point which enemy is hidden")]
    protected EnemyHidenPoint pointsTaken;
    [SerializeField]
    private Transform rayStart;
    [SerializeField]
    protected float rayLength;
    [SerializeField]
    protected float rayWidth;

    [Header("Enemy Attack")]
    [SerializeField]
    protected float attackInterval;
    [Tooltip("the damage for melee enemy")]
    public float EnemyDamage;

    protected Vector3 goal;//enemy move destination
    protected Transform player;//player, enemy will look at this point
    protected Vector3 enemyTarget;//enemy will stop at this point

    protected NavMeshAgent agent;

    //count the attack interval
    protected float timer;
    //attack check for ground enemy
    protected bool beginAttack;

    RaycastHit m_Hit;
    bool m_HitDetect;

    #endregion

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("XR Origin").transform; //'GameObject.Find' need to be replaced,this is the target enemy should look at
        enemyTarget =FindObjectOfType<EnemyTarget>().transform.position;//this is the point enemy will reach
        enemyTarget = new Vector3(enemyTarget.x, enemyTarget.y, Random.Range( enemyTarget.z-30f, enemyTarget.z + 30f));
    }
    protected virtual void FixedUpdate()
    {
        CheckFrontEnemy();
        if (HP <= 0)
        {
            EnemyDie();
        }
    }

    #region Function(ALL)
    protected void InitializeEnemy() 
    {
        transform.position = new Vector3(Random.Range(bornArea.x, bornArea.y), 0, Random.Range(bornArea.z, bornArea.w));
        //check the ground point
        Ray ray = new Ray(transform.position, -Vector3.up);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 50, 1 << 7);
        //Debug.Log(hit.point);
        transform.position += new Vector3(0, hit.point.y + 1, 0);
        transform.LookAt(player);

        agent.enabled = true;
    }

    //destroy this enemy
    public void EnemyDie()
    {        
        GameObject.Find("RightHand Controller").GetComponent<VibrateManager>().VibrateController(10, 500);
        if (pointsTaken)
            pointsTaken.isTaken = false;
        EnemySystem.Instance.enemyAlive.Remove(gameObject);
        Destroy(gameObject);
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(rayStart.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(rayStart.position + transform.forward * m_Hit.distance,2* new Vector3(10, 10, rayWidth / 2));
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(rayStart.position, transform.forward * rayLength);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(rayStart.position + transform.forward * rayLength,2* new Vector3(10, 10, rayWidth / 2));
        }
    }
    private void CheckFrontEnemy()
    {
        //Debug.DrawLine(rayStart.position-new Vector3(), rayStart.position+transform.forward*rayLength,Color.red);
        m_HitDetect=Physics.BoxCast(rayStart.position,new Vector3(10,10,rayWidth/2),transform.forward,out m_Hit,transform.rotation,rayLength,1<<9);
        if (m_HitDetect)
        {
            //Output the name of the Collider your Box hit
            Debug.Log("Hit : " + m_Hit.collider.name);
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
        //if (Physics.Raycast(rayStart.position, transform.forward, rayLength, 1 << 9))
        //{
        //    agent.isStopped = true;
        //}
        //else
        //{
        //    agent.isStopped = false;
        //}
    }
    #endregion

    #region Function(GROUND)
    protected void GroundEnemyAttack()
    {
        if (beginAttack)
        {
            timer += Time.deltaTime;
            if (timer > attackInterval)
            {
                timer = 0;
                FindObjectOfType<Base>().GetComponent<Base>().BaseHp -= EnemyDamage;
            }
        }
    }
    #endregion

    //protected List<T> GetAvailablePointsBeforeEnemy<T>(List<T> points) where T : EnemyHidenPoint
    //{
    //    List<T> availablePoints = new List<T>();
    //    foreach (T point in points)
    //    {
    //       // Debug.Log(point.transform.position.x > transform.position.x);
    //        if (!point.isTaken&&point.transform.position.x<transform.position.x)
    //            availablePoints.Add(point);
    //    }
    //    return availablePoints;
    //}
}
