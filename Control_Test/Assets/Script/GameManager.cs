using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool Isdead;

    public GameObject GameOverUI;
    public GameObject WinUI;
    public GameObject PauseUI;
    public GameObject AirDrop;
    public GameObject WarningUI;
    public GameObject AudioManager;
    public GameObject[] Barrels;
 
    public GameObject Base;

    public SpawnBoxMagazine[] BoxMagazinePrefeb;

    public TextMeshProUGUI BulletNumber;
    public TextMeshProUGUI FortressHP;
    public TextMeshProUGUI CD;
    public TextMeshProUGUI BarrelLV;
    public TextMeshProUGUI AmmoCpacityLV;
    public TextMeshProUGUI OverLoadLastingTimeLV;
    public TextMeshProUGUI OverloadCDLV;
    public TextMeshProUGUI BulletDamageLV;
    //public TextMeshProUGUI OverHeatRestTime;
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
    public float AirDropMinTime;
    public float AirDropMaxTime;

    private int OverloadCDLevel = 1;
    private int BarrelLevel = 1;
    private int BoxMagazineLevel = 1;
    private int DamageLevel = 1;
    private int OverloadTimeLevel = 1;
    private int ShootingRateLevel = 1;

    public float AirDropTimer;
    private float AirDropTime;
    // Start is called before the first frame update

    private void Awake()
    {
        RocketCapacity = 100;
        BulletCapacity = 1000;
        BulletDamage = 3;
        oriRocketDamage = 20;
        AirDropTime = Random.Range(AirDropMinTime, AirDropMaxTime);
    }

    void Start()
    {
        Time.timeScale = 1;
        
        Base = FindObjectOfType<Base>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Barrels[0].GetComponent<Shoot>().IsOverHeat == true|| Barrels[0].GetComponent<Shoot>().isDizzy==true)
        {
            WarningUI.SetActive(true);
        }
        else if(Barrels[0].GetComponent<Shoot>().IsOverHeat == false)
        {
            WarningUI.SetActive(false);
        }
        else if (Barrels[0].GetComponent<Shoot>().isDizzy == false)
        {
            WarningUI.SetActive(false);
        }
        //AirDropTimer += Time.timeScale;
        if (AirDropTimer >= AirDropTime)
        {
            Instantiate(AirDrop, this.transform.position, this.transform.rotation);
            AirDropTime = Random.Range(AirDropMinTime, AirDropMaxTime);
            AirDropTimer =0;
        }
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

        //if(Input.GetKeyDown(KeyCode.Joystick1Button0))
        //{
        //    PauseGame();
        //}
        //if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        //{
        //    GameObject.Find("LeftHand Controller").GetComponent<VibrateManager>().VibrateController(10, 1);
        //}

        if(BulletNumber!=null)
        {
          
            BulletNumber.text = FindObjectOfType<GunComponent>().AmmoCount.ToString();
        }
        if (FortressHP!= null)
        {

            FortressHP.text = FindObjectOfType<Base>().BaseHp.ToString();
        }

        //CD.text = (FindObjectOfType<Shoot>().OverLoadCD - FindObjectOfType<Shoot>().OverLoadCDTimer).ToString();
        if(Barrels[0].GetComponent<Shoot>().canOverLoad == true)
        {
            CD.text = "0.00";
        }
        else CD.text = string.Format("{0:N2}", Barrels[0].GetComponent<Shoot>().OverLoadCD - Barrels[0].GetComponent<Shoot>().OverLoadCDTimer);
        //OverHeatRestTime.text = string.Format("{0:N2}", Barrels[0].GetComponent<Shoot>().OverHeatTime - Barrels[0].GetComponent<Shoot>().OverHeatTimer);
    }
    public void StartGame()
    {
        AudioManager.GetComponent<AudioManager>().UIChoose.Play();
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1;
    }

    
    public void PauseGame()
    {
        AudioManager.GetComponent<AudioManager>().UIChoose.Play();
        Time.timeScale = 0.0001f;
        PauseUI.SetActive(true);
    }
    public void ResumeGame()
    {
        AudioManager.GetComponent<AudioManager>().UIChoose.Play();
        PauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGameScene()
    {
        AudioManager.GetComponent<AudioManager>().UIChoose.Play();
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        AudioManager.GetComponent<AudioManager>().UIChoose.Play();
        Application.Quit();
    }
    public void Cancel()
    {
      
    }

///////////////////////////////////////////////////////////////////


    public void OverloadCDUP()
    {
        //audio clip: button pushed
        AudioManager.GetComponent<AudioManager>().UIChoose.Play();
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
        
        if (BarrelLevel == BarrelMaxLevel)
        {
            AudioManager.GetComponent<AudioManager>().UIChoose.Play();
            return;
        }
            
        else
        {
            if (BarrelLevel == 1)
            {
                if (EXP >= BarrelLevelEXP[0])
                {
                    AudioManager.GetComponent<AudioManager>().LevelUP.Play();
                    Barrels[2].gameObject.SetActive(true);
                    EXP -= BarrelLevelEXP[0];
                    BarrelLevel++;
                }
                else
                {
                    AudioManager.GetComponent<AudioManager>().UIChoose.Play();
                }
            }
            else if (BarrelLevel == 2)
            {
                if (EXP >= BarrelLevelEXP[1])
                {
                    AudioManager.GetComponent<AudioManager>().LevelUP.Play();
                    Barrels[2].gameObject.SetActive(false);
                    Barrels[3].gameObject.SetActive(true);
                    Barrels[4].gameObject.SetActive(true);
                    EXP -= BarrelLevelEXP[1];
                    BarrelLevel++;

                }
                else
                {
                    AudioManager.GetComponent<AudioManager>().UIChoose.Play();
                }
            }
        }
    }

/// //////////////////////////////////////////////////////////////////////
    public void BoxMagazineUP()
    {
        //audio clip: button pushed
        //audio clip: upgrade confirmed
        if (BoxMagazineLevel == BoxMagazineMaxLevel)
        {
            AudioManager.GetComponent<AudioManager>().UIChoose.Play();
            return;
        } 
        if(EXP>= BoxMagazineLevelEXP[BoxMagazineLevel-1])
        {
            AudioManager.GetComponent<AudioManager>().LevelUP.Play();
            EXP -= BoxMagazineLevelEXP[BoxMagazineLevel - 1];
            BoxMagazineLevel++;
            BulletCapacity = BoxMagazine[BoxMagazineLevel - 2];
            RocketCapacity = BoxMagazine_Rocket[BoxMagazineLevel - 2];
        }
        else
        {
            AudioManager.GetComponent<AudioManager>().UIChoose.Play();
        }
    }

    /// //////////////////////////////////////////////////////////////////////

    
    public void DamageUP()
    {
        //audio clip: button pushed
        //audio clip: upgrade success
        if (DamageLevel == DamageMaxLevel) 
        {
            AudioManager.GetComponent<AudioManager>().UIChoose.Play();
            return;
        }
        
        if (EXP >= DamageLevelEXP[DamageLevel - 1])
        {
            AudioManager.GetComponent<AudioManager>().LevelUP.Play();
            EXP -= DamageLevelEXP[DamageLevel - 1];
            DamageLevel++;
            BulletDamage = Damage[DamageLevel - 2];
            oriRocketDamage = RocketDamage[DamageLevel - 2];
        }
        else
        {
            AudioManager.GetComponent<AudioManager>().UIChoose.Play();
        }
    }
   
/// //////////////////////////////////////////////////////////////////////

    public void OverloadTimeUP()
    {
                                                                                                        //audio clip: button pushed
                                                                                                        //audio clip: upgrade success
        if(OverloadTimeLevel == OverloadTimeMaxLevel)
        {
            AudioManager.GetComponent<AudioManager>().UIChoose.Play();
            return;
        }
        if (EXP >= OverloadTimeLevelEXP[OverloadTimeLevel - 1])
        {
            AudioManager.GetComponent<AudioManager>().LevelUP.Play();
            EXP -= OverloadTimeLevelEXP[OverloadTimeLevel - 1];
            OverloadTimeLevel++;
            for (int i = 0; i < Barrels.Length; i++)
            {
                Barrels[i].GetComponent<Shoot>().OverLoadMaxTime = OverloadTime[OverloadTimeLevel - 2];
            }
        }
        else
        {
            AudioManager.GetComponent<AudioManager>().UIChoose.Play();
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
            AudioManager.GetComponent<AudioManager>().LevelUP.Play();
            EXP -= AddHPCost;
            Base.GetComponent<Base>().BaseHp += 20;
        }
        else
        {
            AudioManager.GetComponent<AudioManager>().UIChoose.Play();
        }
    }
}
