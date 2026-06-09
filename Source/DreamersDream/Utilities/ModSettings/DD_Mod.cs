using System;
using UnityEngine;
using Verse;

namespace DreamersDream;

public class DD_Mod : Mod
{
    private static Vector2 scrollPos = Vector2.zero;

    private static readonly float columnHeight = 25f;
    private DD_Settings settings;

    public DD_Mod(ModContentPack content) : base(content)
    {
        settings = GetSettings<DD_Settings>();
    }

    public override void DoSettingsWindowContents(Rect inRect)
    {
        Widgets.DrawTextureFitted(new Rect(inRect.x, inRect.y, inRect.width, inRect.height),
            Textures.SettingsBackGround, 1);

        var masterRect = new Rect(inRect.x + (0.1f * inRect.width), inRect.y + 40, 0.8f * inRect.width, 936);

        var listingTop = new Listing_Standard();
        var topSettings = new Rect(masterRect.x, masterRect.y, masterRect.width, 45);

        listingTop.Begin(topSettings); //column 1
        listingTop.ColumnWidth = topSettings.width / 3.2f;

        if (listingTop.ButtonText("DrDr.ModActive".Translate(DD_Settings.isDreamingActive.ToString())))
        {
            DD_Settings.isDreamingActive = !DD_Settings.isDreamingActive;
        }

        var midSettings = new Rect(topSettings.x, topSettings.y + listingTop.CurHeight, masterRect.width, 180f);

        listingTop.NewColumn(); // column 2
        if (DD_Settings.isDreamingActive)
        {
            if (listingTop.ButtonText("DrDr.DefaultSettings".Translate(DD_Settings.isDefaultSettings.ToString())))
            {
                DD_Settings.isDefaultSettings = !DD_Settings.isDefaultSettings;
            }
        }

        listingTop.NewColumn(); //column 3
        if (listingTop.ButtonText("DrDr.Reset".Translate(), "DrDr.SetNow".Translate()))
        {
            ResetValues();
        }

        listingTop.End();

        var listingMid = new Listing_Standard();
        if (!DD_Settings.isDreamingActive || DD_Settings.isDefaultSettings)
        {
            return;
        }

        listingMid.Begin(midSettings);

        listingMid.GapLine();

        listingMid.Label("DrDr.dreamChancePerNight".Translate(DD_Settings.dreamChancePerNight.ToStringPercent()));
        DD_Settings.dreamChancePerNight =
            listingMid.Slider(DD_Settings.dreamChancePerNight, 0, 1);


        if (DD_Settings.sleepwalkerTraitModif == 0)
        {
            listingMid.Label("DrDr.SleepwalkingOff".Translate());
            listingMid.Gap();
            listingMid.Gap();
            listingMid.Gap();
        }
        else
        {
            listingMid.Label("DrDr.SleepwalkingOccasional".Translate(DD_Settings.occasionalSleepwalkerTraitModif *
                                                                     DD_Settings.sleepwalkerTraitModif));
            listingMid.Label("DrDr.Sleepwalking".Translate(DD_Settings.normalSleepwalkerTraitModif *
                                                           DD_Settings.sleepwalkerTraitModif));
            listingMid.Label("DrDr.SleepwalkingUsual".Translate(DD_Settings.usualSleepwalkerTraitModif *
                                                                DD_Settings.sleepwalkerTraitModif));
        }

        DD_Settings.sleepwalkerTraitModif =
            (float)Math.Round(listingMid.Slider(DD_Settings.sleepwalkerTraitModif, 0, 2), 2);


        listingMid.GapLine();

        var tableSettings = new Rect(masterRect.x, midSettings.y + listingMid.CurHeight, 320f, inRect.height);
        tableSettings.height = inRect.height - tableSettings.y;

        listingMid.End();

        var tagTable = new Rect(tableSettings.x, tableSettings.y, tableSettings.width - 10f,
            columnHeight * CountDisplayableTags());
        Widgets.BeginScrollView(tableSettings, ref scrollPos, tagTable);

        DrawTagsRows(tagTable);

        Widgets.EndScrollView();
    }

    private static void DrawTagsRows(Rect TableRect)
    {
        float count = 0;

        var ScrollYPos = TableRect.y;

        var columnTags = new Rect(TableRect.x, ScrollYPos, 100f, columnHeight);
        ModSettingsUtility.DrawTableFirstRow(ref columnTags, "DrDr.Tags".Translate());

        var columnChance = new Rect(columnTags.x + columnTags.width, ScrollYPos, 146f, columnHeight);
        ModSettingsUtility.DrawTableFirstRow(ref columnChance, "DrDr.Chance".Translate());

        var columnNotify = new Rect(columnChance.x + columnChance.width, ScrollYPos, 50f, columnHeight);
        ModSettingsUtility.DrawTableFirstRow(ref columnNotify, "DrDr.Notify".Translate());

        foreach (var dreamTag in DreamTracker.GetAllDreamTags)
        {
            if (dreamTag.chance == 0)
            {
                continue;
            }

            ResolveAlternatingBG(count, columnTags, Textures.TableEntryBGCat1, Textures.TableEntryBGCat2);
            DrawColumnCategory(columnTags, dreamTag);

            ResolveAlternatingBG(count, columnChance, Textures.TableEntryBGChance1, Textures.TableEntryBGChance2);
            DrawColumnChance(columnChance, dreamTag);

            ResolveAlternatingBG(count, columnNotify, Textures.TableEntryBGNotify1, Textures.TableEntryBGNotify1);
            DrawColumnNotify(columnNotify, dreamTag);

            count++;

            columnTags.y += columnHeight;
            columnChance.y += columnHeight;
            columnNotify.y += columnHeight;
        }
    }

    private static void DrawColumnCategory(Rect column, DreamTagDef tag)
    {
        Widgets.Label(ModSettingsUtility.GetMiddleOfRectForString(column, tag.label), tag.defName);
    }

    private static void DrawColumnChance(Rect column, DreamTagDef tag)
    {
        var chance = ModSettingsUtility.ResolveCustomChanceForTag(tag);

        ModSettingsUtility.DrawChanceButtons(column, ref chance);

        var label = $"{Math.Round(
            PawnDreamTagsOddsTracker.ChanceInPercentages(chance,
                ModSettingsUtility.AddUpChancesForQualities()), 2)}%";
        Widgets.Label(ModSettingsUtility.GetMiddleOfRectForString(column, label), label);

        ModSettingsUtility.UpdateCustomChanceForTag(tag, chance);
    }

    private static void DrawColumnNotify(Rect column, DreamTagDef tag)
    {
        var notify = tag.isSpecial;
        ModSettingsUtility.CheckIfHasCustomNotifyAndAddIfNot(tag, ref notify);

        if (Widgets.ButtonText(column, notify.ToString().CapitalizeFirst()))
        {
            notify = !notify;
        }

        DD_Settings.TagsCustomNotify[tag.defName] = notify;
    }

    private static void ResetValues()
    {
        DD_Settings.TagsCustomChances.Clear();

        DD_Settings.TagsCustomNotify.Clear();

        DD_Settings.sleepwalkerTraitModif = 1;
    }

    private static void ResolveAlternatingBG(float alternatingBGCount, Rect column, Texture texture1, Texture texture2)
    {
        switch (alternatingBGCount % 2)
        {
            case 0:
                Widgets.DrawTextureFitted(column, texture1, 1);
                break;

            case 1:
                Widgets.DrawTextureFitted(column, texture2, 1);
                break;
        }
    }

    private static float CountDisplayableTags()
    {
        float numberOfRows = 1;

        foreach (var tag in DreamTracker.GetAllDreamTags)
        {
            if (tag.chance != 0)
            {
                numberOfRows++;
            }
        }

        return numberOfRows;
    }

    public override string SettingsCategory()
    {
        return "Dreamer's Dreams";
    }
}