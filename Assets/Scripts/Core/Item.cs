using Core;
using UnityEngine;

namespace Core
{
   public class Item : MonoBehaviour
    {
        [Header("Item Type")]
        [SerializeField] bool isItem;
        [SerializeField] bool isWeapon;
        [SerializeField] bool isArmor;

        [Header("Item Details")] 
        [SerializeField] string itemName;
        [SerializeField] string description;
        [SerializeField] int value;
        [SerializeField] Sprite itemSprite;

        [Header("Item Details")]
        [SerializeField] int amountToChange;
        [SerializeField] bool affectHP, affectMP, affectStr;

        [Header("Weapon/Armor Details")]
        [SerializeField] int weaponStrength;
        [SerializeField] int armorStrength;

        public bool IsItem()
        {
            return isItem;
        }

        public bool IsWeapon()
        {
            return isWeapon;
        }

        public bool IsArmor()
        {
            return isArmor;
        }

        public string GetItemName()
        {
            return itemName;
        }

        public string GetDescription()
        {
            return description;
        }

        public int GetValue()
        {
            return value;
        }

        public Sprite GetItemSprite()
        {
            return itemSprite;
        }

        public int GetAmountToChange()
        {
            return amountToChange;
        }

        public bool GetAffectHP()
        {
            return affectHP;
        }

        public bool GetAffectMP()
        {
            return affectMP;
        }

        public bool GetAffectStr()
        {
            return affectStr;
        }

        public int GetWeaponStr()
        {
            return weaponStrength;
        }

        public int GetArmorStr()
        {
            return armorStrength;
        }

        public void Use(int charToUseOn)
        {
            var selectedChar = GameManager.instance.GetPlayerStats()[charToUseOn];

            if (isItem)
            {
                if (affectHP)
                {
                    selectedChar.ChangeHealth(amountToChange);
                }

                if (affectMP)
                { 
                    selectedChar.ChangeMana(amountToChange);
                }

                if (affectStr)
                {
                    selectedChar.ChangeStrength(amountToChange);
                }
            }

            if (isWeapon)
            {
                if (selectedChar.GetWeapon() != "")
                {
                    GameManager.instance.AddItem(selectedChar.GetWeapon());
                }

                selectedChar.ChangeWeapon(itemName);
                selectedChar.ChangeWpnPwr(weaponStrength);
            }

            if (isArmor)
            {
                if (selectedChar.GetArmor() != "")
                {
                    GameManager.instance.AddItem(selectedChar.GetArmor());
                }

                selectedChar.ChangeArmor(itemName);
                selectedChar.ChangeArmrPwr(armorStrength);
            }

            GameManager.instance.RemoveItem(itemName);
        }
    } 
}