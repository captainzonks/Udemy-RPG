using Core;
using UnityEngine;

namespace Core
{
   public class Item : MonoBehaviour
    {
        public bool IsItem { get; set; }
        public bool IsWeapon { get; set; }
        public bool IsArmor { get; set; }

        public string ItemName { get; set; }
        public string Description { get; set; }
        public int Value { get; set; }
        public Sprite ItemSprite { get; set; }

        private int AmountToChange { get; set; }
        private bool AffectHp { get; set; }
        private bool AffectMp { get; set; }
        private bool AffectStr { get; set; }

        private int WeaponStrength { get; set; }
        private int ArmorStrength { get; set; }

        public void Use(int charToUseOn)
        {
            var selectedChar = GameManager.Instance.GetPlayerStats()[charToUseOn];

            if (IsItem)
            {
                if (AffectHp)
                {
                    selectedChar.ChangeHealth(AmountToChange);
                }

                if (AffectMp)
                { 
                    selectedChar.ChangeMana(AmountToChange);
                }

                if (AffectStr)
                {
                    selectedChar.Strength = AmountToChange;
                }
            }

            if (IsWeapon)
            {
                if (selectedChar.EquippedWpn != "")
                {
                    GameManager.Instance.AddItem(selectedChar.EquippedWpn);
                }

                selectedChar.EquippedWpn = ItemName;
                selectedChar.WpnPwr = WeaponStrength;
            }

            if (IsArmor)
            {
                if (selectedChar.EquippedArmr != "")
                {
                    GameManager.Instance.AddItem(selectedChar.EquippedArmr);
                }

                selectedChar.EquippedArmr = ItemName;
                selectedChar.ArmrPwr = ArmorStrength;
            }

            GameManager.Instance.RemoveItem(ItemName);
        }
    } 
}