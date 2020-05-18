using UnityEngine;

namespace Character
{
    public class CharStats : MonoBehaviour
    {

        [SerializeField] string charName;
        [SerializeField] int playerLevel = 1;
        [SerializeField] int currentEXP;
        [SerializeField] int[] expToNextLevel;
        [SerializeField] int maxLevel = 100;
        [SerializeField] int baseEXP = 1000;
        [SerializeField] int currentHP;
        [SerializeField] int maxHP = 100;
        [SerializeField] int currentMP;
        [SerializeField] int maxMP = 30;
        [SerializeField] int[] mpLvlBonus;
        [SerializeField] int strength;
        [SerializeField] int defense;
        [SerializeField] int wpnPwr;
        [SerializeField] int armrPwr;
        [SerializeField] string equippedWpn;
        [SerializeField] string equippedArmr;
        [SerializeField] Sprite charImage;

        void Start()
        {
            expToNextLevel = new int[maxLevel];
            expToNextLevel[1] = baseEXP;

            for (var i = 2; i < expToNextLevel.Length; i++)
            {
                expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                AddExp(1000);
            }
        }

        public string GetCharName()
        {
            return charName;
        }

        public int GetPlayerLevel()
        {
            return playerLevel;
        }

        public int GetCurrentXP()
        {
            return currentEXP;
        }

        public int GetXPToNextLevel(int level)
        {
            return expToNextLevel[level];
        }

        public int GetMaxLevel()
        {
            return maxLevel;
        }

        public int GetBaseXP()
        {
            return baseEXP;
        }

        public int GetCurrentHP()
        {
            return currentHP;
        }

        public int GetMaxHP()
        {
            return maxHP;
        }

        public int GetCurrentMP()
        {
            return currentMP;
        }

        public int GetMaxMP()
        {
            return maxMP;
        }

        public int GetStrength()
        {
            return strength;
        }

        public int GetDefense()
        {
            return defense;
        }

        public int GetWpnPwr()
        {
            return wpnPwr;
        }

        public int GetArmrPwr()
        {
            return armrPwr;
        }

        public string GetWeapon()
        {
            return equippedWpn;
        }

        public string GetArmor()
        {
            return equippedArmr;
        }

        public Sprite GetSprite()
        {
            return charImage;
        }

        public void ChangeHealth(int change)
        {
            currentHP += change;
            if (currentHP > maxHP)
                currentHP = maxHP;
        }

        public void ChangeMana(int change)
        {
            currentMP += change;
            if (currentMP > maxMP)
                currentMP = maxMP;
        }

        public void ChangeStrength(int change)
        {
            strength += change;
        }

        public void ChangeWeapon(string newWpn)
        {
            equippedWpn = newWpn;
        }

        public void ChangeArmor(string newArmr)
        {
            equippedArmr = newArmr;
        }

        public void ChangeWpnPwr(int newWpnPwr)
        {
            wpnPwr = newWpnPwr;
        }

        public void ChangeArmrPwr(int newArmrPwr)
        {
            armrPwr = newArmrPwr;
        }

        void AddExp(int expToAdd)
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