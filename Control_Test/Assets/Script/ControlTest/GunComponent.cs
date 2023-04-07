using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunComponent : MonoBehaviour
{
    public int AmmoCount;
    public GameObject loadingBoxMagazine;
    public bool IsAmmoEmpty;
    public bool IsConnected = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
