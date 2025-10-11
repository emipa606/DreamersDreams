# Dreamer's Dreams (Continued) - GitHub Copilot Instructions

## Overview

**Mod Name**: Dreamer's Dreams (Continued)

**Purpose**: This mod extends upon Iwo Rosiak’s original mod to introduce dynamic and immersive dream mechanics to the game RimWorld. Designed to enrich the narrative and depth of the game, it allows player characters (pawns) to experience dreams that reflect their personality and status, which can influence their mental state and behavior.

**Mod Description**: 
Every night, players' pawns can experience dreams based on various factors such as personality traits, current situations, and overall game conditions. These dreams will have short-term or long-lasting effects, potentially inducing chaos or stability in the colony. The dream content is crafted by a professional writer and draws inspiration from H.P. Lovecraft’s dreamland stories.

## Key Features and Systems

- **Dream Mechanic**: 
  - Pawns receive dream thoughts as seen in the Needs tab when they sleep.
  - Over 100 unique dreams tailored to specific personality types and situations (e.g., cannibals, prisoners, widowed pawns).

- **Dream Customization**: 
  - Customizable settings allow for adjusting dream frequency and impact to fit different playstyles.
  - Players can suggest new dream ideas via GitHub or the mod's dedicated platforms.

- **Sleepwalking Mechanic**: 
  - Adds unpredictability by allowing pawns to perform actions while sleepwalking, influenced by their dreams.

- **Compatibility**: 
  - Compatible with most race mods and can be added to existing save games.

## Coding Patterns and Conventions

- **Object-Oriented Programming**:
  - Mod heavily relies on OOP principles using C#.
  - Modular design with coherent class responsibilities (e.g., `DreamDef`, `DreamTagDef`, `DreamSelector`).
  
- **Static Helper Classes**:
  - Use of `internal static` classes for utility functions, such as (`DreamsCompHelper`, `ModSettingsUtility`).

- **Encapsulation**:
  - Private methods used within classes to encapsulate functionality (e.g., `ConvertOddsForDreamsToPercent` in `PawnDreamOddsTracker`).

## XML Integration

- **Defs and XML Usage**: 
  - Dream definitions and configurations are managed via XML files using custom Def classes, which facilitate extensibility and easy modifications.
  - XML is utilized for defining dream properties that can easily be extended or overridden by additional mods.

## Harmony Patching

- **Harmony**: 
  - Implements Harmony for safe and non-destructive patching to integrate with existing game systems.
  - Suggested to explore potential methods and hooks for adding or altering dream mechanics without directly modifying core game code. Ensure patches are reversible and critical sections are well-documented.

## Suggestions for Copilot

1. **Dream Content**: 
   - Suggest narrative snippets for new dream sequences considering current game conditions and defined personalities.

2. **Algorithm Enhancement**: 
   - Assist in developing complex algorithms for better dream scenario selection based on pawn attributes.

3. **Modular Extensions**: 
   - Generate code snippets for extending current features, e.g., adding new types of sleepwalking actions.

4. **Compatibility Checks**: 
   - Suggest coding best practices for ensuring compatibility with other mods, particularly race and behavior mods.

5. **Debugging Utilities**:
   - Automate debugging code generation, especially for tracking dream activation and pawn states post-dream.

6. **User Interface Improvements**: 
   - Generate UI code snippets for better visualization and customization options within mod settings.

## Contributor Guidelines

- **Submit a Dream**: 
  - Contributors are encouraged to submit dream ideas that can be credited and potentially incorporated.
  
- **Testing and Feedback**: 
  - Active participation by using the mod in different scenarios and reporting issues on GitHub to improve the mod’s stability and feature set.

- **Code Contribution**: 
  - Contributions to code will be welcomed via GitHub pull requests, ensuring alignment with the outlined coding patterns and conventions.

## Community and Support

- Join the mod's [Discord server](#) to be part of an active community where you can share ideas, report issues, and propose improvements.

Happy modding and dream weaving!
