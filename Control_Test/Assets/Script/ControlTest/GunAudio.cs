using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAudio : MonoBehaviour
{
    private AudioManager audioManager;
    public int shootCount;
    public int RocketCount;
    public int hitCount;
    public GameObject BulletSound;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shootCount >= 2)
        {
            Instantiate(BulletSound);
            shootCount -= 2;
        }
        if (RocketCount >= 2)
        {
            AudioSource.PlayClipAtPoint(audioManager.RocketLunch, this.transform.position);
            RocketCount -= 2;
        }
        
    }
    public void OverHeat()
    {
        audioManager.Overheat.Play();
    }
}
