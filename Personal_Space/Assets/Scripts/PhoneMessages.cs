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

    

    private void Start()
    {
        contents.GetComponent<RectTransform>().position = new Vector3(contents.GetComponent<RectTransform>().position.x, 0, 0);
    }

    public void EmojiMessage(Sprite emoji)
    {
        //GameObject newMessage = Instantiate(emojiMessagePrefab, contents.transform);
        //newMessage.GetComponent<EmojiMessage>().emoji.sprite = emoji;
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

        //newMessage = Instantiate(responsePrefab);

        //GameObject spacerObj = Instantiate(spacer, contents.transform);
        //spacerObj.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 10);
        //spacerObj.transform.localScale = new Vector3(1, 1, 1);

        RectTransform messageBox = newMessage.GetComponent<NewMessage>().messageBox;
        TextMeshProUGUI messageText = newMessage.GetComponent<NewMessage>().messageText;
        TextMeshProUGUI messageName = newMessage.GetComponent<NewMessage>().messageName;
        messageName.text = message.Name;

        if (message.messageText.Length > 35)
        {
            List<string> lines = ComposeMessage(message.messageText);
            string composedMessage = "";
            foreach (string line in lines)
            {
                composedMessage += line;
            }

            messageText.text = composedMessage;
            messageBox.sizeDelta = new Vector2(1650, lines.Count * 200 + 25);
            newMessage.GetComponent<RectTransform>().sizeDelta = new Vector2(75, 20 + (lines.Count * 200 - 100) / 100);

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
            messageText.text = message.messageText;
            /* Vector3 scale = messageBox.localScale;
             scale.x = 0.03f + 0.001f * message.Length;
             messageBox.localScale = scale;
             Vector3 textScale = messageText.GetComponent<RectTransform>().localScale;

             textScale.x = 3 / ((scale.x / 0.0414f) - 0.3f) ;
             messageText.GetComponent<RectTransform>().localScale = textScale;
             */
            int calc = Mathf.Min(1650, 60 * message.messageText.Length +100);
            messageBox.sizeDelta = new Vector2(calc,250);
        }
        //newMessage.transform.parent = contents.transform;
        //newMessage.transform.localScale = new Vector3(1, 1, 1);

       
        
        

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
