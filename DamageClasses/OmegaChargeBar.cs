using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria;
using arkimedeezMod.Items.Weapons.SkySplitter;

namespace arkimedeezMod.DamageClasses
{
    // This custom UI will show whenever the player is holding the ExampleCustomResourceWeapon item and will display the player's custom resource amounts that are tracked in ExampleResourcePlayer
    internal class OmegaChargeBar : UIState
    {
        // For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
        // Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
        private UIText text;
        private UIElement area;
        private UIImage barFrame;
        private Color gradientA;
        private Color gradientB;

        private void MethodName_OnUpdate(UIElement affectedElement)
        {
            //area.Left.Set(-((Main.screenWidth / 2) + ModContent.GetInstance<UnityModConfig>().OmegaBarXOffset) + ModContent.GetInstance<UnityModConfig>().OmegaBarXScreenPlacement, 1f);
            //area.Top.Set((Main.screenHeight / 2) + ModContent.GetInstance<UnityModConfig>().OmegaBarYOffset + ModContent.GetInstance<UnityModConfig>().OmegaBarYScreenPlacement, 0f);
            area.Left.Set((Main.screenWidth / -2), 1f);
            area.Top.Set((Main.screenHeight / 2) + 30, 0f);
        }

        public override void OnInitialize()
        {
            OnUpdate += MethodName_OnUpdate;
            // Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
            // UIElement is invisible and has no padding.
            area = new UIElement();
            //area.Left.Set(-(Main.screenWidth/2) - 164, 1f); // Place the resource bar to the left of the hearts.  ModContent.GetInstance
            //area.Left.Set(-((Main.screenWidth / 2) + ModContent.GetInstance<UnityModConfig>().OmegaBarXOffset), 1f);
            //area.Top.Set((Main.screenHeight / 2) + ModContent.GetInstance<UnityModConfig>().OmegaBarYOffset, 0f);
            area.Left.Set((Main.screenWidth / -2), 1f);
            area.Top.Set((Main.screenHeight / 2) + 30, 0f);
            area.Width.Set(182, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
            area.Height.Set(60, 0f);

            barFrame = new UIImage(ModContent.Request<Texture2D>("arkimedeezMod/DamageClasses/OmegaChargeBar")); // Frame of our resource bar
            barFrame.Left.Set(-67, 0f);
            barFrame.Top.Set(0, 0f);
            barFrame.Width.Set(134, 0f);
            barFrame.Height.Set(34, 0f);

            text = new UIText(" ", 0.8f); // text to show stat
            text.Width.Set(138, 0f);
            text.Height.Set(34, 0f);
            text.Top.Set(40, 0f);
            text.Left.Set(0, 0f);

            gradientA = new Color(92, 40, 133); // A dark purple
            gradientB = new Color(98, 75, 253); // A light purple

            area.Append(text);
            area.Append(barFrame);
            Append(area);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // This prevents drawing unless we are using an ExampleCustomResourceWeapon     ModItem.countAsClass(DamageClass.Melee)
            if (!Main.LocalPlayer.HeldItem.CountsAsClass(ModContent.GetInstance<OmegaDamage>()))
                return;
            base.Draw(spriteBatch);
        }

        // Here we draw our UI
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var modPlayer = Main.LocalPlayer.GetModPlayer<UnityPlayer>();
            // Calculate quotient       player.GetModPlayer<NameOfYouPlayerClass>().variable ModPlayerClass.variable

            float quotient = (float)UnityPlayer.OmegaChargeCurrent / 100; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.
            quotient = Utils.Clamp(quotient, 0f, 1f); // Clamping it to 0-1f so it doesn't go over that.

            // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.
            Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
            hitbox.X += 12;
            hitbox.Width -= 24;
            hitbox.Y += 6;
            hitbox.Height -= 16;


            // Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
            int left = hitbox.Left;
            int right = hitbox.Right;
            int steps = (int)((right - left) * quotient);
            for (int i = 0; i < steps; i += 1)
            {
                // float percent = (float)i / steps; // Alternate Gradient Approach
                float percent = (float)i / (right - left);
                spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(left + i, hitbox.Y, 1, hitbox.Height + 4), Color.Lerp(gradientA, gradientB, percent));
            }
        }
        public override void Update(GameTime gameTime)
        {
            if (Main.LocalPlayer.HeldItem.ModItem is not SkySplitter)
                return;

            var modPlayer = Main.LocalPlayer.GetModPlayer<UnityPlayer>();
            // Setting the text per tick to update and show our resource values.
            text.SetText(" ");
          base.Update(gameTime);
        }
    }

    // This class will only be autoloaded/registered if we're not loading on a server
    [Autoload(Side = ModSide.Client)]
    internal class ExampleResourceUISystem : ModSystem
    {
        private UserInterface OmegaChargeBarBarUserInterface;

        internal OmegaChargeBar OmegaChargeBar;

        public static LocalizedText OmegaChargeText { get; private set; }

        public override void Load()
        {
            OmegaChargeBar = new();
            OmegaChargeBarBarUserInterface = new();
            OmegaChargeBarBarUserInterface.SetState(OmegaChargeBar);

            string category = "UI";
            OmegaChargeText ??= Mod.GetLocalization($"{category}.ExampleResource");
        }



        public override void UpdateUI(GameTime gameTime)
        {
            OmegaChargeBarBarUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "ExampleMod: Example Resource Bar",
                    delegate {
                        OmegaChargeBarBarUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
