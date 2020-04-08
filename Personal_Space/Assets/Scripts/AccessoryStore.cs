using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessoryStore : MonoBehaviour
{
    [Header("Headwear")]
    public GameObject[] headwear;
    [Header("Hair")]
    public int hasHair;
    public int totalHair;
    public GameObject[] hair;
    [Header("Eyewear")]
    public int hasEyewear;
    public int totalEyewear;
    public GameObject[] eyewear;
    [Header("Noses")]
    public GameObject[] noses;
    [Header("Ties")]
    public int hasTie;
    public int totalTies;
    public GameObject[] ties;
    [Header("Badges")]
    public int hasBadge;
    public int totalBadge;
    public GameObject[] badges;
    [Header("FacialHair")]
    public int hasFacialHair;
    public int totalFacialHair;
    public GameObject[] facialhair;
}
