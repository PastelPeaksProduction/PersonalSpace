using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public GameObject objectiveDialog;
    // To flag whether is triggered
    private bool activeDialog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkDialogStatus();
    }

    private void checkDialogStatus()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            objectiveDialog.SetActive(false);
            Time.timeScale = 1;

        }
    }

    public void showDialog(string description)
    {
        objectiveDialog.SetActive(true);
        objectiveDialog.GetComponentInChildren<TextMeshProUGUI>().text = description;
        Time.timeScale = 0;
    }

}
