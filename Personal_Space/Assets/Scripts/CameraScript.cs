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

    /* FPP fields */
    Vector2 mouseLook;
    Vector2 sommthV;
    public float sensitivity = 3.0f;
    public float smoothing = 2.0f;

    void Start()
    {
        Player = GameObject.Find("Player");
        perspective = false; // False is TPP, True is FPP
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            perspective = !perspective;
        }
        if (perspective)
        {
            if (transform.localPosition == Vector3.zero)
            {
                Debug.Log("zeroed");
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
        transform.position = Vector3.MoveTowards(transform.position, targetPositionTPP.transform.position, CameraSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetPositionTPP.transform.rotation, RotationSpeed * Time.deltaTime);
        if ((Player != null) && (Vector3.Distance(transform.position, targetPositionTPP.transform.position) < 0.001f))
        {      
            Player.GetComponent<PlayerController>().canMove = true;
        }
    }

    private void FirstPersonPerspective()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        sommthV.x = Mathf.Lerp(sommthV.x, md.x, 1f / smoothing);
        sommthV.y = Mathf.Lerp(sommthV.y, md.y, 1f / smoothing);
        mouseLook += sommthV;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        Player.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Player.transform.up);
    }
}
