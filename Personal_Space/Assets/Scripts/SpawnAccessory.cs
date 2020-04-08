using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAccessory : MonoBehaviour
{
    //public GameObject[] options;
    private GameObject headwear;
    private GameObject eyewear;
    private GameObject nose;
    private GameObject tie;
    private GameObject badge;
    private GameObject facialhair;


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
        GameObject accStoreObj = GameObject.Find("AccessoryStorageObj"); 
        //AccessoryStore accStore = (AccessoryStore) accStoreObj.GetComponent("AccessoryStore");
        if(accStoreObj != null)
        {
            int headLength = accStoreObj.GetComponent<AccessoryStore>().headwear.Length;
            int hairLength = accStoreObj.GetComponent<AccessoryStore>().hair.Length;
            int eyeLength = accStoreObj.GetComponent<AccessoryStore>().eyewear.Length;
            int noseLength = accStoreObj.GetComponent<AccessoryStore>().noses.Length;
            int facialHairLength = accStoreObj.GetComponent<AccessoryStore>().facialhair.Length;
            int badgeLength = accStoreObj.GetComponent<AccessoryStore>().badges.Length;
            int tieLength = accStoreObj.GetComponent<AccessoryStore>().ties.Length;
            
            int randomHeadInd = 0;
            int randomHairInd = 0;
            int randomEyeInd = 0;
            int randomNoseInd = 0;
            int randomFacHairInd = 0;
            int randomBadgeInd = 0;
            int randomTieInd = 0;

            bool hasHair = false;
            GameObject hairAccessory = null;

            if (hairLength != 0)
            {
                int assign = Random.Range(0, accStoreObj.GetComponent<AccessoryStore>().totalHair);
                if (assign < accStoreObj.GetComponent<AccessoryStore>().hasHair)
                {
                    randomHairInd = Random.Range(0, hairLength);
                    if (accStoreObj.GetComponent<AccessoryStore>().hair[randomHairInd] != null)
                    {
                        hasHair = true;
                        hairAccessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().hair[randomHairInd], transform.position, transform.rotation);
                        hairAccessory.transform.parent = this.gameObject.transform;
                        Color randColor = Color.black;
                        while (randColor.r + randColor.g + randColor.b < 1.0f)
                        {
                            randColor = new Color(Random.value, Random.value, Random.value);
                        }

                        Renderer rend = hairAccessory.GetComponent<Renderer>();
                        rend.material.color = randColor;
                    }
                }

            }
            if (headLength != 0)
            {
                int assign = Random.Range(0, 2);
                if(assign < 1 && !hasHair)
                {
                    randomHeadInd = Random.Range(0, headLength);
                    if (accStoreObj.GetComponent<AccessoryStore>().headwear[randomHeadInd] != null)
                    {
                        var headAccessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().headwear[randomHeadInd], transform.position, transform.rotation);
                        headAccessory.transform.parent = this.gameObject.transform;
                        Color randColor = Color.black;
                        while (randColor.r + randColor.g + randColor.b < 1.0f)
                        {
                            randColor = new Color(Random.value, Random.value, Random.value);
                        }

                        Renderer rend = headAccessory.GetComponent<Renderer>();
                        rend.material.color = randColor;
                    }
                }
                
            }
            if(eyeLength != 0)
            {
                int assign = Random.Range(0, accStoreObj.GetComponent<AccessoryStore>().totalEyewear);
                if (assign < accStoreObj.GetComponent<AccessoryStore>().hasEyewear)
                {
                    randomEyeInd = Random.Range(0, eyeLength);
                    if (accStoreObj.GetComponent<AccessoryStore>().eyewear[randomEyeInd] != null)
                    {
                        var eyeAccessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().eyewear[randomEyeInd], transform.position, transform.rotation);
                        eyeAccessory.transform.parent = this.gameObject.transform;
                    }
                }
            }
            if(noseLength != 0)
            {
                randomNoseInd =  Random.Range(0,noseLength);
                if(accStoreObj.GetComponent<AccessoryStore>().noses[randomNoseInd] != null)
                {
                    var noseAccessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().noses[randomNoseInd], transform.position, transform.rotation);
                    noseAccessory.transform.parent = this.gameObject.transform;
                    noseAccessory.GetComponent<Renderer>().material.color = this.transform.parent.GetComponent<Renderer>().material.color;
                }
                
            }
            if(facialHairLength != 0)
            {
                int assign = Random.Range(0, accStoreObj.GetComponent<AccessoryStore>().totalFacialHair);
                if (assign < accStoreObj.GetComponent<AccessoryStore>().hasFacialHair)
                {
                    randomFacHairInd = Random.Range(0, facialHairLength);
                    if (accStoreObj.GetComponent<AccessoryStore>().facialhair[randomFacHairInd] != null)
                    {
                        var facHairAccessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().facialhair[randomFacHairInd], transform.position, transform.rotation);
                        facHairAccessory.transform.parent = this.gameObject.transform;
                        if (hasHair)
                        {
                            Color color = hairAccessory.GetComponent<Renderer>().material.color;
                            Renderer rend = facHairAccessory.GetComponent<Renderer>();
                            rend.material.color = color;
                        }
                        else
                        {
                            Color randColor = Color.black;
                            while (randColor.r + randColor.g + randColor.b < 1.0f)
                            {
                                randColor = new Color(Random.value, Random.value, Random.value);
                            }

                            Renderer rend = facHairAccessory.GetComponent<Renderer>();
                            rend.material.color = randColor;
                        }
                    }
                }
            }
            if(badgeLength != 0)
            {
                int assign = Random.Range(0, accStoreObj.GetComponent<AccessoryStore>().totalBadge);
                if (assign < accStoreObj.GetComponent<AccessoryStore>().hasBadge)
                {
                    randomBadgeInd = Random.Range(0, badgeLength);
                    if (accStoreObj.GetComponent<AccessoryStore>().badges[randomBadgeInd])
                    {
                        var badgeAccessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().badges[randomBadgeInd], transform.position, transform.rotation);
                        badgeAccessory.transform.parent = this.gameObject.transform;
                    }
                }

            }
            if(tieLength != 0)
            {
                int assign = Random.Range(0, accStoreObj.GetComponent<AccessoryStore>().totalTies);
                if (assign < accStoreObj.GetComponent<AccessoryStore>().hasTie)
                {
                    randomTieInd = Random.Range(0, tieLength);
                    if (accStoreObj.GetComponent<AccessoryStore>().ties[randomTieInd] != null)
                    {
                        var tieAccessory = Instantiate(accStoreObj.GetComponent<AccessoryStore>().ties[randomTieInd], transform.position, transform.rotation);
                        tieAccessory.transform.parent = this.gameObject.transform;
                    }
                }
                
            }

           
        }

       /* if(options != null)
        {
            if(options.Length > 1)
            {
                optionsLength = options.Length;
                
                int randomAccInd =  Random.Range(0,optionsLength);
                //Debug.Log(""+randomAccInd);
                var accessory = Instantiate(options[randomAccInd], transform.position, transform.rotation);
                accessory.transform.parent = this.gameObject.transform;
            }
            else
            {
                var accessory = Instantiate(options[0], transform.position, transform.rotation);
                accessory.transform.parent = this.gameObject.transform;
            } 
        } */   
    }
}
