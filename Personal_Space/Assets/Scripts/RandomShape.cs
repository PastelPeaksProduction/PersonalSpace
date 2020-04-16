using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShape : MonoBehaviour
{
    // Start is called before the first frame update
    public string shapeName;
    void Start()
    {
        GameObject[] shapes = GameObject.FindGameObjectWithTag("ShapeLibrary").GetComponent<RandomShapesLibrary>().shapes;
        int random = Random.Range(0, shapes.Length);
        GameObject shape = shapes[random];
        GameObject child = Instantiate(shape, this.transform.position, this.transform.rotation);
        shapeName = child.name;
        child.transform.parent = this.transform;
        //child.transform.localScale = new Vector3(1, 1, 1)/ (this.transform.localScale.x);
        
        child.GetComponent<MeshRenderer>().material = this.GetComponent<MeshRenderer>().material;
        this.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
