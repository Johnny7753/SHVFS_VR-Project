using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Attributes")]
    public float HP;

    [Header("Boss Call")]
    [SerializeField]
    private float hornInterval=15f;
    [SerializeField]
    private int murlocNum = 6;
    [SerializeField]
    private int mmNum = 2;

    [Header("Shock Wave")]
    [SerializeField]
    private GameObject wavePrefab;

    [Header("Fireball")]
    [SerializeField]
    private GameObject fireballPrefab;
    [SerializeField]
    private int fireballNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
