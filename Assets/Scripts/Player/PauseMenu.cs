using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject goneGun;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {

                Pause();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GameIsPaused)
            {
                QuitGame();
            }
        }
        //else if (Input.GetKeyDown(KeyCode.M))
        //{
            //if (GameIsPaused)
            //{
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            //}
            //else
            //{

                //Pause();
            //}
        //}
    }

    public void Resume()
    {
        goneGun.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        goneGun.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()

    {
        //Time.timeScale = 1f;
        //UPDATE THIS WITH MENU INFO
        //SceneManager.LoadScene("tittle");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
