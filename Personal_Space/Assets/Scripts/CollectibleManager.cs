using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour
{
    /* 
     * Items that can be collected. 
     * Initialize it when doing scene editing
     */
    public int numOfBackPacks;
    public int numOfJackets;

    /* 
     * Get the necessary game objects
     * and initialize if set
     */
    public Image backPack;
    public Image jacket;
    void Start()
    {
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
        if (name.IndexOf("Backpack", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            backPack.color = Color.gray;
        }
        else if (name.IndexOf("Jacket", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            jacket.color = Color.gray;
        }
    }




}
