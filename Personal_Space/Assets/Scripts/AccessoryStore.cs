using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Accessory
{
    public GameObject obj;
    public Vector3 offset;
    public bool isHat;
}

public class AccessoryStore : MonoBehaviour
{
    
    
    public int hasAccessory;
    public int total;

    public Accessory[] egg;

    public Accessory[] coneUpAcc;

    public Accessory[] coneDownAcc;

    public Accessory[] mikeAcc;

    public Accessory[] ikeAcc;

    public Accessory[] hunkAcc;

    public Accessory[] rectAcc;

    public Accessory[] tabAcc;

}
