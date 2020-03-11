using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneMessages : MonoBehaviour
{
    public GameObject newMessage;
    

    private void Start()
    {
        
    }

    public void NewMessage(string message)
    {
       
        RectTransform messageBox = newMessage.GetComponent<NewMessage>().messageBox;
        TextMeshProUGUI messageText = newMessage.GetComponent<NewMessage>().messageText;

        if(message.Length > 26)
        {
            List<string> lines = ComposeMessage(message);
            string composedMessage = "";
            foreach (string line in lines)
            {
                composedMessage += line;
            }

            messageText.text = composedMessage;
            Vector3 scale = messageBox.localScale;
            scale.y = 0.01f + 0.01f * lines.Count;
            scale.x = 0.0414f;
            messageBox.localScale = scale;

        }
        else
        {
            //resize box horizontal size
            messageText.text = message;
            Vector3 scale = messageBox.localScale;
            scale.x = 0.005f + 0.0014f * message.Length;
            messageBox.localScale = scale;
        }
    }

    private List<string> ComposeMessage(string message)
    {
        string[] words = message.Split(' ');
        List<string> lines = new List<string>();
        string currentLine = "";
        foreach(string word in words)
        {
            if(currentLine.Length+word.Length+1 <= 30)
            {
                currentLine = currentLine + " " + word;
            }
            else
            {
                currentLine = currentLine + '\n';
                lines.Add(currentLine);
                currentLine = word;
            }
        }
        lines.Add(currentLine);

        
        return lines;
    }
}
