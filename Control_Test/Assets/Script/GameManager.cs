using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool Isdead;
    public bool IsBossDie;
    public GameObject GameOverUI;
    public GameObject WinUI;
    public GameObject PauseUI;
    public GameObject AirDrop;
    public GameObject WarningUI;
    public GameObject AudioManager;
    public GameObject[] Barrels;
    public GameObject[] Magazines;
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
    public TextMeshProUGUI Score;
    public TextMeshProUGUI BarrelCost;
    public TextMeshProUGUI AmmoCpacityCost;
    public TextMeshProUGUI OverLoadLastingTimeCost;
    public TextMeshProUGUI OverloadCDCost;
    public TextMeshProUGUI BulletDamageCost;
    public TextMeshProUGUI HPCost;
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
    private float WinUITimer;
    // Start is called before the first frame update

    private void Awake()
    {
        EXP = 0;
        RocketCapacity = 100;
        BulletCapacity = 500;
        BulletDamage = 4.5f;
        oriRocketDamage = 20;
        AirDropTime = Random.Range(AirDropMinTime, AirDropMaxTime);
    }

    void Start()
    {
        if(!FindObjectOfType<Tutorial>())
        {
            Time.timeScale = 1;
        }
        BarrelCost.text = BarrelLevelEXP[0].ToString();
        AmmoCpacityCost.text = BoxMagazineLevelEXP[0].ToString();
        OverLoadLastingTimeCost.text = OverloadTimeLevelEXP[0].ToString();
        OverloadCDCost.text = OverloadCDLevelEXP[0].ToString();
        BulletDamageCost.text = DamageLevelEXP[0].ToString();
        HPCost.text = AddHPCost.ToString();
        OverloadCDLV.text = "LV:" + OverloadCDLevel.ToString();
        BarrelLV.text = "LV:" + BarrelLevel.ToString();
        AmmoCpacityLV.text = "LV:" + BoxMagazineLevel.ToString();
        OverLoadLastingTimeLV.text = "LV:" + OverloadTimeLevel.ToString();
        BulletDamageLV.text = "LV" + DamageLevel.ToString();
        Base = FindObjectOfType<Base>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Score:" + EXP.ToString();
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
        if (IsBossDie)
        {
            
            AudioManager.GetComponent<AudioManager>().Win.Play();
            WinUITimer+=Time.deltaTime;
            if(WinUITimer >= 3)
            {
                WinUI.SetActive(true);
                Time.timeScale = 0.0001f;
            }
        }

        if(Isdead&&GameOverUI!=null)
        {
            //Debug.Log("Dead!");
            AudioManager.GetComponent<AudioManager>().Loose.Play();
            GameOverUI.SetActive(true);
            Time.timeScale = 0.0001f;
        }
        if(FindObjectOfType<EnemySystem>())
        {
            if (WinUI != null && FindObjectOfType<EnemySystem>().isWin)
            {
                Debug.Log("Win!");
                WinUI.SetActive(true);
                Time.timeScale = 0.0001f;
            }
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
        
        
        if (OverloadCDLevel == OverloadCDMaxLevel)
        {
            AudioManager.GetComponent<AudioManager>().UIChoose_Fail.Play();
            return;
        }
        
        if (EXP >= OverloadCDLevelEXP[OverloadCDLevel-1])
        {
            AudioManager.GetComponent<AudioManager>().LevelUP.Play();
            EXP -= OverloadCDLevelEXP[OverloadCDLevel - 1];
            OverloadCDLevel++;
            if(OverloadCDLevel < OverloadCDMaxLevel)
            {
                OverloadCDCost.text = OverloadCDLevelEXP[OverloadCDLevel - 1].ToString();
                OverloadCDLV.text = "LV:" + OverloadCDLevel.ToString();
            }
            else
            {
                OverloadCDCost.text = "-";
                OverloadCDLV.text = "Max";
                
            }
            for (int i = 0; i < Barrels.Length; i++)
            {
                Barrels[i].GetComponent<Shoot>().OverLoadCD = OverloadCD[OverloadCDLevel - 2];
            }

        }
        else
        {
            AudioManager.GetComponent<AudioManager>().UIChoose_Fail.Play();
        }
    }
/// //////////////////////////////////////////////////////////////////////

    public void BarrelUP()
    {
        //audio clip: button pushed
        //audio clip: upgrading success
        
        if (BarrelLevel == BarrelMaxLevel)
        {
            AudioManager.GetComponent<AudioManager>().UIChoose_Fail.Play();
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
                    BarrelCost.text = BarrelLevelEXP[1].ToString();
                    BarrelLV.text = "LV:" + BarrelLevel.ToString();
                }
                else
                {
                    AudioManager.GetComponent<AudioManager>().UIChoose_Fail.Play();
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
                    BarrelCost.text = "-";
                    BarrelLV.text = "Max";
                }
                else
                {
                    AudioManager.GetComponent<AudioManager>().UIChoose_Fail.Play();
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
            AudioManager.GetComponent<AudioManager>().UIChoose_Fail.Play();
            return;
        }
        if (EXP >= BoxMagazineLevelEXP[BoxMagazineLevel - 1])
        {
            AudioManager.GetComponent<AudioManager>().LevelUP.Play();
            EXP -= BoxMagazineLevelEXP[BoxMagazineLevel - 1];
            if(Magazines[0].GetComponent<SpawnBoxMagazine>().BoxMagazine!= null)
            {
                Destroy(Magazines[0].GetComponent<SpawnBoxMagazine>().BoxMagazine);
                Magazines[0].GetComponent<SpawnBoxMagazine>().BoxMagazine = null;
                
                Magazines[0].GetComponent<SpawnBoxMagazine>().Spawn();
            }
            if (Magazines[1].GetComponent<SpawnBoxMagazine>().BoxMagazine != null)
            {
                Destroy(Magazines[1].GetComponent<SpawnBoxMagazine>().BoxMagazine);
                Magazines[1].GetComponent<SpawnBoxMagazine>().BoxMagazine = null;
                
                Magazines[1].GetComponent<SpawnBoxMagazine>().Spawn();
            }
            BoxMagazineLevel++;
            if (BoxMagazineLevel < BoxMagazineMaxLevel)
            {
                AmmoCpacityCost.text = BoxMagazineLevelEXP[BoxMagazineLevel - 1].ToString();
                AmmoCpacityLV.text = "LV:" + BoxMagazineLevel.ToString();
            }
            else
            {
                AmmoCpacityCost.text = "-";
                AmmoCpacityLV.text = "Max";
            }
            BulletCapacity = BoxMagazine[BoxMagazineLevel - 2];
            RocketCapacity = BoxMagazine_Rocket[BoxMagazineLevel - 2];
        }
        else
        {
            AudioManager.GetComponent<AudioManager>().UIChoose_Fail.Play();
        }
    }

    /// //////////////////////////////////////////////////////////////////////

    
    public void DamageUP()
    {
        //audio clip: button pushed
        //audio clip: upgrade success
        if (DamageLevel == DamageMaxLevel) 
        {
            AudioManager.GetComponent<AudioManager>().UIChoose_Fail.Play();
            return;
        }
        
        if (EXP >= DamageLevelEXP[DamageLevel - 1])
        {
            AudioManager.GetComponent<AudioManager>().LevelUP.Play();
            EXP -= DamageLevelEXP[DamageLevel - 1];
            DamageLevel++;
            if(DamageLevel < DamageMaxLevel)
            {
                BulletDamageLV.text = "LV" + DamageLevel.ToString();
                BulletDamageCost.text = DamageLevelEXP[DamageLevel - 1].ToString();
            }
            else
            {
                BulletDamageCost.text = "-";
                BulletDamageLV.text = "Max";
            }
            BulletDamage = Damage[DamageLevel - 2];
            oriRocketDamage = RocketDamage[DamageLevel - 2];
        }
        else
        {
            AudioManager.GetComponent<AudioManager>().UIChoose_Fail.Play();
        }
    }
   
/// //////////////////////////////////////////////////////////////////////

    public void OverloadTimeUP()
    {
                                                                                                        //audio clip: button pushed
                                                                                                        //audio clip: upgrade success
        if(OverloadTimeLevel == OverloadTimeMaxLevel)
        {
            AudioManager.GetComponent<AudioManager>().UIChoose_Fail.Play();
            return;
        }
        if (EXP >= OverloadTimeLevelEXP[OverloadTimeLevel - 1])
        {
            AudioManager.GetComponent<AudioManager>().LevelUP.Play();
            EXP -= OverloadTimeLevelEXP[OverloadTimeLevel - 1];
            OverloadTimeLevel++;
            if(OverloadTimeLevel < OverloadTimeMaxLevel)
            {
                OverLoadLastingTimeLV.text = "LV:" + OverloadTimeLevel.ToString();
                OverLoadLastingTimeCost.text = OverloadTimeLevelEXP[OverloadTimeLevel - 1].ToString();
            }
            else
            {
                OverLoadLastingTimeCost.text = "-";
                OverLoadLastingTimeLV.text = "Max";
            }
            for (int i = 0; i < Barrels.Length; i++)
            {
                Barrels[i].GetComponent<Shoot>().OverLoadMaxTime = OverloadTime[OverloadTimeLevel - 2];
            }
        }
        else
        {
            AudioManager.GetComponent<AudioManager>().UIChoose_Fail.Play();
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
            AudioManager.GetComponent<AudioManager>().UIChoose_Fail.Play();
        }
    }
}
