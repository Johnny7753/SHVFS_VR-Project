using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool Isdead;

    public GameObject GameOverUI;
    public GameObject WinUI;
    public GameObject PauseUI;
    public GameObject[] Barrels;
 
    public GameObject Base;

    public SpawnBoxMagazine[] BoxMagazinePrefeb;

    public TextMeshProUGUI BulletNumber;
    public TextMeshProUGUI FortressHP;
    public int EXP;
    public int AddHPCost;
    public int[] OverloadCDLevelEXP;
    public int[] BarrelLevelEXP;
    public int[] BoxMagazineLevelEXP;
    public int[] DamageLevelEXP;
    public int[] OverloadTimeLevelEXP;
    public int[] ShootingRateLevelEXP;

    public float[] OverloadCD;
    public int[] BoxMagazine;
    public int[] BoxMagazine_Rocket;
    public float[] Damage;
    public float[] RocketDamage;
    public float[] OverloadTime;
    public float[] ShootingRate;

    public int OverloadCDMaxLevel;
    public int BarrelMaxLevel;
    public int BoxMagazineMaxLevel;
    public int DamageMaxLevel;
    public int OverloadTimeMaxLevel;
    public int ShootingRateMaxLevel;
    public int BulletCapacity;
    public int RocketCapacity;

    public float BulletDamage;
    public float oriRocketDamage;

    private int OverloadCDLevel = 1;
    private int BarrelLevel = 1;
    private int BoxMagazineLevel = 1;
    private int DamageLevel = 1;
    private int OverloadTimeLevel = 1;
    private int ShootingRateLevel = 1;
    // Start is called before the first frame update

    private void Awake()
    {
        RocketCapacity = 100;
        BulletCapacity = 1000;
        BulletDamage = 3;
        oriRocketDamage = 10;
    }

    void Start()
    {
        Time.timeScale = 1;
        Base = FindObjectOfType<Base>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if(Isdead&&GameOverUI!=null)
        {
            Debug.Log("Dead!");
            GameOverUI.SetActive(true);
            Time.timeScale = 0.0001f;
        }


        if(WinUI !=null && FindObjectOfType<EnemySystem>().isWin)
        {
            Debug.Log("Win!");
            WinUI.SetActive(true);
            Time.timeScale = 0.0001f;
        }

        if(Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            GameObject.Find("LeftHand Controller").GetComponent<VibrateManager>().VibrateController(10, 500);
        }

        if(BulletNumber!=null)
        {
          
            BulletNumber.text = FindObjectOfType<GunComponent>().AmmoCount.ToString();
        }
        if (FortressHP!= null)
        {

            FortressHP.text = FindObjectOfType<Base>().BaseHp.ToString();
        }

    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1;
    }

    
    public void PauseGame()
    {
        Time.timeScale = 0.0001f;
        PauseUI.SetActive(true);
    }
    public void ResumeGame()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGameScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void Cancel()
    {
      
    }

///////////////////////////////////////////////////////////////////


    public void OverloadCDUP()
    {
                                                                                                 //audio clip: button pushed
        if (OverloadCDLevel == OverloadCDMaxLevel) return; //Max Level
        
        if (EXP >= OverloadCDLevelEXP[OverloadCDLevel-1])
        {
            EXP -= OverloadCDLevelEXP[OverloadCDLevel - 1];
            OverloadCDLevel++;
            for (int i = 0; i < Barrels.Length; i++)
            {
                Barrels[i].GetComponent<Shoot>().OverLoadCD = OverloadCD[OverloadCDLevel - 2];
            }

        }
    }
/// //////////////////////////////////////////////////////////////////////

    public void BarrelUP()
    {                                  
                                                                                                       //audio clip: button pushed
                                                                                                       //audio clip: upgrading success
        if(BarrelLevel == BarrelMaxLevel) return;
        else
        {
            if (BarrelLevel == 1)
            {
                if (EXP >= BarrelLevelEXP[0])
                {
                    Barrels[2].gameObject.SetActive(true);
                    EXP -= BarrelLevelEXP[0];
                    BarrelLevel++;
                }
            }
            else if (BarrelLevel == 2)
            {
                if (EXP >= BarrelLevelEXP[1])
                {
                    Barrels[2].gameObject.SetActive(false);
                    Barrels[3].gameObject.SetActive(true);
                    Barrels[4].gameObject.SetActive(true);
                    EXP -= BarrelLevelEXP[1];
                    BarrelLevel++;

                }
            }
        }
    }

/// //////////////////////////////////////////////////////////////////////
    public void BoxMagazineUP()
    {
                                                                                                      //audio clip: button pushed
                                                                                                      //audio clip: upgrade confirmed
        if (BoxMagazineLevel == BoxMagazineMaxLevel) return;
        if(EXP>= BoxMagazineLevelEXP[BoxMagazineLevel-1])
        {
            EXP -= BoxMagazineLevelEXP[BoxMagazineLevel - 1];
            BoxMagazineLevel++;
            BulletCapacity = BoxMagazine[BoxMagazineLevel - 2];
            RocketCapacity = BoxMagazine_Rocket[BoxMagazineLevel - 2];
        }
    }

    /// //////////////////////////////////////////////////////////////////////

    
    public void DamageUP()
    {
                                                                                                       //audio clip: button pushed
                                                                                                       //audio clip: upgrade success
        if(DamageLevel == DamageMaxLevel) return;
        if (EXP >= DamageLevelEXP[DamageLevel - 1])
        {
            EXP -= DamageLevelEXP[DamageLevel - 1];
            DamageLevel++;
            BulletDamage = Damage[DamageLevel - 2];
            oriRocketDamage = RocketDamage[DamageLevel - 2];
        }
    }
   
/// //////////////////////////////////////////////////////////////////////

    public void OverloadTimeUP()
    {
                                                                                                        //audio clip: button pushed
                                                                                                        //audio clip: upgrade success
        if(OverloadTimeLevel == OverloadTimeMaxLevel) return;
        if (EXP >= OverloadTimeLevelEXP[OverloadTimeLevel - 1])
        {
            EXP -= OverloadTimeLevelEXP[OverloadTimeLevel - 1];
            OverloadTimeLevel++;
            for (int i = 0; i < Barrels.Length; i++)
            {
                Barrels[i].GetComponent<Shoot>().OverLoadMaxTime = OverloadTime[OverloadTimeLevel - 2];
            }
        }
    }
/// //////////////////////////////////////////////////////////////////////

    public void ShootingRateUP()
    {
                                                                                                        //audio clip: button pushed
        if (ShootingRateLevel == ShootingRateMaxLevel) return;                                          //audio clip: upgrade success;
        if (EXP >= ShootingRateLevelEXP[ShootingRateLevel - 1])
        {
            EXP -= ShootingRateLevelEXP[ShootingRateLevel - 1];
            ShootingRateLevel++;
            for (int i = 0; i < Barrels.Length; i++)
            {
                Barrels[i].GetComponent<Shoot>().oriTime = ShootingRate[ShootingRateLevel - 2];
                Barrels[i].GetComponent<Shoot>().currentTime = ShootingRate[ShootingRateLevel - 2];
            }
        }
    }
    public void AddHp()
    {
        if(EXP >= AddHPCost)
        {
            EXP -= AddHPCost;
            Base.GetComponent<Base>().BaseHp += 20;
        }
    }
}
