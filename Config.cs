using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace GuideToggles;

internal class Config : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;
    public static Config Instance;

    internal bool Right => Corner is Corners.TopRight or Corners.BottomRight;
    internal bool Bottom => Corner is Corners.BottomLeft or Corners.BottomRight;


    [ReloadRequired]
    [DefaultValue(false)]
    public bool EnableKeybinds;

    [DrawTicks]
    [DefaultValue(ButtonModes.InventoryOnly)]
    [JsonConverter(typeof(StringEnumConverter))]
    public ButtonModes ButtonMode;


    [Header("Position")]

    [DefaultValue(false)]
    public bool Horizontal;
    
    [DrawTicks]
    [DefaultValue(Corners.TopLeft)]
    [JsonConverter(typeof(StringEnumConverter))]
    public Corners Corner;

    [DefaultValue(320)]
    [Range(0, int.MaxValue)]
    [Increment(10)]
    public int Offset;
}

internal enum ButtonModes
{
    InventoryOnly, AlwaysVisible, AlwaysInteractable
}

internal enum Corners
{
    TopLeft, BottomLeft, TopRight, BottomRight
}