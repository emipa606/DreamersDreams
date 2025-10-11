using Verse;

namespace DreamersDream;

[StaticConstructorOnStartup]
internal static class DreamersDreamsInitialisation
{
    static DreamersDreamsInitialisation()
    {
        LoadDreams();
        LoadDreamTags();
    }

    private static void LoadDreams()
    {
        var totalDreams = 0;

        foreach (var def in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(DreamDef)))
        {
            var dream = (DreamDef)def;
            if (!dream.tags.NullOrEmpty())
            {
                totalDreams++;
                DreamTracker.GetAllDreams.Add(dream);
            }
            else
            {
                Log.Warning($"Dream {dream.defName} does not have a tag, so it will not be loaded.");
            }
        }

        Log.Message($"Dreamer's Dreams: successfully loaded {totalDreams} dreams.");
    }

    private static void LoadDreamTags()
    {
        var totalTags = 0;

        foreach (var def in GenDefDatabase.GetAllDefsInDatabaseForDef(typeof(DreamTagDef)))
        {
            var tag = (DreamTagDef)def;
            if (!(tag.chance > 0))
            {
                continue;
            }

            totalTags++;
            DreamTracker.GetAllDreamTags.Add(tag);
        }

        DD_Settings.PurgeDicts();

        Log.Message($"Dreamer's Dreams: successfully loaded {totalTags} dream tags.");
    }
}