using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayPoint : MonoBehaviour
{
    public Image indicator;
    public Transform target;
    public ObjectivesManager obj_man;
    // Start is called before the first frame update
    void Start()
    {
        obj_man = gameObject.transform.GetComponentInParent<ObjectivesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (obj_man.GetCurrentObjective() == null)
        {
            return;
        }
        target = obj_man.GetCurrentObjective().transform;
        float minX = indicator.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = indicator.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;
        Vector2 pos = Camera.main.WorldToScreenPoint(target.position);

        if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y + 60, minY, maxY);

        indicator.transform.position = pos;

    }
}
