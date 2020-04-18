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
        AccessoryStore accStore = GameObject.Find("ShapeBased").GetComponent<AccessoryStore>();
        //AccessoryStore accStore = (AccessoryStore) accStoreObj.GetComponent("AccessoryStore");
        if (accStore != null)
        {
            int eggLen = accStore.egg.Length;
            int coneUpLen = accStore.coneUpAcc.Length;
            int coneDownLen = accStore.coneDownAcc.Length;
            int mikeLen = accStore.mikeAcc.Length;
            int ikeLen = accStore.ikeAcc.Length;
            int hunkLen = accStore.hunkAcc.Length;
            int rectLen = accStore.rectAcc.Length;
            int tabLen = accStore.tabAcc.Length;



            //if (this.transform.parent.GetComponent<RandomShape>())
            {
                //shapeName = this.transform.parent.GetComponent<RandomShape>().shapeName;
                if (shapeName.Contains("ConeDown"))
                {
                    if (coneDownLen != 0)
                    {
                        int assign = Random.Range(0, accStore.GetComponent<AccessoryStore>().total);
                        if (assign < accStore.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, coneDownLen);
                            if (accStore.GetComponent<AccessoryStore>().coneDownAcc[index] != null)
                            {
                                Accessory access = accStore.GetComponent<AccessoryStore>().coneDownAcc[index];


                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                Transform[] children = this.transform.parent.gameObject.GetComponentsInChildren<Transform>();
                                Transform parent = this.transform;

                                foreach (Transform c in children)
                                {
                                    if (access.isHat && c.gameObject.tag == "Head")
                                    {
                                        parent = c;
                                        break;
                                    }
                                    else if (!access.isHat && c.gameObject.tag == "Waist")
                                    {
                                        parent = c;
                                        break;
                                    }
                                }

                                accessory.transform.parent = parent;

                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                
                            }
                        }
                    }
                }
                else if (shapeName.Contains("ConeUp"))
                {
                    if (coneUpLen != 0)
                    {
                        int assign = Random.Range(0, accStore.GetComponent<AccessoryStore>().total);
                        if (assign < accStore.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, coneUpLen);
                            if (accStore.GetComponent<AccessoryStore>().coneUpAcc[index] != null)
                            {
                                Accessory access = accStore.GetComponent<AccessoryStore>().coneUpAcc[index];
                               
                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                Transform[] children = this.transform.parent.gameObject.GetComponentsInChildren<Transform>();
                                Transform parent = this.transform;

                                foreach (Transform c in children)
                                {
                                    if (access.isHat && c.gameObject.tag == "Head")
                                    {
                                        parent = c;
                                    }
                                    else if (!access.isHat && c.gameObject.tag == "Waist")
                                    {
                                        parent = c;
                                    }
                                }

                                accessory.transform.parent = parent;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                
                            }
                        }
                    }
                }
                else if (shapeName.Contains("Egg"))
                {
                    if (eggLen != 0)
                    {
                        int assign = Random.Range(0, accStore.GetComponent<AccessoryStore>().total);
                        if (assign < accStore.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, eggLen);
                            if (accStore.GetComponent<AccessoryStore>().egg[index] != null)
                            {
                                Accessory access = accStore.GetComponent<AccessoryStore>().egg[index];
                                
                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                Transform[] children = this.transform.parent.gameObject.GetComponentsInChildren<Transform>();
                                Transform parent = this.transform;

                                foreach (Transform c in children)
                                {
                                    if (access.isHat && c.gameObject.tag == "Head")
                                    {
                                        parent = c;
                                    }
                                    else if (!access.isHat && c.gameObject.tag == "Waist")
                                    {
                                        parent = c;
                                    }
                                }

                                accessory.transform.parent = parent;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                
                            }
                        }
                    }
                }
                else if (shapeName.Contains("Mike"))
                {
                    if (mikeLen != 0)
                    {
                        int assign = Random.Range(0, accStore.GetComponent<AccessoryStore>().total);
                        if (assign < accStore.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, mikeLen);
                            if (accStore.GetComponent<AccessoryStore>().mikeAcc[index] != null)
                            {
                                Accessory access = accStore.GetComponent<AccessoryStore>().mikeAcc[index];
                                
                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                Transform[] children = this.transform.parent.gameObject.GetComponentsInChildren<Transform>();
                                Transform parent = this.transform;

                                foreach (Transform c in children)
                                {
                                    if (access.isHat && c.gameObject.tag == "Head")
                                    {
                                        parent = c;
                                    }
                                    else if (!access.isHat && c.gameObject.tag == "Waist")
                                    {
                                        parent = c;
                                    }
                                }

                                accessory.transform.parent = parent;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                
                            }
                        }
                    }
                }
                else if (shapeName.Contains("Ike"))
                {
                    if (ikeLen != 0)
                    {
                        int assign = Random.Range(0, accStore.GetComponent<AccessoryStore>().total);
                        if (assign < accStore.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, ikeLen);
                            if (accStore.GetComponent<AccessoryStore>().ikeAcc[index] != null)
                            {
                                Accessory access = accStore.GetComponent<AccessoryStore>().ikeAcc[index];
                                
                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                Transform[] children = this.transform.parent.gameObject.GetComponentsInChildren<Transform>();
                                Transform parent = this.transform;

                                foreach (Transform c in children)
                                {
                                    if (access.isHat && c.gameObject.tag == "Head")
                                    {
                                        parent = c;
                                    }
                                    else if (!access.isHat && c.gameObject.tag == "Waist")
                                    {
                                        parent = c;
                                    }
                                }

                                accessory.transform.parent = parent;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                               
                            }
                        }
                    }
                }
                else if (shapeName.Contains("Hunk"))
                {
                    if (hunkLen != 0)
                    {
                        int assign = Random.Range(0, accStore.GetComponent<AccessoryStore>().total);
                        if (assign < accStore.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, hunkLen);
                            if (accStore.GetComponent<AccessoryStore>().hunkAcc[index] != null)
                            {
                                Accessory access = accStore.GetComponent<AccessoryStore>().hunkAcc[index];


                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                Transform[] children = this.transform.parent.gameObject.GetComponentsInChildren<Transform>();
                                Transform parent = this.transform;

                                foreach (Transform c in children)
                                {
                                    if (access.isHat && c.gameObject.tag == "Head")
                                    {
                                        parent = c;
                                    }
                                    else if (!access.isHat && c.gameObject.tag == "Waist")
                                    {
                                        parent = c;
                                    }
                                }

                                accessory.transform.parent = parent;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                
                            }
                        }
                    }
                }
                else if (shapeName.Contains("Rect"))
                {
                    if (rectLen != 0)
                    {
                        int assign = Random.Range(0, accStore.GetComponent<AccessoryStore>().total);
                        if (assign < accStore.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, rectLen);
                            if (accStore.GetComponent<AccessoryStore>().rectAcc[index] != null)
                            {
                                Accessory access = accStore.GetComponent<AccessoryStore>().rectAcc[index];
                                
                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                Transform[] children = this.transform.parent.gameObject.GetComponentsInChildren<Transform>();
                                Transform parent = this.transform;

                                foreach (Transform c in children)
                                {
                                    if (access.isHat && c.gameObject.tag == "Head")
                                    {
                                        parent = c;
                                    }
                                    else if (!access.isHat && c.gameObject.tag == "Waist")
                                    {
                                        parent = c;
                                    }
                                }

                                accessory.transform.parent = parent;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                
                            }
                        }
                    }
                }
                else if (shapeName.Contains("Tab"))
                {
                    if (tabLen != 0)
                    {
                        int assign = Random.Range(0, accStore.GetComponent<AccessoryStore>().total);
                        if (assign < accStore.GetComponent<AccessoryStore>().hasAccessory)
                        {
                            int index = Random.Range(0, tabLen);
                            if (accStore.GetComponent<AccessoryStore>().tabAcc[index] != null)
                            {
                                Accessory access = accStore.GetComponent<AccessoryStore>().tabAcc[index];
                                
                                var accessory = Instantiate(access.obj, transform.position, transform.rotation);
                                Transform[] children = this.transform.parent.gameObject.GetComponentsInChildren<Transform>();
                                Transform parent = this.transform;

                                foreach (Transform c in children)
                                {
                                    if (access.isHat && c.gameObject.tag == "Head")
                                    {
                                        parent = c;
                                    }
                                    else if (!access.isHat && c.gameObject.tag == "Waist")
                                    {
                                        parent = c;
                                    }
                                }

                                accessory.transform.parent = parent;
                                accessory.transform.localPosition = accessory.transform.localPosition + (access.offset);
                                
                            }
                        }
                    }
                }
            }
        }
    }
}

