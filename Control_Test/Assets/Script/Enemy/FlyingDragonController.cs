using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDragonController : EnemyController
{    
    [Header("Enemy Shoot")]
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    protected Transform shootPoint;

    protected override void Start()
    {
        base.Start();
        InitializeEnemy();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Attack();
    }

    //private Vector3 FindStandPoint()
    //{
    //    availablePoints = GetAvailablePoints(points);
    //    if (availablePoints.Count != 0)
    //    {
    //        int nextPointIndex = Random.Range(0, availablePoints.Count);
    //        availablePoints[nextPointIndex].isTaken = true;
    //        pointsTaken = availablePoints[nextPointIndex];
    //        goal = availablePoints[nextPointIndex].transform.position;
    //    }
    //    else
    //    {
    //        pointsTaken = null;
    //        goal = new Vector3(Random.Range(bornArea.x, bornArea.y), 0, Random.Range(bornArea.z, bornArea.w));
    //    }
    //    return goal;       
    //}
    //protected List<T> GetAvailablePoints<T>(List<T> points) where T : EnemyHidenPoint
    //{
    //    List<T> availablePoints = new List<T>();
    //    foreach (T point in points)
    //    {
    //        if (!point.isTaken)
    //            availablePoints.Add(point);
    //    }
    //    return availablePoints;
    //}

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
