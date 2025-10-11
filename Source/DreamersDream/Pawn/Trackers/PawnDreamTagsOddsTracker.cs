using System.Collections.Generic;
using Verse;

namespace DreamersDream;

public class PawnDreamTagsOddsTracker(Pawn parent)
{
    private readonly Dictionary<DreamTagDef, float> DreamTagOddsPercent = new();

    public Dictionary<DreamTagDef, float> GetUpdatedTagsWithChances()
    {
        ConvertOddsForDreamTagsToPercent();
        return DreamTagOddsPercent;
    }

    private Dictionary<DreamTagDef, float> UpdateOddsForDreamTags()
    {
        var DreamTagOdds = new Dictionary<DreamTagDef, float>();
        foreach (var dreamTag in DreamTracker.GetAllDreamTags)
        {
            var chanceForTag = dreamTag?.CalculateChanceFor(parent) ?? 0;

            if (chanceForTag != 0)
            {
                DreamTagOdds.Add(dreamTag, chanceForTag);
            }
        }

        return DreamTagOdds;
    }

    private void ConvertOddsForDreamTagsToPercent()
    {
        float chanceForTagPercent = 0;
        DreamTagOddsPercent.Clear();
        foreach (var dreamTag in UpdateOddsForDreamTags())
        {
            chanceForTagPercent += ChanceInPercentages(dreamTag.Value, AddUpChancesForTags());

            DreamTagOddsPercent.Add(dreamTag.Key, chanceForTagPercent);
        }
    }

    private float AddUpChancesForTags()
    {
        float sumOfCollectionChances = 0;
        foreach (var item in UpdateOddsForDreamTags())
        {
            sumOfCollectionChances += item.Value;
        }

        return sumOfCollectionChances;
    }

    public static float ChanceInPercentages(float chance, float sumOfChances)
    {
        return chance / sumOfChances * 100;
    }
}