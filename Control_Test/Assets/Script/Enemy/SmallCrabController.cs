using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCrabController : MurlocController
{
    private MeshRenderer mat;
    protected override void Start()
    {
        base.Start();

        mat = GetComponent<MeshRenderer>();
    }
    protected override void FixedUpdate()
    {
        //look at player to attack
        if (!beginAttack&& Vector3.Distance(transform.position, new Vector3(goal.x, transform.position.y, goal.z)) < 1.5f)
        {
            transform.LookAt(player);
            if (!hasFoundNextPoint)
            {
                Invoke("RefreshStandPoint", Random.Range(waitSeconds.x, waitSeconds.y));
                hasFoundNextPoint = true;
            }
        }
        if (beginAttack)
        {
            Debug.Log(mat.material.color);
            mat.material.color = Color.red;
            transform.LookAt(player);
            timer += Time.deltaTime;
            if (timer > attackInterval)
            {
                timer = 0;
                Debug.Log("explode");
                EnemyDie();
            }
        }
    }
}
