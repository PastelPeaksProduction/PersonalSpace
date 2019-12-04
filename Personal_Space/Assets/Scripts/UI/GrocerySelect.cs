using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GrocerySelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        switch(gameObject.name){
            case "01Grocery": SceneManager.LoadScene("01GroceryStore");break;
            case "02SchoolDance": SceneManager.LoadScene("02SchoolDance"); break;
            case "03HouseParty": SceneManager.LoadScene("03HouseParty"); break;
            case "04Office": SceneManager.LoadScene("04WorkParty"); break;
        }

    }
}
