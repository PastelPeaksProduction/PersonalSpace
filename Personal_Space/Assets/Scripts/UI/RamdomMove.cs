using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamdomMove : MonoBehaviour
{

    public bool waiting = false;
    Vector3 randomPosition;
    float moveDelay = 1;

    private void FixedUpdate()
    {

        if (waiting == false)
        {
            SendMessage("LerpEnemy");
        }
        else
        {
           
            transform.position = Vector3.Lerp(transform.position, randomPosition, Random.Range(1,5) * Time.deltaTime);
        }
    }


    private IEnumerator LerpEnemy()
    {
        randomPosition = transform.position + new Vector3(Random.Range(6, -6),0, Random.Range(5, -5));
        waiting = true;
        moveDelay = Random.Range(1, 3) * Random.Range(0.2f,2);
        yield return new WaitForSeconds(moveDelay);
        waiting = false;
        //Debug.Log ("waited");
    }
}
