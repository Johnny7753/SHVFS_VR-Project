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
    public float HP;
    public EnemyType type;
    [Tooltip("player")]
    [SerializeField]
    private Transform player;
    [Tooltip("enemy chase goal")]
    [SerializeField]
    private Transform goal;
    [Tooltip("the time range for finding next standing point")]
    [SerializeField]
    private Vector2 waitSeconds=new Vector2(5,10);

    private NavMeshAgent agent;
    private List<EnemyHidenPoint> points;
    private int nextPointIndex;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();        

        //find first hide point
        points = EnemySystem.Instance.points;
        nextPointIndex = Random.Range(0, points.Count);
        Debug.Log(points[nextPointIndex]);
        goal = points[nextPointIndex].transform;
        agent.destination = goal.position;
        //update next point every 5 seconds
        InvokeRepeating("FindNextPosition",5,5);
    }
    private void FixedUpdate()
    {
        //Debug.Log(Vector3.Distance(transform.position, points[nextPointIndex].transform.position));
       if(Vector3.Distance(transform.position, points[nextPointIndex].transform.position)<1.5f)
       {
            transform.LookAt(player);
       }
    }

    private void FindNextPosition()
    {
        nextPointIndex = Random.Range(nextPointIndex, points.Count);
        goal = points[nextPointIndex].transform;
        agent.destination = goal.position;

    }
}
