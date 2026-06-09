using RimWorld;
using RimWorld.Planet;
using Verse;

namespace DreamersDream;

internal static class DreamsCompHelper
{
    extension(Pawn pawn)
    {
        public bool CanGetDreamNow()
        {
            return pawn.IsPawnCapableOfDreaming() && !pawn.IsAwake() && pawn.IsPawnRestedEnough() &&
                   !pawn.HasDreamAlready() && pawn.ageTracker.Adult && Rand.Chance(DD_Settings.dreamChancePerNight);
        }

        private bool IsPawnCapableOfDreaming()
        {
            return !pawn.Dead && pawn.needs.mood != null && pawn.needs?.rest != null && !pawn.Dead &&
                   (pawn.Spawned || pawn.IsCaravanMember());
        }

        private bool IsAwake()
        {
            if (pawn.needs.rest.GUIChangeArrow == 1)
            {
                return false;
            }

            return pawn.needs.rest.GUIChangeArrow == -1;
        }

        private bool IsPawnRestedEnough()
        {
            return pawn.needs.rest.CurCategory == RestCategory.Rested;
        }

        private bool HasDreamAlready()
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

        public bool ShouldSleepwalkNow()
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

        private float GetPawnSleepwalkingChance()
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

    //Sleepwalking
}