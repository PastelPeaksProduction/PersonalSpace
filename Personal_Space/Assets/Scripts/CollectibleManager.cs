using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    // Initialize this when there's collectible in the scene
    private Text itemsToBeCollected;

    /* 
     * Items that can be collected. 
     * Initialize it when doing scene editing
     */
    public int numOfBackPacks;
    public int numOfJackets;

    /* 
     * Actual text to show the counts
     * Initialize it when doing scene editing
     */
    private Text numOfBackPacksText;
    private Text numOfJacketsText;

    /* 
     * Get the necessary game objects
     * and initialize if set
     */
    void Start()
    {
        // Repeat the if/else for more collectibles added in.
        if (numOfBackPacks != 0)
        {
            GameObject obj = GameObject.Find("BackpackUIText");
            numOfBackPacksText = obj.GetComponent<Text>();
            numOfBackPacksText.text = "X " + numOfBackPacks.ToString();
        }
        if(numOfJackets != 0)
        {
            GameObject obj = GameObject.Find("JacketUIText");
            numOfJacketsText = obj.GetComponent<Text>();
            numOfJacketsText.text = "X " + numOfJackets.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* 
     * Public method used in Player controller
     * 
     */
    public void ItemCollected(string name)
    {
        if (name.Contains("Backpack"))
        {
            if(--numOfBackPacks != 0)
            {
                numOfBackPacksText.text = "X " + numOfBackPacks;
            }
            else
            {
                numOfBackPacksText.text = "✔";
            } 
        }
        else if (name.Contains("Jacket"))
        {
            if (--numOfJackets != 0)
            {
                numOfJacketsText.text = "X " + numOfBackPacks;
            }
            else
            {
                numOfJacketsText.text = "✔";
            }
        }
    }



}
