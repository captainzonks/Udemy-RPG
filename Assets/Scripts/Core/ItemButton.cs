using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ItemButton : MonoBehaviour
    {
        public Image buttonImage;
        public Text amountText;
        public int buttonValue;

        public void Press()
        {
            if (GameMenu.Instance.theMenu.activeInHierarchy)
            {
                if (GameManager.Instance.itemsHeld[buttonValue] != "")
                {
                    GameMenu.Instance.SelectItem(
                        GameManager.Instance.GetItemDetails(GameManager.Instance.itemsHeld[buttonValue]));
                }
            }

            if (!Shop.Instance.shopMenu.activeInHierarchy) return;
            if (Shop.Instance.buyMenu.activeInHierarchy)
            {
                Shop.Instance.SelectBuyItem(
                    GameManager.Instance.GetItemDetails(Shop.Instance.itemsForSale[buttonValue]));
            }

            if (Shop.Instance.sellMenu.activeInHierarchy)
            {
                Shop.Instance.SelectSellItem(
                    GameManager.Instance.GetItemDetails(GameManager.Instance.itemsHeld[buttonValue]));
            }
        }
    }
}