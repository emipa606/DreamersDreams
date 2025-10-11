using Verse;

namespace DreamersDream;

internal static class DreamChanceCalculator
{
    public static float CalculateChanceFor(this DreamDef dream, Pawn pawn)
    {
        var chance = dream.tags[0].CalculateChanceFor(pawn);

        if (!dream.IsMeetingRequirements(pawn))
        {
            return 0;
        }

        return chance;
    }

    public static float CalculateChanceFor(this DreamTagDef dreamTag)
    {
        return GetCustomChance(dreamTag);
    }

    public static float CalculateChanceFor(this DreamTagDef dreamTag, Pawn pawn)
    {
        var chance = GetCustomChance(dreamTag);

        if (!dreamTag.IsMeetingRequirements(pawn))
        {
            return 0;
        }

        return chance;
    }

    private static float GetCustomChance(DreamTagDef tag)
    {
        if (DD_Settings.TagsCustomChances?.ContainsKey(tag.defName) == true && !DD_Settings.isDefaultSettings)
        {
            return DD_Settings.TagsCustomChances[tag.defName];
        }

        return tag.chance;
    }
}