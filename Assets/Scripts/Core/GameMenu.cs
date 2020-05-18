using Character;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private GameObject theMenu;
        [SerializeField] private GameObject[] windows;

        private CharStats[] playerStats;

        [SerializeField] private Text[] nameText, hpText, mpText, lvlText, expText;
        [SerializeField] private Slider[] expSlider;
        [SerializeField] private Image[] charImage;
        [SerializeField] private GameObject[] charStatHolder;

        [SerializeField] private GameObject[] statusButtons;

        [SerializeField] private Text statusName, statusHP, statusMP, statusStr,
            statusDef, statusWpnEqpd, statusWpnPwr, statusArmrEqpd, 
            statusArmrPwr, statusExp;

        [SerializeField] private Image statusImage;

        [SerializeField] private ItemButton[] itemButtons;
        [SerializeField] private string selectedItem;
        [SerializeField] private Item activeItem;
        [SerializeField] private Text itemName, itemDescription, useButtonText;

        [SerializeField] private GameObject itemCharChoiceMenu;
        [SerializeField] private Text[] itemCharChoiceNames;

        public static GameMenu Instance { get; set; }
        [SerializeField] private Text goldText;

        private void Start()
        {
            Instance = this;
        }

        private void Update()
        {
            if (!Input.GetButtonDown("Fire2")) return;
            if(theMenu.activeInHierarchy)
            {
                CloseMenu();
            }
            else
            {
                theMenu.SetActive(true);
                UpdateMainStats();
                GameManager.Instance.ModifyGameMenu(true);
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

        private void UpdateMainStats()
        {
            playerStats = GameManager.Instance.GetPlayerStats();

            for(var i = 0; i < playerStats.Length; i++)
            {
                if(playerStats[i].gameObject.activeInHierarchy)
                {
                    charStatHolder[i].SetActive(true);

                    nameText[i].text = playerStats[i].CharName;
                    hpText[i].text = "HP: " + playerStats[i].CurrentHp + "/" + playerStats[i].MaxHp;
                    mpText[i].text = "MP: " + playerStats[i].CurrentMp + "/" + playerStats[i].MaxMp;
                    lvlText[i].text = "Lvl: " + playerStats[i].PlayerLevel;
                    expText[i].text = "" + playerStats[i].CurrentExp + "/" + playerStats[i].ExpToNextLevel[playerStats[i].PlayerLevel];
                    expSlider[i].maxValue = playerStats[i].ExpToNextLevel[playerStats[i].PlayerLevel];
                    expSlider[i].value = playerStats[i].CurrentExp;
                    charImage[i].sprite = playerStats[i].CharImage;
                }
                else
                {
                    charStatHolder[i].SetActive(false);
                }
            }

            goldText.text = GameManager.Instance.CurrentGold() + "g";
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

            GameManager.Instance.ModifyGameMenu(false);

            itemCharChoiceMenu.SetActive(false);
        }

        public void OpenStatus()
        {
            UpdateMainStats();

            StatusChar(0);

            for(var i = 0; i < statusButtons.Length; i++)
            {
                statusButtons[i].SetActive(playerStats[i].gameObject.activeInHierarchy);
                statusButtons[i].GetComponentInChildren<Text>().text = playerStats[i].CharName;
            }
        }

        public void StatusChar(int selected)
        {
            statusName.text = playerStats[selected].CharName;
            statusHP.text = playerStats[selected].CurrentHp + "/" + playerStats[selected].MaxHp;
            statusMP.text = playerStats[selected].CurrentMp + "/" + playerStats[selected].MaxMp;
            statusStr.text = playerStats[selected].Strength.ToString();
            statusDef.text = playerStats[selected].Defense.ToString();
            
            if(playerStats[selected].EquippedWpn != "")
            {
                statusWpnEqpd.text = playerStats[selected].EquippedWpn;
            }
            statusWpnPwr.text = playerStats[selected].WpnPwr.ToString();

            if(playerStats[selected].EquippedArmr != "")
            {
                statusArmrEqpd.text = playerStats[selected].EquippedArmr;
            }
            statusArmrPwr.text = playerStats[selected].ArmrPwr.ToString();

            statusExp.text = (playerStats[selected].ExpToNextLevel[playerStats[selected].PlayerLevel] - playerStats[selected].CurrentExp).ToString();

            statusImage.sprite = playerStats[selected].CharImage;
        }

        public void ShowItems()
        {
            GameManager.Instance.SortItems();

            for(var i = 0; i < itemButtons.Length; i++)
            {
                itemButtons[i].ButtonValue = i;

                if(GameManager.Instance.ItemsHeld()[i] != "")
                {
                    itemButtons[i].ButtonImage.gameObject.SetActive(true);
                    itemButtons[i].ButtonImage.sprite = GameManager.Instance.GetItemDetails(GameManager.Instance.ItemsHeld()[i]).ItemSprite;
                    itemButtons[i].AmountText.text = GameManager.Instance.NumberOfItems()[i].ToString();
                }
                else
                {
                    itemButtons[i].ButtonImage.gameObject.SetActive(false);
                    itemButtons[i].AmountText.text = "";
                }
            }
        }

        public void SelectItem(Item newItem)
        {
            activeItem = newItem;

            if (activeItem.IsItem)
            {
                useButtonText.text = "Use";
            }

            if (activeItem.IsWeapon || activeItem.IsArmor)
            {
                useButtonText.text = "Equip";
            }

            itemName.text = activeItem.ItemName;
            itemDescription.text = activeItem.Description;
        }

        public void DiscardItem()
        {
            if (activeItem != null)
            {
                GameManager.Instance.RemoveItem(activeItem.ItemName);
            }
        }

        public void OpenItemCharChoice()
        {
            itemCharChoiceMenu.SetActive(true);

            for (var i = 0; i < itemCharChoiceNames.Length; i++)
            {
                itemCharChoiceNames[i].text = GameManager.Instance.GetPlayerStats()[i].CharName;
                itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.Instance.GetPlayerStats()[i].gameObject.activeInHierarchy);
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
    }
}


