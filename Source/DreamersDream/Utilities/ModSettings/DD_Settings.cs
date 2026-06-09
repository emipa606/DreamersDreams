using System.Collections.Generic;
using Verse;

namespace DreamersDream;

public class DD_Settings : ModSettings
{
    public static float dreamChancePerNight = 1f;
    public static bool isDreamingActive = true;

    public static bool isDefaultSettings = true;

    public static bool canNonSleepwalkerSleepwalk = true;

    public static readonly bool isDebugMode = false;

    public static Dictionary<string, float> TagsCustomChances = new();

    public static Dictionary<string, bool> TagsCustomNotify = new();

    //sleepwalk
    public static float sleepwalkerTraitModif = 1;

    public static readonly float occasionalSleepwalkerTraitModif = 3f;
    public static readonly float normalSleepwalkerTraitModif = 12.0f;
    public static readonly float usualSleepwalkerTraitModif = 30.0f;

    public static readonly float calmSleepwalkBaseChance = 1.0f;

    public override void ExposeData()
    {
        Scribe_Collections.Look(ref TagsCustomChances, "TagsCustomChances", LookMode.Value, LookMode.Value);
        Scribe_Collections.Look(ref TagsCustomNotify, "TagsCustomNotify", LookMode.Value, LookMode.Value);
        Scribe_Values.Look(ref isDreamingActive, "isDreamingActive", true);
        Scribe_Values.Look(ref isDefaultSettings, "isDefaultSettings", true);
        Scribe_Values.Look(ref sleepwalkerTraitModif, "sleepwalkerTraitModif", 1);
        Scribe_Values.Look(ref dreamChancePerNight, "dreamChancePerNight", 1f);

        base.ExposeData();
    }

    public static void PurgeDicts()
    {
        //chances
        if (TagsCustomChances.EnumerableNullOrEmpty())
        {
            TagsCustomChances = new Dictionary<string, float>();
        }

        var tempDictChances = new Dictionary<string, float>();

        foreach (var tag in DreamTracker.GetAllDreamTags)
        {
            if (TagsCustomChances.ContainsKey(tag.defName) && TagsCustomChances[tag.defName] != tag.chance)
            {
                tempDictChances.Add(tag.defName, TagsCustomChances[tag.defName]);
            }
        }

        TagsCustomChances.Clear();
        TagsCustomChances = tempDictChances;

        //notifications
        if (TagsCustomNotify.EnumerableNullOrEmpty())
        {
            TagsCustomNotify = new Dictionary<string, bool>();
        }

        var tempDictNotify = new Dictionary<string, bool>();

        foreach (var tag in DreamTracker.GetAllDreamTags)
        {
            if (TagsCustomNotify.TryGetValue(tag.defName, out var value))
            {
                tempDictNotify.Add(tag.defName, value);
            }
        }

        TagsCustomNotify.Clear();
        TagsCustomNotify = tempDictNotify;
    }
}