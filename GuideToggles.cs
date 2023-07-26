using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GuideToggles;

public class GuideToggles : Mod
{
    internal static Mod Mod;

    public static List<Button> Buttons { get; set; }

    public override void Load()
    {
        Mod = this;

        Asset<Texture2D> Border0 = ModContent.Request<Texture2D>("GuideToggles/Images/Border1");
        Asset<Texture2D> Border1 = ModContent.Request<Texture2D>("GuideToggles/Images/Border0");

        Buttons = new()
        {
            new Button(
                "CritterBook",
                Language.GetOrRegister("Mods.GuideToggles.Buttons.CritterOff"),
                Language.GetOrRegister("Mods.GuideToggles.Buttons.CritterOn"),
                ModContent.Request<Texture2D>("GuideToggles/Images/Critter1"),
                ModContent.Request<Texture2D>("GuideToggles/Images/Critter0"),
                Border0,
                Border1,
                ItemID.DontHurtCrittersBookInactive,
                ItemID.DontHurtCrittersBook
            ),

            new Button(
                "NatureBook",
                Language.GetOrRegister("Mods.GuideToggles.Buttons.EnvironmentOff"),
                Language.GetOrRegister("Mods.GuideToggles.Buttons.EnvironmentOn"),
                ModContent.Request<Texture2D>("GuideToggles/Images/Environment1"),
                ModContent.Request<Texture2D>("GuideToggles/Images/Environment0"),
                Border0,
                Border1,
                ItemID.DontHurtNatureBookInactive,
                ItemID.DontHurtNatureBook
            ),

            new Button(
                "ComboBook",
                Language.GetOrRegister("Mods.GuideToggles.Buttons.PeaceOff"),
                Language.GetOrRegister("Mods.GuideToggles.Buttons.PeaceOn"),
                ModContent.Request<Texture2D>("GuideToggles/Images/Peace1"),
                ModContent.Request<Texture2D>("GuideToggles/Images/Peace0"),
                Border0,
                Border1,
                ItemID.DontHurtComboBookInactive,
                ItemID.DontHurtComboBook
            ),
        };

        if (Config.Instance.EncumberingStone)
        {
            Buttons.Add(new Button(
                "Encumbering",
                Language.GetOrRegister("Mods.GuideToggles.Buttons.StoneOff"),
                Language.GetOrRegister("Mods.GuideToggles.Buttons.StoneOn"),
                ModContent.Request<Texture2D>("GuideToggles/Images/Stone1"),
                ModContent.Request<Texture2D>("GuideToggles/Images/Stone0"),
                ModContent.Request<Texture2D>("GuideToggles/Images/StoneBorder1"),
                ModContent.Request<Texture2D>("GuideToggles/Images/StoneBorder0"),
                ItemID.UncumberingStone,
                ItemID.EncumberingStone
            ));
        }
    }

    public override void Unload()
    {
        Mod = null;
        Buttons = null;
    }
}