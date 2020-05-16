using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private bool canOpen;

    public string[] itemsForSale = new string[40];

    // Update is called once per frame
    private void Update()
    {
        if (!canOpen || !Input.GetButtonDown("Fire1") || !PlayerController.instance.canMove ||
            Shop.instance.shopMenu.activeInHierarchy) return;
        Shop.instance.itemsForSale = itemsForSale;

        Shop.instance.OpenShop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = true;
        }
    }    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
        }
    }
}
