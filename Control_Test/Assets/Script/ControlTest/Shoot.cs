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
    public float OverLoadMaxTime;
    public float OverLoadCD;
    public float OverHeatTime;

    private float oriTime;
    private float amp = 0.2f;
    private float invokeTime;
    private float OverLoadTimer;
    private float OverLoadCDTimer;
    private float OverHeatTimer;

    private bool canOverLoad = true;
    private bool IsOverLoad = false;
    private bool IsOverHeat = false;
    
    // Start is called before the first frame update
    void Start()
    {
        oriTime = currentTime;
        invokeTime = currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOverLoad == true)
        {
            if (Gun.GetComponent<GunComponent>().IsAmmoEmpty == true)
            {
                IsOverLoad = false;
                canOverLoad = false;
            }
            currentTime = oriTime / 2;
            amp = 1;
            OverLoadTimer += Time.deltaTime;
            
            //overload sound
        }
        else
        {
            OverLoadTimer = 0;
            currentTime = oriTime;
            amp = 0.2f;
        }
        if(OverLoadTimer >= OverLoadMaxTime)
        {
            IsOverHeat = true;
            canOverLoad = false;
            IsOverLoad = false;
        }
        
        if(canOverLoad == false)
        {
            OverLoadCDTimer += Time.deltaTime;
            if(OverLoadCDTimer >= OverLoadCD)
            {
                OverLoadCDTimer = 0;
                canOverLoad = true;
            }
        }
        if (IsOverHeat == true)
        {
            OverHeatTimer += Time.deltaTime;
            if (OverHeatTimer >= OverHeatTime)
            {
                OverHeatTimer = 0;
                IsOverHeat = false;
            }
        }
        if (IsOverHeat == false)
        {
            if (LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == true && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == true)
            {
                if (Gun.GetComponent<GunComponent>().IsAmmoEmpty == false && Gun.GetComponent<GunComponent>().IsConnected == true)
                {
                    if (Input.GetAxisRaw("RightTrigger") >= 0.1 && Input.GetAxisRaw("LeftTrigger") >= 0.1)
                    {
                        if (IsOverLoad == false && canOverLoad == true)
                        {
                            if (Input.GetKey(KeyCode.Joystick1Button1) && Input.GetKey(KeyCode.Joystick1Button3))
                            {
                                IsOverLoad = true;
                            }
                        }
                        else
                        {
                            if (Input.GetKeyUp(KeyCode.Joystick1Button1))
                            {
                                IsOverLoad = false;
                                canOverLoad = false;
                            }
                            else if (Input.GetKeyUp(KeyCode.Joystick1Button3))
                            {
                                IsOverLoad = false;
                                canOverLoad = false;
                            }
                        }
                        invokeTime += Time.deltaTime;
                        if (invokeTime - currentTime > 0)
                        {
                            LeftGrip.GetComponent<LeftGripComponent>().leftHandController.GetComponent<VibrateManager>().VibrateController(amp, 0.1f);
                            RightGrip.GetComponent<RightGripComponent>().rightHandController.GetComponent<VibrateManager>().VibrateController(amp, 0.1f);
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
                            LeftGrip.GetComponent<LeftGripComponent>().leftHandController.GetComponent<VibrateManager>().VibrateController(amp, 0.1f);
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
                            RightGrip.GetComponent<RightGripComponent>().rightHandController.GetComponent<VibrateManager>().VibrateController(amp, 0.1f);
                            GameObject bullet;
                            bullet = Instantiate(Bullet, SpawnPoint.position, SpawnPoint.rotation);
                            Gun.GetComponent<GunComponent>().AmmoCount--;
                            Gun.GetComponent<GunComponent>().loadingBoxMagazine.GetComponent<BoxMagazineComponent>().BulletCapacity--;
                            invokeTime = 0;
                        }
                    }
                }
            }
            if (LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == true && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == false)
            {
                if (IsOverLoad==true)
                {
                    IsOverLoad = false;
                    canOverLoad = false;
                }

                if (Gun.GetComponent<GunComponent>().IsAmmoEmpty == false && Gun.GetComponent<GunComponent>().IsConnected == true)
                {
                    if (Input.GetAxisRaw("LeftTrigger") >= 0.1)
                    {
                        invokeTime += Time.deltaTime;
                        if (invokeTime - currentTime > 0)
                        {
                            LeftGrip.GetComponent<LeftGripComponent>().leftHandController.GetComponent<VibrateManager>().VibrateController(amp, 0.1f);
                            GameObject bullet;
                            bullet = Instantiate(Bullet, SpawnPoint.position, SpawnPoint.rotation);
                            Gun.GetComponent<GunComponent>().AmmoCount--;
                            Gun.GetComponent<GunComponent>().loadingBoxMagazine.GetComponent<BoxMagazineComponent>().BulletCapacity--;
                            invokeTime = 0;
                        }
                    }
                }
            }
            if (LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == false && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == true)
            {
                if (IsOverLoad==true)
                {
                    IsOverLoad = false;
                    canOverLoad = false;
                }
                if (Gun.GetComponent<GunComponent>().IsAmmoEmpty == false && Gun.GetComponent<GunComponent>().IsConnected == true)
                {
                    if (Input.GetAxisRaw("RightTrigger") >= 0.1)
                    {
                        invokeTime += Time.deltaTime;
                        if (invokeTime - currentTime > 0)
                        {
                            RightGrip.GetComponent<RightGripComponent>().rightHandController.GetComponent<VibrateManager>().VibrateController(amp, 0.1f);
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

}
