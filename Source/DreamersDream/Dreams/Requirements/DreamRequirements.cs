using RimWorld;
using Verse;

namespace DreamersDream;

internal static class DreamRequirements
{
    public static bool IsMeetingRequirements(this DreamDef dream, Pawn pawn)
    {
        return ThoughtUtility.CanGetThought(pawn, dream, true) && dream.CheckRequirementForPawn(pawn);
    }

    public static bool IsMeetingRequirements(this DreamTagDef tag, Pawn pawn)
    {
        return true;
    }
}