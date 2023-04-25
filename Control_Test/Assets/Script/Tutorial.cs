using System.Collections;
using System.Collections.Generic;
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
    public bool tutorialOver;
    public bool NormalBox;
    public bool RocketBox;
    public bool Isconnected;
    public bool IsleftGrab;
    public bool IsrightGrab;
    public bool shoot;
    public int flag=1;

    private void Start()
    {
        NormalBox = FindObjectOfType<BoxMagazineComponent>().AmmoisGrabed;
        RocketBox = FindObjectOfType<BoxMagazineComponent_Rocket>().AmmoisGrabed;
        Isconnected = FindObjectOfType<GunComponent>().IsConnected;
        IsleftGrab = FindObjectOfType<LeftGripComponent>().isLeftGripCaught;
        IsrightGrab = FindObjectOfType<RightGripComponent>().isRightGripCaught;
        shoot = FindObjectOfType<Shoot>().isShooting;
        tutorialOver = FindObjectOfType<EnemySystem>().isWin;
    }
    private void Update()
    {
        if((NormalBox|| RocketBox) &&(flag==1))
        {
            grabAmmo.SetActive(false);
            flag++;
        }
        if(Isconnected&&(flag==2))
        {
            leftgrab.SetActive(true);
            flag++;
        }
        if ((IsleftGrab||IsrightGrab) && (flag == 3))
        {
            leftgrab.SetActive(false);
            leftshoot.SetActive(true);
            flag++;
        }
        if(shoot)
        {
            Overload.SetActive(true);
            EnemySystemAppear.SetActive(true);
        }

        if (Over != null && tutorialOver)
        {
            Over.SetActive(true);
            Time.timeScale = 0.0001f;
        }

    }


    public void skiptutorial()
    {
        SceneManager.LoadScene(1);
    }

    public void begintutorial()
    {
        StartTutorial.SetActive(false);
        grabAmmo.SetActive(true);
        
    }

    public void RestratTutorial()
    {
        SceneManager.LoadScene(0);
    }

}
