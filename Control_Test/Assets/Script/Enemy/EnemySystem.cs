using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    public List<EnemyHidenPoint> points;
    public override void Awake()
    {
        base.Awake();
        EnemyHidenPoint[] _points = FindObjectsOfType<EnemyHidenPoint>();
        points = new List<EnemyHidenPoint>(_points);
        SortPointByX();
    }
    //give points an order by x from smallest to biggest
    public void SortPointByX()
    {
        points.Sort((x,y)=>(x.transform.position.x.CompareTo(y.transform.position.x)));
        //foreach (var point in points)
        //{
        //    Debug.Log(point.transform.position);
        //}
    }
}
