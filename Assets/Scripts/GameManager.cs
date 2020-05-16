using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas, shopActive;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    public int currentGold;

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        SortItems();
    }

    // Update is called once per frame
    private void Update()
    {
        if(gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }

        // debug test
        if (!Input.GetKeyDown(KeyCode.J)) return;
        AddItem("Iron Armor");
        AddItem("Fooey");

        RemoveItem("Health Potion");
        RemoveItem("Falsey");
    }

    public Item GetItemDetails(string itemToGrab)
    {
        return referenceItems.FirstOrDefault(t => t.itemName == itemToGrab);
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

    public void AddItem(string itemToAdd)
    {
        var newItemPosition = 0;
        var foundSpace = false;

        for (var i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] != "" && itemsHeld[i] != itemToAdd) continue;
            newItemPosition = i;
            i = itemsHeld.Length;
            foundSpace = true;
        }

        if (foundSpace)
        {
            var itemExists = false;
            for (var i = 0; i < referenceItems.Length; i++)
            {
                if (referenceItems[i].itemName != itemToAdd) continue;
                itemExists = true;

                i = referenceItems.Length;
            }

            if (itemExists)
            {
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            }
            else
            {
                Debug.LogError(itemToAdd + " Does Not Exist!!");
            }
        }

        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemToRemove)
    {
        var foundItem = false;
        var itemPosition = 0;

        for (var i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] != itemToRemove) continue;
            foundItem = true;
            itemPosition = i;

            i = itemsHeld.Length;
        }

        if (foundItem)
        {
            numberOfItems[itemPosition]--;

            if (numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }

            GameMenu.instance.ShowItems();
        }
        else
        {
            Debug.LogError("Could't find " + itemToRemove);
        }
    }
}
