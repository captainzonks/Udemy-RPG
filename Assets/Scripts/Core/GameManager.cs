using System.Linq;
using Character;
using Movement;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            // debug for saving and loading data
            if (Input.GetKeyDown(KeyCode.O))
            {
                SaveData();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                LoadData();
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

        public void SaveData()
        {
            // save player position
            PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
            var position = PlayerController.Instance.transform.position;
            PlayerPrefs.SetFloat("Player_Position_X", position.x);
            PlayerPrefs.SetFloat("Player_Position_Y", position.y);
            PlayerPrefs.SetFloat("Player_Position_Z", position.z);

            // save player data
            foreach (var character in playerStats)
            {
                PlayerPrefs.SetInt("Player_" + character.charName + "_Active", character.gameObject.activeInHierarchy ? 1 : 0);

                PlayerPrefs.SetInt("Player_" + character.charName + "_Level", character.playerLevel);
                PlayerPrefs.SetInt("Player_" + character.charName + "_CurrentExp", character.currentEXP);
                PlayerPrefs.SetInt("Player_" + character.charName + "_CurrentHP", character.currentHP);
                PlayerPrefs.SetInt("Player_" + character.charName + "_MaxHP", character.maxHP);
                PlayerPrefs.SetInt("Player_" + character.charName + "_CurrentMP", character.currentMP);
                PlayerPrefs.SetInt("Player_" + character.charName + "_MaxMP", character.maxMP);
                PlayerPrefs.SetInt("Player_" + character.charName + "_Strength", character.strength);
                PlayerPrefs.SetInt("Player_" + character.charName + "_Defense", character.defense);
                PlayerPrefs.SetInt("Player_" + character.charName + "_WpnPwr", character.wpnPwr);
                PlayerPrefs.SetInt("Player_" + character.charName + "_ArmrPwr", character.armrPwr);
                PlayerPrefs.SetString("Player_" + character.charName + "_EquippedWpn", character.equippedWpn);
                PlayerPrefs.SetString("Player_" + character.charName + "_EquippedArmr", character.equippedArmr);
            }

            // save inventory data
            for (var i = 0; i < itemsHeld.Length; i++)
            {
                PlayerPrefs.SetString("ItemInInventory_" + i, itemsHeld[i]);
                PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItems[i]);
            }
        }

        public void LoadData()
        {
            // load character position
            PlayerController.Instance.transform.position = new Vector3(
                PlayerPrefs.GetFloat("Player_Position_X"),
                PlayerPrefs.GetFloat("Player_Position_Y"),
                PlayerPrefs.GetFloat("Player_Position_Z")
                );

            // load character data
            foreach (var character in playerStats)
            {
                character.gameObject
                    .SetActive(PlayerPrefs.GetInt("Player_" + character.charName + "_Active") != 0);

                character.playerLevel = PlayerPrefs.GetInt("Player_" + character.charName + "_Level");
                character.currentEXP = PlayerPrefs.GetInt("Player_" + character.charName + "_CurrentExp");
                character.currentHP = PlayerPrefs.GetInt("Player_" + character.charName + "_CurrentHP");
                character.maxHP = PlayerPrefs.GetInt("Player_" + character.charName + "_MaxHP");
                character.currentMP = PlayerPrefs.GetInt("Player_" + character.charName + "_CurrentMP");
                character.maxMP = PlayerPrefs.GetInt("Player_" + character.charName + "_MaxMP");
                character.strength = PlayerPrefs.GetInt("Player_" + character.charName + "_Strength");
                character.defense = PlayerPrefs.GetInt("Player_" + character.charName + "_Defense");
                character.wpnPwr = PlayerPrefs.GetInt("Player_" + character.charName + "_WpnPwr");
                character.armrPwr = PlayerPrefs.GetInt("Player_" + character.charName + "_ArmrPwr");
                character.equippedWpn = PlayerPrefs.GetString("Player_" + character.charName + "_EquippedWpn");
                character.equippedArmr = PlayerPrefs.GetString("Player_" + character.charName + "_EquippedArmr");
            }

            // load character inventory data
            for (var i = 0; i < itemsHeld.Length; i++)
            {
                itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
                numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
            }
        }
    }
}