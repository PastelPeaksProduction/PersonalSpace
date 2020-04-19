using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhoneMessages : MonoBehaviour
{
    public GameObject newMessagePrefab;
    public GameObject responsePrefab;
    public GameObject contents;
    public GameObject spacer;
    

    private void Start()
    {
        
    }

    public void NewMessage(string message)
    {
        GameObject newMessage;
        /*if (isReposnse)
        {
            newMessage = Instantiate(responsePrefab);
        }
        else
        {
            newMessage = Instantiate(newMessagePrefab);
        }*/

        newMessage = Instantiate(responsePrefab);


        RectTransform messageBox = newMessage.GetComponent<NewMessage>().messageBox;
        TextMeshProUGUI messageText = newMessage.GetComponent<NewMessage>().messageText;

        if(message.Length > 35)
        {
            List<string> lines = ComposeMessage(message);
            string composedMessage = "";
            foreach (string line in lines)
            {
                composedMessage += line;
            }

            messageText.text = composedMessage;
            messageBox.sizeDelta = new Vector2(2000, lines.Count * 200 + 200);

            //Vector3 scale = messageBox.localScale;

            //scale.y = 0.01f * lines.Count + 0.02f;
            //scale.x = 0.03f + 0.001f * 35;
           // messageBox.localScale = scale;
            //Vector3 textScale = messageText.GetComponent<RectTransform>().localScale;
            //textScale.x = 3 / ((scale.x / 0.0414f));
            //textScale.y = (textScale.y -0.02f)/ 3;
            //messageText.GetComponent<RectTransform>().localScale = textScale;

        }
        else
        {
            //resize box horizontal size
            messageText.text = message;
            /* Vector3 scale = messageBox.localScale;
             scale.x = 0.03f + 0.001f * message.Length;
             messageBox.localScale = scale;
             Vector3 textScale = messageText.GetComponent<RectTransform>().localScale;

             textScale.x = 3 / ((scale.x / 0.0414f) - 0.3f) ;
             messageText.GetComponent<RectTransform>().localScale = textScale;
             */
            messageBox.sizeDelta = new Vector2(55*message.Length+100, 300);
        }
        newMessage.transform.parent = contents.transform;
        newMessage.transform.localScale = new Vector3(1, 1, 1);

        GameObject spacerObj = Instantiate(spacer, contents.transform);
        spacerObj.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 10);
        spacerObj.transform.localScale = new Vector3(1, 1, 1);
        
        

    }

    private List<string> ComposeMessage(string message)
    {
        string[] words = message.Split(' ');
        List<string> lines = new List<string>();
        string currentLine = "";
        foreach(string word in words)
        {
            if(currentLine.Length+word.Length+1 <= 35)
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
