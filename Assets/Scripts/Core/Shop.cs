using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class Shop : MonoBehaviour
    {
        public static Shop instance;

        public GameObject shopMenu;
        public GameObject buyMenu;
        public GameObject sellMenu;

        public Text goldText;

        public string[] itemsForSale;

        public ItemButton[] buyItemButtons;
        public ItemButton[] sellItemButtons;

        public Item selectedItem;
        public Text buyItemName, buyItemDescription, buyItemValue;
        public Text sellItemName, sellItemDescription, sellItemValue;

        // Start is called before the first frame update
        private void Start()
        {
            instance = this;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K) && !shopMenu.activeInHierarchy)
            {
                OpenShop();
            }
        }

        public void OpenShop()
        {
            shopMenu.SetActive(true);
            OpenBuyMenu();

            GameManager.instance.SetShopActive(true);

            goldText.text = GameManager.instance.CurrentGold() + "g";
        }

        public void CloseShop()
        {
            shopMenu.SetActive(false);

            GameManager.instance.SetShopActive(false);
        }

        public void OpenBuyMenu()
        {
            buyItemButtons[0].Press();

            buyMenu.SetActive(true);
            sellMenu.SetActive(false);

            for (var i = 0; i < buyItemButtons.Length; i++)
            {
                buyItemButtons[i].SetButtonValue(i);

                if (itemsForSale[i] != "")
                {
                    buyItemButtons[i].GetButtonImage().gameObject.SetActive(true);
                    buyItemButtons[i].GetButtonImage().sprite =
                        GameManager.instance.GetItemDetails(itemsForSale[i]).GetItemSprite();
                    buyItemButtons[i].GetAmountText().text = "";
                }
                else
                {
                    buyItemButtons[i].GetButtonImage().gameObject.SetActive(false);
                    buyItemButtons[i].GetAmountText().text = "";
                }
            }
        }

        public void OpenSellMenu()
        {
            sellItemButtons[0].Press();

            sellMenu.SetActive(true);
            buyMenu.SetActive(false);

            ShowSellItems();
        }

        private void ShowSellItems()
        {
            GameManager.instance.SortItems();
            for (var i = 0; i < sellItemButtons.Length; i++)
            {
                sellItemButtons[i].SetButtonValue(i);

                if (GameManager.instance.ItemsHeld()[i] != "")
                {
                    sellItemButtons[i].GetButtonImage().gameObject.SetActive(true);
                    sellItemButtons[i].GetButtonImage().sprite = GameManager.instance
                        .GetItemDetails(GameManager.instance.ItemsHeld()[i]).GetItemSprite();
                    sellItemButtons[i].GetAmountText().text = GameManager.instance.NumberOfItems()[i].ToString();
                }
                else
                {
                    sellItemButtons[i].GetButtonImage().gameObject.SetActive(false);
                    sellItemButtons[i].GetAmountText().text = "";
                }
            }
        }

        public void SelectBuyItem(Item buyItem)
        {
            selectedItem = buyItem;
            buyItemName.text = selectedItem.GetItemName();
            buyItemDescription.text = selectedItem.GetDescription();
            buyItemValue.text = "Value: " + selectedItem.GetValue() + "g";
        }

        public void SelectSellItem(Item sellItem)
        {
            selectedItem = sellItem;
            sellItemName.text = selectedItem.GetItemName();
            sellItemDescription.text = selectedItem.GetDescription();
            sellItemValue.text = "Value: " + Mathf.FloorToInt(selectedItem.GetValue() * .5f) + "g";
        }

        public void BuyItem()
        {
            if (selectedItem != null)
            {
                if (GameManager.instance.CurrentGold() >= selectedItem.GetValue())
                {
                    GameManager.instance.SubtractGold(selectedItem.GetValue());

                    GameManager.instance.AddItem(selectedItem.GetItemName());
                }
            }

            goldText.text = GameManager.instance.CurrentGold() + "g";
        }

        public void SellItem()
        {
            if (selectedItem != null)
            {
                GameManager.instance.AddGold(Mathf.FloorToInt(selectedItem.GetValue() * .5f));

                GameManager.instance.RemoveItem(selectedItem.GetItemName());
            }

            goldText.text = GameManager.instance.CurrentGold() + "g";

            ShowSellItems();
        }
    }
}
