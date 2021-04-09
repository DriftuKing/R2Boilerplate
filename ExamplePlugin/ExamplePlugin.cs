using BepInEx;
using On.RoR2;
using System.Collections.Generic;

namespace ExamplePlugin
{
    [BepInPlugin("com.Dual.CustomStats", "CustomStats", "1.0.0")]

    public class ExamplePlugin : BaseUnityPlugin
    {
        private bool isBuffApplied = false;
        private readonly List<string> survivors = new List<string> { "Acrid", "Artificer", "Bandit", "Captain", "Commando", "Engineer", "Huntress", "Loader", "Mercenary", "MUL-T", "REX" };

        public void Awake()
        {
            GlobalEventManager.OnCharacterHitGround += LevelUpStatsModifier;
        }

        private void LevelUpStatsModifier(GlobalEventManager.orig_OnCharacterHitGround orig, RoR2.GlobalEventManager self, RoR2.CharacterBody characterBody, UnityEngine.Vector3 impactVelocity)
        {
            // Check if the character is a survivor. Without this, the buff is applied to every single entity.
            // Checking if the buff is applied is not needed right now, but if playing with bonus stats it prevents the stats from increasing non-stop.
            if (!isBuffApplied && survivors.IndexOf(characterBody.GetDisplayName()) != -1) 
            {
                characterBody.baseAttackSpeed = 30f;
                characterBody.baseCrit = 100f;
                characterBody.baseDamage = 10f;
                isBuffApplied = true;
            }
            orig(self, characterBody, impactVelocity);
        }

        public void Update()
        {
            
        }
    }
}
