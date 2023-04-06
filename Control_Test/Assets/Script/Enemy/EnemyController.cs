using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyType
{
    A,
    B
};
public class EnemyController : MonoBehaviour
{
    [Header("Attrabutes")]
    public float HP;
    public EnemyType type;

    [Header("Enemy Movement")]
    [Tooltip("enemy chase goal")]
    [SerializeField]
    private Transform goal;
    [Tooltip("the time range for finding next standing point")]
    [SerializeField]
    private Vector2 waitSeconds=new Vector2(5,10);

    [Header("EnemyAttack")]
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private GameObject bullet;

    private Transform player;
    private NavMeshAgent agent;
    private List<EnemyHidenPoint> points;
    private int nextPointIndex;
    //make sure invoke run once
    private bool hasFoundNextPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("FakePlayer").transform; //'GameObject.Find' need to be replaced

        //find first hide point
        points = EnemySystem.Instance.points;
        do
        {
            nextPointIndex = Random.Range(0, points.Count);
        } while (points[nextPointIndex].isTaken);
        points[nextPointIndex].isTaken = true;
        goal = points[nextPointIndex].transform;
        agent.destination = goal.position;
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            EnemyShoot();
        }
    }
    private void FixedUpdate()
    {
       //look at player to attack
        if(Vector3.Distance(transform.position, goal.position) <1.5f)
       {
            transform.LookAt(player);
            if (!hasFoundNextPoint)
            {
                Invoke("FindNextPosition", Random.Range(waitSeconds.x, waitSeconds.y));
                hasFoundNextPoint = true;
            }

        }
    }

    private void FindNextPosition()
    {
        int lastIndex = nextPointIndex;
        nextPointIndex = Random.Range(nextPointIndex, points.Count);
        if(points[nextPointIndex].isTaken)
        {
            nextPointIndex = lastIndex;
            hasFoundNextPoint = false;
            return;
        }
        points[lastIndex].isTaken = false;
        points[nextPointIndex].isTaken = true;
        goal = points[nextPointIndex].transform;
        agent.destination = goal.position;
        hasFoundNextPoint = false;
    }
    //destroy this enemy
    public void EnemyDie()
    {
        points[nextPointIndex].isTaken = false;
        Destroy(gameObject);
    }
    private void EnemyShoot()
    {
       var newBullet= Instantiate(bullet,shootPoint.position,shootPoint.rotation);
       newBullet.GetComponent<Rigidbody>().AddForce(5000*transform.forward);
    }
}
