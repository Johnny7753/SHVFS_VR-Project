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
    public GameObject BoxMagazinePrefab;
    public GameObject BulletPrefeb;
    public GameObject[] Barrels;
    public TextMeshProUGUI BulletNumber;
    public TextMeshProUGUI FortressHP;
    public int EXP;
    public int[] OverloadCDLevelEXP;
    public int[] BarrelLevelEXP;
    public int[] BoxMagazineLevelEXP;
    public int[] DamageLevelEXP;
    public int[] OverloadTimeLevelEXP;
    public int[] ShootingRateLevelEXP;

    public float[] OverloadCD;
    public int[] BoxMagazine;
    public float[] Damage;
    public float[] OverloadTime;
    public float[] ShootingRate;

    public int OverloadCDMaxLevel;
    private int BarrelMaxLevel;
    private int BoxMagazineMaxLevel;
    private int DamageMaxLevel;
    public int OverloadTimeMaxLevel;
    private int ShootingRateMaxLevel;

    private int OverloadCDLevel=1;
    private int BarrelLevel=1;
    private int BoxMagazineLevel = 1;
    private int DamageLevel = 1;
    private int OverloadTimeLevel = 1;
    private int ShootingRateLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
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

    public void OverloadCDUP()
    {
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
    public void BarrelUP()
    {
        if(BarrelLevel == BarrelMaxLevel) return;
        if(BarrelLevel == 1)
        {
            if (EXP >= BarrelLevelEXP[0])
            {
                Barrels[2].gameObject.SetActive(true);
                EXP -= BarrelLevelEXP[0];
                BarrelLevel++;
            }
        }
        else if(BarrelLevel == 2)
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
    public void BoxMagazineUP()
    {
        if (BoxMagazineLevel == BoxMagazineMaxLevel) return;
        if(EXP>= BoxMagazineLevelEXP[BoxMagazineLevel-1])
        {
            EXP -= BoxMagazineLevelEXP[BoxMagazineLevel-1];
            BoxMagazineLevel++;
            BoxMagazinePrefab.GetComponent<BoxMagazineComponent>().BulletCapacity = BoxMagazine[BoxMagazineLevel - 2];
        }
    }
    public void DamageUP()
    {
        if(DamageLevel == DamageMaxLevel) return;
        if (EXP >= DamageLevelEXP[DamageLevel - 1])
        {
            EXP -= DamageLevelEXP[DamageLevel - 1];
            DamageLevel++;
            BulletPrefeb.GetComponent<Bullet>().bulletDamage = Damage[DamageLevel - 2];
        }
    }
    public void OverloadTimeUP()
    {
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
    public void ShootingRateUP()
    {
        if (ShootingRateLevel == ShootingRateMaxLevel) return;
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
}
