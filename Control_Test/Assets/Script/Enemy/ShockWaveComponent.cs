using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveComponent : MonoBehaviour
{
    [SerializeField]
    private float waveSpeed;
    [SerializeField]
    private float waveSpeed_Acc;
    [SerializeField]
    private float dizzyTime;
    
    private Shoot[] Barrels;

    private Transform player;
    private Rigidbody rigid;

    private GameObject Audiomanager;

    private void Awake()
    {
        Audiomanager = FindObjectOfType<AudioManager>().gameObject;
        player = GameObject.Find("XR Origin").transform;
        rigid = GetComponent<Rigidbody>();
        Barrels = FindObjectsOfType<Shoot>();
        
        

    }
    private void Update()
    {
        var direction = (player.position - transform.position).normalized;
        waveSpeed_Acc += waveSpeed_Acc * Time.deltaTime;
        waveSpeed += waveSpeed_Acc * Time.deltaTime;
        rigid.velocity = direction * Mathf.Pow(waveSpeed,waveSpeed) * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Base>())
        {
            FindObjectOfType<LeftGripComponent>().leftHandController.GetComponent<VibrateManager>().VibrateController(1, 500);
            FindObjectOfType<RightGripComponent>().rightHandController.GetComponent<VibrateManager>().VibrateController(1, 500);
            Audiomanager.GetComponent<AudioManager>().ImpackWave.Play();
            //dizzy player
            for (int i = 0; i < Barrels.Length; i++)
            {
                Barrels[i].dizzyTime = dizzyTime;
            }
            Destroy(gameObject);
        }
    }
}
