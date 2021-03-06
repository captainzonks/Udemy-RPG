﻿using Core;
using UnityEngine;

namespace Character
{
    public class CharStats : MonoBehaviour
    {
        public string charName;
        public int playerLevel = 1;
        public int currentEXP;
        public int[] expToNextLevel;
        public int maxLevel = 100;
        public int baseEXP = 1000;

        public int currentHP;
        public int maxHP = 100;
        public int currentMP;
        public int maxMP = 30;
        public int[] mpLvlBonus;
        public int strength;
        public int defense;
        public int wpnPwr;
        public int armrPwr;
        public string equippedWpn;
        public string equippedArmr;
        public Sprite charImage;

        private void Start()
        {

            expToNextLevel = new int[maxLevel];
            expToNextLevel[1] = baseEXP;

            for (var i = 2; i < expToNextLevel.Length; i++)
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
            }
        }

        private void AddExp(int expToAdd)
        {
            currentEXP += expToAdd;

            if (playerLevel < maxLevel)
            {
                if (currentEXP > expToNextLevel[playerLevel])
                {
                    currentEXP -= expToNextLevel[playerLevel];

                    playerLevel++;

                    // determine whether to add to strength or defense based on odd or even
                    if (playerLevel % 2 == 0)
                    {
                        strength++;
                    }
                    else
                    {
                        defense++;
                    }

                    maxHP = Mathf.FloorToInt(maxHP * 1.05f);
                    currentHP = maxHP;

                    maxMP += mpLvlBonus[playerLevel];
                    currentMP = maxMP;
                }
            }

            if (playerLevel >= maxLevel)
            {
                currentEXP = 0;
            }
        }
    }
}