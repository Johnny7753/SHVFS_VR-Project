using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurlocController : EnemyController
{
    private List<OtherEnemyHiddenPoint> points;
    private List<OtherEnemyHiddenPoint> availablePoints;
    public float EnemyDamage;
    
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
                FindObjectOfType<Base>().GetComponent<Base>().BaseHp -= EnemyDamage;
            }
        }
    }
    protected override void InitializeEnemy()
    {
        transform.position = new Vector3(Random.Range(bornArea.x, bornArea.y),0, Random.Range(bornArea.z, bornArea.w));
        //check the ground point
        Ray ray = new Ray(transform.position,-Vector3.up);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 50,1 << 7);
        //Debug.Log(hit.point);
        transform.position += new Vector3(0, hit.point.y + 1,0);
        transform.LookAt(player);

        agent.enabled = true;
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
            if(Mathf.Abs(transform.position.x- actionArea.x)>2)
                goal = new Vector3(Random.Range(actionArea.x, transform.position.x ), 0, Random.Range(actionArea.z, actionArea.w));
            else
            {
                goal = new Vector3(actionArea.x, 0, Random.Range(player.transform.position.z - 3, player.transform.position.z + 3));
                beginAttack=true;
            }
        }
        Debug.Log(goal);
        return goal;
    }
}
