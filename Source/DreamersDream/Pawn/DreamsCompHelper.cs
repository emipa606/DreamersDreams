using RimWorld;
using RimWorld.Planet;
using Verse;

namespace DreamersDream;

internal static class DreamsCompHelper
{
    public static bool CanGetDreamNow(this Pawn pawn)
    {
        return pawn.IsPawnCapableOfDreaming() && !pawn.IsAwake() && pawn.IsPawnRestedEnough() &&
               !pawn.HasDreamAlready() &&
               pawn.ageTracker.Adult;
    }

    private static bool IsPawnCapableOfDreaming(this Pawn pawn)
    {
        return !pawn.Dead && pawn.needs.mood != null && pawn.needs?.rest != null && !pawn.Dead &&
               (pawn.Spawned || pawn.IsCaravanMember());
    }

    private static bool IsAwake(this Pawn pawn)
    {
        if (pawn.needs.rest.GUIChangeArrow == 1)
        {
            return false;
        }

        return pawn.needs.rest.GUIChangeArrow == -1;
    }

    private static bool IsPawnRestedEnough(this Pawn pawn)
    {
        return pawn.needs.rest.CurCategory == RestCategory.Rested;
    }

    private static bool HasDreamAlready(this Pawn pawn)
    {
        foreach (var memory in pawn.needs.mood.thoughts.memories.Memories)
        {
            if (memory.def.GetType() == typeof(DreamDef))
            {
                return true;
            }
        }

        return false;
    }

    //Sleepwalking

    public static bool ShouldSleepwalkNow(this Pawn pawn)
    {
        if (!pawn.IsSleepwalker())
        {
            return false;
        }

        var sleepwalkChance = pawn.GetPawnSleepwalkingChance();

        if (sleepwalkChance == 0)
        {
            return false;
        }

        float roll = Rand.RangeInclusive(0, 60);

        return roll <= sleepwalkChance;
    }

    private static float GetPawnSleepwalkingChance(this Pawn pawn)
    {
        float sleepwalkChance = 0;
        if (pawn?.story.traits.HasTrait(DD_TraitDefOf.Sleepwalker) != true)
        {
            return sleepwalkChance;
        }

        sleepwalkChance = DD_Settings.sleepwalkerTraitModif;
        switch (pawn.story.traits.DegreeOfTrait(DD_TraitDefOf.Sleepwalker))
        {
            case 1:
                sleepwalkChance *= DD_Settings.occasionalSleepwalkerTraitModif;
                break;

            case 2:
                sleepwalkChance *= DD_Settings.normalSleepwalkerTraitModif;
                break;

            case 3:
                sleepwalkChance *= DD_Settings.usualSleepwalkerTraitModif;
                break;
        }

        return sleepwalkChance;
    }
}