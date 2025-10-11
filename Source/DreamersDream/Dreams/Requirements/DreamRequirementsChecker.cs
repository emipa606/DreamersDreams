using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace DreamersDream;

internal static class DreamRequirementsChecker
{
    private static bool HasBackstory(this BackstoryCategory backstoryCategory, Pawn pawn, bool checksForOpposite)
    {
        var hasBackStory = pawn.story?.GetBackstory(BackstorySlot.Childhood)?.spawnCategories
            .FirstOrDefault(category => category == backstoryCategory.ToString()) != null;

        if (checksForOpposite)
        {
            hasBackStory = !hasBackStory;
        }

        return hasBackStory;
    }

    private static bool CheckStandingStatus(this StandingStatus standing, Pawn pawn, bool invert)
    {
        var hasStanding = true;
        switch (standing)
        {
            case StandingStatus.prisoner:
                if (!pawn.IsPrisoner)
                {
                    hasStanding = false;
                }

                break;

            case StandingStatus.colonist:
                if (!pawn.IsColonist)
                {
                    hasStanding = false;
                }

                break;

            case StandingStatus.guest:

                break;

            case StandingStatus.slave:
                if (!pawn.IsSlave)
                {
                    hasStanding = false;
                }

                break;
        }

        if (invert)
        {
            hasStanding = !hasStanding;
        }

        return hasStanding;
    }

    private static bool CheckBodyState(this BodyState health, Pawn pawn, bool invert)
    {
        var isHealthy = true;
        switch (health)
        {
            case BodyState.wounded:
                if (!pawn.health.hediffSet.HasNaturallyHealingInjury())
                {
                    isHealthy = false;
                }

                break;

            case BodyState.healthy:
                if (pawn.health.hediffSet.HasNaturallyHealingInjury() ||
                    pawn.health.hediffSet.AnyHediffMakesSickThought)
                {
                    isHealthy = false;
                }

                break;

            case BodyState.ill:
                if (!pawn.health.hediffSet.AnyHediffMakesSickThought)
                {
                    isHealthy = false;
                }

                break;

            case BodyState.hungry:
                if ((float)pawn.needs.food.CurCategory == 0)
                {
                    isHealthy = false;
                }

                break;

            case BodyState.fed:
                if ((float)pawn.needs.food.CurCategory != 0)
                {
                    isHealthy = false;
                }

                break;

            case BodyState.starving:
                if (!pawn.needs.food.Starving)
                {
                    isHealthy = false;
                }

                break;

            case BodyState.young:
                if (pawn.ageTracker.AgeBiologicalYears > 30)
                {
                    isHealthy = false;
                }

                break;

            case BodyState.old:
                if (pawn.ageTracker.AgeBiologicalYears < 55)
                {
                    isHealthy = false;
                }

                break;

            case BodyState.male:
                if (pawn.gender != Gender.Male)
                {
                    isHealthy = false;
                }

                break;

            case BodyState.female:
                if (pawn.gender != Gender.Female)
                {
                    isHealthy = false;
                }

                break;

            case BodyState.disabled:
                if (!CheckIfPartsAreMissing(pawn))
                {
                    isHealthy = false;
                }

                break;

            case BodyState.travelling:
                if (!pawn.IsCaravanMember())
                {
                    isHealthy = false;
                }

                break;

            /*case HealthStatus.reachedSkillCap:
                foreach (var skill in pawn.skills.skills)
                {
                    skill.LearningSaturatedToday;
                    flag = false;
                }
                break;*/
        }

        if (invert)
        {
            isHealthy = !isHealthy;
        }

        return isHealthy;
    }

    private static bool CheckIfPartsAreMissing(Pawn pawn)
    {
        var bodyParts = new List<BodyPartRecord>();

        bodyParts.AddRange(GetBodyPartsOfDef(BodyPartDefOf.Leg, pawn));
        bodyParts.AddRange(GetBodyPartsOfDef(BodyPartDefOf.Arm, pawn));

        foreach (var part in bodyParts)
        {
            if (pawn.health.hediffSet.PartIsMissing(part))
            {
                return true;
            }
        }

        return false;
    }

    private static List<BodyPartRecord> GetBodyPartsOfDef(BodyPartDef def, Pawn pawn)
    {
        var bodyParts = new List<BodyPartRecord>();
        foreach (var part in pawn.RaceProps.body.GetPartsWithDef(def))
        {
            bodyParts.Add(part);
        }

        return bodyParts;
    }

    private static bool CheckMindState(this MindState social, Pawn pawn, bool invert)
    {
        var validMindState = true;
        switch (social)
        {
            case MindState.married:

            case MindState.bonded:

            case MindState.hasFriend:

                break;

            case MindState.killer:
                if (pawn.records.GetValue(RecordDefOf.KillsHumanlikes) <= 0)
                {
                    validMindState = false;
                }

                break;

            case MindState.guilty:
                if (!pawn.guilt.IsGuilty)
                {
                    validMindState = false;
                }

                break;

            case MindState.hasEx:
                break;

            case MindState.aloneMap:
                if (pawn.Map.PlayerPawnsForStoryteller.EnumerableCount() != 1)
                {
                    validMindState = false;
                }

                break;

            case MindState.aloneWorld:
                if (Find.World.PlayerPawnsForStoryteller.EnumerableCount() != 1)
                {
                    validMindState = false;
                }

                break;
        }

        if (invert)
        {
            validMindState = !validMindState;
        }

        return validMindState;
    }

    private static void AssignMaxMoodValues(ref float moodValue, MoodStatus mood, Pawn pawn)
    {
        switch (mood)
        {
            case MoodStatus.happy:
                moodValue = 1f;
                break;

            case MoodStatus.content:
                moodValue = 0.9f;
                break;

            case MoodStatus.neutral:
                moodValue = 0.65f;
                break;

            case MoodStatus.stressed:
                moodValue = pawn.mindState.mentalBreaker.BreakThresholdMinor;
                break;

            case MoodStatus.onEdge:
                moodValue = pawn.mindState.mentalBreaker.BreakThresholdExtreme + 0.05f;
                break;

            case MoodStatus.aboutToBreak:
                moodValue = pawn.mindState.mentalBreaker.BreakThresholdExtreme;
                break;

            case MoodStatus.none:
                moodValue = 1f;
                break;
        }
    }

    private static void AssignMinMoodValues(ref float moodValue, MoodStatus mood, Pawn pawn)
    {
        switch (mood)
        {
            case MoodStatus.happy:
                moodValue = 0.9f;
                break;

            case MoodStatus.content:
                moodValue = 0.65f;
                break;

            case MoodStatus.neutral:
                moodValue = pawn.mindState.mentalBreaker.BreakThresholdMinor;
                break;

            case MoodStatus.stressed:
                moodValue = pawn.mindState.mentalBreaker.BreakThresholdExtreme + 0.05f;
                break;

            case MoodStatus.onEdge:
                moodValue = pawn.mindState.mentalBreaker.BreakThresholdExtreme;
                break;

            case MoodStatus.aboutToBreak:

            case MoodStatus.none:
                moodValue = 0;
                break;
        }
    }

    private static bool CheckMoodStatus(MoodStatus moodMin, MoodStatus moodMax, Pawn pawn)
    {
        var currentMood = pawn.needs.mood.CurLevel;

        float minMood = 0;
        float maxMood = 1;

        AssignMinMoodValues(ref minMood, moodMin, pawn);

        AssignMaxMoodValues(ref maxMood, moodMax, pawn);

        return !(minMood > currentMood || maxMood < currentMood);
    }

    internal static bool CheckRequirementForPawn(this DreamDef dream, Pawn pawn)
    {
        //backstory
        foreach (var req in dream.conflictingBackstory)
        {
            if (!req.HasBackstory(pawn, true))
            {
                return false;
                //Log.Message("21");
            }
        }

        foreach (var req in dream.requiredBackstory)
        {
            if (!req.HasBackstory(pawn, false))
            {
                return false;
                //Log.Message("22");
            }
        }

        foreach (var req in dream.requiredOneOfBackstory)
        {
            if (req.HasBackstory(pawn, false))
            {
                break;
                //Log.Message("23");
            }
        }

        //standing
        foreach (var req in dream.conflictingStanding)
        {
            if (!req.CheckStandingStatus(pawn, true))
            {
                return false;
                //Log.Message("31");
            }
        }

        foreach (var req in dream.requiredStanding)
        {
            if (!req.CheckStandingStatus(pawn, false))
            {
                return false;
                //Log.Message("32");
            }
        }

        foreach (var req in dream.requiredOneOfStanding)
        {
            if (req.CheckStandingStatus(pawn, false))
            {
                break;
                //Log.Message("33");
            }
        }

        //health
        foreach (var req in dream.conflictingBodyStates)
        {
            if (!req.CheckBodyState(pawn, true))
            {
                return false;
                //Log.Message("41");
            }
        }

        foreach (var req in dream.requiredBodyStates)
        {
            if (!req.CheckBodyState(pawn, false))
            {
                return false;
                //Log.Message("42");
            }
        }

        foreach (var req in dream.requiredOneOfBodyStates)
        {
            if (req.CheckBodyState(pawn, false))
            {
                //Log.Message("43");
                break;
            }
        }

        //social
        foreach (var req in dream.conflictingMindStates)
        {
            if (!req.CheckMindState(pawn, true))
            {
                return false;
                //Log.Message("51");
            }
        }

        foreach (var req in dream.requiredMindStates)
        {
            if (!req.CheckMindState(pawn, false))
            {
                return false;
                //Log.Message("52");
            }
        }

        foreach (var req in dream.requiredOneOfMindStates)
        {
            if (req.CheckMindState(pawn, false))
            {
                //Log.Message("53");
                break;
            }
        }

        //thought
        foreach (var req in dream.conflictingThoughts)
        {
            if (!CheckForThought(req, pawn, true))
            {
                return false;
            }
        }

        foreach (var req in dream.requiredThoughts)
        {
            if (!CheckForThought(req, pawn, false))
            {
                return false;
            }
        }

        foreach (var req in dream.requiredOneOfThoughts)
        {
            if (!CheckForThought(req, pawn, false))
            {
                break;
            }
        }

        //mood
        return CheckMoodStatus(dream.minMood, dream.maxMood, pawn);
        //Log.Message("6");
    }

    private static bool CheckForThought(ThoughtDef def, Pawn pawn, bool invert)
    {
        var hasThought = pawn.needs.mood.thoughts.memories.GetFirstMemoryOfDef(def) != null;

        if (invert)
        {
            hasThought = !hasThought;
        }

        return hasThought;
    }
}