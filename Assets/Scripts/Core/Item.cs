using Core;
using UnityEngine;

namespace Core
{
   public class Item : MonoBehaviour
    {
        private readonly bool _isItem;
        private readonly bool _isWeapon;
        private readonly bool _isArmor;

        private readonly string _itemName;
        private readonly string _description;
        private readonly int _value;
        private readonly Sprite _itemSprite;

        private readonly int _amountToChange;
        private readonly bool _affectHp;
        private readonly bool _affectMp;
        private readonly bool _affectStr;

        private readonly int _weaponStrength;
        private readonly int _armorStrength;

        public Item(bool isItem, bool isWeapon, bool isArmor, string itemName, string description, int value, Sprite itemSprite, int amountToChange, bool affectStr, bool affectHp, bool affectMp, int weaponStrength, int armorStrength)
        {
            _isItem = isItem;
            _isWeapon = isWeapon;
            _isArmor = isArmor;
            _itemName = itemName;
            _description = description;
            _value = value;
            _itemSprite = itemSprite;
            _amountToChange = amountToChange;
            _affectStr = affectStr;
            _affectHp = affectHp;
            _affectMp = affectMp;
            _weaponStrength = weaponStrength;
            _armorStrength = armorStrength;
        }

        public bool IsItem()
        {
            return _isItem;
        }

        public bool IsWeapon()
        {
            return _isWeapon;
        }

        public bool IsArmor()
        {
            return _isArmor;
        }

        public string GetItemName()
        {
            return _itemName;
        }

        public string GetDescription()
        {
            return _description;
        }

        public int GetValue()
        {
            return _value;
        }

        public Sprite GetItemSprite()
        {
            return _itemSprite;
        }

        public int GetAmountToChange()
        {
            return _amountToChange;
        }

        public bool GetAffectHP()
        {
            return _affectHp;
        }

        public bool GetAffectMP()
        {
            return _affectMp;
        }

        public bool GetAffectStr()
        {
            return _affectStr;
        }

        public int GetWeaponStr()
        {
            return _weaponStrength;
        }

        public int GetArmorStr()
        {
            return _armorStrength;
        }

        public void Use(int charToUseOn)
        {
            var selectedChar = GameManager.instance.GetPlayerStats()[charToUseOn];

            if (_isItem)
            {
                if (_affectHp)
                {
                    selectedChar.ChangeHealth(_amountToChange);
                }

                if (_affectMp)
                { 
                    selectedChar.ChangeMana(_amountToChange);
                }

                if (_affectStr)
                {
                    selectedChar.ChangeStrength(_amountToChange);
                }
            }

            if (_isWeapon)
            {
                if (selectedChar.GetWeapon() != "")
                {
                    GameManager.instance.AddItem(selectedChar.GetWeapon());
                }

                selectedChar.ChangeWeapon(_itemName);
                selectedChar.ChangeWpnPwr(_weaponStrength);
            }

            if (_isArmor)
            {
                if (selectedChar.GetArmor() != "")
                {
                    GameManager.instance.AddItem(selectedChar.GetArmor());
                }

                selectedChar.ChangeArmor(_itemName);
                selectedChar.ChangeArmrPwr(_armorStrength);
            }

            GameManager.instance.RemoveItem(_itemName);
        }
    } 
}