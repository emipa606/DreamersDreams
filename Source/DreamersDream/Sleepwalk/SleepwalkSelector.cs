using System.Collections.Generic;
using RimWorld;
using Verse;

namespace DreamersDream;

internal static class SleepwalkSelector
{
    private static readonly Dictionary<SleepwalkingTypes, float> SleepwalkChancesThresholds = new();

    private static float sumOfChances;

    public static bool IsSleepwalker(this Pawn pawn)
    {
        return pawn.story.traits.HasTrait(DD_TraitDefOf.Sleepwalker);
    }

    public static DD_MentalStateDef ChooseSleepwalkState(this Pawn pawn)
    {
        switch (ChooseSleepwalkingType(pawn))
        {
            case SleepwalkingTypes.calm:
                Messages.Message("DrDr.WalkAround".Translate(pawn.Name.ToStringShort, pawn.gender.GetPossessive()),
                    pawn, MessageTypeDefOf.NeutralEvent);
                return DD_MentalStateDefOf.Sleepwalk;

            case SleepwalkingTypes.rage:
                Messages.Message("DrDr.Rage".Translate(pawn.Name.ToStringShort, pawn.gender.GetPossessive()), pawn,
                    MessageTypeDefOf.NeutralEvent);
                return DD_MentalStateDefOf.SleepwalkBerserk;

            case SleepwalkingTypes.food:
                Messages.Message("DrDr.Eat".Translate(pawn.Name.ToStringShort, pawn.gender.GetPossessive()),
                    pawn, MessageTypeDefOf.NeutralEvent);
                return DD_MentalStateDefOf.SleepwalkBingingFood;

            case SleepwalkingTypes.drugs:

            case SleepwalkingTypes.tantrum:
                Messages.Message("DrDr.Break".Translate(pawn.Name.ToStringShort, pawn.gender.GetPossessive()),
                    pawn, MessageTypeDefOf.NeutralEvent);
                return DD_MentalStateDefOf.SleepwalkTantrum;

            default:
                Log.Warning("Dreamer's Dreams: Failed to choose sleepwalking state.");
                Messages.Message("DrDr.WalkAround".Translate(pawn.Name.ToStringShort, pawn.gender.GetPossessive()),
                    pawn, MessageTypeDefOf.NeutralEvent);
                return DD_MentalStateDefOf.Sleepwalk;
        }
    }

    private static SleepwalkingTypes ChooseSleepwalkingType(Pawn pawn)
    {
        SetupChancesForSleewalk(pawn);

        var roll = Rand.Range(0, sumOfChances);

        foreach (var sleepwalkType in SleepwalkChancesThresholds)
        {
            if (roll <= sleepwalkType.Value)
            {
                return sleepwalkType.Key;
            }
        }

        Log.Warning("Dreamer's Dreams: Failed to choose sleepwalking type. Returning default.");
        return SleepwalkingTypes.calm;
    }

    private static void SetupChancesForSleewalk(Pawn pawn)
    {
        SleepwalkChancesThresholds.Clear();
        var currentThreshold = DD_Settings.calmSleepwalkBaseChance;
        SleepwalkChancesThresholds.Add(SleepwalkingTypes.calm, currentThreshold);

        currentThreshold += GetChanceForFoodSleepwalk(pawn);
        SleepwalkChancesThresholds.Add(SleepwalkingTypes.food, currentThreshold);

        currentThreshold += GetChanceForRageSleepwalk(pawn);
        SleepwalkChancesThresholds.Add(SleepwalkingTypes.rage, currentThreshold);

        currentThreshold += GetChanceForTantrumSleepwalk(pawn);
        SleepwalkChancesThresholds.Add(SleepwalkingTypes.tantrum, currentThreshold);

        sumOfChances = currentThreshold;
    }

    private static float GetChanceForRageSleepwalk(this Pawn pawn)
    {
        float aggressiveness = 0;

        foreach (var trait in pawn.story.traits.allTraits)
        {
            switch (trait.def.defName)
            {
                case "Bloodlust":
                    aggressiveness += 1.5f;
                    break;

                case "Psychopath":
                    aggressiveness++;
                    break;

                case "Cannibal":
                    aggressiveness += 0.5f;
                    break;

                case "Brawler":
                    aggressiveness += 0.2f;
                    break;
            }
        }

        return aggressiveness;
    }

    private static float GetChanceForTantrumSleepwalk(Pawn pawn)
    {
        float anger = 0;

        foreach (var trait in pawn.story.traits.allTraits)
        {
            switch (trait.def.defName)
            {
                case "Nerves":
                    switch (pawn.story.traits.DegreeOfTrait(DD_TraitDefOf.Nerves))
                    {
                        case -1:
                            anger++;
                            break;

                        case -2:
                            anger += 1.5f;
                            break;
                    }

                    break;

                case "Neurotic":
                    anger++;
                    break;
            }
        }

        return anger;
    }

    private static float GetChanceForFoodSleepwalk(Pawn pawn)
    {
        float foodDesire = 0;

        foreach (var trait in pawn.story.traits.allTraits)
        {
            switch (trait.def.defName)
            {
                case "Gourmand":
                    foodDesire += 2f;
                    break;

                case "NaturalMood":
                    switch (pawn.story.traits.DegreeOfTrait(DD_TraitDefOf.NaturalMood))
                    {
                        case -1:
                            foodDesire += 0.5f;
                            break;

                        case -2:
                            foodDesire++;
                            break;
                    }

                    break;
            }
        }

        return foodDesire;
    }
}