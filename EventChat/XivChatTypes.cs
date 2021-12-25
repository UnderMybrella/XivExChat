using Dalamud.Game.Text;

namespace EventChat
{
    public static class XivChatTypes
    {
        public const XivChatType Say = XivChatType.Say;
        public const XivChatType Yell = XivChatType.Yell;
        public const XivChatType Shout = XivChatType.Shout;
        public const XivChatType TellOutgoing = XivChatType.TellOutgoing;
        public const XivChatType TellIncoming = XivChatType.TellIncoming;
        public const XivChatType Party = XivChatType.Party;
        public const XivChatType Alliance = XivChatType.Alliance;
        public const XivChatType FreeCompany = XivChatType.FreeCompany;
        public const XivChatType PvPTeam = XivChatType.PvPTeam;
        public const XivChatType CrossWorldLinkshell1 = XivChatType.CrossLinkShell1;
        public const XivChatType CrossWorldLinkshell2 = XivChatType.CrossLinkShell2;
        public const XivChatType CrossWorldLinkshell3 = XivChatType.CrossLinkShell3;
        public const XivChatType CrossWorldLinkshell4 = XivChatType.CrossLinkShell4;
        public const XivChatType CrossWorldLinkshell5 = XivChatType.CrossLinkShell5;
        public const XivChatType CrossWorldLinkshell6 = XivChatType.CrossLinkShell6;
        public const XivChatType CrossWorldLinkshell7 = XivChatType.CrossLinkShell7;
        public const XivChatType CrossWorldLinkshell8 = XivChatType.CrossLinkShell8;
        public const XivChatType Linkshell1 = XivChatType.Ls1;
        public const XivChatType Linkshell2 = XivChatType.Ls2;
        public const XivChatType Linkshell3 = XivChatType.Ls3;
        public const XivChatType Linkshell4 = XivChatType.Ls4;
        public const XivChatType Linkshell5 = XivChatType.Ls5;
        public const XivChatType Linkshell6 = XivChatType.Ls6;
        public const XivChatType Linkshell7 = XivChatType.Ls7;
        public const XivChatType Linkshell8 = XivChatType.Ls8;
        public const XivChatType NoviceNetwork = XivChatType.NoviceNetwork;
        public const XivChatType StandardEmotes = XivChatType.StandardEmote;
        public const XivChatType CustomEmotes = XivChatType.CustomEmote;

        public const XivChatType DamageDealtByYou = (XivChatType) 2729;
        public const XivChatType FailedAttacksByYou = (XivChatType) 2730;
        public const XivChatType ActionsInitiatedByYou = (XivChatType) 2091; //2291 You begin casting Diagnosis, 2091: You cast Diagnosis
        public const XivChatType ItemsUsedByYou = (XivChatType) 2092; //2348: You ready a tuft of phoenix down, 2092: you use a tuft of phoenix down
        public const XivChatType ItemUsedByYouReady = (XivChatType) 2348;
        public const XivChatType EffectsOfHealingSpellsCastByYou = (XivChatType) 2221;
        public const XivChatType BeneficialEffectsGrantedByYou = (XivChatType) 2222;
        public const XivChatType DetrimentalEffectsInflictedByYou = (XivChatType) 2863;
        public const XivChatType DamageYouAreDealt = (XivChatType) 2857;
        public const XivChatType FailedAttacksOnYou = (XivChatType) 10410;
        public const XivChatType ActionsUsedOnYou = (XivChatType) 2219;
        public const XivChatType ItemsUsedOnYou = (XivChatType) 2220; //2220, 2092
        public const XivChatType EffectsOfHealingSpellsCastOnYou = (XivChatType) 16557; // Also seems to encompass 2221
        public const XivChatType BeneficialEffectsGrantedToYou = (XivChatType) 2222;
        public const XivChatType DetrimentalEffectsInflictedOnYou = (XivChatType) 2223;
        public const XivChatType BeneficialEffectsOnYouEnding = (XivChatType) 2224;
        public const XivChatType DetrimentalEffectsOnYouEnding = (XivChatType) 2225;

        public const XivChatType DamageDealtByPartyMembers = (XivChatType) 4905;
        public const XivChatType FailedAttacksByPartyMembers = (XivChatType) 4906;
        public const XivChatType ActionsInitiatedByPartyMembers = (XivChatType) 4139;
        public const XivChatType ItemsUsedByPartyMembers = (XivChatType) 4140;
        public const XivChatType EffectsOfHealingSpellsCastByPartyMembers = (XivChatType) 4397;
        public const XivChatType BeneficialEffectsGrantedByPartyMembers = (XivChatType) 4270;
        public const XivChatType DetrimentalEffectsInflictedByPartyMembers = (XivChatType) 4911;
        public const XivChatType DamagePartyMembersAreDealt = (XivChatType) 12585;
        public const XivChatType FailedAttacksOnPartyMembers = (XivChatType) 12586;
        public const XivChatType ActionsUsedOnPartyMembers = (XivChatType) 2347; //You begin casting Esuna
        public const XivChatType ItemsUsedOnPartyMembers = (XivChatType) 0xFFFF; // TODO: Figure this out; most items fall under ItemsUsedByPartyMembers?
        public const XivChatType EffectsOfHealingSpellsCastOnPartyMembers = (XivChatType) 2349;
        public const XivChatType BeneficialEffectsGrantedToPartyMembers = (XivChatType) 4398;
        public const XivChatType DetrimentalEffectsInflictedOnPartyMembers = (XivChatType) 4399;
        public const XivChatType BeneficialEffectsOnPartyMembersEnding = (XivChatType) 4400;
        public const XivChatType DetrimentalEffectsOnPartyMembersEnding = (XivChatType) 4401;

        public const XivChatType DamageDealtByAllianceMembers = (XivChatType) 6953;
        public const XivChatType FailedAttacksByAllianceMembers = (XivChatType) 6570;
        public const XivChatType ActionsInitiatedByAllianceMembers = (XivChatType) 6187;
        public const XivChatType ItemsUsedByAllianceMembers = (XivChatType) 6188;
        public const XivChatType EffectsOfHealingSpellsCastByAllianceMembers = (XivChatType) 6537;
        public const XivChatType BeneficialEffectsGrantedByAllianceMembers = (XivChatType) 6574;
        public const XivChatType DetrimentalEffectsInflictedByAllianceMembers = (XivChatType) 6959;
        public const XivChatType DamageAllianceMembersAreDealt = (XivChatType) 12713; //10665: x hits y for z damage.
        public const XivChatType FailedAttacksOnAllianceMembers = (XivChatType) 10666; //10666 and 12714
        public const XivChatType ActionsUsedOnAllianceMembers = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedOnAllianceMembers = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastOnAllianceMembers = (XivChatType) 0xFFFF; // TODO: What's the difference between this and cast by
        public const XivChatType BeneficialEffectsGrantedToAllianceMembers = (XivChatType) 2478;
        public const XivChatType DetrimentalEffectsInflictedOnAllianceMembers = (XivChatType) 6575; //6575 and 12719
        public const XivChatType BeneficialEffectsOnAllianceMembersEnding = (XivChatType) 6576;
        public const XivChatType DetrimentalEffectsOnAllianceMembersEnding = (XivChatType) 6577;

        public const XivChatType DamageDealtByOthers = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksByOthers = (XivChatType) 0xFFFF;
        public const XivChatType ActionsInitiatedByOthers = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedByOthers = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastByOthers = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedByOthers = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedByOthers = (XivChatType) 0xFFFF;
        public const XivChatType DamageOthersAreDealt = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksOnOthers = (XivChatType) 0xFFFF;
        public const XivChatType ActionsUsedOnOthers = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedOnOthers = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastOnOthers = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedToOthers = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedOnOthers = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsOnOthersEnding = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsOnOthersEnding = (XivChatType) 0xFFFF;

        public const XivChatType DamageDealtByEngagedEnemies = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksByEngagedEnemies = (XivChatType) 0xFFFF;
        public const XivChatType ActionsInitiatedByEngagedEnemies = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedByEngagedEnemies = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastByEngagedEnemies = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedByEngagedEnemies = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedByEngagedEnemies = (XivChatType) 0xFFFF;
        public const XivChatType DamageEngagedEnemiesAreDealt = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksOnEngagedEnemies = (XivChatType) 0xFFFF;
        public const XivChatType ActionsUsedOnEngagedEnemies = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedOnEngagedEnemies = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastOnEngagedEnemies = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedToEngagedEnemies = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedOnEngagedEnemies = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsOnEngagedEnemiesEnding = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsOnEngagedEnemiesEnding = (XivChatType) 0xFFFF;

        public const XivChatType DamageDealtByUnengagedEnemy = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksByUnengagedEnemy = (XivChatType) 0xFFFF;
        public const XivChatType ActionsInitiatedByUnengagedEnemy = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedByUnengagedEnemy = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastByUnengagedEnemy = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedByUnengagedEnemy = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedByUnengagedEnemy = (XivChatType) 0xFFFF;
        public const XivChatType DamageUnengagedEnemyAreDealt = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksOnUnengagedEnemy = (XivChatType) 0xFFFF;
        public const XivChatType ActionsUsedOnUnengagedEnemy = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedOnUnengagedEnemy = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastOnUnengagedEnemy = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedToUnengagedEnemy = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedOnUnengagedEnemy = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsOnUnengagedEnemyEnding = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsOnUnengagedEnemyEnding = (XivChatType) 0xFFFF;

        public const XivChatType DamageDealtByFriendlyNPCs = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksByFriendlyNPCs = (XivChatType) 0xFFFF;
        public const XivChatType ActionsInitiatedByFriendlyNPCs = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedByFriendlyNPCs = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastByFriendlyNPCs = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedByFriendlyNPCs = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedByFriendlyNPCs = (XivChatType) 0xFFFF;
        public const XivChatType DamageFriendlyNPCsAreDealt = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksOnFriendlyNPCs = (XivChatType) 0xFFFF;
        public const XivChatType ActionsUsedOnFriendlyNPCs = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedOnFriendlyNPCs = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastOnFriendlyNPCs = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedToFriendlyNPCs = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedOnFriendlyNPCs = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsOnFriendlyNPCsEnding = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsOnFriendlyNPCsEnding = (XivChatType) 0xFFFF;

        public const XivChatType DamageDealtByPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksByPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ActionsInitiatedByPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedByPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastByPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedByPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedByPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType DamagePetsAndCompanionsAreDealt = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksOnPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ActionsUsedOnPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedOnPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastOnPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedToPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedOnPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsOnPetsAndCompanionsEnding = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsOnPetsAndCompanionsEnding = (XivChatType) 0xFFFF;

        public const XivChatType DamageDealtByPartyMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksByPartyMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ActionsInitiatedByPartyMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedByPartyMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastByPartyMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedByPartyMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedByPartyMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType DamagePartyMembersPetsAndCompanionsAreDealt = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksOnPartyMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ActionsUsedOnPartyMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedOnPartyMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastOnPartyMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedToPartyMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedOnPartyMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsOnPartyMembersPetsAndCompanionsEnding = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsOnPartyMembersPetsAndCompanionsEnding = (XivChatType) 0xFFFF;

        public const XivChatType DamageDealtByAllianceMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksByAllianceMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ActionsInitiatedByAllianceMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedByAllianceMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastByAllianceMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedByAllianceMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedByAllianceMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType DamageAllianceMembersPetsAndCompanionsAreDealt = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksOnAllianceMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ActionsUsedOnAllianceMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedOnAllianceMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastOnAllianceMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedToAllianceMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedOnAllianceMembersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsOnAllianceMembersPetsAndCompanionsEnding = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsOnAllianceMembersPetsAndCompanionsEnding = (XivChatType) 0xFFFF;

        public const XivChatType DamageDealtByOthersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksByOthersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ActionsInitiatedByOthersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedByOthersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastByOthersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedByOthersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedByOthersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType DamageOthersPetsAndCompanionsAreDealt = (XivChatType) 0xFFFF;
        public const XivChatType FailedAttacksOnOthersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ActionsUsedOnOthersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType ItemsUsedOnOthersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType EffectsOfHealingSpellsCastOnOthersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsGrantedToOthersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsInflictedOnOthersPetsAndCompanions = (XivChatType) 0xFFFF;
        public const XivChatType BeneficialEffectsOnOthersPetsAndCompanionsEnding = (XivChatType) 0xFFFF;
        public const XivChatType DetrimentalEffectsOnOthersPetsAndCompanionsEnding = (XivChatType) 0xFFFF;

        public const XivChatType SystemMessages = (XivChatType) 0xFFFF;
        public const XivChatType OwnBattleSystemMessages = (XivChatType) 0xFFFF;
        public const XivChatType OthersBattleSystemMessages = (XivChatType) 0xFFFF;
        public const XivChatType GatheringSystemMessages = (XivChatType) 0xFFFF;
        public const XivChatType ErrorMessages = (XivChatType) 0xFFFF;
        public const XivChatType Echo = (XivChatType) 0xFFFF;
        public const XivChatType NoviceNetworkNotifications = (XivChatType) 0xFFFF;
        public const XivChatType FreeCompanyAnnouncements = (XivChatType) 0xFFFF;
        public const XivChatType PvPTeamAnnouncements = (XivChatType) 0xFFFF;
        public const XivChatType FreeCompanyMemberLoginNotifications = (XivChatType) 0xFFFF;
        public const XivChatType PvPTeamMemberLoginNotifications = (XivChatType) 0xFFFF;
        public const XivChatType RetainerSaleNotifications = (XivChatType) 0xFFFF;
        public const XivChatType NPCDialogue = (XivChatType) 0x3D;
        public const XivChatType NPCDialogueAnnouncements = (XivChatType) 0xFFFF;
        public const XivChatType LootNotices = (XivChatType) 0xFFFF;
        public const XivChatType OwnProgressionMessages = (XivChatType) 0xFFFF;
        public const XivChatType PartyMembersProgressionMessages = (XivChatType) 0xFFFF;
        public const XivChatType OthersProgressionMessages = (XivChatType) 0xFFFF;
        public const XivChatType OwnLootMessages = (XivChatType) 0xFFFF;
        public const XivChatType OthersLootMessages = (XivChatType) 0xFFFF;
        public const XivChatType OwnSynthesisMessages = (XivChatType) 0xFFFF;
        public const XivChatType OthersSynthesisMessages = (XivChatType) 0xFFFF;
        public const XivChatType OwnGatheringMessages = (XivChatType) 0xFFFF;
        public const XivChatType OthersFishingMessages = (XivChatType) 0xFFFF;
        public const XivChatType PeriodicRecruitmentNotifications = (XivChatType) 0xFFFF;
        public const XivChatType SignMessagesForPCTargets = (XivChatType) 0xFFFF;
        public const XivChatType RandomNumberMessages = (XivChatType) 0xFFFF;
        public const XivChatType CurrentOrchestrionTrackMessages = (XivChatType) 0xFFFF;
        public const XivChatType MessageBookAlert = (XivChatType) 0xFFFF;
        public const XivChatType AlarmNotifications = (XivChatType) 0xFFFF;

        /// <summary>
        /// 
        /// </summary>
        public const XivChatType Event = (XivChatType) 0x3D;

        public const XivChatType PartiesRecruiting = (XivChatType) 0x48;
        public const XivChatType UseAction = (XivChatType) 0x82B;
        public const XivChatType JobChange = (XivChatType) 0x839;
        public const XivChatType ObtainItemOrGil = (XivChatType) 0x83E;
        public const XivChatType GainStatus = (XivChatType) 0x8AE;
        public const XivChatType ReadyAction = (XivChatType) 0x8AB;
        public const XivChatType LostStatus = (XivChatType) 0x8B0;
        public const XivChatType PayRetainers = (XivChatType) 0xC39;
        public const XivChatType RetainerGainedExp = (XivChatType) 0x4040;

        public const XivChatType Heal = (XivChatType) 2221; //0x8AD;
        public const XivChatType DamageTaken = (XivChatType) 2857; //0xB29;
        public const XivChatType EnemyTakesDamage = (XivChatType) 2729; //0xAA9;
        public const XivChatType YouTakeDamageFromEngagedEnemy = (XivChatType) 10409;
        public const XivChatType EngagedEnemyUsesAction = (XivChatType) 10283;
        public const XivChatType DefeatedEngagedEnemy = (XivChatType) 2874;

        public const XivChatType GainedExp = (XivChatType) 2112;
        public const XivChatType GainedItems = (XivChatType) 2115; //Also used for 'You finished gathering'
        

        public const XivChatType LoggedOut = (XivChatType) 0x2246;
    }
}