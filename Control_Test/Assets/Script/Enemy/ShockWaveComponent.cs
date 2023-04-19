using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveComponent : MonoBehaviour
{
    [SerializeField]
    private float waveSpeed;
    [SerializeField]
    private float dizzyTime;

    private Transform player;
    private Rigidbody rigid;

    private void Awake()
    {
        player = GameObject.Find("XR Origin").transform;
        rigid = GetComponent<Rigidbody>();

        var direction = (player.position - transform.position).normalized;
        rigid.velocity = direction * waveSpeed*Time.deltaTime;

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Base>())
        {
            //dizzy player
            Debug.Log("dizzy");
        }
    }
}
