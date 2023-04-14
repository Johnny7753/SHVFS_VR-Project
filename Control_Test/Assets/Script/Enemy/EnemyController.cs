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
    [Tooltip("the time range for finding next standing point")]
    [SerializeField]
    protected Vector2 waitSeconds=new Vector2(5,10);
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
        player = GameObject.Find("XR Origin").transform; //'GameObject.Find' need to be replaced
        nextPointIndex = -1;
    }
    protected virtual void FixedUpdate()
    {
        
        if (HP <= 0)
        {
            EnemyDie();
        }
        //look at player to attack
        if (!beginAttack&&Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 1.5f)
        {
            transform.LookAt(new Vector3(player.transform.position.x,transform.position.y, player.transform.position.z));
            if (!hasFoundNextPoint)
            {
                Invoke("RefreshStandPoint", Random.Range(waitSeconds.x, waitSeconds.y));
                hasFoundNextPoint = true;
            }
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
    //private void EnemyShoot()
    //{
    //   var newBullet= Instantiate(bullet,shootPoint.position,shootPoint.rotation);
    //   newBullet.GetComponent<Rigidbody>().AddForce(5000*transform.forward);
    //}

    protected List<T> GetAvailablePoints<T>(List<T> points) where T:EnemyHidenPoint
    {
        List<T> availablePoints = new List<T>();
        foreach (T point in points)
        {
            if (!point.isTaken)
                availablePoints.Add(point);
        }
        return availablePoints;
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
