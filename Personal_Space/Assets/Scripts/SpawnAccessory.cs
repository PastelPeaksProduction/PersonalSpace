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
            int coneUpLen = accStoreObj.GetComponent<AccessoryStore>().coneUpAcc.Length;
            int coneDownLen = accStoreObj.GetComponent<AccessoryStore>().coneDownAcc.Length;
            int mikeLen = accStoreObj.GetComponent<AccessoryStore>().mikeAcc.Length;
            int ikeLen = accStoreObj.GetComponent<AccessoryStore>().ikeAcc.Length;
            int hunkLen = accStoreObj.GetComponent<AccessoryStore>().hunkAcc.Length;
            int rectLen = accStoreObj.GetComponent<AccessoryStore>().rectAcc.Length;



            //if (this.transform.parent.GetComponent<RandomShape>())
            {
                //shapeName = this.transform.parent.GetComponent<RandomShape>().shapeName;
                if (shapeName.Contains("ConeDown"))
                {
                    if (coneDownLen != 0)
                    {
                        int assign = Random.Range(0, accStoreObj.GetComponent<AccessoryStore>().total);
                        if (assign < accStoreObj.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, coneDownLen);
                            if (accStoreObj.GetComponent<AccessoryStore>().coneDownAcc[index] != null)
                            {
                                Accessory access = accStoreObj.GetComponent<AccessoryStore>().coneDownAcc[index];
                                Debug.Log("NAME: " + access.obj.name + " POS:" + transform.position + "OFF:" + access.offset + "SUM:" + (transform.position + access.offset));

                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                accessory.transform.parent = this.transform;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                Debug.Log("NAME: " + access.obj.name + " POS:" + accessory.transform.position);

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
                            if (accStoreObj.GetComponent<AccessoryStore>().coneUpAcc[index] != null)
                            {
                                Accessory access = accStoreObj.GetComponent<AccessoryStore>().coneUpAcc[index];
                                Debug.Log("NAME: " + access.obj.name + " POS:" + transform.position + "OFF:" + access.offset + "SUM:" + (transform.position + access.offset));

                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                accessory.transform.parent = this.transform;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                Debug.Log("NAME: " + access.obj.name + " POS:" + accessory.transform.position);

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
                                Accessory access = accStoreObj.GetComponent<AccessoryStore>().egg[index];
                                Debug.Log("NAME: "+access.obj.name+" POS:" + transform.position + "OFF:" + access.offset + "SUM:" + (transform.position + access.offset));

                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                accessory.transform.parent = this.transform;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                Debug.Log("NAME: " + access.obj.name + " POS:" + accessory.transform.position);
                                
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
                            int index = Random.Range(0, coneDownLen);
                            if (accStoreObj.GetComponent<AccessoryStore>().mikeAcc[index] != null)
                            {
                                Accessory access = accStoreObj.GetComponent<AccessoryStore>().mikeAcc[index];
                                Debug.Log("NAME: " + access.obj.name + " POS:" + transform.position + "OFF:" + access.offset + "SUM:" + (transform.position + access.offset));

                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                accessory.transform.parent = this.transform;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                Debug.Log("NAME: " + access.obj.name + " POS:" + accessory.transform.position);

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
                            if (accStoreObj.GetComponent<AccessoryStore>().ikeAcc[index] != null)
                            {
                                Accessory access = accStoreObj.GetComponent<AccessoryStore>().ikeAcc[index];
                                Debug.Log("NAME: " + access.obj.name + " POS:" + transform.position + "OFF:" + access.offset + "SUM:" + (transform.position + access.offset));

                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                accessory.transform.parent = this.transform;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                Debug.Log("NAME: " + access.obj.name + " POS:" + accessory.transform.position);

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
                            if (accStoreObj.GetComponent<AccessoryStore>().hunkAcc[index] != null)
                            {
                                Accessory access = accStoreObj.GetComponent<AccessoryStore>().hunkAcc[index];
                                Debug.Log("NAME: " + access.obj.name + " POS:" + transform.position + "OFF:" + access.offset + "SUM:" + (transform.position + access.offset));

                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                accessory.transform.parent = this.transform;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                Debug.Log("NAME: " + access.obj.name + " POS:" + accessory.transform.position);

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
                            if (accStoreObj.GetComponent<AccessoryStore>().rectAcc[index] != null)
                            {
                                Accessory access = accStoreObj.GetComponent<AccessoryStore>().rectAcc[index];
                                Debug.Log("NAME: " + access.obj.name + " POS:" + transform.position + "OFF:" + access.offset + "SUM:" + (transform.position + access.offset));

                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                accessory.transform.parent = this.transform;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                Debug.Log("NAME: " + access.obj.name + " POS:" + accessory.transform.position);

                            }
                        }
                    }
                }
            }
        }
    }
}

