using System.Linq;
using Character;
using Movement;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance { get; private set; }

        [SerializeField] private CharStats[] playerStats;

        [SerializeField] private bool gameMenuOpen, dialogActive, fadingBetweenAreas, shopActive;

        [SerializeField] private string[] itemsHeld;
        [SerializeField] private int[] numberOfItems;
        [SerializeField] private Item[] referenceItems;

        [SerializeField] private int currentGold;

        private void Start()
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            SortItems();
        }

        private void Update()
        {
            if(gameMenuOpen || dialogActive || fadingBetweenAreas || shopActive)
            {
                PlayerController.Instance.ModifyMovement(false);
            }
            else
            {
                PlayerController.Instance.ModifyMovement(true);
            }

            // debug test
            if (!Input.GetKeyDown(KeyCode.J)) return;
            AddItem("Iron Armor");

            RemoveItem("Health Potion");
        }

        public CharStats[] GetPlayerStats()
        {
            return playerStats;
        }

        public bool GameMenuOpen()
        {
            return gameMenuOpen;
        }

        public void ModifyGameMenu(bool menu)
        {
            gameMenuOpen = menu;
        }

        public bool DialogActive()
        {
            return dialogActive;
        }

        public void ModifyDialogActive(bool active)
        {
            dialogActive = active;
        }

        public bool FadingBetweenAreas()
        {
            return fadingBetweenAreas;
        }

        public void ModifyFading(bool fading)
        {
            fadingBetweenAreas = fading;
        }

        public bool ShopActive()
        {
            return shopActive;
        }

        public void SetShopActive(bool active)
        {
            shopActive = active;
        }

        public string[] ItemsHeld()
        {
            return itemsHeld;
        }

        public int[] NumberOfItems()
        {
            return numberOfItems;
        }

        public Item[] ReferenceItems()
        {
            return referenceItems;
        }

        public int CurrentGold()
        {
            return currentGold;
        }

        public void SubtractGold(int change)
        {
            currentGold -= change;
        }

        public void AddGold(int change)
        {
            currentGold += change;
        }

        public Item GetItemDetails(string itemToGrab)
        {
            return referenceItems.FirstOrDefault(t => t.ItemName == itemToGrab);
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
                    if (referenceItems[i].ItemName != itemToAdd) continue;
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