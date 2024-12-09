using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour
{
    public GameObject endScoreMenu;
    public static bool GameEndMenuActive;
    //public static int scoreValue = 0;
    public TextMeshProUGUI score;

    private void OnTriggerEnter(Collider other)
    {
        GameEndMenuActive = false;

        if (other.CompareTag("Player")) // Ensure the Player has the "Player" tag
        {
            endScoreMenu.SetActive(true);
            score.text = "Final Score:" + ScoreScript.scoreValue.ToString();
            Time.timeScale = 0f;
            Debug.Log("Player fell through the hole!");
            GameEndMenuActive=true;
            if (Input.GetKeyDown(KeyCode.R))
            {
                ScoreScript.scoreValue = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {

                Application.Quit();

            }
        }
    }
    void Update()
    {
        score.text = "Final Score:" + ScoreScript.scoreValue.ToString();
        if (Input.GetKeyDown(KeyCode.R) && GameEndMenuActive)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Q) && GameEndMenuActive)
        {

            Application.Quit();

        }
    }


}
