using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    [Tooltip("just to show dont need to set values")]
    public List<EnemyHidenPoint> points;
    [SerializeField]
    private List<GameObject> enemyAlive;
    [Tooltip("the boundary of sea, x-> min x, y->max x, z-> min y w-> max y")]
    [SerializeField]
    private Vector4 bornBoundary;
    [Tooltip("the time for fresh new enemies")]
    [SerializeField]
    private int freshTimeInterval;
    [Tooltip("the enemy's number range for refreshing")]
    [SerializeField]
    private Vector2 numRangeToFresh;
    [SerializeField]
    private GameObject enemyPrefab;

    public override void Awake()
    {
        base.Awake();
        //get and sort all hidden point
        EnemyHidenPoint[] _points = FindObjectsOfType<EnemyHidenPoint>();
        points = new List<EnemyHidenPoint>(_points);
        SortPointByX();
        enemyAlive = new List<GameObject>();
        //fresh enemies
        RefreshEnemies();
    }
    private void Update()
    {
            //some cheat key
            //kill all enemy
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (var enemy in enemyAlive)
            {
                enemy.GetComponent<EnemyController>().EnemyDie();
            }
            enemyAlive.Clear();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            RefreshEnemies();
        }
    }

    //give points an order by x from smallest to biggest
    public void SortPointByX()
    {
        points.Sort((x,y)=>(x.transform.position.x.CompareTo(y.transform.position.x)));
    }

    private void RefreshEnemies()
    {
        int numToFresh = (int)Random.Range(numRangeToFresh.x, numRangeToFresh.y);
        for (int i = 0; i < numToFresh; i++)
        {
            //Prevent the infinite loop when the hidden point cannot be found
            if (enemyAlive.Count<points.Count)
                RefreshOneEnemy();
        }
    }
    private void RefreshOneEnemy()
    {
        Vector3 position = new Vector3(Random.Range(bornBoundary.x,bornBoundary.y),0, Random.Range(bornBoundary.z, bornBoundary.w));
        GameObject newEnemy= Instantiate(enemyPrefab,position,Quaternion.Euler( Vector3.right));
        enemyAlive.Add(newEnemy);
    }
}
