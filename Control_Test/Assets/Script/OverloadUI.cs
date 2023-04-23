using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverloadUI : MonoBehaviour
{
    
    public Image load3;
    public Image load4;
    public GameObject Barrel;
    //public Slider load2;
    private float timer;
    private float CD;

    private void Start()
    {
        Barrel = FindObjectOfType<Shoot>().gameObject;

    }
    void Update()
    {
        timer = Barrel.GetComponent<Shoot>().OverLoadTimer;
        CD = Barrel.GetComponent<Shoot>().OverLoadMaxTime;
        timer += Time.deltaTime;
        if (timer < CD)
        {
           
            load3.fillAmount = timer/CD; 
            load4.fillAmount = timer/CD;
           // int a = (int)(timer * 100);
        }

        if(timer>=CD)
        {
            timer = 0;
        }


    }
}
