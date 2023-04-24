using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private GameObject enemy;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void ResetAttackAnim()
    {
        anim.SetBool("IsAttack", false);
    }
    public void DestroyEnemy()
    {
        Destroy(enemy);
    }
}
