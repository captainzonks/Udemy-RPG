﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private bool canPickup;

    // Update is called once per frame
    private void Update()
    {
        if (!canPickup || !Input.GetButtonDown("Fire1") || !PlayerController.instance.canMove) return;
        GameManager.instance.AddItem(GetComponent<Item>().itemName);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
        }
    }
}
