﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMenuOpen || dialogActive || fadingBetweenAreas)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
    }

    public Item GetItemDetails(string itemToGrab)
    {
        foreach (var t in referenceItems)
        {
            if(t.itemName == itemToGrab)
            {
                return t;
            }
        }

        return null;
    }

    public void SortItems()
    {
        var itemAfterSpace = true;

        while (itemAfterSpace)
        {
            itemAfterSpace = false;
            for (var i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] != "") continue;
                itemsHeld[i] = itemsHeld[i + 1];
                itemsHeld[i + 1] = "";

                numberOfItems[i] = numberOfItems[i + 1];
                numberOfItems[i + 1] = 0;

                if (itemsHeld[i] != "")
                {
                    itemAfterSpace = true;
                }
            }
        }
    }
}
