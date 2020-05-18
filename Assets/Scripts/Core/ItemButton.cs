using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ItemButton : MonoBehaviour
    {
        public Image ButtonImage { get; set; }
        public Text AmountText { get; set; }
        public int ButtonValue { get; set; }

        public void Press()
        {
            if (GameMenu.Instance.GetTheMenu().activeInHierarchy)
            {
                if (GameManager.Instance.ItemsHeld()[ButtonValue] != "")
                {
                    GameMenu.Instance.SelectItem(
                        GameManager.Instance.GetItemDetails(GameManager.Instance.ItemsHeld()[ButtonValue]));
                }
            }

            if (!Shop.Instance.ShopMenu.activeInHierarchy) return;
            if (Shop.Instance.BuyMenu.activeInHierarchy)
            {
                Shop.Instance.SelectBuyItem(
                    GameManager.Instance.GetItemDetails(Shop.Instance.ItemsForSale[ButtonValue]));
            }

            if (Shop.Instance.SellMenu.activeInHierarchy)
            {
                Shop.Instance.SelectSellItem(
                    GameManager.Instance.GetItemDetails(GameManager.Instance.ItemsHeld()[ButtonValue]));
            }
        }
    }
}
