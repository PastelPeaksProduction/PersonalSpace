using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    private PlayerController player;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag ( "Player" ).GetComponent<PlayerController>();
    }

    void Update()
    {

        this.GetComponent<Slider>().value = player.health;
    }
}
