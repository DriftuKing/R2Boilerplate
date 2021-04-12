using BepInEx;
using On.RoR2;
using UnityEngine;

namespace CustomStats
{
    [BepInPlugin("com.Dual.CustomStats", "CustomStats", "1.0.3")]

    public class CustomStats : BaseUnityPlugin
    {
        RoR2.CharacterBody player;
        CharacterStats defaultCharacter;
        CharacterStats customCharacter;
        int multiplierVal;
        string user;
        bool flag;

        private void CharacterBody_Start(CharacterBody.orig_Start orig, RoR2.CharacterBody self)
        {
            if (user == self.GetUserName())
            {
                player = self;
                defaultCharacter = new CharacterStats(self.baseAttackSpeed, self.baseDamage, self.baseCrit, self.baseMoveSpeed, self.baseJumpPower, self.baseMaxHealth, self.baseMaxShield, self.baseJumpCount);
                if (flag)
                {
                    customCharacter = new CharacterStats(self.baseAttackSpeed, self.baseDamage, self.baseCrit, self.baseMoveSpeed, self.baseJumpPower, self.baseMaxHealth, self.baseMaxShield, self.baseJumpCount);
                    flag = false;
                }
                InitializeStats(self);
            }
            orig(self);
        }

        private void InitializeStats(RoR2.CharacterBody player)
        {
            player.baseAttackSpeed = customCharacter.attackSpeed;
            player.baseDamage = customCharacter.damage;
            player.baseCrit = customCharacter.crit;
            player.baseMoveSpeed = customCharacter.moveSpeed;
            player.baseJumpPower = customCharacter.jumpPower;
            player.baseJumpCount = customCharacter.jumpCount;
            player.baseMaxHealth = customCharacter.maxHealth;
            player.baseMaxShield = customCharacter.maxShield;
        }

        private void ResetStats(ref RoR2.CharacterBody player)
        {
            player.baseAttackSpeed = defaultCharacter.attackSpeed;
            player.baseDamage = defaultCharacter.damage;
            player.baseCrit = defaultCharacter.crit;
            player.baseMoveSpeed = defaultCharacter.moveSpeed;
            player.baseJumpPower = defaultCharacter.jumpPower;
            player.baseJumpCount = defaultCharacter.jumpCount;
            player.baseMaxHealth = defaultCharacter.maxHealth;
            player.baseMaxShield = defaultCharacter.maxShield;
            customCharacter = new CharacterStats(player.baseAttackSpeed, player.baseDamage, player.baseCrit, player.baseMoveSpeed, player.baseJumpPower, player.baseMaxHealth, player.baseMaxShield, player.baseJumpCount);
            RoR2.Chat.AddMessage(user + "s stats have been <color=#48C9B0>reset</color>.");
        }

        private void IncreaseStat(ref float baseValue, ref float customBaseValue, float value, string stat)
        {
            customBaseValue += value * multiplierVal;
            if (customBaseValue < 1f)
            {
                customBaseValue = 1f;
            }
            baseValue = customBaseValue;
            RoR2.Chat.AddMessage(user + "s " + stat + " changed to <color=#85C1E9>" + baseValue + "</color>.");
        }

        private void IncreaseStat(ref int baseValue, ref int customBaseValue, int value, string stat)
        {
            customBaseValue += value * multiplierVal;
            if (customBaseValue < 1)
            {
                customBaseValue = 1;
            }
            baseValue = customBaseValue;
            RoR2.Chat.AddMessage(user + "s " + stat + " changed to <color=#85C1E9>" + baseValue + "</color>.");
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

        public void Awake()
        {
            CharacterBody.Start += CharacterBody_Start;
            UserProfile.OnLogin += UserProfile_OnLogin;
            RoR2.Run.onRunStartGlobal += Run_onRunStartGlobal;
            multiplierVal = 1;
        }

        private void Run_onRunStartGlobal(RoR2.Run obj)
        {
            flag = true;
        }

        private void UserProfile_OnLogin(UserProfile.orig_OnLogin orig, RoR2.UserProfile self)
        {
            user = self.name;
            UserProfile.OnLogin -= UserProfile_OnLogin;
            orig(self);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                IncreaseStat(ref player.baseAttackSpeed, ref customCharacter.attackSpeed, 0.5f, "Attack speed");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                IncreaseStat(ref player.baseDamage, ref customCharacter.damage, 5f, "Damage");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                IncreaseStat(ref player.baseCrit, ref customCharacter.crit, 10f, "Crit chance");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                IncreaseStat(ref player.baseMoveSpeed, ref customCharacter.moveSpeed, 2f, "Movement speed");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                IncreaseStat(ref player.baseJumpPower, ref customCharacter.jumpPower, 2f, "Jump power");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                IncreaseStat(ref player.baseJumpCount, ref customCharacter.jumpCount, 1, "Jump count");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                IncreaseStat(ref player.baseMaxHealth, ref customCharacter.maxHealth, 50f, "Max health");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                IncreaseStat(ref player.baseMaxShield, ref customCharacter.maxShield, 50f, "Max shield");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                ResetStats(ref player);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                ChangeOperationType();
            }
        }
    }

    public class CharacterStats
    {
        public float attackSpeed, damage, crit, moveSpeed, jumpPower, maxHealth, maxShield;
        public int jumpCount;
        
        public CharacterStats(float attackSpeed, float damage, float crit, float moveSpeed, float jumpPower, float maxHealth, float maxShield, int jumpCount)
        {
            this.attackSpeed = attackSpeed;
            this.damage = damage;
            this.crit = crit;
            this.moveSpeed = moveSpeed;
            this.jumpPower = jumpPower;
            this.maxHealth = maxHealth;
            this.maxShield = maxShield;
            this.jumpCount = jumpCount;
        }
    }
}
