using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoxMagazine : MonoBehaviour
{
    public GameObject BoxMagazine;
    public GameObject BoxMagazinePrefeb;
    public GameObject GameManager;
    private bool needToSpawn = false;
    public bool needStartSpawn;
    public bool needLoopSpawn;
    public float SpawnTime;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (needStartSpawn)
        {
            Spawn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (needLoopSpawn)
        {
            if (needToSpawn)
            {
                timer += Time.deltaTime;
                if (timer > SpawnTime)
                {
                    Spawn();
                    timer = 0;
                }
            }
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if(collision.GetComponent<BoxMagazineComponent>() != null && collision.tag != "Spawned")
        {
            timer = 0;
            BoxMagazine.tag = "Spawned";
            BoxMagazine = null;
            needToSpawn = true;
        }
        if (collision.GetComponent<BoxMagazineComponent_Rocket>() != null && collision.tag != "Spawned")
        {
            timer = 0;
            BoxMagazine.tag = "Spawned";
            BoxMagazine = null;
            needToSpawn = true;
        }
    }
    public void Spawn()
    {
        if (BoxMagazine != null) return;
        BoxMagazine = Instantiate(BoxMagazinePrefeb, transform.position, transform.rotation);
        //BoxMagazine.GetComponent<BoxMagazineComponent>().BulletCapacity = GameManager.GetComponent<GameManager>().BulletCapacity;
        needToSpawn = false;
    }
}
