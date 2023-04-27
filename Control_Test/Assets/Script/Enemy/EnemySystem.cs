using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    [HideInInspector]
    public int waveUIIndex;//use for UI
    [HideInInspector]
    public int timeUI;//use for UI to count down
    private float _timerUI;
    [SerializeField]
    private int partIndex;
    //[HideInInspector]
    //public int partUIIndex;//use for UI
    public TextMeshProUGUI WaveIndexUI;
    public TextMeshProUGUI WaveTimer;
    public GameObject WaveUI;

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
    public GameObject[] enemyPrefab;

    [Header("Boss")]
    [SerializeField]
    private GameObject bossPrefab;
    [SerializeField]
    private Transform instantiatePos;

    private bool isRefreshing;//set it true each time enemy is refreshed，判断是否在刷新过程中
    private bool finishRefreshing;//保证所有的刷新只刷新一遍

    private float timer;
    [HideInInspector]
    public int enemyNum;
    public bool isWin;
    public bool isTutorialWIn ;

    private bool bossAppear;

    private bool inWaveInterval;//to check if game is in the interval between waves

    private void OnEnable()
    {
        isWin = false;
        isTutorialWIn = false;
        finishRefreshing = false;
        waveIndex = 0;
        enemyNum = 0;
       

        //get and sort all hidden point
        OtherEnemyHiddenPoint[] _points = FindObjectsOfType<OtherEnemyHiddenPoint>();
        FlyingDragonHiddenPoint[] _fpPoints = FindObjectsOfType<FlyingDragonHiddenPoint>();
        points = new List<OtherEnemyHiddenPoint>(_points);
        fdPoints = new List<FlyingDragonHiddenPoint>(_fpPoints);

        SortPointByX();

        enemyAlive = new List<GameObject>();
        waveIndex = 0;
        partIndex = 0;
        //partUIIndex = partIndex + 1;
        waveUIIndex = waveIndex + 1;

        //fresh enemies
        timer = 0;
        enemyNum = 0;
        isRefreshing = true;
        finishRefreshing = false;
        //RefreshEnemies();

    }
    public void Awake()
    {
        //base.Awake();

        //get and sort all hidden point
        OtherEnemyHiddenPoint[] _points = FindObjectsOfType<OtherEnemyHiddenPoint>();
        FlyingDragonHiddenPoint[] _fpPoints = FindObjectsOfType<FlyingDragonHiddenPoint>();
        points = new List<OtherEnemyHiddenPoint>(_points);
        fdPoints = new List<FlyingDragonHiddenPoint>(_fpPoints);

        SortPointByX();

        enemyAlive = new List<GameObject>();
        waveIndex = 0;
        partIndex = 0;
        //partUIIndex = partIndex + 1;
        waveUIIndex = waveIndex + 1;

        //fresh enemies
        timer = 0;
        enemyNum = 0;
        isRefreshing = true;
        finishRefreshing = false;
        //RefreshEnemies();
    }

    public void Start()
    {
        waveIndex = 0;
        enemyNum = 0;
    }
    private void Update()
    {
        //some cheat key
        //kill all enemy
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    foreach (var enemy in enemyAlive.ToList())
        //    {
        //        enemy.GetComponent<EnemyController>().EnemyDie();
        //    }
        //    enemyAlive.Clear();
        //}
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            enemyAlive[(int)Random.Range(0, enemyAlive.Count)].GetComponent<EnemyController>().EnemyDie();
        }
    }
    private void FixedUpdate()
    {
        //detect if enemies are all killed, enter next wave
        EnterNextWave();

        //change part in wave
        EnterNextPart();
        Debug.Log(isRefreshing);
        if (isRefreshing)
        {
            RefreshEnemies();
        }
    }

    //detect if enemies are all killed, enter next wave
    private void EnterNextWave()
    {
        if (enemyAlive.Count == 0 && partIndex >= waves[waveIndex].parts.Length - 1 && !isRefreshing && finishRefreshing)
        {
            if ((waveIndex + 1) < waves.Length)
            {
                timer = 0;
                enemyNum = 0;
                finishRefreshing = false;
                waveIndex++;

                wavenumber = waveIndex;                                          //                    by Hardy   in 4/13
                partIndex = 0;
                inWaveInterval = true;
                //Invoke("StartRefreshing", freshTimeInterval[waveIndex - 1]);
            }
            else
            {
                isTutorialWIn = true;
                finishRefreshing = false;
                InstantiateBoss();
                //Debug.Log("no more enemies!");
            }
        }
        if(inWaveInterval)
        {
            //Debug.Log("inWaveInterval");
            WaveUI.SetActive(true);
            WaveIndexUI.text = "Wave "+ waveUIIndex.ToString() +" is coming";
            WaveTimer.text = timeUI.ToString();
            _timerUI += Time.deltaTime;
            timeUI = (int)freshTimeInterval[waveIndex - 1]-(int)_timerUI;
            if (_timerUI >= freshTimeInterval[waveIndex - 1])
            {
                _timerUI = 0;
                WaveUI.SetActive(false);
                inWaveInterval = false;
                StartRefreshing();
            }
        }
    }
    //enter next part
    private void EnterNextPart()
    {
        if (finishRefreshing && !isRefreshing && partIndex + 1 < waves[waveIndex].parts.Length)
        {
            switch (waves[waveIndex].parts[partIndex].switchType)
            {
                case SwitchType.EnemyLeft:
                    if (enemyAlive.Count <= waves[waveIndex].parts[partIndex].leftEnemy)
                    {
                        timer = 0;
                        enemyNum = 0;
                        finishRefreshing = false;
                        partIndex++;
                        StartRefreshing();
                        //RefreshEnemies();
                    }
                    break;
                case SwitchType.Time:
                    if (enemyAlive.Count == 0)
                    {
                        timer = 0;
                        enemyNum = 0;
                        finishRefreshing = false;
                        partIndex++;
                        Invoke("StartRefreshing", waves[waveIndex].parts[partIndex].switchTime);
                    }
                    break;
            }

        }

    }

    //give points an order by x from smallest to biggest
    public void SortPointByX()
    {
        points.Sort((x, y) => (-x.transform.position.x.CompareTo(y.transform.position.x)));
    }
    private void StartRefreshing()
    {
        waveUIIndex = waveIndex+1;
        //partUIIndex = partIndex+1;
        isRefreshing = true;
    }
    private void RefreshEnemies()
    {
        timer += Time.deltaTime;
        //Debug.Log((int)waves[waveIndex].parts[partIndex].enemyType);
        if (timer >= enemyPrefab[(int)waves[waveIndex].parts[partIndex].enemyType].GetComponent<EnemyController>().refreshInterval)
        {
            timer = 0;
            RefreshOneEnemy(enemyPrefab[(int)waves[waveIndex].parts[partIndex].enemyType]);
        }
        //Debug.Log(enemyNum);
        if (enemyNum == waves[waveIndex].parts[partIndex].enemyNum)
        {
            isRefreshing = false;
            finishRefreshing = true;
        }

    }
    public void RefreshOneEnemy(GameObject enemy)
    {
        enemyNum++;
        //Vector3 position = new Vector3(Random.Range(bornBoundary.x, bornBoundary.y), 0, Random.Range(bornBoundary.z, bornBoundary.w));
        GameObject newEnemy = Instantiate(enemy);
        enemyAlive.Add(newEnemy);
    }

    private void InstantiateBoss()
    {
        if (bossPrefab)
            Instantiate(bossPrefab, instantiatePos.position, instantiatePos.rotation);
        else
        {
            //Debug.Log("no boss");
        }
    }
}
