using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    private PlayerController player;

    public Sprite twenty;
    public Sprite fourty;
    public Sprite sixty;
    public Sprite eighty;
    public Sprite hundred;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag ( "Player" ).GetComponent<PlayerController>();
    }

    void Update()
    {
        if(player.health <= 100 && player.health > 80)
        {
            GetComponent<Image>().sprite = hundred;
        }
        else if(player.health <= 80 && player.health > 60)
        {
            Debug.Log("80");
            GetComponent<Image>().sprite = eighty;
        }
        else if (player.health <= 60 && player.health > 40)
        {
            GetComponent<Image>().sprite = sixty;
        }
        else if (player.health <= 40 && player.health > 20)
        {
            GetComponent<Image>().sprite = fourty;
        }
        else if (player.health <= 20)
        {
            GetComponent<Image>().sprite = twenty;
        }
    }
}
