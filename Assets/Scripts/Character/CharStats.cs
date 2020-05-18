using UnityEngine;

namespace Character
{
    public class CharStats : MonoBehaviour
    {

        public string CharName { get; set; }
        public int PlayerLevel { get; private set; } = 1;
        public int CurrentExp { get; private set; }
        public int[] ExpToNextLevel { get; private set; }
        private int MaxLevel { get; } = 100;
        private int BaseExp { get; } = 1000;
        public int CurrentHp { get; private set; }
        public int MaxHp { get; private set; } = 100;
        public int CurrentMp { get; private set; }
        public int MaxMp { get; private set; } = 30;
        private int[] MpLvlBonus { get; set; }
        public int Strength { get; set; }
        public int Defense { get; private set; }
        public int WpnPwr { get; internal set; }
        public int ArmrPwr { get; internal set; }
        public string EquippedWpn { get; internal set; }
        public string EquippedArmr { get; internal set; }
        public Sprite CharImage { get; set; }

        private void Start()
        {
            ExpToNextLevel = new int[MaxLevel];
            ExpToNextLevel[1] = BaseExp;

            for (var i = 2; i < ExpToNextLevel.Length; i++)
            {
                ExpToNextLevel[i] = Mathf.FloorToInt(ExpToNextLevel[i - 1] * 1.05f);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                AddExp(1000);
            }
        }

        public void ChangeHealth(int change)
        {
            CurrentHp += change;
            if (CurrentHp > MaxHp)
                CurrentHp = MaxHp;
        }

        public void ChangeMana(int change)
        {
            CurrentMp += change;
            if (CurrentMp > MaxMp)
                CurrentMp = MaxMp;
        }

        private void AddExp(int expToAdd)
        {
            CurrentExp += expToAdd;

            if (PlayerLevel < MaxLevel)
            {
                if (CurrentExp > ExpToNextLevel[PlayerLevel])
                {
                    CurrentExp -= ExpToNextLevel[PlayerLevel];

                    PlayerLevel++;

                    // determine whether to add to strength or defense based on odd or even
                    if (PlayerLevel % 2 == 0)
                    {
                        Strength++;
                    }
                    else
                    {
                        Defense++;
                    }

                    MaxHp = Mathf.FloorToInt(MaxHp * 1.05f);
                    CurrentHp = MaxHp;

                    MaxMp += MpLvlBonus[PlayerLevel];
                    CurrentMp = MaxMp;
                }
            }

            if (PlayerLevel >= MaxLevel)
            {
                CurrentExp = 0;
            }
        }
    }
}