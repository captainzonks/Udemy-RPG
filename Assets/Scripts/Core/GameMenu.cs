using Character;
using Quest;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameMenu : MonoBehaviour
    {
        public GameObject theMenu;
        public GameObject[] windows;

        private CharStats[] playerStats;

        public Text[] nameText, hpText, mpText, lvlText, expText;
        public Slider[] expSlider;
        public Image[] charImage;
        public GameObject[] charStatHolder;

        public GameObject[] statusButtons;
        public Text statusName, statusHP, statusMP, statusStr,
            statusDef, statusWpnEqpd, statusWpnPwr, statusArmrEqpd, 
            statusArmrPwr, statusExp;
        public Image statusImage;

        public ItemButton[] itemButtons;
        public string selectedItem;
        public Item activeItem;
        public Text itemName, itemDescription, useButtonText;

        public GameObject itemCharChoiceMenu;
        public Text[] itemCharChoiceNames;

        public static GameMenu Instance;
        public Text goldText;

        private void Start()
        {
            Instance = this;
        }

        private void Update()
        {
            if (GameManager.Instance.consoleOpen) return;

            if (!Input.GetButtonDown("Fire2")) return;
            if(theMenu.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.Instance.gameMenuOpen = true;
            }
        }

        private void UpdateMainStats()
        {
            playerStats = GameManager.Instance.playerStats;

            for(var i = 0; i < playerStats.Length; i++)
            {
                if(playerStats[i].gameObject.activeInHierarchy)
                {
                    charStatHolder[i].SetActive(true);

                    nameText[i].text = playerStats[i].charName;
                    hpText[i].text = "HP: " + playerStats[i].currentHP + "/" + playerStats[i].maxHP;
                    mpText[i].text = "MP: " + playerStats[i].currentMP + "/" + playerStats[i].maxMP;
                    lvlText[i].text = "Lvl: " + playerStats[i].playerLevel;
                    expText[i].text = "" + playerStats[i].currentEXP + "/" + playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                    expSlider[i].maxValue = playerStats[i].expToNextLevel[playerStats[i].playerLevel];
                    expSlider[i].value = playerStats[i].currentEXP;
                    charImage[i].sprite = playerStats[i].charImage;
                }
                else
                {
                    charStatHolder[i].SetActive(false);
                }
            }

            goldText.text = GameManager.Instance.currentGold.ToString() + "g";
        }

        public void ToggleWindow(int windowNumber)
        {
            UpdateMainStats();

            for(var i = 0; i < windows.Length; i++)
            {
                if(i == windowNumber)
                {
                    windows[i].SetActive(!windows[i].activeInHierarchy);
                }
                else
                {
                    windows[i].SetActive(false);
                }
            }

            itemCharChoiceMenu.SetActive(false);
        }

        public void CloseMenu()
        {
            foreach (var t in windows)
            {
                t.SetActive(false);
            }

            theMenu.SetActive(false);

            GameManager.Instance.gameMenuOpen = false;

            itemCharChoiceMenu.SetActive(false);
        }

        public void OpenStatus()
        {
            UpdateMainStats();

            StatusChar(0);

            for(var i = 0; i < statusButtons.Length; i++)
            {
                statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
                statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].charName;
            }
        }

        public void StatusChar(int selected)
        {
            statusName.text = playerStats[selected].charName;
            statusHP.text = playerStats[selected].currentHP + "/" + playerStats[selected].maxHP;
            statusMP.text = playerStats[selected].currentMP + "/" + playerStats[selected].maxMP;
            statusStr.text = playerStats[selected].strength.ToString();
            statusDef.text = playerStats[selected].defense.ToString();
            
            statusWpnEqpd.text = playerStats[selected].equippedWpn != "" ? playerStats[selected].equippedWpn : "No Weapon";
            statusWpnPwr.text = playerStats[selected].wpnPwr.ToString();

            statusArmrEqpd.text = playerStats[selected].equippedArmr != "" ? playerStats[selected].equippedArmr : "No Armor";
            statusArmrPwr.text = playerStats[selected].armrPwr.ToString();

            statusExp.text = (playerStats[selected].expToNextLevel[playerStats[selected].playerLevel] - playerStats[selected].currentEXP).ToString();

            statusImage.sprite = playerStats[selected].charImage;
        }

        public void ShowItems()
        {
            GameManager.Instance.SortItems();

            for(var i = 0; i < itemButtons.Length; i++)
            {
                itemButtons[i].buttonValue = i;

                if(GameManager.Instance.itemsHeld[i] != "")
                {
                    itemButtons[i].buttonImage.gameObject.SetActive(true);
                    itemButtons[i].buttonImage.sprite = GameManager.Instance.GetItemDetails(GameManager.Instance.itemsHeld[i]).itemSprite;
                    itemButtons[i].amountText.text = GameManager.Instance.numberOfItems[i].ToString();
                }
                else
                {
                    itemButtons[i].buttonImage.gameObject.SetActive(false);
                    itemButtons[i].amountText.text = "";
                }
            }
        }

        public void SelectItem(Item newItem)
        {
            activeItem = newItem;

            if (activeItem.isItem)
            {
                useButtonText.text = "Use";
            }

            if (activeItem.isWeapon || activeItem.isArmor)
            {
                useButtonText.text = "Equip";
            }

            itemName.text = activeItem.itemName;
            itemDescription.text = activeItem.description;
        }

        public void DiscardItem()
        {
            if (activeItem != null)
            {
                GameManager.Instance.RemoveItem(activeItem.itemName);
            }
        }

        public void OpenItemCharChoice()
        {
            itemCharChoiceMenu.SetActive(true);

            for (var i = 0; i < itemCharChoiceNames.Length; i++)
            {
                itemCharChoiceNames[i].text = GameManager.Instance.playerStats[i].charName;
                itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.Instance.playerStats[i].gameObject.activeInHierarchy);
            }
        }

        private void CloseItemCharChoice()
        {
            itemCharChoiceMenu.SetActive(false);
        }

        public void UseItem(int selectChar)
        {
            activeItem.Use(selectChar);
            CloseItemCharChoice();
        }

        public void SaveGame()
        {
            GameManager.Instance.SaveData();
            QuestManager.Instance.SaveQuestData();
        }
    }
}


