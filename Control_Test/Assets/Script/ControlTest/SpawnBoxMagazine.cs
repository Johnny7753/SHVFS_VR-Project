using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoxMagazine : MonoBehaviour
{
    public GameObject BoxMagazine;
    public GameObject BoxMagazinePrefeb;
    public bool needToSpawn = false;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        BoxMagazine = Instantiate(BoxMagazinePrefeb, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (needToSpawn)
        {
            timer += Time.deltaTime;
            if(timer > 5)
            {
                BoxMagazine = Instantiate(BoxMagazinePrefeb, transform.position, transform.rotation);
                timer = 0;
                needToSpawn = false;
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
    }
}