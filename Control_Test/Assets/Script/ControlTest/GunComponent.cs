using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunComponent : MonoBehaviour
{
    public int AmmoCount;
    public GameObject loadingBoxMagazine;
    public GameObject[] Barrels;
    public bool IsAmmoEmpty;
    public bool IsConnected = false;
    


    public float NormalshootingRate;
    public float RocketshootingRate;

    // Start is called before the first frame update
    void Start()
    {
        NormalshootingRate = FindObjectOfType<GameManager>().GetComponent<GameManager>().oriNormalShootingRate;
        RocketshootingRate = FindObjectOfType<GameManager>().GetComponent<GameManager>().oriRocketShootingRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (AmmoCount <= 0)
        {
            IsAmmoEmpty = true;
        }
        else
        {
            IsAmmoEmpty = false;
        }
        if(IsConnected == false)
        {
            AmmoCount = 0;
        }
        if(loadingBoxMagazine != null)
        {
            if (loadingBoxMagazine.GetComponent<BoxMagazineComponent_Rocket>() != null)
            {
                for(int i = 0; i < Barrels.Length; i++)
                {
                    Barrels[i].GetComponent<Shoot>().oriTime = RocketshootingRate;
                }
            }
            else if (loadingBoxMagazine.GetComponent<BoxMagazineComponent>() != null)
            {
                for (int i = 0; i < Barrels.Length; i++)
                {
                    Barrels[i].GetComponent<Shoot>().oriTime = NormalshootingRate;
                }
            }
        }
        
    }
}
