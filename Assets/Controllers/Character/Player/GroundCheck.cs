﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //Script duoc dung boi Main
    public PlayerController player;


    // Use this for initialization
    void Start()
    {
        player = gameObject.GetComponent<PlayerController>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == false)
            player.grounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.isTrigger == false || collision.CompareTag("Dat"))
            player.grounded = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.isTrigger == false || collision.CompareTag("Dat"))
            player.grounded = false;
    }
}
