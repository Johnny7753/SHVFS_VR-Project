using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject StartTutorial;
    public GameObject grabAmmo;
    public GameObject LoadAmmo;
    public GameObject leftgrab;
    public GameObject leftshoot;
    public GameObject Overload;
    public GameObject EnemySystemAppear;
    public GameObject Over;
    public GameObject UpgradeSystem;
    public GameObject ReleaseUI;
    public GameObject LooktoLeft;
    public GameObject overheated;
    public bool tutorialOverUpdate;
    public bool NormalBoxUpdate;
    public bool RocketBoxUpdate;
    public bool IsconnectedUpdate;
    public bool IsleftGrabUpdate;
    public bool IsrightGrabUpdate;
    public bool shootUpdate;
    public bool IsoverloadUpdate;
    public bool UpgradeTutorial = false;
    public int flag=1;

    public int Credits;
    public int FormalCredits;
    private BoxMagazineComponent NormalBox;
    private BoxMagazineComponent_Rocket RocketBox;
    private GunComponent Isconnected;
    private LeftGripComponent IsleftGrab;
    private RightGripComponent IsrightGrab;
    private Shoot shoot;
    private EnemySystem tutorialOver;
    private GameManager Upgrade;
    private Shoot Isoverload;
  
    private void Awake()
    {
        
    }
    private void Start()
    {
        tutorialOverUpdate = false;
        Time.timeScale = 0.001f;
        NormalBox = FindObjectOfType<BoxMagazineComponent>();
        RocketBox = FindObjectOfType<BoxMagazineComponent_Rocket>();
        Isconnected = FindObjectOfType<GunComponent>();
        IsleftGrab = FindObjectOfType<LeftGripComponent>();
        IsrightGrab = FindObjectOfType<RightGripComponent>();
        shoot = FindObjectOfType<Shoot>();
        tutorialOver = FindObjectOfType<EnemySystem>();
        Upgrade = FindObjectOfType<GameManager>();
        Isoverload = FindObjectOfType<Shoot>();
       
       // tutorialover = findobjectoftype<enemysystem>().iswin;
        EnemySystemAppear.SetActive(false);
        FormalCredits = 0;
        Credits = 0;
    }
    private void Update()
    {

        NormalBoxUpdate = NormalBox.AmmoisGrabed;
        RocketBoxUpdate = RocketBox.AmmoisGrabed;
        
        IsconnectedUpdate = Isconnected.IsConnected;
        IsleftGrabUpdate = IsleftGrab.isLeftGripCaught;
        IsrightGrabUpdate = IsrightGrab.isRightGripCaught;
        shootUpdate = shoot.isShooting;
        IsoverloadUpdate = Isoverload.IsOverLoad;
        Credits = Upgrade.EXP;
        tutorialOverUpdate = tutorialOver.isTutorialWIn;
        // Debug.Log(FindObjectOfType<GunComponent>().IsConnected);
        if ((NormalBoxUpdate||RocketBoxUpdate)&&(flag ==1))
        {
            grabAmmo.SetActive(false);
            LoadAmmo.SetActive(true);
            flag++;
        }

        if ((IsconnectedUpdate) && (flag == 2))
        {
            LoadAmmo.SetActive(false);
            leftgrab.SetActive(true);
            flag++;
        }
        if ((IsleftGrabUpdate || IsrightGrabUpdate) && (flag == 3))
        {
            leftgrab.SetActive(false);
            LoadAmmo.SetActive(false);
            leftshoot.SetActive(true);
            flag++;
        }

        

        if (shootUpdate)
        {
            if (shoot.OverLoadTimer <= 1.5)
            {
                Overload.SetActive(true);
                ReleaseUI.SetActive(false);
            }
            if(!tutorialOverUpdate)
            {
                if (flag == 4)
                {
                    leftshoot.SetActive(false);

                    EnemySystemAppear.SetActive(true);

                    //Overload.SetActive(true);

                    
                    tutorialOverUpdate = tutorialOver.isTutorialWIn;
                    flag++;
                }
            }
        }

        if((shoot.OverLoadTimer>1.5f)&&(shoot.OverLoadTimer<3))
        {
            Overload.SetActive(false);
            ReleaseUI.SetActive(true);
        }

        if(shoot.IsOverHeat)
        {
            ReleaseUI.SetActive(false);
            overheated.SetActive(true);
        }
        else
        {
            overheated.SetActive(false);
        }


        if((EnemySystemAppear!=null)&&flag==5)
        {
            if ((Over != null && tutorialOverUpdate))
            {
                flag++;
                EnemySystemAppear.SetActive(false);
                UpgradeSystem.SetActive(true);
                LooktoLeft.SetActive(true);
                
                // Time.timeScale = 0.0001f;
            }
            FormalCredits = Credits;
        }

        if(flag>=6)
        {
            if (Credits != FormalCredits)
            {
                UpgradeTutorial = true;
            }
            
        }

        if (Over != null  && UpgradeTutorial && (flag == 6))
        {

            
            EnemySystemAppear.SetActive(true);
            tutorialOverUpdate = tutorialOver.isTutorialWIn;
            flag++;
        }


        if ((Over != null && UpgradeTutorial) && (flag == 7))
        {
            LooktoLeft.SetActive(false);
            tutorialOverUpdate = tutorialOver.isTutorialWIn;
            flag++;
        }

        if(tutorialOverUpdate&&flag==8)
        {
            Over.SetActive(true);
        }

    }


    public void skiptutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void begintutorial()
    {
        Time.timeScale = 1;
        StartTutorial.SetActive(false);
        grabAmmo.SetActive(true);
        
    }

    public void RestratTutorial()
    {
        SceneManager.LoadScene(0);
    }

  

    
}
