using System;
using System.Linq;

namespace EventChat
{
    using System.Diagnostics.CodeAnalysis;
    using Dalamud.Game.Text;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602",
        MessageId = "Enumeration items should be documented", Justification = "Lazy")]
    public enum ExChatType : ushort
    {
        [ExChatTypeAttribute("Say", "Cloud Strife: Never mind...", 700)]
        Say = XivChatType.Say,

        [ExChatTypeAttribute("Yell", "Cloud Strife: Aeriiiiiith!", 573)]
        Yell = XivChatType.Yell,

        [ExChatTypeAttribute("Shout", "Cloud Strife: Can someone give me some gil?", 557)]
        Shout = XivChatType.Shout,

        [ExChatTypeAttribute("Tell (Outgoing)", ">> Cloud Strife: Nice shirt.", 537)]
        TellOutgoing = XivChatType.TellOutgoing,

        [ExChatTypeAttribute("Tell (Incoming)", "Tifa Lockhart >> Eyes up here, Cloud.", 537)]
        TellIncoming = XivChatType.TellIncoming,

        [ExChatTypeAttribute("Party", "(Cloud Strife) Where is everybody?", 576)]
        Party = XivChatType.Party,

        [ExChatTypeAttribute("Alliance", "((Cloud Strife)) All right, everyone, let's mosey.", 500)]
        Alliance = XivChatType.Alliance,

        [ExChatTypeAttribute("Free Company", "[FC]<Cloud Strife> Can I invite my friend, Zack?", 69)]
        FreeCompany = XivChatType.FreeCompany,

        [ExChatTypeAttribute("PvP Team", "[PvP]<Cloud Strife> There's no \"I\" in team!")]
        PvPTeam = XivChatType.PvPTeam,

        [ExChatTypeAttribute("Cross-world Linkshell [1]", "[CWLS1]<Cloud Strife> You guys seen Hojo anywhere?")]
        CrossWorldLinkshell1 = XivChatType.CrossLinkShell1,

        [ExChatTypeAttribute("Cross-world Linkshell [2]", "[CWLS2]<Cloud Strife> You guys seen Hojo anywhere?")]
        CrossWorldLinkshell2 = XivChatType.CrossLinkShell2,

        [ExChatTypeAttribute("Cross-world Linkshell [3]", "[CWLS3]<Cloud Strife> You guys seen Hojo anywhere?")]
        CrossWorldLinkshell3 = XivChatType.CrossLinkShell3,

        [ExChatTypeAttribute("Cross-world Linkshell [4]", "[CWLS4]<Cloud Strife> You guys seen Hojo anywhere?")]
        CrossWorldLinkshell4 = XivChatType.CrossLinkShell4,

        [ExChatTypeAttribute("Cross-world Linkshell [5]", "[CWLS5]<Cloud Strife> You guys seen Hojo anywhere?")]
        CrossWorldLinkshell5 = XivChatType.CrossLinkShell5,

        [ExChatTypeAttribute("Cross-world Linkshell [6]", "[CWLS6]<Cloud Strife> You guys seen Hojo anywhere?")]
        CrossWorldLinkshell6 = XivChatType.CrossLinkShell6,

        [ExChatTypeAttribute("Cross-world Linkshell [7]", "[CWLS7]<Cloud Strife> You guys seen Hojo anywhere?")]
        CrossWorldLinkshell7 = XivChatType.CrossLinkShell7,

        [ExChatTypeAttribute("Cross-world Linkshell [8]", "[CWLS8]<Cloud Strife> You guys seen Hojo anywhere?")]
        CrossWorldLinkshell8 = XivChatType.CrossLinkShell8,

        [ExChatTypeAttribute("Linkshell [1]", "[1]<Cloud Strife> Do I have time to grab my gunblade?")]
        Linkshell1 = XivChatType.Ls1,

        [ExChatTypeAttribute("Linkshell [2]", "[2]<Cloud Strife> Do I have time to grab my gunblade?")]
        Linkshell2 = XivChatType.Ls2,

        [ExChatTypeAttribute("Linkshell [3]", "[3]<Cloud Strife> Do I have time to grab my gunblade?")]
        Linkshell3 = XivChatType.Ls3,

        [ExChatTypeAttribute("Linkshell [4]", "[4]<Cloud Strife> Do I have time to grab my gunblade?")]
        Linkshell4 = XivChatType.Ls4,

        [ExChatTypeAttribute("Linkshell [5]", "[5]<Cloud Strife> Do I have time to grab my gunblade?")]
        Linkshell5 = XivChatType.Ls5,

        [ExChatTypeAttribute("Linkshell [6]", "[6]<Cloud Strife> Do I have time to grab my gunblade?")]
        Linkshell6 = XivChatType.Ls6,

        [ExChatTypeAttribute("Linkshell [7]", "[7]<Cloud Strife> Do I have time to grab my gunblade?")]
        Linkshell7 = XivChatType.Ls7,

        [ExChatTypeAttribute("Linkshell [8]", "[8]<Cloud Strife> Do I have time to grab my gunblade?")]
        Linkshell8 = XivChatType.Ls8,

        [ExChatTypeAttribute("Novice Network", "[NOVICE] Cloud Strife: Follow me")]
        NoviceNetwork = XivChatType.NoviceNetwork,

        [ExChatTypeAttribute("Standard Emotes", "Cloud Strife points at Bayohne.")]
        StandardEmotes = XivChatType.StandardEmote,

        [ExChatTypeAttribute("Custom Emotes", "Cloud Strife wallows in self-pity...")]
        CustomEmotes = XivChatType.CustomEmote,

        DamageDealtByYou = 2729,
        FailedAttacksByYou = 2730,

        ActionsInitiatedByYou = 2091, //2291 You begin casting Diagnosis, 2091: You cast Diagnosis

        ItemsUsedByYou =
            2092, //2348: You ready a tuft of phoenix down, 2092: you use a tuft of phoenix down

        ItemUsedByYouReady = 2348,
        EffectsOfHealingSpellsCastByYou = 2221,
        BeneficialEffectsGrantedByYou = 2222,
        DetrimentalEffectsInflictedByYou = 2863,
        DamageYouAreDealt = 2857,
        FailedAttacksOnYou = 10410,
        ActionsUsedOnYou = 2219,
        ItemsUsedOnYou = 2220, //2220, 2092
        EffectsOfHealingSpellsCastOnYou = 16557, // Also seems to encompass 2221
        BeneficialEffectsGrantedToYou = 2222,
        DetrimentalEffectsInflictedOnYou = 2223,
        BeneficialEffectsOnYouEnding = 2224,
        DetrimentalEffectsOnYouEnding = 2225,

        DamageDealtByPartyMembers = 4905,
        FailedAttacksByPartyMembers = 4906,
        ActionsInitiatedByPartyMembers = 4139,
        ItemsUsedByPartyMembers = 4140,
        EffectsOfHealingSpellsCastByPartyMembers = 4397,
        BeneficialEffectsGrantedByPartyMembers = 4270,
        DetrimentalEffectsInflictedByPartyMembers = 4911,
        DamagePartyMembersAreDealt = 12585,
        FailedAttacksOnPartyMembers = 12586,
        ActionsUsedOnPartyMembers = 2347, //You begin casting Esuna

        ItemsUsedOnPartyMembers =
            0xFFFF, // TODO: Figure this out, most items fall under ItemsUsedByPartyMembers?

        EffectsOfHealingSpellsCastOnPartyMembers = 2349,
        BeneficialEffectsGrantedToPartyMembers = 4398,
        DetrimentalEffectsInflictedOnPartyMembers = 4399,
        BeneficialEffectsOnPartyMembersEnding = 4400,
        DetrimentalEffectsOnPartyMembersEnding = 4401,

        DamageDealtByAllianceMembers = 6953,
        FailedAttacksByAllianceMembers = 6570,
        ActionsInitiatedByAllianceMembers = 6187,
        ItemsUsedByAllianceMembers = 6188,
        EffectsOfHealingSpellsCastByAllianceMembers = 6537,
        BeneficialEffectsGrantedByAllianceMembers = 6574,
        DetrimentalEffectsInflictedByAllianceMembers = 6959,
        DamageAllianceMembersAreDealt = 12713, //10665: x hits y for z damage.
        FailedAttacksOnAllianceMembers = 10666, //10666 and 12714
        ActionsUsedOnAllianceMembers = 0xFFFF,
        ItemsUsedOnAllianceMembers = 0xFFFF,

        EffectsOfHealingSpellsCastOnAllianceMembers =
            0xFFFF, // TODO: What's the difference between this and cast by

        BeneficialEffectsGrantedToAllianceMembers = 2478,
        DetrimentalEffectsInflictedOnAllianceMembers = 6575, //6575 and 12719
        BeneficialEffectsOnAllianceMembersEnding = 6576,
        DetrimentalEffectsOnAllianceMembersEnding = 6577,

        DamageDealtByOthers = 0xFFFF,
        FailedAttacksByOthers = 0xFFFF,
        ActionsInitiatedByOthers = 0xFFFF,
        ItemsUsedByOthers = 0xFFFF,
        EffectsOfHealingSpellsCastByOthers = 0xFFFF,
        BeneficialEffectsGrantedByOthers = 0xFFFF,
        DetrimentalEffectsInflictedByOthers = 0xFFFF,
        DamageOthersAreDealt = 0xFFFF,
        FailedAttacksOnOthers = 0xFFFF,
        ActionsUsedOnOthers = 0xFFFF,
        ItemsUsedOnOthers = 0xFFFF,
        EffectsOfHealingSpellsCastOnOthers = 0xFFFF,
        BeneficialEffectsGrantedToOthers = 0xFFFF,
        DetrimentalEffectsInflictedOnOthers = 0xFFFF,
        BeneficialEffectsOnOthersEnding = 0xFFFF,
        DetrimentalEffectsOnOthersEnding = 0xFFFF,

        DamageDealtByEngagedEnemies = 0xFFFF,
        FailedAttacksByEngagedEnemies = 0xFFFF,
        ActionsInitiatedByEngagedEnemies = 0xFFFF,
        ItemsUsedByEngagedEnemies = 0xFFFF,
        EffectsOfHealingSpellsCastByEngagedEnemies = 0xFFFF,
        BeneficialEffectsGrantedByEngagedEnemies = 0xFFFF,
        DetrimentalEffectsInflictedByEngagedEnemies = 0xFFFF,
        DamageEngagedEnemiesAreDealt = 0xFFFF,
        FailedAttacksOnEngagedEnemies = 0xFFFF,
        ActionsUsedOnEngagedEnemies = 0xFFFF,
        ItemsUsedOnEngagedEnemies = 0xFFFF,
        EffectsOfHealingSpellsCastOnEngagedEnemies = 0xFFFF,
        BeneficialEffectsGrantedToEngagedEnemies = 0xFFFF,
        DetrimentalEffectsInflictedOnEngagedEnemies = 0xFFFF,
        BeneficialEffectsOnEngagedEnemiesEnding = 0xFFFF,
        DetrimentalEffectsOnEngagedEnemiesEnding = 0xFFFF,

        DamageDealtByUnengagedEnemy = 0xFFFF,
        FailedAttacksByUnengagedEnemy = 0xFFFF,
        ActionsInitiatedByUnengagedEnemy = 0xFFFF,
        ItemsUsedByUnengagedEnemy = 0xFFFF,
        EffectsOfHealingSpellsCastByUnengagedEnemy = 0xFFFF,
        BeneficialEffectsGrantedByUnengagedEnemy = 0xFFFF,
        DetrimentalEffectsInflictedByUnengagedEnemy = 0xFFFF,
        DamageUnengagedEnemyAreDealt = 0xFFFF,
        FailedAttacksOnUnengagedEnemy = 0xFFFF,
        ActionsUsedOnUnengagedEnemy = 0xFFFF,
        ItemsUsedOnUnengagedEnemy = 0xFFFF,
        EffectsOfHealingSpellsCastOnUnengagedEnemy = 0xFFFF,
        BeneficialEffectsGrantedToUnengagedEnemy = 0xFFFF,
        DetrimentalEffectsInflictedOnUnengagedEnemy = 0xFFFF,
        BeneficialEffectsOnUnengagedEnemyEnding = 0xFFFF,
        DetrimentalEffectsOnUnengagedEnemyEnding = 0xFFFF,

        DamageDealtByFriendlyNPCs = 0xFFFF,
        FailedAttacksByFriendlyNPCs = 0xFFFF,
        ActionsInitiatedByFriendlyNPCs = 0xFFFF,
        ItemsUsedByFriendlyNPCs = 0xFFFF,
        EffectsOfHealingSpellsCastByFriendlyNPCs = 0xFFFF,
        BeneficialEffectsGrantedByFriendlyNPCs = 0xFFFF,
        DetrimentalEffectsInflictedByFriendlyNPCs = 0xFFFF,
        DamageFriendlyNPCsAreDealt = 0xFFFF,
        FailedAttacksOnFriendlyNPCs = 0xFFFF,
        ActionsUsedOnFriendlyNPCs = 0xFFFF,
        ItemsUsedOnFriendlyNPCs = 0xFFFF,
        EffectsOfHealingSpellsCastOnFriendlyNPCs = 0xFFFF,
        BeneficialEffectsGrantedToFriendlyNPCs = 0xFFFF,
        DetrimentalEffectsInflictedOnFriendlyNPCs = 0xFFFF,
        BeneficialEffectsOnFriendlyNPCsEnding = 0xFFFF,
        DetrimentalEffectsOnFriendlyNPCsEnding = 0xFFFF,

        DamageDealtByPetsAndCompanions = 0xFFFF,
        FailedAttacksByPetsAndCompanions = 0xFFFF,
        ActionsInitiatedByPetsAndCompanions = 0xFFFF,
        ItemsUsedByPetsAndCompanions = 0xFFFF,
        EffectsOfHealingSpellsCastByPetsAndCompanions = 0xFFFF,
        BeneficialEffectsGrantedByPetsAndCompanions = 0xFFFF,
        DetrimentalEffectsInflictedByPetsAndCompanions = 0xFFFF,
        DamagePetsAndCompanionsAreDealt = 0xFFFF,
        FailedAttacksOnPetsAndCompanions = 0xFFFF,
        ActionsUsedOnPetsAndCompanions = 0xFFFF,
        ItemsUsedOnPetsAndCompanions = 0xFFFF,
        EffectsOfHealingSpellsCastOnPetsAndCompanions = 0xFFFF,
        BeneficialEffectsGrantedToPetsAndCompanions = 0xFFFF,
        DetrimentalEffectsInflictedOnPetsAndCompanions = 0xFFFF,
        BeneficialEffectsOnPetsAndCompanionsEnding = 0xFFFF,
        DetrimentalEffectsOnPetsAndCompanionsEnding = 0xFFFF,

        DamageDealtByPartyMembersPetsAndCompanions = 0xFFFF,
        FailedAttacksByPartyMembersPetsAndCompanions = 0xFFFF,
        ActionsInitiatedByPartyMembersPetsAndCompanions = 0xFFFF,
        ItemsUsedByPartyMembersPetsAndCompanions = 0xFFFF,
        EffectsOfHealingSpellsCastByPartyMembersPetsAndCompanions = 0xFFFF,
        BeneficialEffectsGrantedByPartyMembersPetsAndCompanions = 0xFFFF,
        DetrimentalEffectsInflictedByPartyMembersPetsAndCompanions = 0xFFFF,
        DamagePartyMembersPetsAndCompanionsAreDealt = 0xFFFF,
        FailedAttacksOnPartyMembersPetsAndCompanions = 0xFFFF,
        ActionsUsedOnPartyMembersPetsAndCompanions = 0xFFFF,
        ItemsUsedOnPartyMembersPetsAndCompanions = 0xFFFF,
        EffectsOfHealingSpellsCastOnPartyMembersPetsAndCompanions = 0xFFFF,
        BeneficialEffectsGrantedToPartyMembersPetsAndCompanions = 0xFFFF,
        DetrimentalEffectsInflictedOnPartyMembersPetsAndCompanions = 0xFFFF,
        BeneficialEffectsOnPartyMembersPetsAndCompanionsEnding = 0xFFFF,
        DetrimentalEffectsOnPartyMembersPetsAndCompanionsEnding = 0xFFFF,

        DamageDealtByAllianceMembersPetsAndCompanions = 0xFFFF,
        FailedAttacksByAllianceMembersPetsAndCompanions = 0xFFFF,
        ActionsInitiatedByAllianceMembersPetsAndCompanions = 0xFFFF,
        ItemsUsedByAllianceMembersPetsAndCompanions = 0xFFFF,
        EffectsOfHealingSpellsCastByAllianceMembersPetsAndCompanions = 0xFFFF,
        BeneficialEffectsGrantedByAllianceMembersPetsAndCompanions = 0xFFFF,
        DetrimentalEffectsInflictedByAllianceMembersPetsAndCompanions = 0xFFFF,
        DamageAllianceMembersPetsAndCompanionsAreDealt = 0xFFFF,
        FailedAttacksOnAllianceMembersPetsAndCompanions = 0xFFFF,
        ActionsUsedOnAllianceMembersPetsAndCompanions = 0xFFFF,
        ItemsUsedOnAllianceMembersPetsAndCompanions = 0xFFFF,
        EffectsOfHealingSpellsCastOnAllianceMembersPetsAndCompanions = 0xFFFF,
        BeneficialEffectsGrantedToAllianceMembersPetsAndCompanions = 0xFFFF,
        DetrimentalEffectsInflictedOnAllianceMembersPetsAndCompanions = 0xFFFF,
        BeneficialEffectsOnAllianceMembersPetsAndCompanionsEnding = 0xFFFF,
        DetrimentalEffectsOnAllianceMembersPetsAndCompanionsEnding = 0xFFFF,

        DamageDealtByOthersPetsAndCompanions = 0xFFFF,
        FailedAttacksByOthersPetsAndCompanions = 0xFFFF,
        ActionsInitiatedByOthersPetsAndCompanions = 0xFFFF,
        ItemsUsedByOthersPetsAndCompanions = 0xFFFF,
        EffectsOfHealingSpellsCastByOthersPetsAndCompanions = 0xFFFF,
        BeneficialEffectsGrantedByOthersPetsAndCompanions = 0xFFFF,
        DetrimentalEffectsInflictedByOthersPetsAndCompanions = 0xFFFF,
        DamageOthersPetsAndCompanionsAreDealt = 0xFFFF,
        FailedAttacksOnOthersPetsAndCompanions = 0xFFFF,
        ActionsUsedOnOthersPetsAndCompanions = 0xFFFF,
        ItemsUsedOnOthersPetsAndCompanions = 0xFFFF,
        EffectsOfHealingSpellsCastOnOthersPetsAndCompanions = 0xFFFF,
        BeneficialEffectsGrantedToOthersPetsAndCompanions = 0xFFFF,
        DetrimentalEffectsInflictedOnOthersPetsAndCompanions = 0xFFFF,
        BeneficialEffectsOnOthersPetsAndCompanionsEnding = 0xFFFF,
        DetrimentalEffectsOnOthersPetsAndCompanionsEnding = 0xFFFF,

        SystemMessages = 0xFFFF,
        OwnBattleSystemMessages = 0xFFFF,
        OthersBattleSystemMessages = 0xFFFF,
        GatheringSystemMessages = 0xFFFF,
        ErrorMessages = 0xFFFF,
        Echo = 0xFFFF,
        NoviceNetworkNotifications = 0xFFFF,
        FreeCompanyAnnouncements = 0xFFFF,
        PvPTeamAnnouncements = 0xFFFF,
        FreeCompanyMemberLoginNotifications = 0xFFFF,
        PvPTeamMemberLoginNotifications = 0xFFFF,
        RetainerSaleNotifications = 0xFFFF,
        NPCDialogue = 0x3D,
        NPCDialogueAnnouncements = 0xFFFF,
        LootNotices = 0xFFFF,
        OwnProgressionMessages = 0xFFFF,
        PartyMembersProgressionMessages = 0xFFFF,
        OthersProgressionMessages = 0xFFFF,
        OwnLootMessages = 0xFFFF,
        OthersLootMessages = 0xFFFF,
        OwnSynthesisMessages = 0xFFFF,
        OthersSynthesisMessages = 0xFFFF,
        OwnGatheringMessages = 0xFFFF,
        OthersFishingMessages = 0xFFFF,
        PeriodicRecruitmentNotifications = 0xFFFF,
        SignMessagesForPCTargets = 0xFFFF,
        RandomNumberMessages = 0xFFFF,
        CurrentOrchestrionTrackMessages = 0xFFFF,
        MessageBookAlert = 0xFFFF,
        AlarmNotifications = 0xFFFF,

        /// <summary>
        /// 
        /// </summary>
        Event = 0x3D,

        PartiesRecruiting = 0x48,
        UseAction = 0x82B,
        JobChange = 0x839,
        ObtainItemOrGil = 0x83E,
        GainStatus = 0x8AE,
        ReadyAction = 0x8AB,
        LostStatus = 0x8B0,
        PayRetainers = 0xC39,
        RetainerGainedExp = 0x4040,

        Heal = 2221, //0x8AD,
        DamageTaken = 2857, //0xB29,
        EnemyTakesDamage = 2729, //0xAA9,
        YouTakeDamageFromEngagedEnemy = 10409,
        EngagedEnemyUsesAction = 10283,
        DefeatedEngagedEnemy = 2874,

        GainedExp = 2112,
        GainedItems = 2115, //Also used for 'You finished gathering'


        LoggedOut = 0x2246
    }

    public static class XivChatTypes
    {
        public static (string, ExChatType[])[] TypeGroups = new[]
        {
            ("Chat",
                new[]
                {
                    ExChatType.Say,
                    ExChatType.Yell,
                    ExChatType.Shout,
                    ExChatType.TellIncoming,
                    ExChatType.TellOutgoing,
                    ExChatType.Party,
                    ExChatType.Alliance,
                    ExChatType.FreeCompany,
                    ExChatType.NoviceNetwork,
                    ExChatType.StandardEmotes,
                    ExChatType.CustomEmotes,
                }),

            ("Linkshells",
                new[]
                {
                    ExChatType.CrossWorldLinkshell1,
                    ExChatType.CrossWorldLinkshell2,
                    ExChatType.CrossWorldLinkshell3,
                    ExChatType.CrossWorldLinkshell4,
                    ExChatType.CrossWorldLinkshell5,
                    ExChatType.CrossWorldLinkshell6,
                    ExChatType.CrossWorldLinkshell7,
                    ExChatType.CrossWorldLinkshell8,
                    ExChatType.Linkshell1,
                    ExChatType.Linkshell2,
                    ExChatType.Linkshell3,
                    ExChatType.Linkshell4,
                    ExChatType.Linkshell5,
                    ExChatType.Linkshell6,
                    ExChatType.Linkshell7,
                    ExChatType.Linkshell8,
                }),

            ("Battle (Subject: You)", new[]
            {
                ExChatType.DamageDealtByYou,
                ExChatType.FailedAttacksByYou,
                ExChatType.ActionsInitiatedByYou,
                ExChatType.ItemsUsedByYou,
                ExChatType.ItemUsedByYouReady,
                ExChatType.EffectsOfHealingSpellsCastByYou,
                ExChatType.BeneficialEffectsGrantedByYou,
                ExChatType.DetrimentalEffectsInflictedByYou,
                ExChatType.DamageYouAreDealt,
                ExChatType.FailedAttacksOnYou,
                ExChatType.ActionsUsedOnYou,
                ExChatType.ItemsUsedOnYou,
                ExChatType.EffectsOfHealingSpellsCastOnYou,
                ExChatType.BeneficialEffectsGrantedToYou,
                ExChatType.DetrimentalEffectsInflictedOnYou,
                ExChatType.BeneficialEffectsOnYouEnding,
                ExChatType.DetrimentalEffectsOnYouEnding,
            }),
            
            ("Battle (Subject: Party Member)", new[]
            {
                ExChatType.DamageDealtByPartyMembers,
                ExChatType.FailedAttacksByPartyMembers,
                ExChatType.ActionsInitiatedByPartyMembers,
                ExChatType.ItemsUsedByPartyMembers,
                ExChatType.EffectsOfHealingSpellsCastByPartyMembers,
                ExChatType.BeneficialEffectsGrantedByPartyMembers,
                ExChatType.DetrimentalEffectsInflictedByPartyMembers,
                ExChatType.DamagePartyMembersAreDealt,
                ExChatType.FailedAttacksOnPartyMembers,
                ExChatType.ActionsUsedOnPartyMembers,
                ExChatType.ItemsUsedOnPartyMembers,
                ExChatType.EffectsOfHealingSpellsCastOnPartyMembers,
                ExChatType.BeneficialEffectsGrantedToPartyMembers,
                ExChatType.DetrimentalEffectsInflictedOnPartyMembers,
                ExChatType.BeneficialEffectsOnPartyMembersEnding,
                ExChatType.DetrimentalEffectsOnPartyMembersEnding,
            }),
            
            ("Battle (Subject: Alliance Member)", new[]
            {
                ExChatType.DamageDealtByAllianceMembers,
                ExChatType.FailedAttacksByAllianceMembers,
                ExChatType.ActionsInitiatedByAllianceMembers,
                ExChatType.ItemsUsedByAllianceMembers,
                ExChatType.EffectsOfHealingSpellsCastByAllianceMembers,
                ExChatType.BeneficialEffectsGrantedByAllianceMembers,
                ExChatType.DetrimentalEffectsInflictedByAllianceMembers,
                ExChatType.DamageAllianceMembersAreDealt,
                ExChatType.FailedAttacksOnAllianceMembers,
                ExChatType.ActionsUsedOnAllianceMembers,
                ExChatType.ItemsUsedOnAllianceMembers,
                ExChatType.EffectsOfHealingSpellsCastOnAllianceMembers,
                ExChatType.BeneficialEffectsGrantedToAllianceMembers,
                ExChatType.DetrimentalEffectsInflictedOnAllianceMembers,
                ExChatType.BeneficialEffectsOnAllianceMembersEnding,
                ExChatType.DetrimentalEffectsOnAllianceMembersEnding,
            }),
            
            ("Battle (Subject: Other PC)", new[]
            {
                ExChatType.DamageDealtByOthers,
                ExChatType.FailedAttacksByOthers,
                ExChatType.ActionsInitiatedByOthers,
                ExChatType.ItemsUsedByOthers,
                ExChatType.EffectsOfHealingSpellsCastByOthers,
                ExChatType.BeneficialEffectsGrantedByOthers,
                ExChatType.DetrimentalEffectsInflictedByOthers,
                ExChatType.DamageOthersAreDealt,
                ExChatType.FailedAttacksOnOthers,
                ExChatType.ActionsUsedOnOthers,
                ExChatType.ItemsUsedOnOthers,
                ExChatType.EffectsOfHealingSpellsCastOnOthers,
                ExChatType.BeneficialEffectsGrantedToOthers,
                ExChatType.DetrimentalEffectsInflictedOnOthers,
                ExChatType.BeneficialEffectsOnOthersEnding,
                ExChatType.DetrimentalEffectsOnOthersEnding,
            }),
            
            ("Battle (Subject: Engaged Enemy)", new[]
            {
                ExChatType.DamageDealtByEngagedEnemies,
                ExChatType.FailedAttacksByEngagedEnemies,
                ExChatType.ActionsInitiatedByEngagedEnemies,
                ExChatType.ItemsUsedByEngagedEnemies,
                ExChatType.EffectsOfHealingSpellsCastByEngagedEnemies,
                ExChatType.BeneficialEffectsGrantedByEngagedEnemies,
                ExChatType.DetrimentalEffectsInflictedByEngagedEnemies,
                ExChatType.DamageEngagedEnemiesAreDealt,
                ExChatType.FailedAttacksOnEngagedEnemies,
                ExChatType.ActionsUsedOnEngagedEnemies,
                ExChatType.ItemsUsedOnEngagedEnemies,
                ExChatType.EffectsOfHealingSpellsCastOnEngagedEnemies,
                ExChatType.BeneficialEffectsGrantedToEngagedEnemies,
                ExChatType.DetrimentalEffectsInflictedOnEngagedEnemies,
                ExChatType.BeneficialEffectsOnEngagedEnemiesEnding,
                ExChatType.DetrimentalEffectsOnEngagedEnemiesEnding,
            }),
            
            ("Battle (Subject: Unengaged Enemies)", new[]
            {
                ExChatType.DamageDealtByUnengagedEnemy,
                ExChatType.FailedAttacksByUnengagedEnemy,
                ExChatType.ActionsInitiatedByUnengagedEnemy,
                ExChatType.ItemsUsedByUnengagedEnemy,
                ExChatType.EffectsOfHealingSpellsCastByUnengagedEnemy,
                ExChatType.BeneficialEffectsGrantedByUnengagedEnemy,
                ExChatType.DetrimentalEffectsInflictedByUnengagedEnemy,
                ExChatType.DamageUnengagedEnemyAreDealt,
                ExChatType.FailedAttacksOnUnengagedEnemy,
                ExChatType.ActionsUsedOnUnengagedEnemy,
                ExChatType.ItemsUsedOnUnengagedEnemy,
                ExChatType.EffectsOfHealingSpellsCastOnUnengagedEnemy,
                ExChatType.BeneficialEffectsGrantedToUnengagedEnemy,
                ExChatType.DetrimentalEffectsInflictedOnUnengagedEnemy,
                ExChatType.BeneficialEffectsOnUnengagedEnemyEnding,
                ExChatType.DetrimentalEffectsOnUnengagedEnemyEnding,
            }),
            
            ("Battle (Subject: Friendly NPCs)", new[]
            {
                ExChatType.DamageDealtByFriendlyNPCs,
                ExChatType.FailedAttacksByFriendlyNPCs,
                ExChatType.ActionsInitiatedByFriendlyNPCs,
                ExChatType.ItemsUsedByFriendlyNPCs,
                ExChatType.EffectsOfHealingSpellsCastByFriendlyNPCs,
                ExChatType.BeneficialEffectsGrantedByFriendlyNPCs,
                ExChatType.DetrimentalEffectsInflictedByFriendlyNPCs,
                ExChatType.DamageFriendlyNPCsAreDealt,
                ExChatType.FailedAttacksOnFriendlyNPCs,
                ExChatType.ActionsUsedOnFriendlyNPCs,
                ExChatType.ItemsUsedOnFriendlyNPCs,
                ExChatType.EffectsOfHealingSpellsCastOnFriendlyNPCs,
                ExChatType.BeneficialEffectsGrantedToFriendlyNPCs,
                ExChatType.DetrimentalEffectsInflictedOnFriendlyNPCs,
                ExChatType.BeneficialEffectsOnFriendlyNPCsEnding,
                ExChatType.DetrimentalEffectsOnFriendlyNPCsEnding,
            }),
            
            ("Battle (Subject: Pets/Companions)", new[]
            {
                ExChatType.DamageDealtByPetsAndCompanions,
                ExChatType.FailedAttacksByPetsAndCompanions,
                ExChatType.ActionsInitiatedByPetsAndCompanions,
                ExChatType.ItemsUsedByPetsAndCompanions,
                ExChatType.EffectsOfHealingSpellsCastByPetsAndCompanions,
                ExChatType.BeneficialEffectsGrantedByPetsAndCompanions,
                ExChatType.DetrimentalEffectsInflictedByPetsAndCompanions,
                ExChatType.DamagePetsAndCompanionsAreDealt,
                ExChatType.FailedAttacksOnPetsAndCompanions,
                ExChatType.ActionsUsedOnPetsAndCompanions,
                ExChatType.ItemsUsedOnPetsAndCompanions,
                ExChatType.EffectsOfHealingSpellsCastOnPetsAndCompanions,
                ExChatType.BeneficialEffectsGrantedToPetsAndCompanions,
                ExChatType.DetrimentalEffectsInflictedOnPetsAndCompanions,
                ExChatType.BeneficialEffectsOnPetsAndCompanionsEnding,
                ExChatType.DetrimentalEffectsOnPetsAndCompanionsEnding,
            }),
            
            ("Battle (Subject: Pets/Companions (Party))", new[]
            {
                ExChatType.DamageDealtByPartyMembersPetsAndCompanions,
                ExChatType.FailedAttacksByPartyMembersPetsAndCompanions,
                ExChatType.ActionsInitiatedByPartyMembersPetsAndCompanions,
                ExChatType.ItemsUsedByPartyMembersPetsAndCompanions,
                ExChatType.EffectsOfHealingSpellsCastByPartyMembersPetsAndCompanions,
                ExChatType.BeneficialEffectsGrantedByPartyMembersPetsAndCompanions,
                ExChatType.DetrimentalEffectsInflictedByPartyMembersPetsAndCompanions,
                ExChatType.DamagePartyMembersPetsAndCompanionsAreDealt,
                ExChatType.FailedAttacksOnPartyMembersPetsAndCompanions,
                ExChatType.ActionsUsedOnPartyMembersPetsAndCompanions,
                ExChatType.ItemsUsedOnPartyMembersPetsAndCompanions,
                ExChatType.EffectsOfHealingSpellsCastOnPartyMembersPetsAndCompanions,
                ExChatType.BeneficialEffectsGrantedToPartyMembersPetsAndCompanions,
                ExChatType.DetrimentalEffectsInflictedOnPartyMembersPetsAndCompanions,
                ExChatType.BeneficialEffectsOnPartyMembersPetsAndCompanionsEnding,
                ExChatType.DetrimentalEffectsOnPartyMembersPetsAndCompanionsEnding,
            }),
            
            ("Battle (Subject: Pets/Companions (Alliance))", new[]
            {
                ExChatType.DamageDealtByAllianceMembersPetsAndCompanions,
                ExChatType.FailedAttacksByAllianceMembersPetsAndCompanions,
                ExChatType.ActionsInitiatedByAllianceMembersPetsAndCompanions,
                ExChatType.ItemsUsedByAllianceMembersPetsAndCompanions,
                ExChatType.EffectsOfHealingSpellsCastByAllianceMembersPetsAndCompanions,
                ExChatType.BeneficialEffectsGrantedByAllianceMembersPetsAndCompanions,
                ExChatType.DetrimentalEffectsInflictedByAllianceMembersPetsAndCompanions,
                ExChatType.DamageAllianceMembersPetsAndCompanionsAreDealt,
                ExChatType.FailedAttacksOnAllianceMembersPetsAndCompanions,
                ExChatType.ActionsUsedOnAllianceMembersPetsAndCompanions,
                ExChatType.ItemsUsedOnAllianceMembersPetsAndCompanions,
                ExChatType.EffectsOfHealingSpellsCastOnAllianceMembersPetsAndCompanions,
                ExChatType.BeneficialEffectsGrantedToAllianceMembersPetsAndCompanions,
                ExChatType.DetrimentalEffectsInflictedOnAllianceMembersPetsAndCompanions,
                ExChatType.BeneficialEffectsOnAllianceMembersPetsAndCompanionsEnding,
                ExChatType.DetrimentalEffectsOnAllianceMembersPetsAndCompanionsEnding,
            }),
            
            ("Battle (Subject: Pets/Companions (Other PC))", new[]
            {
                ExChatType.DamageDealtByOthersPetsAndCompanions,
                ExChatType.FailedAttacksByOthersPetsAndCompanions,
                ExChatType.ActionsInitiatedByOthersPetsAndCompanions,
                ExChatType.ItemsUsedByOthersPetsAndCompanions,
                ExChatType.EffectsOfHealingSpellsCastByOthersPetsAndCompanions,
                ExChatType.BeneficialEffectsGrantedByOthersPetsAndCompanions,
                ExChatType.DetrimentalEffectsInflictedByOthersPetsAndCompanions,
                ExChatType.DamageOthersPetsAndCompanionsAreDealt,
                ExChatType.FailedAttacksOnOthersPetsAndCompanions,
                ExChatType.ActionsUsedOnOthersPetsAndCompanions,
                ExChatType.ItemsUsedOnOthersPetsAndCompanions,
                ExChatType.EffectsOfHealingSpellsCastOnOthersPetsAndCompanions,
                ExChatType.BeneficialEffectsGrantedToOthersPetsAndCompanions,
                ExChatType.DetrimentalEffectsInflictedOnOthersPetsAndCompanions,
                ExChatType.BeneficialEffectsOnOthersPetsAndCompanionsEnding,
                ExChatType.DetrimentalEffectsOnOthersPetsAndCompanionsEnding,
            }),
            
            ("Notifications", new[]
            {
                ExChatType.NoviceNetworkNotifications,
                ExChatType.FreeCompanyMemberLoginNotifications,
                ExChatType.PvPTeamMemberLoginNotifications,
                ExChatType.PeriodicRecruitmentNotifications,
                ExChatType.AlarmNotifications,
            }),
            
            ("System Messages", new[]
            {
                ExChatType.SystemMessages,
                ExChatType.OwnBattleSystemMessages,
                ExChatType.OthersBattleSystemMessages,
                ExChatType.GatheringSystemMessages,
                ExChatType.ErrorMessages,
                ExChatType.OwnProgressionMessages,
                ExChatType.PartyMembersProgressionMessages,
                ExChatType.OthersProgressionMessages,
                ExChatType.OwnLootMessages,
                ExChatType.OthersLootMessages,
                ExChatType.OwnSynthesisMessages,
                ExChatType.OthersSynthesisMessages,
                ExChatType.OwnGatheringMessages,
                ExChatType.OthersFishingMessages,
                ExChatType.SignMessagesForPCTargets,
                ExChatType.RandomNumberMessages,
                ExChatType.CurrentOrchestrionTrackMessages,
            }),
            
            ("Announcements", new[]
            {
                ExChatType.Echo,
                ExChatType.FreeCompanyAnnouncements,
                ExChatType.PvPTeamAnnouncements,
                ExChatType.NPCDialogue,
                ExChatType.NPCDialogueAnnouncements,
                ExChatType.LootNotices,
                ExChatType.MessageBookAlert,
            }),
        };

        public static ExChatTypeAttribute? ExChatAttribute(this ExChatType value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            return name == null
                ? null
                : type.GetField(name)
                    ?.GetCustomAttributes(false)
                    ?.OfType<ExChatTypeAttribute>()
                    ?.SingleOrDefault<ExChatTypeAttribute>();
        }
    }
}