using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurlocController : EnemyController
{
    [Header("Enemy Movement")]
    [Tooltip("the time range for finding next standing point")]
    [SerializeField]
    protected Vector2 waitSeconds = new Vector2(5, 10);
    [SerializeField]
    [Range(0, 10)]
    private float momentum = 3;//the enemy momentum
    [SerializeField]
    private float checkHiddenPointRadius;

    [Header("EnemyAttack")]
    public float EnemyDamage;


    private int numHidden;//the count of enemy to hide

    private List<OtherEnemyHiddenPoint> points;
    private OtherEnemyHiddenPoint nearestPoint;
    private bool isHidden;

    protected override void Start()
    {
        base.Start();

        //find first hide point and set destination
        numHidden = 0;
        //availablePoints = new List<OtherEnemyHiddenPoint>();
        points = EnemySystem.Instance.points;
        InitializeEnemy();
       // RefreshState();
        goal = agent.destination = enemyTarget;
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.LookAt(player);
        if (!isHidden)
            CheckHiddenPoint();
        //Debug.Log(Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)));
        //look at player to attack
        //if (goal!=enemyTarget && Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 8f)
        //{
        //    transform.LookAt(player);
        //    if (!hasFoundNextPoint)
        //    {
        //        Invoke("RefreshState", Random.Range(waitSeconds.x, waitSeconds.y));
        //        hasFoundNextPoint = true;
        //    }
        //}
        if (goal == enemyTarget && Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 20f)
        {
            goal = agent.destination = transform.position;
            beginAttack = true;
        }
        if(beginAttack)
        {
            timer += Time.deltaTime;
            if (timer > attackInterval)
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
    private void CheckHiddenPoint()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkHiddenPointRadius,1<<8);
        if(hitColliders.Length>0)
        {
            foreach (var hit in hitColliders)
            {
                if(hit.transform.position.x<transform.position.x-10 && hit.GetComponent<EnemyHidenPoint>().isTaken==false)
                {
                    Debug.Log("hide");
                    isHidden = true;
                    goal = agent.destination = hit.transform.position;
                    pointsTaken = hit.GetComponent<EnemyHidenPoint>();
                    pointsTaken.isTaken = true;
                    Invoke("ReTargetPlayer", Random.Range(waitSeconds.x, waitSeconds.y));
                    break;
                }
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkHiddenPointRadius);
    }
#endif
    private void ReTargetPlayer()
    {
        isHidden = false;
        pointsTaken.isTaken = false;
        pointsTaken = null;
        goal = agent.destination = enemyTarget;
    }
    private void RefreshState()
    {
        if(pointsTaken)
            pointsTaken.isTaken = false;
        //if murloc has hidden 2 hiddenpoints, it will go straight to player
        if (numHidden >= 2)
        {
            pointsTaken = null;
            goal=agent.destination = enemyTarget;
            return;
        }
        int way = Random.Range(0, 10);
        if(way<momentum)
        {
            pointsTaken = null;
            goal = agent.destination = enemyTarget;
        }
        else
        {
            agent.destination = FindStandPoint();
        }
        hasFoundNextPoint = false;
    }
    private Vector3 FindStandPoint()
    {
        nearestPoint = GetNearestPointsBeforeEnemy(points);
        if (nearestPoint!=null)
        {
            Debug.Log(pointsTaken+"+"+nearestPoint);
            numHidden++;
            nearestPoint.isTaken = true;
            pointsTaken = nearestPoint;
            goal = nearestPoint.transform.position;
        }
        else
        {
            pointsTaken = null;
            goal = enemyTarget;
        }
        return goal;
    }
    private OtherEnemyHiddenPoint GetNearestPointsBeforeEnemy(List<OtherEnemyHiddenPoint> points)
    {
        float nearestDis = 10000;
        OtherEnemyHiddenPoint nearestPoint=null;
        foreach (OtherEnemyHiddenPoint point in points)
        {
            //find the nearest hidden point before player
            if (point.transform.position.x < transform.position.x&&Vector3.Distance(point.transform.position,transform.position)<nearestDis)
            {
                nearestDis = Vector3.Distance(point.transform.position, transform.position);
                nearestPoint = point;
            }
        }
        if(nearestPoint!=null&&nearestPoint.isTaken==false&&nearestPoint!=pointsTaken)
            return nearestPoint;
        return null;
    }
}
