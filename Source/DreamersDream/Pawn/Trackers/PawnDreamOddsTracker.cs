using System.Collections.Generic;
using Verse;

namespace DreamersDream;

public class PawnDreamOddsTracker(Pawn parent)
{
    private readonly Dictionary<DreamDef, float> DreamOdds = new();

    private readonly Dictionary<DreamDef, float> DreamOddsPercent = new();

    public Dictionary<DreamDef, float> GetUpdatedDreamsWithChances(DreamTagDef tag)
    {
        UpdateOddsForDreams(tag);
        ConvertOddsForDreamsToPercent();
        return DreamOddsPercent;
    }

    private void UpdateOddsForDreams(DreamTagDef tag)
    {
        DreamOdds.Clear();

        foreach (var dream in IsolateDreamsOfTag(tag))
        {
            var chanceForDream = dream?.CalculateChanceFor(parent) ?? 0;

            if (chanceForDream != 0)
            {
                DreamOdds.Add(dream, chanceForDream);
            }
        }
    }

    private void ConvertOddsForDreamsToPercent()
    {
        DreamOddsPercent.Clear();
        float chanceForTagPercent = 0;
        foreach (var dream in DreamOdds)
        {
            chanceForTagPercent += ChanceInPercentages(dream.Value, AddUpChancesForDreams());

            DreamOddsPercent.Add(dream.Key, chanceForTagPercent);
        }
    }

    private float AddUpChancesForDreams()
    {
        float sumOfCollectionChances = 0;
        foreach (var item in DreamOdds)
        {
            sumOfCollectionChances += item.Value;
        }

        return sumOfCollectionChances;
    }

    private static List<DreamDef> IsolateDreamsOfTag(DreamTagDef tag)
    {
        var MatchingDreams = new List<DreamDef>();

        foreach (var dream in DreamTracker.GetAllDreams)
        {
            if (dream.tags.Contains(tag))
            {
                MatchingDreams.Add(dream);
            }
        }

        if (MatchingDreams.NullOrEmpty())
        {
            Log.Error($"{tag.defName} has no compatible dreams for this pawn or is empty.");
        }

        return MatchingDreams;
    }

    private static float ChanceInPercentages(float chance, float sumOfChances)
    {
        return chance / sumOfChances * 100;
    }
}