using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurlocController : EnemyController
{
    private List<OtherEnemyHiddenPoint> points;
    private List<OtherEnemyHiddenPoint> availablePoints;

    private bool beginAttack;
    protected override void Start()
    {
        base.Start();

        //find first hide point and set destination        
        points = EnemySystem.Instance.points;
        InitializeEnemy();
        agent.destination = FindStandPoint();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        //Debug.Log(Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)));
        if(beginAttack)
        {
            transform.LookAt(player);
            timer += Time.deltaTime;
            if(timer>attackInterval)
            {
                timer = 0;
                Debug.Log("fortress be attacked");
            }
        }
    }
    protected override void InitializeEnemy()
    {
        transform.position = new Vector3(Random.Range(bornArea.x, bornArea.y), 0, Random.Range(bornArea.z, bornArea.w));
        transform.LookAt(player);
    }
    private void RefreshStandPoint()
    {
        OtherEnemyHiddenPoint lastPoint = (nextPointIndex > -1) ? availablePoints[nextPointIndex] : null;
        agent.destination = FindStandPoint();
        if (lastPoint != null)
            lastPoint.isTaken = false;
        hasFoundNextPoint = false;
    }
    private Vector3 FindStandPoint()
    {
        availablePoints = GetAvailablePointsBeforeEnemy(points);
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
            if(Mathf.Abs(transform.position.x- actionArea.y)>10)
                goal = new Vector3(Random.Range(transform.position.x, actionArea.y), 0, Random.Range(actionArea.z, actionArea.w));
            else
            {
                goal = new Vector3(actionArea.y, 0, Random.Range(player.transform.position.z - 3, player.transform.position.z + 3));
                beginAttack=true;
            }
        }
        return goal;
    }
}
