using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Gun;
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
            if(Gun.GetComponent<GunComponent>().IsAmmoEmpty == false && Gun.GetComponent<GunComponent>().IsConnected == true)
            {
                if (Input.GetAxisRaw("RightTrigger") >= 0.1 && Input.GetAxisRaw("LeftTrigger") >= 0.1)
                {
                    invokeTime += Time.deltaTime;
                    if (invokeTime - currentTime > 0)
                    {
                        LeftGrip.GetComponent<LeftGripComponent>().leftHandController.GetComponent<VibrateManager>().VibrateController(0.2f,1);
                        RightGrip.GetComponent<RightGripComponent>().rightHandController.GetComponent<VibrateManager>().VibrateController(0.2f, 1);
                        GameObject bullet;
                        bullet = Instantiate(Bullet, SpawnPoint.position, SpawnPoint.rotation);
                        Gun.GetComponent<GunComponent>().AmmoCount--;
                        Gun.GetComponent<GunComponent>().loadingBoxMagazine.GetComponent<BoxMagazineComponent>().BulletCapacity--;
                        invokeTime = 0;
                    }
                }
                else if (Input.GetAxisRaw("RightTrigger") >= 0.1 && Input.GetAxisRaw("LeftTrigger") == 0)
                {
                    invokeTime += Time.deltaTime;
                    if (invokeTime - currentTime > 0)
                    {
                        RightGrip.GetComponent<RightGripComponent>().rightHandController.GetComponent<VibrateManager>().VibrateController(0.2f, 1);
                        GameObject bullet;
                        bullet = Instantiate(Bullet, SpawnPoint.position, SpawnPoint.rotation);
                        Gun.GetComponent<GunComponent>().AmmoCount--;
                        Gun.GetComponent<GunComponent>().loadingBoxMagazine.GetComponent<BoxMagazineComponent>().BulletCapacity--;
                        invokeTime = 0;
                    }
                }
                else if (Input.GetAxisRaw("RightTrigger") == 0 && Input.GetAxisRaw("LeftTrigger") >= 0.1)
                {
                    invokeTime += Time.deltaTime;
                    if (invokeTime - currentTime > 0)
                    {
                        LeftGrip.GetComponent<LeftGripComponent>().leftHandController.GetComponent<VibrateManager>().VibrateController(0.2f, 1);
                        GameObject bullet;
                        bullet = Instantiate(Bullet, SpawnPoint.position, SpawnPoint.rotation);
                        Gun.GetComponent<GunComponent>().AmmoCount--;
                        Gun.GetComponent<GunComponent>().loadingBoxMagazine.GetComponent<BoxMagazineComponent>().BulletCapacity--;
                        invokeTime = 0;
                    }
                }
            }
        }     
    }
}
