using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAudio : MonoBehaviour
{
    private GameObject Audiomanager;
    public int shootCount;
    public int RocketCount;
    public int hitCount;
    // Start is called before the first frame update
    void Start()
    {
        Audiomanager = FindObjectOfType<AudioManager>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (shootCount >= 2)
        {
            AudioSource.PlayClipAtPoint(Audiomanager.GetComponent<AudioManager>().Shoot, this.transform.position);
            shootCount -= 2;
        }
        if (RocketCount >= 2)
        {
            AudioSource.PlayClipAtPoint(Audiomanager.GetComponent<AudioManager>().RocketLunch, this.transform.position);
            RocketCount -= 2;
        }
        
    }
    public void OverHeat()
    {
        Audiomanager.GetComponent<AudioManager>().Overheat.Play();
    }
}
