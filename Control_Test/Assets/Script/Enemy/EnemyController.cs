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
    [Tooltip("enemy chase goal")]
    [SerializeField]
    protected Vector3 goal;
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
    private Transform shootPoint;
    [SerializeField]
    private GameObject bullet;

    protected Transform player;
    protected NavMeshAgent agent;
    //private List<EnemyHidenPoint> points;
    //private int nextPointIndex;
    //make sure invoke run once
    protected bool hasFoundNextPoint;
    protected int nextPointIndex;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("FakePlayer").transform; //'GameObject.Find' need to be replaced
        nextPointIndex = -1;
    }
    private void FixedUpdate()
    {
        //look at player to attack
        if (Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 1.5f)
        {
            transform.LookAt(player);
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
        if(pointsTaken)
            pointsTaken.isTaken = false;
        EnemySystem.Instance.enemyAlive.Remove(gameObject);
        Destroy(gameObject);
    }
    private void EnemyShoot()
    {
       var newBullet= Instantiate(bullet,shootPoint.position,shootPoint.rotation);
       newBullet.GetComponent<Rigidbody>().AddForce(5000*transform.forward);
    }

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
            if (!point.isTaken&&point.transform.position.x>transform.position.x)
                availablePoints.Add(point);
        }
        return availablePoints;
    }
}
