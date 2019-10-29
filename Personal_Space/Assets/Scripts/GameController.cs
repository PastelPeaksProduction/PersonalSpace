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
            if (Input.GetKeyDown(KeyCode.Q))
            {
                deadDialog.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            PauseGame();
            pauseDialog.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ContinueGame();
            pauseDialog.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("House Party");

        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("School Dance");

        }
    }
}
