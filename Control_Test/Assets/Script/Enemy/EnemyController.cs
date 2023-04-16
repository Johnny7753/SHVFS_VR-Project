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

    [Header("Enemy Attack")]
    [SerializeField]
    protected float attackInterval;
    [Tooltip("the damage for melee enemy")]
    public float EnemyDamage;
    [SerializeField]
    protected Vector3 goal;//enemy move destination
    protected Transform player;//player, enemy will look at this point
    protected Vector3 enemyTarget;//enemy will stop at this point

    protected NavMeshAgent agent;

    //make sure invoke run once
    protected bool hasFoundNextPoint;
    //count the attack interval
    protected float timer;
    //attack check for ground enemy
    protected bool beginAttack;

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
