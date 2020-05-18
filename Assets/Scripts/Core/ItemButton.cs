using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ItemButton : MonoBehaviour
    {
        [SerializeField] Image buttonImage;
        [SerializeField] Text amountText;
        [SerializeField] int buttonValue;

        public void Press()
        {
            if (GameMenu.instance.GetTheMenu().activeInHierarchy)
            {
                if (GameManager.instance.ItemsHeld()[buttonValue] != "")
                {
                    GameMenu.instance.SelectItem(
                        GameManager.instance.GetItemDetails(GameManager.instance.ItemsHeld()[buttonValue]));
                }
            }

            if (!Shop.instance.shopMenu.activeInHierarchy) return;
            if (Shop.instance.buyMenu.activeInHierarchy)
            {
                Shop.instance.SelectBuyItem(
                    GameManager.instance.GetItemDetails(Shop.instance.itemsForSale[buttonValue]));
            }

            if (Shop.instance.sellMenu.activeInHierarchy)
            {
                Shop.instance.SelectSellItem(
                    GameManager.instance.GetItemDetails(GameManager.instance.ItemsHeld()[buttonValue]));
            }
        }

        public Image GetButtonImage()
        {
            return buttonImage;
        }

        public Text GetAmountText()
        {
            return amountText;
        }

        public int GetButtonValue()
        {
            return buttonValue;
        }

        public void SetButtonValue(int set)
        {
            buttonValue = set;
        }
    }
}
