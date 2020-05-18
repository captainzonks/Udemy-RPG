using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ItemButton : MonoBehaviour
    {
        private Image _buttonImage;
        private Text _amountText;
        private int _buttonValue;

        void Start()
        {
            _buttonImage = GetComponent<Image>();
            _amountText = GetComponent<Text>();
            _buttonValue = 0;
        }

        public void Press()
        {
            if (GameMenu.instance.GetTheMenu().activeInHierarchy)
            {
                if (GameManager.instance.ItemsHeld()[_buttonValue] != "")
                {
                    GameMenu.instance.SelectItem(
                        GameManager.instance.GetItemDetails(GameManager.instance.ItemsHeld()[_buttonValue]));
                }
            }

            if (!Shop.instance.shopMenu.activeInHierarchy) return;
            if (Shop.instance.buyMenu.activeInHierarchy)
            {
                Shop.instance.SelectBuyItem(
                    GameManager.instance.GetItemDetails(Shop.instance.itemsForSale[_buttonValue]));
            }

            if (Shop.instance.sellMenu.activeInHierarchy)
            {
                Shop.instance.SelectSellItem(
                    GameManager.instance.GetItemDetails(GameManager.instance.ItemsHeld()[_buttonValue]));
            }
        }

        public Image GetButtonImage()
        {
            return _buttonImage;
        }

        public Text GetAmountText()
        {
            return _amountText;
        }

        public int GetButtonValue()
        {
            return _buttonValue;
        }

        public void SetButtonValue(int set)
        {
            _buttonValue = set;
        }
    }
}
