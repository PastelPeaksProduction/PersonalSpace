using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private PlayerController player;

    public GameObject pauseDialog;
    public GameObject deadDialog;
    public GameObject Stressbar;
    public GameObject PlayerAngle;
    public GameObject FOV;
    public GameObject TextBubbleCanvas;
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
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKey("joystick button 18") || Input.GetKey("joystick button 2"))
            {
                deadDialog.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (Input.GetKey(KeyCode.Tab) || Input.GetKey("joystick button 17") || Input.GetKey("joystick button 1"))
        {
            PauseGame();
            pauseDialog.SetActive(true);
            Stressbar.SetActive(true);
            PlayerAngle.SetActive(true);
            FOV.SetActive(true);
            TextBubbleCanvas.SetActive(false);

            isPaused = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp("joystick button 17") || Input.GetKeyUp("joystick button 1"))
        {
            ContinueGame();
            pauseDialog.SetActive(false);
            Stressbar.SetActive(false);
            PlayerAngle.SetActive(false);
            FOV.SetActive(false);
            TextBubbleCanvas.SetActive(true);

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
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("WorkParty");

        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("GroceryStore");

        }
    }

    public void AdvanceLevel()
    {
        if(SceneManager.GetActiveScene().name == "03House Party")
        {
            SceneManager.LoadScene("00StartMenu");
        }

        if (SceneManager.GetActiveScene().name == "02SchoolDance" || SceneManager.GetActiveScene().name == "SchoolDanceUI")
        {
            SceneManager.LoadScene("03HouseParty");
        }
    }
}
