using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamdomMove : MonoBehaviour
{
    private bool collision = false;
    public bool waiting = false;
    Vector3 randomPosition;
    float moveDelay = 1;
    private Vector3 startPos;

    private void Start()
    {
        startPos = this.transform.position;
    }
    private void FixedUpdate()
    {

        if (waiting == false)
        {
            SendMessage("LerpEnemy");
        }
        else
        {
            if(!collision && Vector3.Distance(startPos, randomPosition) < 10)
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

    private void OnTriggerEnter(Collider other)
    {

        collision = true;
        float force = 50;
        Vector3 direction = transform.position - other.transform.position;
        direction.Normalize();
        this.GetComponent<Rigidbody>().AddForce(direction * force);
    }

    private void OnTriggerExit(Collider other)
    {
        collision = false;
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
