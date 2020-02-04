using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float CameraSpeed;
    public float RotationSpeed;
    public GameObject targetPositionFPP;
    public GameObject targetPositionTPP;
    public float speed; //Speed of mouse rotation

    private GameObject Player;
    private Vector3 rot = new Vector3(0, 0, 0);
    private bool perspective;
    private string horizontalController;

    /* FPP fields */
    Vector2 mouseLook;
    Vector2 sommthV;
    public float sensitivityFirstPerson = 3.0f;
    public float sensitivityThirdPerson = 0.15f;
    public float smoothing = 1.0f;
    private bool isPaused;

    void Start()
    {
        Player = GameObject.Find("Player");
        perspective = false; // False is TPP, True is FPP
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            horizontalController = "Horizontal X Windows";
        }
        else if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            horizontalController = "Horizontal X Mac";
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            perspective = !perspective;
        }
        if (perspective)
        {
            if (transform.localPosition == targetPositionFPP.transform.localPosition)
            {
                FirstPersonPerspective();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPositionFPP.transform.position, CameraSpeed * Time.deltaTime);

            }

        }
        else
        {
            ThirdPersonPerspective();
        }


    }

    private void ThirdPersonPerspective()
    {
        if (!isPaused)
        {
            var md = new Vector2(0, 0);
            if (Input.GetAxisRaw(horizontalController) > 0)
            {
                md = new Vector2(Input.GetAxisRaw(horizontalController), 0);
                md *= 10;
                Debug.Log("Controller Input :" + md);
            }
            else
            {
                if (!Input.GetAxisRaw("Mouse X").Equals(0))
                {
                    md = new Vector2(Input.GetAxisRaw(horizontalController) + Input.GetAxisRaw("Mouse X"), 0);
                }
                else
                {
                    md = new Vector2(Input.GetAxisRaw(horizontalController), 0);
                    md *= 10;
                }

                // Debug.Log("Mouse Input :" + md);
            }
            md = Vector2.Scale(md, new Vector2(sensitivityThirdPerson * smoothing, sensitivityThirdPerson * smoothing));
            sommthV.x = Mathf.Lerp(sommthV.x, md.x, 1f / smoothing);
            mouseLook += sommthV;

            Player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Player.transform.up);

            transform.position = Vector3.MoveTowards(transform.position, targetPositionTPP.transform.position, CameraSpeed * Time.deltaTime);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetPositionTPP.transform.rotation, RotationSpeed * Time.deltaTime);

            if ((Player != null) && (Vector3.Distance(transform.position, targetPositionTPP.transform.position) < 0.001f))
            {
                Player.GetComponent<PlayerController>().canMove = true;
            }
        }
    }

    private void FirstPersonPerspective()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivityFirstPerson * smoothing, sensitivityFirstPerson * smoothing));
        sommthV.x = Mathf.Lerp(sommthV.x, md.x, 1f / smoothing);
        sommthV.y = Mathf.Lerp(sommthV.y, md.y, 1f / smoothing);
        mouseLook += sommthV;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        Player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Player.transform.up);
    }

    public void SetPause()
    {
        isPaused = !isPaused;
    }
}
