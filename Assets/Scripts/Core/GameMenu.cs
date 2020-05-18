using Character;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] GameObject theMenu;
        [SerializeField] GameObject[] windows;

        CharStats[] playerStats;

        [SerializeField] Text[] nameText, hpText, mpText, lvlText, expText;
        [SerializeField] Slider[] expSlider;
        [SerializeField] Image[] charImage;
        [SerializeField] GameObject[] charStatHolder;

        [SerializeField] GameObject[] statusButtons;

        [SerializeField] Text statusName, statusHP, statusMP, statusStr,
            statusDef, statusWpnEqpd, statusWpnPwr, statusArmrEqpd, 
            statusArmrPwr, statusExp;

        [SerializeField] Image statusImage;

        [SerializeField] ItemButton[] itemButtons;
        [SerializeField] string selectedItem;
        [SerializeField] Item activeItem;
        [SerializeField] Text itemName, itemDescription, useButtonText;

        [SerializeField] GameObject itemCharChoiceMenu;
        [SerializeField] Text[] itemCharChoiceNames;

        public static GameMenu instance;
        [SerializeField] Text goldText;

        void Start()
        {
            instance = this;
        }

        void Update()
        {
            if (!Input.GetButtonDown("Fire2")) return;
            if(theMenu.activeInHierarchy)
            {
                //theMenu.SetActive(false);
                //GameManager.instance.gameMenuOpen = false;
                CloseMenu();
            }
            else
            {
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.instance.ModifyGameMenu(true);
            }
        }

        public GameObject GetTheMenu()
        {
            return theMenu;
        }

        public GameObject[] GetWindows()
        {
            return windows;
        }

        public CharStats[] GetCharStats()
        {
            return playerStats;
        }

        public Text[] GetNameText()
        {
            return nameText;
        }

        public Text[] GetHPText()
        {
            return hpText;
        }

        public Text[] GetMPText()
        {
            return mpText;
        }

        public Text[] GetLvlText()
        {
            return lvlText;
        }

        public Text[] GetExpText()
        {
            return expText;
        }

        public Slider[] GetExpSlider()
        {
            return expSlider;
        }

        public Image[] GetImage()
        {
            return charImage;
        }

        public GameObject[] GetCharStatHolder()
        {
            return charStatHolder;
        }

        public GameObject[] GetStatusButtons()
        {
            return statusButtons;
        }

        public Text GetStatusName()
        {
            return statusName;
        }

        public Text GetStatusHP()
        {
            return statusHP;
        }

        public Text GetStatusMP()
        {
            return statusMP;
        }

        public Text GetStatusStr()
        {
            return statusStr;
        }

        public Text GetStatusDef()
        {
            return statusDef;
        }

        public Text GetStatusWpnEqpd()
        {
            return statusWpnEqpd;
        }

        public Text GetStatusWpnPwr()
        {
            return statusWpnPwr;
        }

        public Text GetStatusArmrEqpd()
        {
            return statusArmrEqpd;
        }

        public Text GetStatusArmrPwr()
        {
            return statusArmrPwr;
        }

        public Text GetStatusExp()
        {
            return statusExp;
        }

        public Image GetStatusImage()
        {
            return statusImage;
        }



        void UpdateMainStats()
        {
            playerStats = GameManager.instance.GetPlayerStats();

            for(var i = 0; i < playerStats.Length; i++)
            {
                if(playerStats[i].gameObject.activeInHierarchy)
                {
                    charStatHolder[i].SetActive(true);

                    nameText[i].text = playerStats[i].GetCharName();
                    hpText[i].text = "HP: " + playerStats[i].GetCurrentHP() + "/" + playerStats[i].GetMaxHP();
                    mpText[i].text = "MP: " + playerStats[i].GetCurrentMP() + "/" + playerStats[i].GetMaxMP();
                    lvlText[i].text = "Lvl: " + playerStats[i].GetPlayerLevel();
                    expText[i].text = "" + playerStats[i].GetCurrentXP() + "/" + playerStats[i].GetXPToNextLevel(playerStats[i].GetPlayerLevel());
                    expSlider[i].maxValue = playerStats[i].GetXPToNextLevel(playerStats[i].GetPlayerLevel());
                    expSlider[i].value = playerStats[i].GetCurrentXP();
                    charImage[i].sprite = playerStats[i].GetSprite();
                }
                else
                {
                    charStatHolder[i].SetActive(false);
                }
            }

            goldText.text = GameManager.instance.CurrentGold().ToString() + "g";
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

            GameManager.instance.ModifyGameMenu(false);

            itemCharChoiceMenu.SetActive(false);
        }

        public void OpenStatus()
        {
            UpdateMainStats();

            StatusChar(0);

            for(var i = 0; i < statusButtons.Length; i++)
            {
                statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
                statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].GetCharName();
            }
        }

        public void StatusChar(int selected)
        {
            statusName.text = playerStats[selected].GetCharName();
            statusHP.text = playerStats[selected].GetCurrentHP() + "/" + playerStats[selected].GetMaxHP();
            statusMP.text = playerStats[selected].GetCurrentMP() + "/" + playerStats[selected].GetMaxMP();
            statusStr.text = playerStats[selected].GetStrength().ToString();
            statusDef.text = playerStats[selected].GetDefense().ToString();
            
            if(playerStats[selected].GetWeapon() != "")
            {
                statusWpnEqpd.text = playerStats[selected].GetWeapon();
            }
            statusWpnPwr.text = playerStats[selected].GetWpnPwr().ToString();

            if(playerStats[selected].GetArmor() != "")
            {
                statusArmrEqpd.text = playerStats[selected].GetArmor();
            }
            statusArmrPwr.text = playerStats[selected].GetArmrPwr().ToString();

            statusExp.text = (playerStats[selected].GetXPToNextLevel(playerStats[selected].GetPlayerLevel()) - playerStats[selected].GetCurrentXP()).ToString();

            statusImage.sprite = playerStats[selected].GetSprite();
        }

        public void ShowItems()
        {
            GameManager.instance.SortItems();

            for(var i = 0; i < itemButtons.Length; i++)
            {
                itemButtons[i].SetButtonValue(i);

                if(GameManager.instance.ItemsHeld()[i] != "")
                {
                    itemButtons[i].GetButtonImage().gameObject.SetActive(true);
                    itemButtons[i].GetButtonImage().sprite = GameManager.instance.GetItemDetails(GameManager.instance.ItemsHeld()[i]).GetItemSprite();
                    itemButtons[i].GetAmountText().text = GameManager.instance.NumberOfItems()[i].ToString();
                }
                else
                {
                    itemButtons[i].GetButtonImage().gameObject.SetActive(false);
                    itemButtons[i].GetAmountText().text = "";
                }
            }
        }

        public void SelectItem(Item newItem)
        {
            activeItem = newItem;

            if (activeItem.IsItem())
            {
                useButtonText.text = "Use";
            }

            if (activeItem.IsWeapon() || activeItem.IsArmor())
            {
                useButtonText.text = "Equip";
            }

            itemName.text = activeItem.GetItemName();
            itemDescription.text = activeItem.GetDescription();
        }

        public void DiscardItem()
        {
            if (activeItem != null)
            {
                GameManager.instance.RemoveItem(activeItem.GetItemName());
            }
        }

        public void OpenItemCharChoice()
        {
            itemCharChoiceMenu.SetActive(true);

            for (var i = 0; i < itemCharChoiceNames.Length; i++)
            {
                itemCharChoiceNames[i].text = GameManager.instance.GetPlayerStats()[i].GetCharName();
                itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.GetPlayerStats()[i].gameObject.activeInHierarchy);
            }
        }

        void CloseItemCharChoice()
        {
            itemCharChoiceMenu.SetActive(false);
        }

        public void UseItem(int selectChar)
        {
            activeItem.Use(selectChar);
            CloseItemCharChoice();
        }
    }
}


