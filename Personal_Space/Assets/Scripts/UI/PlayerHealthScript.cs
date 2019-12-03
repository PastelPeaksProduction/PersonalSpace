using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    private PlayerController player;

    public bool isBottom;

    public Sprite twenty;
    public Sprite fourty;
    public Sprite sixty;
    public Sprite eighty;
    public Sprite hundred;

    public Sprite twentySingle;
    public Sprite fourtySingle;
    public Sprite sixtySingle;
    public Sprite eightySingle;
    public Sprite hundredSingle;

    private GameController controller;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag ( "Player" ).GetComponent<PlayerController>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
    }

    void Update()
    {          
        if (isBottom )
        {
            BottomHealth();
        }
        else
        {
            MenuHealth();
        }
    }

    private void BottomHealth()
    {
        gameObject.SetActive(isBottom && !controller.isGamePaused());

        if (player.health <= 100 && player.health > 80)
        {
            GetComponent<Image>().sprite = hundredSingle;
        }
        else if (player.health <= 80 && player.health > 60)
        {
            GetComponent<Image>().sprite = eightySingle;
        }
        else if (player.health <= 60 && player.health > 40)
        {
            GetComponent<Image>().sprite = sixtySingle;
        }
        else if (player.health <= 40 && player.health > 20)
        {
            GetComponent<Image>().sprite = fourtySingle;
        }
        else if (player.health <= 20)
        {
            GetComponent<Image>().sprite = twentySingle;
        }
    }
    private void MenuHealth()
    {
        if (player.health <= 100 && player.health > 80)
        {
            GetComponent<Image>().sprite = hundred;
        }
        else if (player.health <= 80 && player.health > 60)
        {
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
