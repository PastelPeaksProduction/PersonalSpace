using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class DataPoint
{
    public string form_id;
    public string data;
}

public class DrivePost : MonoBehaviour
{
    public bool Clear;
    public DataPoint TrackingPlayerID;
    public DataPoint TrackingLocation;
    public DataPoint TrackingLevel;
    public DataPoint TrackingLevelAttempt;
    public DataPoint TrackingPlayThrough;
    public DataPoint TrackingEventType;

    public DataPoint BugPlayerID;
    public DataPoint BugLocation;
    public DataPoint BugLevel;
    public DataPoint BugDescription;
    public DataPoint BugEmail;

    public GameObject consentWindow;

    private GameObject Player;

    [SerializeField]
    public string BASE_URL_TRACKING;

    [SerializeField]
    public string BASE_URL_BUG;
    private void Start()
    {
        if (Clear)
        {
            PlayerPrefs.DeleteAll();
            Clear = false;
        }

        Player = GameObject.FindGameObjectWithTag("Player");

        if (!PlayerPrefs.HasKey("PlayThrough"))
        {
            PlayerPrefs.SetInt("PlayThrough", 0);
        }

        if (!PlayerPrefs.HasKey("PlayerID"))
        {
            PlayerPrefs.SetString("PlayerID", System.DateTime.Now.ToString() + " " + System.Environment.UserName.GetHashCode());
            if (consentWindow != null)
            {
                consentWindow.SetActive(true);
            }
        }

        if (SceneManager.GetActiveScene().name == "NewStartMenu")
        {
            PlayerPrefs.SetInt("LevelAttempt", 0);
            PlayerPrefs.SetInt("PlayThrough", PlayerPrefs.GetInt("PlayThrough") + 1);
        }
        else
        {
            int attempt = PlayerPrefs.GetInt("LevelAttempt");
            attempt += 1;
            TrackingLevelAttempt.data = attempt.ToString();
            PlayerPrefs.SetInt("LevelAttempt", attempt);
        }

        TrackingPlayThrough.data = PlayerPrefs.GetInt("PlayThrough").ToString();
        TrackingPlayerID.data = PlayerPrefs.GetString("PlayerID");
        BugPlayerID.data = PlayerPrefs.GetString("PlayerID");
        TrackingLevel.data = SceneManager.GetActiveScene().name;
        TrackingLevelAttempt.data = PlayerPrefs.GetInt("LevelAttempt").ToString();
        BugLevel.data = SceneManager.GetActiveScene().name;

        SubmitTracking("StartLevel");
    }


    public void CompleteLevel()
    {
        SubmitTracking("CompleteLevel");
        PlayerPrefs.SetInt("LevelAttempt", 0);
    }

    public void FailLevel()
    {
        SubmitTracking("FailedLevel");
    }

    public void ObjectiveCompleted(string objective)
    {
        SubmitTracking(objective);
    }



    public void Consent(bool consent)
    {
        if (consent)
        {
            PlayerPrefs.SetString("Consent", "DoesConsent");
            SubmitTracking("Consent");
        }
        else
        {
            PlayerPrefs.SetString("Consent", "DoesNotConsent");
            SubmitTracking("DNC");
        }
    }


    private void SubmitTracking(string eventType)
    {
        if (PlayerPrefs.GetString("Consent") != "DoesNotConsent")
        {
            TrackingEventType.data = eventType;
            if (Player != null)
            {
                TrackingLocation.data = Player.transform.position.ToString();
            }
            else
            {
                TrackingLocation.data = "None";
            }
            StartCoroutine(PostTracking());
        }
    }

    public void SetEmail(TextMeshProUGUI email)
    {
        BugEmail.data = email.text;
    }
    public void SubmitBugReport(TextMeshProUGUI description)
    {
        BugDescription.data = description.text;
        if(BugDescription.data.Length > 1)
        {
            if (Player != null)
            {
                BugLocation.data = Player.transform.position.ToString();
            }
            else
            {
                BugLocation.data = "None";
            }
            StartCoroutine(PostBugReport());
        }
    
    }



    IEnumerator PostTracking()
    {
        WWWForm form = new WWWForm();

        form.AddField(TrackingPlayerID.form_id, TrackingPlayerID.data);
        form.AddField(TrackingLocation.form_id, TrackingLocation.data);
        form.AddField(TrackingLevel.form_id, TrackingLevel.data);
        form.AddField(TrackingLevelAttempt.form_id, TrackingLevelAttempt.data);
        form.AddField(TrackingPlayThrough.form_id, TrackingPlayThrough.data);
        form.AddField(TrackingEventType.form_id, TrackingEventType.data);

        using (UnityWebRequest www = UnityWebRequest.Post(BASE_URL_TRACKING + "/formResponse", form))
        {
            www.redirectLimit = 10;
            yield return www.SendWebRequest(); ;
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

            }
        }
    }

    IEnumerator PostBugReport()
    {
        WWWForm form = new WWWForm();

        form.AddField(BugPlayerID.form_id, BugPlayerID.data);
        form.AddField(BugLocation.form_id, BugLocation.data);
        form.AddField(BugLevel.form_id, BugLevel.data);
        form.AddField(BugDescription.form_id, BugDescription.data);
        form.AddField(BugEmail.form_id, BugEmail.data);
        //https://docs.google.com/forms/d/e/1FAIpQLSfUctr4bl-9A_IGJZ-Mfgl5itf9BQ6Ff0RKVdScDNnfYBs_rg

        using (UnityWebRequest www = UnityWebRequest.Post(BASE_URL_BUG + "/formResponse", form))
        {
            www.redirectLimit = 10;
            yield return www.SendWebRequest(); ;
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}

