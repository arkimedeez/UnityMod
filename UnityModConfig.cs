using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace arkimedeezMod
{
    public class UnityModConfig : ModConfig
    {
        // ConfigScope.ClientSide should be used for client side, usually visual or audio tweaks.
        // ConfigScope.ServerSide should be used for basically everything else, including disabling items or changing NPC behaviors
        public override ConfigScope Mode => ConfigScope.ClientSide;

        // The things in brackets are known as "Attributes".

        // A label is the text displayed next to the option. This should usually be a short description of what it does. By default all ModConfig fields and properties have an automatic label translation key, but modders can specify a specific translation key.
        /*[DefaultValue(true)] // This sets the configs default value.

        [Range(-1000f, 1000f)]
        public float OmegaBarXScreenPlacement;
        [Range(-1000f, 1000f)]
        public float OmegaBarYScreenPlacement;

        [Range(-20f, 20f)]
        public float OmegaBarXOffset;
        [Range(-20f, 20f)]
        public float OmegaBarYOffset;*/
    }
}