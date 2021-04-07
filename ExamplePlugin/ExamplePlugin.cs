using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;

namespace ExamplePlugin
{
    //This attribute specifies that we have a dependency on R2API, as we're using it to add our item to the game.
    //You don't need this if you're not using R2API in your plugin, it's just to tell BepInEx to initialize R2API before this plugin so it's safe to use R2API.
    [BepInDependency("com.bepis.r2api")]

    //This attribute is required, and lists metadata for your plugin.
    [BepInPlugin(
        //The GUID should be a unique ID for this plugin, which is human readable (as it is used in places like the config). Java package notation is commonly used, which is "com.[your name here].[your plugin name here]"
        "com.MyName.IHerebyGrantPermissionToDeprecateMyModFromThunderstoreBecauseIHaveNotChangedTheName",
        //The name is the name of the plugin that's displayed on load
        "IHerebyGrantPermissionToDeprecateMyModFromThunderstoreBecauseIHaveNotChangedTheName",
        //The version number just specifies what version the plugin is.
        "1.0.0")]
    //Like seriously, if we see this boilerplate on thunderstore, we will deprecate this mod. Change that name!
    //If you want to test package uploading in general, try using beta.thunderstore.io

    //This is the main declaration of our plugin class. BepInEx searches for all classes inheriting from BaseUnityPlugin to initialize on startup.
    //BaseUnityPlugin itself inherits from MonoBehaviour, so you can use this as a reference for what you can declare and use in your plugin class: https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    public class ExamplePlugin : BaseUnityPlugin
    {

        //The Awake() method is run at the very start when the game is initialized.
        public void Awake()
        {
            Logger.LogMessage("Loaded MyModName!");
            On.EntityStates.Huntress.ArrowRain.OnEnter += (orig, self) =>
            {
                // [The code we want to run]
                Chat.AddMessage("You used Huntress's Arrow Rain!");

                // Call the original function (orig)
                // on the object it's normally called on (self)
                orig(self);
            };
        }

        //The Update() method is run on every frame of the game.
        public void Update()
        {
            
        }
    }
}
