using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;



public class PreSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject contents;
    public Message[] messages;
    public GameObject newMessagePrefab;
    public GameObject responsePrefab;
    public BackgroundSoundController sound;
    public float waitTime;
    public float disapearTime;
    private float timeElapsed;
    private float startTime;
    private int messageIndex = 0;

    private GameController GC;
    private bool setHide = false;
    private bool _notifyMessage;
    private Animation _animation;

    


    private void Start()
    {
        contents.GetComponent<RectTransform>().position = new Vector3(contents.GetComponent<RectTransform>().position.x, 0, 0);
        _animation = GetComponent<Animation>();

        NewMessage(messages[0]);
        messageIndex++;
       
        sound.StartMainLoop();
        sound.NewMessage();
        

        _animation.Play("Cut_Phone_Show");
        startTime = Time.time;


        
    }
    
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        timeElapsed = Time.time - startTime;
        if(timeElapsed >= waitTime)
        {
            
            if(messageIndex < messages.Length)
            {
                NewMessage(messages[messageIndex]);
                startTime = Time.time;
                if (!messages[messageIndex].isResponse)
                {
                    NotifyMessage();
                    sound.NewMessage();
                }
            }
            else
            {
                if (!_animation.IsPlaying("Cut_Phone_Hide") && !setHide)
                {
                    setHide = true;
                    HideMessage();
                    waitTime = disapearTime;
                }
                else if (!_animation.IsPlaying("Cut_Phone_Hide") && setHide)
                {
                    NextScene();
                }

            }
            messageIndex++;

        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    
    public void ShowMessage()
    {
        _animation.Play("Cut_Phone_Show");
    }

    public void HideMessage()
    {
        _animation.Play("Cut_Phone_Hide");

    }

    private void NotifyMessage()
    {
        _animation.Play("Phone_New_Message");
        _notifyMessage = false;

    }

    public void SetNotifyMessage(Message message)
    {
        GC.isPhoneShow = false;
        _notifyMessage = !_notifyMessage;
        contents.GetComponent<PhoneMessages>().NewMessage(message);
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
        }
        else
        {
            //resize box horizontal size
            messageText.text = message.messageText;
            int calc = Mathf.Min(1650, 60 * message.messageText.Length + 100);
            messageBox.sizeDelta = new Vector2(calc, 250);
        }
    }

    private List<string> ComposeMessage(string message)
    {
        string[] words = message.Split(' ');
        List<string> lines = new List<string>();
        string currentLine = "";
        foreach (string word in words)
        {
            if (currentLine.Length + word.Length + 1 <= 35)
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
