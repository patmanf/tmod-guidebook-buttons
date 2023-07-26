using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace GuideToggles;

public class ButtonsUI : UIState
{
    public override void Draw(SpriteBatch spriteBatch)
    {
        int offset = 0;

        foreach (Button button in GuideToggles.Buttons)
        {
            if (!button.Visible) continue;

            Texture2D tex = (button.Enabled ? button.IconOn : button.IconOff).Value;
            Vector2 position = GetPosition(tex, ref offset);
            Rectangle rect = new((int)position.X, (int)position.Y, tex.Width, tex.Height);
            bool hovering = rect.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y);

            spriteBatch.Draw(tex, position, Color.White);

            if (hovering)
            {
                string text = (button.Enabled ? button.TextOn : button.TextOff).Value;
                Main.instance.MouseTextHackZoom(text);

                if (Main.playerInventory || Config.Instance.ButtonMode == ButtonModes.AlwaysInteractable)
                {
                    Main.blockMouse = true;

                    Texture2D borderTex = (button.Enabled ? button.BorderOn : button.BorderOff).Value;
                    spriteBatch.Draw(borderTex, position, Color.Yellow);

                    if (PlayerInput.Triggers.JustPressed.MouseLeft) button.MouseLeft();
                    if (PlayerInput.Triggers.JustPressed.MouseRight) button.MouseRight();
                }
            }
        }
    }

    private static Vector2 GetPosition(Texture2D tex, ref int offset)
    {
        Vector2 position = Config.Instance.Corner switch
        {
            Corners.TopLeft => new(-1, 0),
            Corners.BottomLeft => new(-1, Main.screenHeight - tex.Height),
            Corners.TopRight => new(Main.screenWidth - tex.Width, 0),
            Corners.BottomRight => new(Main.screenWidth - tex.Width, Main.screenHeight - tex.Height),
            _ => new()
        };

        if (Config.Instance.Horizontal)
        {
            position.X += (Config.Instance.Offset + offset) * (Config.Instance.Right ? -1 : 1);
            offset += tex.Width;
        }
        else
        {
            position.Y += (Config.Instance.Offset + offset) * (Config.Instance.Bottom ? -1 : 1);
            offset += tex.Height;
        }

        return position;
    }
}
