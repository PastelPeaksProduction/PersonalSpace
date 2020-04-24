using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhoneMessages : MonoBehaviour
{
    public GameObject newMessagePrefab;
    public GameObject responsePrefab;
    public GameObject emojiMessagePrefab;
    public GameObject contents;
    public GameObject spacer;
    public RectTransform phone;


    private void Start()
    {
        contents.GetComponent<RectTransform>().position = new Vector3(contents.GetComponent<RectTransform>().position.x, 0, 0);
    }

    public void NewMessage(Message message)
    {
        GameObject newMessage;
        if (message.isResponse)
        {
            newMessage = Instantiate(responsePrefab, contents.transform);
        }
        else
        {
            newMessage = Instantiate(newMessagePrefab, contents.transform);
        }

        RectTransform messageBox = newMessage.GetComponent<NewMessage>().messageBox;
        TextMeshProUGUI messageText = newMessage.GetComponent<NewMessage>().messageText;
        TextMeshProUGUI messageName = newMessage.GetComponent<NewMessage>().messageName;
        messageName.text = message.Name;

        if (message.messageText.Length > 32)
        {
            List<string> lines = ComposeMessage(message.messageText);
            string composedMessage = "";
            foreach (string line in lines)
            {
                composedMessage += line;
            }

            messageText.text = composedMessage;
            messageBox.sizeDelta = new Vector2(1650, lines.Count * 200 + 25);
            newMessage.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 12 +(lines.Count-1)*8);
        }
        else
        {
            //resize box horizontal size
            messageText.text = message.messageText;
            int calc = Mathf.Min(1650, 60 * message.messageText.Length +100);
            messageBox.sizeDelta = new Vector2(calc,250);
        }
    }

    private List<string> ComposeMessage(string message)
    {
        string[] words = message.Split(' ');
        List<string> lines = new List<string>();
        string currentLine = "";
        foreach(string word in words)
        {
            if(currentLine.Length+word.Length+1 <= 32)
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
