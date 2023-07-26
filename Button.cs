using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GuideToggles;

public class Button
{
    internal string Name;

    public LocalizedText TextOff;
    public LocalizedText TextOn;

    public Asset<Texture2D> IconOff;
    public Asset<Texture2D> IconOn;

    public Asset<Texture2D> BorderOff;
    public Asset<Texture2D> BorderOn;

    public int ItemOff;
    public int ItemOn;

    public ModKeybind Bind;

    public bool Enabled => Main.LocalPlayer.HasItemInInventoryOrOpenVoidBag(ItemOn);
    public bool Visible => Enabled || Main.LocalPlayer.HasItemInInventoryOrOpenVoidBag(ItemOff);

    public Button(string name, LocalizedText textOff, LocalizedText textOn, Asset<Texture2D> iconOff, Asset<Texture2D> iconOn, Asset<Texture2D> borderOff, Asset<Texture2D> borderOn, int itemOff, int itemOn, bool bind = true)
    {
        Name = name;
        TextOff = textOff;
        TextOn = textOn;
        IconOff = iconOff;
        IconOn = iconOn;
        BorderOff = borderOff;
        BorderOn = borderOn;
        ItemOff = itemOff;
        ItemOn = itemOn;
        
        if (bind && Config.Instance.EnableKeybinds)
        {
            KeybindLoader.RegisterKeybind(GuideToggles.Mod, "Toggle" + name, Keys.None);
        }
    }

    public virtual void Toggle()
    {
        int find = Enabled ? ItemOn : ItemOff;
        int replace = Enabled ? ItemOff : ItemOn;

        bool replaced = ReplaceItems(Main.LocalPlayer.inventory, find, replace);
        replaced = ReplaceItems(Main.LocalPlayer.bank4.item, find, replace) || replaced;

        if (!replaced) return;

        SoundEngine.PlaySound(SoundID.Unlock);
    }

    private static bool ReplaceItems(Item[] inv, int find, int replace)
    {
        bool replaced = false;
        for (int i = 0; i < inv.Length; i++)
        {
            Item item = inv[i];
            if (item.type == find)
            {
                item.ChangeItemType(replace);
                replaced = true;
            }
        }
        return replaced;
    }

    public virtual void MouseLeft()
    {
        Toggle();
    }

    public virtual void MouseRight()
    {
    }

    public virtual void KeybindPressed()
    {
        Toggle();
        string text = $"[i:{(Enabled ? ItemOn : ItemOff)}] {(Enabled ? TextOn : TextOff).Value}";
        Color color = Enabled ? new(102, 255, 102) : new(255, 102, 102);
        Main.NewText(text, color);
    }
}
