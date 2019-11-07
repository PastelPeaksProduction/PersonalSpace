using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private PlayerController player;

    public GameObject pauseDialog;
    public GameObject deadDialog;
    private bool activeDialog = false;
    public bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckGameStatus();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
    }

    private void CheckGameStatus()
    {
        if (player.health <= 0)
        {
            PauseGame();
            deadDialog.SetActive(true);

            //Q to Restart game
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKey("joystick button 18"))
            {
                deadDialog.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (Input.GetKey(KeyCode.Tab) || Input.GetKey("joystick button 17"))
        {
            PauseGame();
            pauseDialog.SetActive(true);
            isPaused = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp("joystick button 17"))
        {
            ContinueGame();
            pauseDialog.SetActive(false);
            isPaused = false;
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("House Party");

        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("SchoolDance");

        }
    }

    public void AdvanceLevel()
    {
        if(SceneManager.GetActiveScene().name == "House Party")
        {
            SceneManager.LoadScene("StartMenu");
        }

        if (SceneManager.GetActiveScene().name == "SchoolDance")
        {
            SceneManager.LoadScene("House Party");
        }
    }
}
