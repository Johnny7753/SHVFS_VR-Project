using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool Isdead;
    
    public GameObject EndUI;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Isdead&&EndUI!=null)
        {
            Debug.Log("Dead!");
            EndUI.SetActive(true);
            Time.timeScale = 0;
        }

        
    }


    void StartGame()
    {
        SceneManager.LoadScene(1);

    }
}
