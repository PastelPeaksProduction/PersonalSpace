using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControllerMenus : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] selectableButtons;
    int buttonIndex = 0;
    private string horizontalController;
    private bool isPressed = false;
    public EventSystem system;

    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            horizontalController = "Horizontal X Windows";
        }
        else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            horizontalController = "Horizontal X Mac";
        }
    }

    // Update is called once per frame
    void Update()
    {
        //get input, swap through buttons on input 
        if (Input.GetAxisRaw(horizontalController) != 0)
        {
            if (!isPressed)
            {
                Debug.Log("INPUT " + Input.GetAxisRaw(horizontalController));
                isPressed = true;
                if (Input.GetAxisRaw(horizontalController) > 0)
                {
                    //right

                }
                else
                {
                    //left
                }
            }

        }
    }

    private void MoveRight()
    {
        buttonIndex++;
        if(buttonIndex >= selectableButtons.Length)
        {
            buttonIndex = 0;
        }
        system.SetSelectedGameObject(selectableButtons[buttonIndex].gameObject);
    }
    private void MoveLeft()
    {
        buttonIndex--;
        if (buttonIndex < 0)
        {
            buttonIndex = selectableButtons.Length - 1;
        }
        system.SetSelectedGameObject(selectableButtons[buttonIndex].gameObject);
    }
    private IEnumerator buttonCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isPressed = false;
    }

}