using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverloadUI : MonoBehaviour
{
    public Image load1;
    public Image load2;
    //public Slider load2;
    float timer = 0f;
    float CD = 3f;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer < CD)
        {
            load1.fillAmount = timer/CD;
            load2.fillAmount = timer/CD;
           // int a = (int)(timer * 100);
           


        }

    }
}
