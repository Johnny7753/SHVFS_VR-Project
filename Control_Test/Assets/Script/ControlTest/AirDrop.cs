using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDrop : MonoBehaviour
{
    public float DropSpeed;
    // Start is called before the first frame update
    void Start()
    {
        SpawnAirDrop();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * DropSpeed);
    }

    public void SpawnAirDrop()
    {
        Vector3 pos = new Vector3(0,100,0);
        pos.x = Random.Range(50,200);
        pos.z = Random.Range(-60, 60);
        transform.position = pos;
    }
}
