using RimWorld;

namespace DreamersDream;

[DefOf]
public static class DD_TraitDefOf
{
    public static TraitDef Sleepwalker;

    public static TraitDef Gourmand;

    public static TraitDef NaturalMood;

    public static TraitDef Nerves;

    static DD_TraitDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(DD_TraitDefOf));
    }
}