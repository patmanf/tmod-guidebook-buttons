using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace GuideToggles;

public class Player : ModPlayer
{
    public override void ProcessTriggers(TriggersSet triggersSet)
    {
        if (!Config.Instance.EnableKeybinds) return;

        foreach (Button button in GuideToggles.Buttons)
        {
            if (button.Bind == null) continue;
            if (button.Bind.JustPressed) button.KeybindPressed();
        }
    }

    public override void PostUpdateEquips()
    {
        if (!Config.Instance.EncumberingStone) return;
        if (Main.LocalPlayer.HasItemInInventoryOrOpenVoidBag(ItemID.EncumberingStone))
        {
            Main.LocalPlayer.preventAllItemPickups = true;
        }
    }
}
