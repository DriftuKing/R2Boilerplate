using BepInEx;
using On.RoR2;
using System.Collections.Generic;
using UnityEngine;

namespace ExamplePlugin
{
    [BepInPlugin("com.Dual.CustomStats", "CustomStats", "1.0.0")]

    public class ExamplePlugin : BaseUnityPlugin
    {
        private readonly List<string> survivors = new List<string> { "Acrid", "Artificer", "Bandit", "Captain", "Commando", "Engineer", "Huntress", "Loader", "Mercenary", "MUL-T", "REX" };
        RoR2.CharacterBody player;
        int multiplierVal = 1;

        public void Awake()
        {
            CharacterBody.Awake += CharacterBody_Awake;
        }

        private void CharacterBody_Awake(CharacterBody.orig_Awake orig, RoR2.CharacterBody self)
        {
            if (survivors.IndexOf(self.GetDisplayName()) != -1)
            {
                player = self;
            }
            orig(self);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                IncreaseStat(ref player.baseAttackSpeed, 0.25f, "Attack speed");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                IncreaseStat(ref player.baseDamage, 5f, "Damage");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                IncreaseStat(ref player.baseCrit, 10f, "Crit chance");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                IncreaseStat(ref player.baseMoveSpeed, 3f, "Movement speed");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                IncreaseStat(ref player.baseArmor, 10f, "Armor");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                IncreaseStat(ref player.baseJumpPower, 2f, "Jump power");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                IncreaseStat(ref player.baseJumpCount, 1, "Jump count");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                IncreaseStat(ref player.baseMaxHealth, 50f, "Max health");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                IncreaseStat(ref player.baseMaxShield, 50f, "Max shield");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                ChangeOperationType();
            }
        }

        private void IncreaseStat(ref float baseValue, float value, string stat)
        {
            baseValue += value * multiplierVal;
            if (baseValue < 1f)
            {
                baseValue = 1f;
            }
            RoR2.Chat.AddMessage(stat + " changed to <color=#85C1E9>" + baseValue + ".</color>");
        }

        private void IncreaseStat(ref int baseValue, int value, string stat)
        {
            baseValue += value * multiplierVal;
            if (baseValue < 1)
            {
                baseValue = 1;
            }
            RoR2.Chat.AddMessage(stat + " changed to <color=#85C1E9>" + baseValue + ".</color>");
        }

        public void ChangeOperationType()
        {
            string multiplierType;
            if (multiplierVal == 1)
            {
                multiplierVal = -1;
                multiplierType = "decrease";
            }
            else
            {
                multiplierVal = 1;
                multiplierType = "increase";
            }
            RoR2.Chat.AddMessage("Stats will now <color=#48C9B0>" + multiplierType + "</color>.");
        }
    }
}