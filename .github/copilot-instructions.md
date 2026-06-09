# GitHub Copilot Instructions for Dreamer's Dreams (Continued)

## Mod Overview and Purpose
The "Dreamer's Dreams (Continued)" mod, originally created by Iwo Rosiak and now continued by the community, enhances the gameplay experience in RimWorld by introducing a dream system for pawns. Inspired by the dreamland stories of H.P. Lovecraft, the mod allows pawns to experience dreams when they sleep, adding depth to their personalities and story arcs. Dreams can have varying effects on the mental and emotional states of colonists, thus introducing an element of unpredictability and complexity in the management of the colony.

## Key Features and Systems
- **Dream System:** Upon falling asleep, pawns are assigned a dream-related thought that appears in the Needs tab.
- **Dream Variety:** Over 100 dreams are crafted, which reflect different qualities and requirements based on the pawn's attributes and circumstances, such as their traits, social status, and mood.
- **Customizable Settings:** The mod provides options in the settings menu to customize dream occurrences and effects.
- **Sleepwalk Mechanic:** Some pawns may exhibit sleepwalking behavior, leading to unexpected nocturnal activities, potentially hazardous in nature.
- **Balance and Compatibility:** Designed with customization in mind to fit different playstyles, the mod works with existing saves and is compatible with most other mods, especially race mods.

## Coding Patterns and Conventions
- **C# Structure:** The project primarily consists of classes for managing dream definitions, calculating dream chances, and integrating traits. Follow the convention of one class per file, naming files to match the primary class they contain.
- **Naming Conventions:** Use PascalCase for class names and publicly accessible methods, and camelCase for private variables and parameters.
- **Comments and Documentation:** Ensure all methods and classes are well-documented with XML-style comments to explain their functionality and any parameters or returns.

## XML Integration
- **Dream Definitions:** Utilize XML files to define dreams, categorized based on types (e.g., BaseDreams, NegativeDreamDefs, PositiveDreamDefs). Each dream is defined using custom XML defs that tie into the C# logic.
- **Consistency:** Maintain a consistent schema for defining dreams to ensure correct parsing and integration by the mod.
- **Error Handling:** Be vigilant of potential XML parsing errors and ensure all references in C# are correctly aligned with XML definitions.

## Harmony Patching
- **Patching Philosophy:** Use Harmony to patch core RimWorld functionality responsibly. Apply patches judiciously to alter behavior without directly modifying base game files.
- **Using Harmony Annotations:** Utilize [HarmonyPatch] attributes to target specific methods in the RimWorld base code.
- **Testing:** Thoroughly test patches in various scenarios to prevent conflicts and ensure robust integration.

## Suggestions for Copilot
- Given the high number of defined dreams, automate or assist in generating new dream scenarios by using Copilot to identify patterns in dream def XML files.
- Leverage Copilot to refactor commonly repeated logic in C# files, such as dream chance calculations, into reusable methods or extensions.
- Utilize Copilot's completion suggestions to maintain consistency in naming conventions and XML structure, reducing manual errors and speeding up development.

## Additional Tips
- When troubleshooting or developing new features, enable detailed logging to quickly identify if issues arise from dream logic or integration points.
- Before releasing any updates, perform comprehensive testing on both new and existing save files to catch errors early and ensure smooth player experiences.
- Engage with the modding community for feedback on dream balancing and feature suggestions. This can inform future iterations of the mod.

Happy dreaming and coding!

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

