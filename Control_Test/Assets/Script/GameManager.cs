using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool Isdead;
    
    public GameObject GameOverUI;
    public GameObject WinUI;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Isdead&&GameOverUI!=null)
        {
            Debug.Log("Dead!");
            GameOverUI.SetActive(true);
            Time.timeScale = 0;
        }

        if(FindObjectOfType<EnemySystem>().wavenumber>2)
        {
            Debug.Log("Win!");
            WinUI.SetActive(true);
            Time.timeScale = 0;
        }
        
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);

    }
}
