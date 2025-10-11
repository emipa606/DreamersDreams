using RimWorld;

namespace DreamersDream;

[DefOf]
public static class DreamTagDefOf
{
    public static DreamTagDef Sleepwalk;

    static DreamTagDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(DreamTagDefOf));
    }
}