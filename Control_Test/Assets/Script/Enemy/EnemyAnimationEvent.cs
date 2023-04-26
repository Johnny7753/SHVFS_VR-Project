using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private GameObject enemy;
    public AudioManager audioManager;
    public AudioSource audioSource;
    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
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
    public void CauseDamage()
    {
        FindObjectOfType<Base>().GetComponent<Base>().BaseHp -= enemy.GetComponent<EnemyController>().enemyDamage;
    }
    public void playBigGrabAttackSound()
    {
        this.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioManager.BigGrabAttack.clip;
        audioSource.Play();
    }
    public void playGhostAttackSound()
    {
        this.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioManager.GhostAttack.clip;
        audioSource.Play();
    }
    public void playMurlocAttackSound()
    {
        this.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioManager.MurlocAttack.clip;
        audioSource.Play();
    }
}
