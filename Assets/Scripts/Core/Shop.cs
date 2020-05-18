using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class Shop : MonoBehaviour
    {
        public static Shop Instance { get; private set; }

        public GameObject ShopMenu { get; set; }
        public GameObject BuyMenu { get; set; }
        public GameObject SellMenu { get; set; }

        private Text GoldText { get; set; }

        public string[] ItemsForSale { get; set; }

        private ItemButton[] BuyItemButtons { get; set; }
        private ItemButton[] SellItemButtons { get; set; }

        private Item SelectedItem { get; set; }
        private Text BuyItemName { get; set; }
        private Text BuyItemDescription { get; set; }
        private Text BuyItemValue { get; set; }
        private Text SellItemName { get; set; }
        private Text SellItemDescription { get; set; }
        private Text SellItemValue { get; set; }

        private void Start()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K) && !ShopMenu.activeInHierarchy)
            {
                OpenShop();
            }
        }

        public void OpenShop()
        {
            ShopMenu.SetActive(true);
            OpenBuyMenu();

            GameManager.Instance.SetShopActive(true);

            GoldText.text = GameManager.Instance.CurrentGold() + "g";
        }

        public void CloseShop()
        {
            ShopMenu.SetActive(false);

            GameManager.Instance.SetShopActive(false);
        }

        public void OpenBuyMenu()
        {
            BuyItemButtons[0].Press();

            BuyMenu.SetActive(true);
            SellMenu.SetActive(false);

            for (var i = 0; i < BuyItemButtons.Length; i++)
            {
                BuyItemButtons[i].ButtonValue = i;

                if (ItemsForSale[i] != "")
                {
                    BuyItemButtons[i].ButtonImage.gameObject.SetActive(true);
                    BuyItemButtons[i].ButtonImage.sprite =
                        GameManager.Instance.GetItemDetails(ItemsForSale[i]).ItemSprite;
                    BuyItemButtons[i].AmountText.text = "";
                }
                else
                {
                    BuyItemButtons[i].ButtonImage.gameObject.SetActive(false);
                    BuyItemButtons[i].AmountText.text = "";
                }
            }
        }

        public void OpenSellMenu()
        {
            SellItemButtons[0].Press();

            SellMenu.SetActive(true);
            BuyMenu.SetActive(false);

            ShowSellItems();
        }

        private void ShowSellItems()
        {
            GameManager.Instance.SortItems();
            for (var i = 0; i < SellItemButtons.Length; i++)
            {
                SellItemButtons[i].ButtonValue = i;

                if (GameManager.Instance.ItemsHeld()[i] != "")
                {
                    SellItemButtons[i].ButtonImage.gameObject.SetActive(true);
                    SellItemButtons[i].ButtonImage.sprite = GameManager.Instance
                        .GetItemDetails(GameManager.Instance.ItemsHeld()[i]).ItemSprite;
                    SellItemButtons[i].AmountText.text = GameManager.Instance.NumberOfItems()[i].ToString();
                }
                else
                {
                    SellItemButtons[i].ButtonImage.gameObject.SetActive(false);
                    SellItemButtons[i].AmountText.text = "";
                }
            }
        }

        public void SelectBuyItem(Item buyItem)
        {
            SelectedItem = buyItem;
            BuyItemName.text = SelectedItem.ItemName;
            BuyItemDescription.text = SelectedItem.Description;
            BuyItemValue.text = "Value: " + SelectedItem.Value + "g";
        }

        public void SelectSellItem(Item sellItem)
        {
            SelectedItem = sellItem;
            SellItemName.text = SelectedItem.ItemName;
            SellItemDescription.text = SelectedItem.Description;
            SellItemValue.text = "Value: " + Mathf.FloorToInt(SelectedItem.Value * .5f) + "g";
        }

        public void BuyItem()
        {
            if (SelectedItem != null)
            {
                if (GameManager.Instance.CurrentGold() >= SelectedItem.Value)
                {
                    GameManager.Instance.SubtractGold(SelectedItem.Value);

                    GameManager.Instance.AddItem(SelectedItem.ItemName);
                }
            }

            GoldText.text = GameManager.Instance.CurrentGold() + "g";
        }

        public void SellItem()
        {
            if (SelectedItem != null)
            {
                GameManager.Instance.AddGold(Mathf.FloorToInt(SelectedItem.Value * .5f));

                GameManager.Instance.RemoveItem(SelectedItem.ItemName);
            }

            GoldText.text = GameManager.Instance.CurrentGold() + "g";

            ShowSellItems();
        }
    }
}
