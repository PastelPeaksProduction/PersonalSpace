using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAccessory : MonoBehaviour
{
    //public GameObject[] options;
    private GameObject accessory;

    public string shapeName;

    //private int optionsLength = 0;
    // Start is called before the first frame update
    void Start()
    {
        // headwear = load random headwear
        // eyewear = load random eyewear
        // nose = load random nose
        // facialhair = load random facialhair
        // badge = load random badge
        // tie = load random tie
        GameObject accStoreObj = GameObject.Find("ShapeBased");
        //AccessoryStore accStore = (AccessoryStore) accStoreObj.GetComponent("AccessoryStore");
        if (accStoreObj != null)
        {
            int eggLen = accStoreObj.GetComponent<AccessoryStore>().egg.Length;
            int coneUpLen = accStoreObj.GetComponent<AccessoryStore>().coneUp.Length;
            int coneDownLen = accStoreObj.GetComponent<AccessoryStore>().coneDown.Length;
            int mikeLen = accStoreObj.GetComponent<AccessoryStore>().mike.Length;
            int ikeLen = accStoreObj.GetComponent<AccessoryStore>().ike.Length;
            int hunkLen = accStoreObj.GetComponent<AccessoryStore>().hunk.Length;
            int rectLen = accStoreObj.GetComponent<AccessoryStore>().rect.Length;



            if (this.transform.parent.GetComponent<RandomShape>())
            {
                shapeName = this.transform.parent.GetComponent<RandomShape>().shapeName;
                if (shapeName.Contains("ConeDown"))
                {
                    if (coneDownLen != 0)
                    {
                        int assign = Random.Range(0, accStoreObj.GetComponent<AccessoryStore>().total);
                        if (assign < accStoreObj.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, coneDownLen);
                            if (accStoreObj.GetComponent<AccessoryStore>().coneDown[index] != null)
                            {
                                var accessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().coneDown[index], transform.position, transform.rotation);
                                accessory.transform.parent = this.transform; 
                            }
                        }
                    }
                }
                if (shapeName.Contains("ConeUp"))
                {
                    if (coneUpLen != 0)
                    {
                        int assign = Random.Range(0, accStoreObj.GetComponent<AccessoryStore>().total);
                        if (assign < accStoreObj.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, coneUpLen);
                            if (accStoreObj.GetComponent<AccessoryStore>().coneUp[index] != null)
                            {
                                var accessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().coneUp[index], transform.position, transform.rotation);
                                accessory.transform.parent = this.transform;
                            }
                        }
                    }
                }
                if (shapeName.Contains("Egg"))
                {
                    if (eggLen != 0)
                    {
                        int assign = Random.Range(0, accStoreObj.GetComponent<AccessoryStore>().total);
                        if (assign < accStoreObj.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, eggLen);
                            if (accStoreObj.GetComponent<AccessoryStore>().egg[index] != null)
                            {
                                var accessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().egg[index], transform.position, transform.rotation);
                                accessory.transform.parent = this.transform;
                            }
                        }
                    }
                }
                if (shapeName.Contains("Mike"))
                {
                    if (mikeLen != 0)
                    {
                        int assign = Random.Range(0, accStoreObj.GetComponent<AccessoryStore>().total);
                        if (assign < accStoreObj.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, mikeLen);
                            if (accStoreObj.GetComponent<AccessoryStore>().mike[index] != null)
                            {
                                var accessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().mike[index], transform.position, transform.rotation);
                                accessory.transform.parent = this.transform;
                            }
                        }
                    }
                }
                if (shapeName.Contains("Ike"))
                {
                    if (ikeLen != 0)
                    {
                        int assign = Random.Range(0, accStoreObj.GetComponent<AccessoryStore>().total);
                        if (assign < accStoreObj.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, ikeLen);
                            if (accStoreObj.GetComponent<AccessoryStore>().ike[index] != null)
                            {
                                var accessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().ike[index], transform.position, transform.rotation);
                                accessory.transform.parent = this.transform;
                            }
                        }
                    }
                }
                if (shapeName.Contains("Hunk"))
                {
                    if (hunkLen != 0)
                    {
                        int assign = Random.Range(0, accStoreObj.GetComponent<AccessoryStore>().total);
                        if (assign < accStoreObj.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, hunkLen);
                            if (accStoreObj.GetComponent<AccessoryStore>().ike[index] != null)
                            {
                                var accessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().hunk[index], transform.position, transform.rotation);
                                accessory.transform.parent = this.transform;
                            }
                        }
                    }
                }
                if (shapeName.Contains("Rect"))
                {
                    if (rectLen != 0)
                    {
                        int assign = Random.Range(0, accStoreObj.GetComponent<AccessoryStore>().total);
                        if (assign < accStoreObj.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, rectLen);
                            if (accStoreObj.GetComponent<AccessoryStore>().rect[index] != null)
                            {
                                var accessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().rect[index], transform.position, transform.rotation);
                                accessory.transform.parent = this.transform;
                            }
                        }
                    }
                }
            }
        }
    }
}

