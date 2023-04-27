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

    private int Credits;
    private int FormalCredits;
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
       
        //tutorialover = findobjectoftype<enemysystem>().iswin;
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
        // Debug.Log(FindObjectOfType<GunComponent>().IsConnected);
        if((NormalBoxUpdate||RocketBoxUpdate)&&(flag ==1))
        {
            grabAmmo.SetActive(false);
            LoadAmmo.SetActive(true);
        }

        if ((IsconnectedUpdate) && (flag == 2))
        {
            LoadAmmo.SetActive(false);
            leftgrab.SetActive(true);
            flag++;
        }
        if ((IsleftGrabUpdate || IsrightGrabUpdate) && (flag == 3))
        {
            LoadAmmo.SetActive(false);
            leftshoot.SetActive(true);
            flag++;
        }
        
        if (shootUpdate&&!tutorialOverUpdate)
        {
            leftshoot.SetActive(false);
            
            EnemySystemAppear.SetActive(true);

            Overload.SetActive(true);

            FormalCredits = Credits;
            tutorialOverUpdate = tutorialOver.isTutorialWIn;
        }

        if(IsoverloadUpdate)
        {
            Invoke("Release", 1.5f);
        }

        if (Over != null && tutorialOverUpdate)
        {
            ReleaseUI.SetActive(false);
            EnemySystemAppear.SetActive(false);
            UpgradeSystem.SetActive(true);
            LooktoLeft.SetActive(true);
            
            if(Credits!= FormalCredits)
            {
                UpgradeTutorial = true;
            }
            
            FormalCredits = Credits;
            // Time.timeScale = 0.0001f;
        }
        if((Over!=null&& tutorialOverUpdate && UpgradeTutorial) && (flag == 4))
        {

            LooktoLeft.SetActive(false);
            EnemySystemAppear.SetActive(true);
            tutorialOverUpdate = tutorialOver.isTutorialWIn;
            flag++;
        }
        if((Over != null && tutorialOverUpdate && UpgradeTutorial) && (flag == 5))
        {
            tutorialOverUpdate = tutorialOver.isTutorialWIn;
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

    void Release()
    {
        Overload.SetActive(false);
        ReleaseUI.SetActive(true);
    }

    
}
