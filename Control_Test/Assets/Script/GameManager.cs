using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool Isdead;
    
    public GameObject GameOverUI;
    public GameObject WinUI;
    public GameObject PauseUI;
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
            Time.timeScale = 0.0001f;
        }


        if(WinUI !=null && FindObjectOfType<EnemySystem>().isWin)
        {
            Debug.Log("Win!");
            WinUI.SetActive(true);
            Time.timeScale = 0.0001f;
        }

        if(Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            PauseGame();
        }
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);

    }
    public void PauseGame()
    {
        //Time.timeScale = 0.0001f;
        PauseUI.SetActive(true);
    }
    public void ResumeGame()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGameScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
