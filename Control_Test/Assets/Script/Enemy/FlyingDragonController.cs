using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDragonController : EnemyController
{    
    private List<FlyingDragonHiddenPoint> points;
    private List<FlyingDragonHiddenPoint> availablePoints;

    [SerializeField]
    private GameObject bullet;

    protected override void Start()
    {
        base.Start();

        //find first hide point and set destination        
        points = EnemySystem.Instance.fdPoints;
        InitializeEnemy();
    }
    protected override void FixedUpdate()
    {
        if (HP <= 0)
        {
            Debug.Log(1);
            EnemyDie();
        }
        //look at player to attack
        if (Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 1.5f)
        {
            transform.LookAt(player);
            Attack();
            if (!hasFoundNextPoint)
            {
                Invoke("RefreshStandPoint", Random.Range(waitSeconds.x, waitSeconds.y));
                hasFoundNextPoint = true;
            }
        }
    }
    protected override void InitializeEnemy()
    {
        transform.position = FindStandPoint();

        //check ground
        Ray ray = new Ray(transform.position, -Vector3.up);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 50, 1 << 7);
        //Debug.Log(hit.point);
        transform.position += new Vector3(0, hit.point.y + 1, 0);
        transform.LookAt(player);

        agent.enabled = true;
    }
    private void RefreshStandPoint()
    {
        FlyingDragonHiddenPoint lastPoint =(FlyingDragonHiddenPoint)pointsTaken;        
        agent.destination = FindStandPoint();
        if (lastPoint != null)
            lastPoint.isTaken = false;
        hasFoundNextPoint = false;
    }
    private Vector3 FindStandPoint()
    {
        availablePoints = GetAvailablePoints(points);
        if (availablePoints.Count != 0)
        {
            nextPointIndex = Random.Range(0, availablePoints.Count);
            availablePoints[nextPointIndex].isTaken = true;
            pointsTaken = availablePoints[nextPointIndex];
            goal = availablePoints[nextPointIndex].transform.position;
        }
        else
        {
            pointsTaken = null;
            nextPointIndex = -1;
            goal = new Vector3(Random.Range(actionArea.x, actionArea.y), 0, Random.Range(actionArea.z, actionArea.w));
        }
        return goal;       
    }

    private void Attack()
    {
        timer += Time.deltaTime;
        if(timer>=attackInterval)
        {
            timer = 0;
            Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        }
    }
}
