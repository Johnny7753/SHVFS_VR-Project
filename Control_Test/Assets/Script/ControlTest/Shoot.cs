using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Rocket;
    public GameObject Gun;
    public GameObject GunAudio;

    public Transform SpawnPoint;
    public Transform LeftGrip;
    public Transform RightGrip;

    public Animator animator;

    public float currentTime;
    public float OverLoadMaxTime;
    public float OverLoadCD;
    public float OverHeatTime;
    public float oriTime;
    public float dizzyTime;
    public float a;
    private float dizzyTimer;

    public bool isDizzy = false;

    private float amp = 0.2f;
    private float invokeTime;
    public float OverLoadTimer;
    public float OverLoadCDTimer;
    public float OverHeatTimer;

    public bool canOverLoad = true;
    private bool IsOverLoad = false;
    public bool IsOverHeat = false;

    private int NormalShoot = Animator.StringToHash("NormalShoot");
    private int Shooting = Animator.StringToHash("Shooting");


    void Start()
    {
        
        animator = GetComponent<Animator>();
        oriTime = currentTime;
        invokeTime = currentTime;
    }

    void Update()
    {
        if (dizzyTime != 0)
        {
            isDizzy = true;
            dizzyTimer += Time.deltaTime;
        }
        if (dizzyTimer >= dizzyTime)
        {
            isDizzy = false;
            dizzyTimer = 0;
            dizzyTime  = 0;
        }
        if (IsOverLoad == true)
        {
            if (Gun.GetComponent<GunComponent>().IsAmmoEmpty == true)
            {
                IsOverLoad = false;
                canOverLoad = false;
            }
            if(currentTime > oriTime / 2)
            {
                currentTime  -= Time.deltaTime * a;
            }
            if (currentTime < oriTime / 2)
            {
                currentTime = oriTime / 2;
            }
            amp = 0.6f;
            OverLoadTimer += Time.deltaTime;
            //overload sound
        }
        else
        {
            OverLoadTimer -= Time.deltaTime;
            currentTime = oriTime;
            amp = 0.2f;
            if (OverLoadTimer > 0)
            {
                OverLoadTimer -= Time.deltaTime;
            }
            else if (OverLoadTimer < 0)
            {
                OverLoadTimer = 0;
            }
        }
        if(OverLoadTimer >= OverLoadMaxTime)
        {
            LeftGrip.GetComponent<LeftGripComponent>().leftHandController.GetComponent<VibrateManager>().VibrateController(1, 200);
            RightGrip.GetComponent<RightGripComponent>().rightHandController.GetComponent<VibrateManager>().VibrateController(1, 200);
            GunAudio.GetComponent<GunAudio>().OverHeat();
            IsOverHeat = true;
            canOverLoad = false;
            IsOverLoad = false;
        }
        
        if(canOverLoad == false && IsOverHeat == false)
        {
            OverLoadCDTimer += Time.deltaTime;    // OverloadCD- OverLoadCDTimer = CD UI
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
        if (isDizzy == false)
        {
            if (IsOverHeat == false)
            {
                if (Gun.GetComponent<GunComponent>().IsAmmoEmpty == true)
                {
                    if (LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == true && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == true)
                    {
                        if (Input.GetAxisRaw("RightTrigger") >= 0.1 && Input.GetAxisRaw("LeftTrigger") >= 0.1)
                        {
                            LeftGrip.GetComponent<LeftGripComponent>().leftHandController.GetComponent<VibrateManager>().VibrateController(1, 100);
                            RightGrip.GetComponent<RightGripComponent>().rightHandController.GetComponent<VibrateManager>().VibrateController(1, 100);
                        }
                        if (Input.GetAxisRaw("RightTrigger") >= 0.1 && Input.GetAxisRaw("LeftTrigger") == 0)
                        {
                            RightGrip.GetComponent<RightGripComponent>().rightHandController.GetComponent<VibrateManager>().VibrateController(1, 100);
                        }
                        if (Input.GetAxisRaw("RightTrigger") == 0 && Input.GetAxisRaw("LeftTrigger") >= 0.1)
                        {
                            LeftGrip.GetComponent<LeftGripComponent>().leftHandController.GetComponent<VibrateManager>().VibrateController(1, 100);
                        }
                    }
                    
                    if(LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == false && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == true)
                    {
                        if (Input.GetAxisRaw("RightTrigger") >= 0.1 && Input.GetAxisRaw("LeftTrigger") == 0)
                        {
                            RightGrip.GetComponent<RightGripComponent>().rightHandController.GetComponent<VibrateManager>().VibrateController(1, 100);
                        }
                    }
                    
                    if(LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == true && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == false)
                    {
                        if (Input.GetAxisRaw("RightTrigger") == 0 && Input.GetAxisRaw("LeftTrigger") >= 0.1)
                        {
                            LeftGrip.GetComponent<LeftGripComponent>().leftHandController.GetComponent<VibrateManager>().VibrateController(1, 100);
                        }
                    }
                    
                }
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
                                    //canOverLoad = false;
                                }
                                else if (Input.GetKeyUp(KeyCode.Joystick1Button3))
                                {
                                    IsOverLoad = false;
                                    //canOverLoad = false;
                                }
                            }
                            invokeTime += Time.deltaTime;
                            if (invokeTime - currentTime > 0)
                            {
                                LeftGrip.GetComponent<LeftGripComponent>().leftHandController.GetComponent<VibrateManager>().VibrateController(amp, 0.1f);
                                RightGrip.GetComponent<RightGripComponent>().rightHandController.GetComponent<VibrateManager>().VibrateController(amp, 0.1f);
                                shooting();
                            }
                        }
                        else if (Input.GetAxisRaw("RightTrigger") == 0 && Input.GetAxisRaw("LeftTrigger") >= 0.1)
                        {
                            invokeTime += Time.deltaTime;
                            if (invokeTime - currentTime > 0)
                            {
                                LeftGrip.GetComponent<LeftGripComponent>().leftHandController.GetComponent<VibrateManager>().VibrateController(amp, 0.1f);
                                shooting();
                            }
                        }
                        else if (Input.GetAxisRaw("RightTrigger") >= 0.1 && Input.GetAxisRaw("LeftTrigger") == 0)
                        {
                            invokeTime += Time.deltaTime;
                            if (invokeTime - currentTime > 0)
                            {
                                RightGrip.GetComponent<RightGripComponent>().rightHandController.GetComponent<VibrateManager>().VibrateController(amp, 0.1f);
                                shooting();
                            }
                        }
                        else if (Input.GetAxisRaw("RightTrigger") == 0 && Input.GetAxisRaw("LeftTrigger") == 0)
                        {

                        }
                    }
                }
                if (LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == true && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == false)
                {
                    if (IsOverLoad == true)
                    {
                        IsOverLoad = false;
                        //canOverLoad = false;
                    }

                    if (Gun.GetComponent<GunComponent>().IsAmmoEmpty == false && Gun.GetComponent<GunComponent>().IsConnected == true)
                    {
                        if (Input.GetAxisRaw("LeftTrigger") >= 0.1)
                        {
                            invokeTime += Time.deltaTime;
                            if (invokeTime - currentTime > 0)
                            {
                                animator.SetBool(NormalShoot, true);
                                LeftGrip.GetComponent<LeftGripComponent>().leftHandController.GetComponent<VibrateManager>().VibrateController(amp, 0.1f);
                                shooting();
                            }
                        }
                        else if (Input.GetAxisRaw("LeftTrigger") == 0)
                        {
                            animator.SetBool(NormalShoot, false);
                        }
                    }
                }
                if (LeftGrip.GetComponent<LeftGripComponent>().isLeftGripCaught == false && RightGrip.GetComponent<RightGripComponent>().isRightGripCaught == true)
                {
                    if (IsOverLoad == true)
                    {
                        IsOverLoad = false;
                        //canOverLoad = false;
                    }
                    if (Gun.GetComponent<GunComponent>().IsAmmoEmpty == false && Gun.GetComponent<GunComponent>().IsConnected == true)
                    {
                        if (Input.GetAxisRaw("RightTrigger") >= 0.1)
                        {
                            invokeTime += Time.deltaTime;
                            if (invokeTime - currentTime > 0)
                            {
                                RightGrip.GetComponent<RightGripComponent>().rightHandController.GetComponent<VibrateManager>().VibrateController(amp, 0.1f);
                                shooting();
                            }
                        }
                        else if (Input.GetAxisRaw("RightTrigger") == 0)
                        {
                            animator.SetBool(NormalShoot, false);
                        }
                    }
                }
            }
        }
        
    }
    public void shooting()
    {
        if (Gun.GetComponent<GunComponent>().loadingBoxMagazine.GetComponent<BoxMagazineComponent>() != null)
        {
            a = 0.03f;
            animator.SetTrigger(Shooting);
            GameObject bullet;
            bullet = Instantiate(Bullet, SpawnPoint.position, SpawnPoint.rotation);
            Gun.GetComponent<GunComponent>().AmmoCount--;
            Gun.GetComponent<GunComponent>().loadingBoxMagazine.GetComponent<BoxMagazineComponent>().BulletCapacity--;
            GunAudio.GetComponent<GunAudio>().shootCount++;
            invokeTime = 0;
        }
        else if(Gun.GetComponent<GunComponent>().loadingBoxMagazine.GetComponent<BoxMagazineComponent_Rocket>() != null)
        {
            a = 0.2f;
            animator.SetTrigger(Shooting);
            GameObject rocket;
            rocket = Instantiate(Rocket, SpawnPoint.position, SpawnPoint.rotation);
            Gun.GetComponent<GunComponent>().AmmoCount--;
            Gun.GetComponent<GunComponent>().loadingBoxMagazine.GetComponent<BoxMagazineComponent_Rocket>().RocketCapacity--;
            invokeTime = 0;
            GunAudio.GetComponent<GunAudio>().RocketCount++;
        }
    }
}
