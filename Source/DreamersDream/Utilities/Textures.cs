using UnityEngine;
using Verse;

namespace DreamersDream;

[StaticConstructorOnStartup]
internal static class Textures
{
    public static readonly Texture2D SettingsBackGround = ContentFinder<Texture2D>.Get("UI/ModSettings/background");

    public static readonly Texture2D TableEntryBGChance1 =
        ContentFinder<Texture2D>.Get("UI/ModSettings/TableEntryBGChance1");

    public static readonly Texture2D TableEntryBGChance2 =
        ContentFinder<Texture2D>.Get("UI/ModSettings/TableEntryBGChance2");

    public static readonly Texture2D TableEntryBGCat1 = ContentFinder<Texture2D>.Get("UI/ModSettings/TableEntryBGCat1");

    public static readonly Texture2D TableEntryBGCat2 = ContentFinder<Texture2D>.Get("UI/ModSettings/TableEntryBGCat2");

    public static readonly Texture2D TableEntryBGNotify1 =
        ContentFinder<Texture2D>.Get("UI/ModSettings/TableEntryBGNotify1");

    public static readonly Texture2D TableEntryBGNotify2 =
        ContentFinder<Texture2D>.Get("UI/ModSettings/TableEntryBGNotify2");
}