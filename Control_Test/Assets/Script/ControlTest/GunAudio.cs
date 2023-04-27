using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAudio : MonoBehaviour
{
    private AudioManager audioManager;
    private GameManager gameManager;
    public int targetCount;
    public int shootCount;
    public int RocketCount;
    public int hitCount;
    public GameObject BulletSound;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.BarrelLevel == 1)
        {
            BulletSound.GetComponent<AudioSource>().volume = 0.3f;
            targetCount = 2;
        }
        else if (gameManager.BarrelLevel == 2)
        {
            BulletSound.GetComponent<AudioSource>().volume = 0.4f;
            targetCount = 3;
        }
        else if (gameManager.BarrelLevel == 3) 
        {
            BulletSound.GetComponent<AudioSource>().volume = 0.5f;
            targetCount = 4;
        }
        if (shootCount >= targetCount)
        {
            Instantiate(BulletSound);
            shootCount -= targetCount;
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
