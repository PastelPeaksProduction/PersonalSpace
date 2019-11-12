using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Camera;
    public GameObject ControlPos;
    public GameObject BackMainPos;
    private bool ControlTrans = false;
    private bool BackTrans = false;


    private void Start()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SchoolDance");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        BackTrans = true;
    }

    public void ShowControls()
    {
        ControlTrans = true;
    }

    private void FixedUpdate()
    {
        if (ControlTrans)
        {
            MoveCam(ControlPos,ref ControlTrans);
        }
        else if (BackTrans)
        {
            MoveCam(BackMainPos, ref BackTrans);

        }
    }

    private void MoveCam(GameObject moveTo, ref bool transBool)
    {
        if (Camera.transform.position == moveTo.transform.position && Camera.transform.rotation == moveTo.transform.rotation)
            transBool = false;

        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, moveTo.transform.position, 20 * Time.deltaTime);
        Camera.transform.rotation = Quaternion.RotateTowards(Camera.transform.rotation, moveTo.transform.rotation, 20 * Time.deltaTime);
    }
}
