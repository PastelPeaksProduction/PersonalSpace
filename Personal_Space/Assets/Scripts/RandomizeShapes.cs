using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeShapes : MonoBehaviour
{
    // Start is called before the first frame update
    public Mesh mesh;
    void Start()
    {
        Mesh[] library = GameObject.FindGameObjectWithTag("ShapeLibrary").GetComponent<RandomShapesLibrary>().shapes;
        int random = Random.Range(0, library.Length);
        mesh = library[random];
        this.GetComponent<MeshFilter>().mesh = mesh;
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    
}
