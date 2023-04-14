using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Attributes")]
    public float HP;
    public float Score;
    public float refreshInterval;
    
    [Header("Enemy Movement")]
    [SerializeField]
    protected Vector4 actionArea;
    [SerializeField]
    protected Vector4 bornArea;
    [SerializeField]
    protected EnemyHidenPoint pointsTaken;

    [Header("EnemyAttack")]
    [SerializeField]
    protected Transform shootPoint;
    [SerializeField]
    protected float attackInterval;

    protected Vector3 goal;
    protected Transform player;
    protected Vector3 enemyTarget;
    protected NavMeshAgent agent;
    //make sure invoke run once
    protected bool hasFoundNextPoint;
    protected int nextPointIndex;
    //count the attack interval
    protected float timer;
    //attack check for ground enemy
    protected bool beginAttack;
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("XR Origin").transform; //'GameObject.Find' need to be replaced,this is the target enemy should look at
        enemyTarget =FindObjectOfType<EnemyTarget>().transform.position;//this is the point enemy will reach
        enemyTarget = new Vector3(enemyTarget.x, enemyTarget.y, Random.Range( enemyTarget.z-30f, enemyTarget.z + 30f));
        nextPointIndex = -1;
    }
    protected virtual void FixedUpdate()
    {        
        if (HP <= 0)
        {
            EnemyDie();
        }
    }
    protected virtual void InitializeEnemy() { }

    //destroy this enemy
    public void EnemyDie()
    {
        
        GameObject.Find("RightHand Controller").GetComponent<VibrateManager>().VibrateController(10, 500);
        if (pointsTaken)
            pointsTaken.isTaken = false;
        EnemySystem.Instance.enemyAlive.Remove(gameObject);
        Destroy(gameObject);
    }


    protected List<T> GetAvailablePointsBeforeEnemy<T>(List<T> points) where T : EnemyHidenPoint
    {
        List<T> availablePoints = new List<T>();
        foreach (T point in points)
        {
           // Debug.Log(point.transform.position.x > transform.position.x);
            if (!point.isTaken&&point.transform.position.x<transform.position.x)
                availablePoints.Add(point);
        }
        return availablePoints;
    }
}
