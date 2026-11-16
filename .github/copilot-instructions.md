# GitHub Copilot Instructions for Dreamer's Dreams (Continued) RimWorld Mod

## Mod Overview and Purpose

**Dreamer's Dreams (Continued)** is an update to Iwo Rosiak's original mod that adds the ability for pawns to have nightdreams. These dreams reflect the pawns' situation and personality, adding an element of instability and unpredictability to the colony. The mod includes various dream scenarios with short-term and long-term effects on the pawns.

## Key Features and Systems

### 1. Dreams
- Pawns gain dream thoughts when they fall asleep.
- Dreams are categorized into positive, negative, anxious, and fever dreams.
- Each dream has unique qualities and requirements based on various factors such as the pawn's traits, needs, and situation.

### 2. Sleepwalk Mechanic
- Pawns can sleepwalk at night, potentially engaging in various activities.
- This adds an extra layer of risk to the colony, as sleepwalkers might perform dangerous actions.

### 3. Customizable Mod Settings
- Players can adjust the frequency and types of dreams experienced by pawns.
- This allows for a wide range of customization to suit different playstyles.

### 4. Mental States
- Dreams can trigger or modify mental states, such as anxiety or fever.
- These mental states can have further effects on the pawn's behavior and well-being.

### 5. Traits and Tags
- Pawns with specific traits or tags are more likely to experience certain types of dreams.
- This adds depth to the pawn's personality and behavior.

## Coding Patterns and Conventions

### 1. XML Definitions
- All XML files are located in the `Defs` directory.
- Each XML file defines various aspects of the mod, such as dreams, mental states, and traits.
- XML definitions follow the RimWorld Defs format.

### 2. C# Code
- C# code is organized into multiple files within the `Source` directory.
- Key files include:
  - `DD_MentalStateDef.cs`: Defines mental state definitions.
  - `DD_TraitDefOf.cs`: Contains constants for trait definitions.
  - `DD_MentalStateDefOf.cs`: Contains constants for mental state definitions.
  - `DreamTagDefOf.cs`: Contains constants for dream tag definitions.
  - `DreamDef.cs`: Defines dream definitions.

### 3. Harmony Patching
- The mod uses Harmony patches to modify existing RimWorld behavior.
- Patches are located in the `Source` directory.
- Harmony patches follow the standard Harmony patching conventions.

### 4. Naming Conventions
- Class and method names follow PascalCase.
- Constant names follow SCREAMING_SNAKE_CASE.
- XML element names follow kebab-case.

## XML Integration

### 1. Defs Files
- The mod uses XML files to define various aspects of the game, such as dreams, mental states, and traits.
- Each XML file contains multiple definitions, each with its own set of properties.
- Example XML files include:
  - `BaseDreams.xml`: Contains base dream definitions.
  - `NegativeDreamDefs.xml`: Contains negative dream definitions.
  - `PositiveDreamDefs.xml`: Contains positive dream definitions.
  - `AnxiousDreams.xml`: Contains anxious dream definitions.
  - `FeverDreams.xml`: Contains fever dream definitions.

### 2. XML Structure
- Each XML file follows the standard RimWorld Defs structure.
- Example XML structure:
  xml
  <Defs>
    <DreamersDream.DreamDef>
      <defName>DD_Dream_Sadness</defName>
      <label>Sadness</label>
      <description>A dream of sadness and despair.</description>
      <positive>false</positive>
      <tags>
        <li>negative</li>
      </tags>
    </DreamersDream.DreamDef>
  </Defs>
  

## Harmony Patching

### 1. Patching Process
- The mod uses Harmony patches to modify existing RimWorld behavior.
- Patches are applied to methods in the RimWorld codebase.
- Example patch:
  csharp
  [HarmonyPatch(typeof(Pawn), "GetGreetingType")]
  public static class Pawn_GetGreetingType_Patch
  {
      public static void Postfix(Pawn __instance, ref GreetingType __result)
      {
          // Custom logic for greeting type
      }
  }
  

### 2. Patch Naming Conventions
- Patch classes are named using the following convention: `[HarmonyPatch]` followed by the class and method being patched.
- Postfix and prefix methods are named using the following convention: `Postfix` or `Prefix` followed by the method name.

## Suggestions for Copilot

### 1. XML Generation
- Generate XML definitions for new dreams, mental states, or traits.
- Example XML:
  xml
  <Defs>
    <DreamersDream.DreamDef>
      <defName>DD_Dream_Happiness</defName>
      <label>Happiness</label>
      <description>A dream of joy and contentment.</description>
      <positive>true</positive>
      <tags>
        <li>positive</li>
      </tags>
    </DreamersDream.DreamDef>
  </Defs>
  

### 2. Harmony Patching
- Generate Harmony patches for modifying existing RimWorld behavior.
- Example patch:
  csharp
  [HarmonyPatch(typeof(Pawn), "GetMood")]
  public static class Pawn_GetMood_Patch
  {
      public static void Postfix(Pawn __instance, ref Thought_Mood __result)
      {
          // Custom logic for mood
      }
  }
  

### 3. C# Code Generation
- Generate C# code for new mental state or trait definitions.
- Example C#:
  csharp
  public class DD_MentalStateDef : MentalStateDef
  {
      public new static DD_MentalStateDefOf DefOf;
  }

  public static class DD_MentalStateDefOf
  {
      public static DD_MentalStateDef DD_Anxiety;
  }
  

### 4. Mod Settings
- Generate mod settings for customizing dream frequency and types.
- Example settings:
  csharp
  public static class ModSettings
  {
      public static int DreamFrequency = 1;
      public static List<string> PositiveDreamTags = new List<string> { "positive" };
      public static List<string> NegativeDreamTags = new List<string> { "negative" };
  }
  

By following these guidelines, developers can efficiently contribute to the Dreamer's Dreams (Continued) mod using GitHub Copilot.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

