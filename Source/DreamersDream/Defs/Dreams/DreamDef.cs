using System.Collections.Generic;
using RimWorld;

namespace DreamersDream;

[DefOf]
public class DreamDef : ThoughtDef
{
    public readonly List<BackstoryCategory> conflictingBackstory = [];
    public readonly List<BodyState> conflictingBodyStates = [];
    public readonly List<MindState> conflictingMindStates = [];
    public readonly List<StandingStatus> conflictingStanding = [];
    public readonly List<ThoughtDef> conflictingThoughts = [];

    public readonly string dreamedBy = "";

    public readonly List<BackstoryCategory> requiredBackstory = [];

    public readonly List<BodyState> requiredBodyStates = [];

    public readonly List<MindState> requiredMindStates = [];
    public readonly List<BackstoryCategory> requiredOneOfBackstory = [];
    public readonly List<BodyState> requiredOneOfBodyStates = [];
    public readonly List<MindState> requiredOneOfMindStates = [];
    public readonly List<StandingStatus> requiredOneOfStanding = [];
    public readonly List<ThoughtDef> requiredOneOfThoughts = [];

    public readonly List<StandingStatus> requiredStanding = [];

    public readonly List<ThoughtDef> requiredThoughts = [];
    public readonly List<DreamTagDef> tags = [];
    public MoodStatus maxMood;

    public MoodStatus minMood;
}