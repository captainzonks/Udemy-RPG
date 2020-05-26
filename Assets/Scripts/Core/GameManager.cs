using System.Linq;
using Character;
using Movement;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;

        public CharStats[] playerStats;

        public bool gameMenuOpen, dialogActive, fadingBetweenAreas, shopActive, consoleOpen;

        public string[] itemsHeld;
        public int[] numberOfItems;
        public Item[] referenceItems;

        public int currentGold;

        private void Start()
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            SortItems();
        }

        private void Update()
        {
            if (gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive || consoleOpen)
            {
                PlayerController.Instance.currentState = PlayerState.Interact;
            }
            else
            {
                if (PlayerController.Instance.currentState != PlayerState.Attack)
                    PlayerController.Instance.currentState = PlayerState.Walk;
            }
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

            GameMenu.Instance.ShowItems();
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

                GameMenu.Instance.ShowItems();
            }
            else
            {
                Debug.LogError("Could't find " + itemToRemove);
            }
        }
    }
}