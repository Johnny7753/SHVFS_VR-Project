using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public int wavenumber;                  //  to transfer wave index                               by Hardy     in 4/13
    [SerializeField]
    private int partIndex;
    
    [HideInInspector]
    public List<OtherEnemyHiddenPoint> points;
    [HideInInspector]
    public List<FlyingDragonHiddenPoint> fdPoints;
    [HideInInspector]
    public List<GameObject> enemyAlive;

    [Header("Enemy Spawn")]
    [Tooltip("the time for fresh new enemies")]
    [SerializeField]
    private float[] freshTimeInterval;
    [Tooltip("The enemy wave")]
    public Wave[] waves;

    [Header("Enemy Type")]
    [SerializeField]
    private GameObject[] enemyPrefab;

    private bool isRefreshing;//set it true each time enemy is refreshed
    private bool finishRefreshing;

    private float timer;
    [HideInInspector]
    public int enemyNum;

    public override void Awake()
    {
        base.Awake();

        //get and sort all hidden point
        OtherEnemyHiddenPoint[] _points = FindObjectsOfType<OtherEnemyHiddenPoint>();
        FlyingDragonHiddenPoint[] _fpPoints = FindObjectsOfType<FlyingDragonHiddenPoint>();
        points = new List<OtherEnemyHiddenPoint>(_points);
        fdPoints = new List<FlyingDragonHiddenPoint>(_fpPoints);

        SortPointByX();

        enemyAlive = new List<GameObject>();
        waveIndex = 0;
        partIndex = 0;

        //fresh enemies
        timer = 0;
        enemyNum = 0;
        isRefreshing = true;
        finishRefreshing = false;
        RefreshEnemies();
    }
    private void Update()
    {
        //some cheat key
        //kill all enemy
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foreach (var enemy in enemyAlive.ToList())
            {
                enemy.GetComponent<EnemyController>().EnemyDie();
            }
            enemyAlive.Clear();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            enemyAlive[(int)Random.Range(0, enemyAlive.Count)].GetComponent<EnemyController>().EnemyDie();
        }


        //detect if enemies are all killed, enter next wave
        EnterNextWave();

        //change part in wave
        EnterNextPart();

        if (isRefreshing)
        {
            RefreshEnemies();
        }
    }

    //detect if enemies are all killed, enter next wave
    private void EnterNextWave()
    {
        if (enemyAlive.Count == 0 && partIndex >= waves[waveIndex].parts.Length - 1 && !isRefreshing&&finishRefreshing)
        {
            finishRefreshing = false;
            waveIndex++;
            wavenumber = waveIndex;                                          //                    by Hardy   in 4/13
            partIndex = 0;
            Invoke("StartRefreshing", freshTimeInterval[waveIndex]);
        }
        else if (waveIndex >= waves.Length)
        {
            Debug.Log("no more enemies!");
        }
    }
    //enter next part
    private void EnterNextPart()
    {
        switch (waves[waveIndex].parts[partIndex].switchType)
        {
            case SwitchType.EnemyLeft:
                if (enemyAlive.Count <= waves[waveIndex].parts[partIndex].leftEnemy && partIndex < waves[waveIndex].parts.Length - 1 && finishRefreshing&&!isRefreshing)
                {
                    timer = 0;
                    enemyNum = 0;
                    finishRefreshing = false;
                    StartRefreshing();
                    partIndex++;
                    //RefreshEnemies();
                }
                break;
            case SwitchType.Time:
                if (enemyAlive.Count == 0 && partIndex < waves[waveIndex].parts.Length - 1 && !isRefreshing&&finishRefreshing)
                {
                    timer = 0;
                    enemyNum = 0;
                    finishRefreshing = false;
                    Invoke("StartRefreshing", waves[waveIndex].parts[partIndex].switchTime);
                    partIndex++;
                }
                break;
        }
    }

    //give points an order by x from smallest to biggest
    public void SortPointByX()
    {
        points.Sort((x, y) => (-x.transform.position.x.CompareTo(y.transform.position.x)));
    }
    private void StartRefreshing()
    {
        isRefreshing = true;
    }
    private void RefreshEnemies()
    {
        timer += Time.deltaTime;
        if (timer >= enemyPrefab[(int)waves[waveIndex].parts[partIndex].enemyType].GetComponent<EnemyController>().refreshInterval)
        {
            timer = 0;
            RefreshOneEnemy(enemyPrefab[(int)waves[waveIndex].parts[partIndex].enemyType]);
        }
        if (enemyNum == waves[waveIndex].parts[partIndex].enemyNum)
        {
            isRefreshing = false;
            finishRefreshing = true;
        }

    }
    private void RefreshOneEnemy(GameObject enemy)
    {
        enemyNum++;
        //Vector3 position = new Vector3(Random.Range(bornBoundary.x, bornBoundary.y), 0, Random.Range(bornBoundary.z, bornBoundary.w));
        GameObject newEnemy = Instantiate(enemy);
        enemyAlive.Add(newEnemy);
    }
}
