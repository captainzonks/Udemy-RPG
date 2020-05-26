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

            if (!Shop.instance.shopMenu.activeInHierarchy) return;
            if (Shop.instance.buyMenu.activeInHierarchy)
            {
                Shop.instance.SelectBuyItem(
                    GameManager.Instance.GetItemDetails(Shop.instance.itemsForSale[buttonValue]));
            }

            if (Shop.instance.sellMenu.activeInHierarchy)
            {
                Shop.instance.SelectSellItem(
                    GameManager.Instance.GetItemDetails(GameManager.Instance.itemsHeld[buttonValue]));
            }
        }
    }
}