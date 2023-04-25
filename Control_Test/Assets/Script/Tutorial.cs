using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    public GameObject StartTutorial;
    public GameObject grabAmmo;
    public GameObject leftgrab;
    public GameObject leftshoot;
    public GameObject Overload;
    public GameObject EnemySystemAppear;
    public GameObject Over;
    public GameObject UpgradeSystem;
    public bool tutorialOverUpdate;
    public bool NormalBoxUpdate;
    public bool RocketBoxUpdate;
    public bool IsconnectedUpdate;
    public bool IsleftGrabUpdate;
    public bool IsrightGrabUpdate;
    public bool shootUpdate;
    
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
    private void Awake()
    {
        
    }
    private void Start()
    {
        Time.timeScale = 0.001f;
        NormalBox = FindObjectOfType<BoxMagazineComponent>();
        RocketBox = FindObjectOfType<BoxMagazineComponent_Rocket>();
        Isconnected = FindObjectOfType<GunComponent>();
        IsleftGrab = FindObjectOfType<LeftGripComponent>();
        IsrightGrab = FindObjectOfType<RightGripComponent>();
        shoot = FindObjectOfType<Shoot>();
        tutorialOver = FindObjectOfType<EnemySystem>();
        Upgrade = FindObjectOfType<GameManager>();
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
        Credits = Upgrade.EXP;
        // Debug.Log(FindObjectOfType<GunComponent>().IsConnected);
        if ((IsconnectedUpdate) && (flag == 1))
        {
            grabAmmo.SetActive(false);
            leftgrab.SetActive(true);
            flag++;
        }
        if ((IsleftGrabUpdate || IsrightGrabUpdate) && (flag == 2))
        {
            leftgrab.SetActive(false);
            leftshoot.SetActive(true);
            flag++;
        }
        
        if (shootUpdate&&!tutorialOverUpdate)
        {
            leftshoot.SetActive(false);
            Overload.SetActive(true);
            EnemySystemAppear.SetActive(true);
            FormalCredits = Credits;
            tutorialOverUpdate = tutorialOver.isTutorialWIn;
            

        }

        if (Over != null && tutorialOverUpdate)
        {
            UpgradeSystem.SetActive(true);
            
            if(Credits!= FormalCredits)
            {
                UpgradeTutorial = true;
            }
            Invoke("waitFor", 0.5f);
            FormalCredits = Credits;
            // Time.timeScale = 0.0001f;
        }
        if(Over!=null&& tutorialOverUpdate && UpgradeTutorial)
        {
            Over.SetActive(true);
        }

        void waitFor()
        {

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
