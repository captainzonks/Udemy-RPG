﻿using UnityEngine;

namespace Core
{
    public class Item : MonoBehaviour
    {
        [Header("Item Type")] 
        public bool isItem;
        public bool isWeapon;
        public bool isArmor;

        [Header("Item Details")] 
        public string itemName;
        public string description;
        public int value;
        public Sprite itemSprite;

        [Header("Item Details")] 
        public int amountToChange;
        public bool affectHP, affectMP, affectStr;

        [Header("Weapon/Armor Details")] 
        public int weaponStrength;

        public int armorStrength;

        public void Use(int charToUseOn)
        {
            var selectedChar = GameManager.Instance.playerStats[charToUseOn];

            if (isItem)
            {
                if (affectHP)
                {
                    selectedChar.currentHP += amountToChange;

                    if (selectedChar.currentHP > selectedChar.maxHP)
                    {
                        selectedChar.currentHP = selectedChar.maxHP;
                    }
                }

                if (affectMP)
                {
                    selectedChar.currentMP += amountToChange;

                    if (selectedChar.currentMP > selectedChar.maxMP)
                    {
                        selectedChar.currentMP = selectedChar.maxMP;
                    }
                }

                if (affectStr)
                {
                    selectedChar.strength += amountToChange;
                }
            }

            if (isWeapon)
            {
                if (selectedChar.equippedWpn != "")
                {
                    GameManager.Instance.AddItem(selectedChar.equippedWpn);
                }

                selectedChar.equippedWpn = itemName;
                selectedChar.wpnPwr = weaponStrength;
            }

            if (isArmor)
            {
                if (selectedChar.equippedArmr != "")
                {
                    GameManager.Instance.AddItem(selectedChar.equippedArmr);
                }

                selectedChar.equippedArmr = itemName;
                selectedChar.armrPwr = armorStrength;
            }

            GameManager.Instance.RemoveItem(itemName);
        }
    }
}