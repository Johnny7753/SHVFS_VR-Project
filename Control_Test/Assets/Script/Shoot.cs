using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject BoxMagazine;
    public Transform SpawnPoint;
    public Transform LeftGrip;
    public Transform RightGrip;
    public float currentTime;
    private float invokeTime;
    
    // Start is called before the first frame update
    void Start()
    {
        invokeTime = currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == true || RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == true)
        {
            if(BoxMagazine.GetComponent<BoxMagazineComponent>().isEmpty == false)
            {
                if (Input.GetAxisRaw("RightTrigger") == 1 || Input.GetAxisRaw("LeftTrigger") == 1)
                {
                    invokeTime += Time.deltaTime;
                    if (invokeTime - currentTime > 0)
                    {
                        GameObject bullet;
                        bullet = Instantiate(Bullet, SpawnPoint.position, SpawnPoint.rotation);
                        BoxMagazine.GetComponent<BoxMagazineComponent>().AmmoCount--;
                        invokeTime = 0;
                    }
                }
            }
        }     
    }
}
