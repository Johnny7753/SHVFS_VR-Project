using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public Part[] parts;
}
    public class EnemySystem : Singleton<EnemySystem>
{
    [Header("Information")]
    [SerializeField]
    private int waveIndex;
    [SerializeField]
    private int partIndex;
    [Tooltip("just to show dont need to set values")]
    public List<EnemyHidenPoint> points;
    [HideInInspector]
    public List<GameObject> enemyAlive;

    [Header("Enemy Spawn")]
    [Tooltip("the boundary of sea, x-> min x, y->max x, z-> min y w-> max y")]
    [SerializeField]
    private Vector4 bornBoundary;
    [Tooltip("the time for fresh new enemies")]
    [SerializeField]
    private float[] freshTimeInterval;
    [Tooltip("The enemy wave")]
    public Wave[] waves;

    [Header("Enemy Type")]
    [SerializeField]
    private GameObject[] enemyPrefab;

    private bool isRefreshing;//set it true each time enemy is refreshed

    public override void Awake()
    {
        base.Awake();
        //get and sort all hidden point
        EnemyHidenPoint[] _points = FindObjectsOfType<EnemyHidenPoint>();
        points = new List<EnemyHidenPoint>(_points);
        SortPointByX();

        enemyAlive = new List<GameObject>();
        waveIndex = 0;
        partIndex = 0;
        //fresh enemies
        isRefreshing = true;
        RefreshEnemies();
    }
    private void Update()
    {
        //some cheat key
        //kill all enemy
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    foreach (var enemy in enemyAlive)
        //    {
        //        enemy.GetComponent<EnemyController>().EnemyDie();
        //    }
        //    enemyAlive.Clear();
        //}
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            enemyAlive[(int)Random.Range(0,enemyAlive.Count)].GetComponent<EnemyController>().EnemyDie();
            //RefreshEnemies();
        }


        //detect if enemies are all killed, enter next wave
        if (enemyAlive.Count == 0 &&partIndex>= waves[waveIndex].parts.Length-1&&!isRefreshing)
        {
            isRefreshing = true;
            waveIndex++;
            partIndex = 0;
            Invoke("RefreshEnemies", freshTimeInterval[waveIndex]);
        }
        else if (waveIndex >= waves.Length)
        {
            Debug.Log("no more enemies!");
        }

        //change part in wave
        switch(waves[waveIndex].parts[partIndex].switchType)
        {
            case SwitchType.EnemyLeft:
                if(enemyAlive.Count<= waves[waveIndex].parts[partIndex].leftEnemy&&partIndex< waves[waveIndex].parts.Length-1&&!isRefreshing)
                {
                    isRefreshing = true;
                    partIndex++;
                    RefreshEnemies();
                }
                break;
            case SwitchType.Time:
                if (enemyAlive.Count == 0 && partIndex < waves[waveIndex].parts.Length-1&&!isRefreshing)
                {
                    isRefreshing = true;
                    Invoke("RefreshEnemies", waves[waveIndex].parts[partIndex].switchTime);
                    partIndex++;
                }
                break;
        }
    }

    //give points an order by x from smallest to biggest
    public void SortPointByX()
    {
        points.Sort((x,y)=>(x.transform.position.x.CompareTo(y.transform.position.x)));
    }
    private void RefreshEnemies()
    {

        for(int i=0;i<waves[waveIndex].parts[partIndex].enemyNum; i++)
        {
            RefreshOneEnemy(enemyPrefab[(int)waves[waveIndex].parts[partIndex].enemyType]);
        }
        isRefreshing = false;
    }
    private void RefreshOneEnemy(GameObject enemy)
    {
        Vector3 position = new Vector3(Random.Range(bornBoundary.x,bornBoundary.y),0, Random.Range(bornBoundary.z, bornBoundary.w));
        GameObject newEnemy= Instantiate(enemy, position,Quaternion.Euler( Vector3.right));
        enemyAlive.Add(newEnemy);
    }
}
