using System.Collections.Generic;
using RimWorld;

namespace DreamersDream;

[DefOf]
public class DreamDef : ThoughtDef
{
    public List<BackstoryCategory> conflictingBackstory = [];
    public List<BodyState> conflictingBodyStates = [];
    public List<MindState> conflictingMindStates = [];
    public List<StandingStatus> conflictingStanding = [];
    public List<ThoughtDef> conflictingThoughts = [];

    public string dreamedBy = "";
    public MoodStatus maxMood;

    public MoodStatus minMood;

    public List<BackstoryCategory> requiredBackstory = [];

    public List<BodyState> requiredBodyStates = [];

    public List<MindState> requiredMindStates = [];
    public List<BackstoryCategory> requiredOneOfBackstory = [];
    public List<BodyState> requiredOneOfBodyStates = [];
    public List<MindState> requiredOneOfMindStates = [];
    public List<StandingStatus> requiredOneOfStanding = [];
    public List<ThoughtDef> requiredOneOfThoughts = [];

    public List<StandingStatus> requiredStanding = [];

    public List<ThoughtDef> requiredThoughts = [];
    public List<DreamTagDef> tags = [];
}