using EventChat.Interface;

#nullable enable
namespace EventChat
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Numerics;
    using System.Reflection;
    using Dalamud.Game.Text;
    using Dalamud.Game.Text.SeStringHandling;
    using Dalamud.Game.Text.SeStringHandling.Payloads;
    using Dalamud.Interface;
    using Dalamud.Interface.Components;
    using Dalamud.Interface.Windowing;
    using Dalamud.Logging;
    using Dalamud.Utility;
    using FFXIVClientStructs.FFXIV.Client.UI;
    using FFXIVClientStructs.FFXIV.Client.UI.Misc;
    using FFXIVClientStructs.FFXIV.Component.GUI;
    using ImGuiNET;
    using ImGuiScene;
    using Lumina.Excel;
    using Lumina.Excel.GeneratedSheets;

    public class ChatWindow : Window, IDisposable
    {
        // var uiModulePtr = this._plugin.GameGui.GetUIModule();
        //     if (uiModulePtr != IntPtr.Zero)
        // {
        //     unsafe
        //     {
        //         var uiModule = (UIModule*) uiModulePtr;
        //         var raptureLogModule = uiModule->GetRaptureLogModule();
        //
        //         var chatStart = ((IntPtr) raptureLogModule) + 0x60;
        //         var chatEnd = ((IntPtr) raptureLogModule) + 0x64;
        //     }
        // }

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

        public const XivChatType LoggedOut = (XivChatType) 0x2246;

        private bool ChatVisible
        {
            get => this._plugin.GameGui.IsChatWindowVisible();
            set => this._plugin.GameGui.SetChatWindowVisibility(value);
        }

        private bool _autoScroll = false;

        private EventChatPlugin _plugin;

        private TextureWrap _iconTest;

        private readonly List<XivChatEntryWithPayloads> chatLog = new();
        private readonly List<ChatTab> chatTabs = new();
        private readonly object renderLock = new();

        private Dictionary<ExChatType, Vector4?> messageColours = new();

        public ChatWindow(EventChatPlugin plugin)
            : base("Game Chat")
        {
            this._plugin = plugin;
            this._iconTest = plugin.DalamudPluginInterface.UiBuilder.LoadImage(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
                    $@"Resources\CONTROLLER_DPAD_DOWN.png"));

            var colours = EventChatPlugin.DataManager.GetExcelSheet<UIColor>();
            if (colours != null)
            {
                void AttemptRegister(ExChatType type, uint rowId)
                {
                    var colour = colours!.GetRow(rowId);

                    if (colour != null)
                    {
                        this.messageColours[type] = colour.UiForegroundAsVector4();
                    }
                }

                foreach (var type in Enum.GetValues<ExChatType>())
                {
                    var attr = type.ExChatAttribute();

                    if (attr != null)
                    {
                        var luminaRowId = attr.DefaultLuminaRow;
                        if (luminaRowId != null)
                        {
                            var uiColour = colours!.GetRow(luminaRowId.Value);
                            if (uiColour != null)
                            {
                                this.messageColours[type] = uiColour.UiForegroundAsVector4();
                            }
                        }
                    }
                }

                AttemptRegister(ExChatType.ErrorMessages, 704);
                AttemptRegister(ExChatType.Party, 576);
                AttemptRegister(ExChatType.Alliance, 500);
                AttemptRegister(ExChatType.Say, 700);
                AttemptRegister(ExChatType.Yell, 573);
                AttemptRegister(ExChatType.Shout, 557);
                AttemptRegister(ExChatType.FreeCompany, 69);
                AttemptRegister(ExChatType.Event, 61);

                AttemptRegister(ExChatType.TellIncoming, 537);
                AttemptRegister(ExChatType.TellOutgoing, 537);

                AttemptRegister((ExChatType) XivChatType.Notice, 708);
                AttemptRegister((ExChatType) XivChatType.Urgent, 508);

                AttemptRegister(ExChatType.StandardEmotes, 39);

                AttemptRegister(ExChatType.RetainerGainedExp, 31);
                AttemptRegister(ExChatType.ObtainItemOrGil, 535);
                AttemptRegister(ExChatType.LoggedOut, 39);
                AttemptRegister(ExChatType.UseAction, 24);

                //These don't have good mappings honestly?
                AttemptRegister(ExChatType.GainStatus, 56);
                AttemptRegister(ExChatType.LostStatus, 56);
            }

            this.Size = new Vector2(500, 400);
            this.SizeCondition = ImGuiCond.FirstUseEver;

            this.RespectCloseHotkey = false;

            this._plugin.ChatGui.ChatMessage += this.QueueMessage;

            this.chatTabs.Add(new ChatTab("All Chat", new()));
            this.chatTabs.Add(new ChatTabWithTypeFilter(
                "General",
                ExChatType.Say,
                ExChatType.Yell,
                ExChatType.Shout,
                ExChatType.TellIncoming,
                ExChatType.TellOutgoing,
                ExChatType.Party,
                ExChatType.Alliance,
                ExChatType.FreeCompany,
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
                ExChatType.NoviceNetwork,
                ExChatType.StandardEmotes,
                ExChatType.CustomEmotes));
            
            this.chatTabs.Add(new ChatTabWithTypeFilter(
                "Battle",
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
                
                ExChatType.OwnBattleSystemMessages));
            
            this.chatTabs.Add(new ChatTabWithTypeFilter("Event", ExChatType.NPCDialogue));
        }

        public void Dispose()
        {
            this._plugin.ChatGui.ChatMessage -= this.QueueMessage;
        }

        private void QueueMessage(XivChatType type, uint senderId, ref SeString sender, ref SeString message,
            ref bool isHandled)
        {
            var chatEntry = new XivChatEntry
            {
                Type = type,
                SenderId = senderId,
                Name = sender,
                Message = message,
            };

            var payloads = this.ProcessChatEntry(chatEntry);
            var chat = new XivChatEntryWithPayloads(chatEntry, payloads, (ExChatType) type);
            this.chatLog.Add(chat);

            foreach (var chatTab in this.chatTabs)
            {
                if (chatTab.ShouldAdd(chat))
                {
                    chatTab.Add(chat);
                }
            }
        }

        public void ProcessSeString(SeString line, List<IChatDisplayPayload> payloads)
        {
            line.Payloads.ForEach(raw =>
            {
                switch (raw)
                {
                    case AutoTranslatePayload autoTranslatePayload:
                        payloads.AddTextPayload(autoTranslatePayload.Text);
                        break;

                    case EmphasisItalicPayload italicsPayload:
                        payloads.AddItalicsPayload(italicsPayload.IsEnabled);
                        break;

                    case IconPayload iconPayload:
                        // ImGui.Image(iconPayload.Icon.);
                        payloads.AddIconPayload(this._iconTest,
                            new Vector2(this._iconTest.Width, this._iconTest.Height));
                        payloads.AddTextPayload($" [{Enum.GetName(iconPayload.Icon) ?? iconPayload.Icon.ToString()}] ");

                        break;

                    case ItemPayload itemPayload:
                        payloads.AddHoverPayload(new HoverPayloadItem(itemPayload.Item));
                        break;
                    
                    case MapLinkPayload:
                    case PlayerPayload:
                    case QuestPayload:
                    case StatusPayload:
                        break;

                    case NewLinePayload:
                        payloads.AddNewLinePayload();
                        break;

                    case SeHyphenPayload hyphenPayload:
                        payloads.AddTextPayload(hyphenPayload.Text);
                        break;

                    case TextPayload textPayload:
                        if (textPayload.Text.Contains("\n"))
                        {
                            var lines = textPayload.Text.Split("\n");
                            for (int i = 0; i < lines.Length - 1; i++)
                            {
                                payloads.AddTextPayload(lines[i]);
                                payloads.AddNewLinePayload();
                            }

                            payloads.AddTextPayload(lines.Last());
                        }
                        else
                        {
                            payloads.AddTextPayload(textPayload.Text);
                        }

                        break;

                    case UIForegroundPayload foregroundPayload:
                        if (foregroundPayload.IsEnabled)
                        {
                            payloads.AddUiForegroundPayload(foregroundPayload.UIColor.UiForegroundAsVector4());
                        }
                        else
                        {
                            payloads.AddPopUiForegroundPayload();
                        }

                        break;

                    case UIGlowPayload glowPayload:
                        if (glowPayload.IsEnabled)
                        {
                            payloads.AddUiGlowPayload(glowPayload.UIColor.UiGlowAsVector4());
                        }
                        else
                        {
                            payloads.AddPopUiGlowPayload();
                        }

                        break;

                    case RawPayload:
                        var chunkType = raw.Encode();
                        switch (chunkType[1])
                        {
                            // Link Terminator
                            case 0x27:
                                payloads.AddLinkTerminatorPayload();
                                break;

                            // Seems to pop both glow and foreground?
                            case 0x13:
                                payloads.AddPopUiForegroundPayload();
                                payloads.AddPopUiGlowPayload();

                                break;

                            //Space??
                            case 0x1D:
                                payloads.AddTextPayload(" ");
                                break;

                            default:
                                payloads.AddTextPayload($"{{{raw.ToString()}}}");
                                break;
                        }

                        break;
                    default:
                        payloads.AddTextPayload($"[{raw.ToString()}]");
                        break;
                }
            });
        }

        public IChatDisplayPayload[] ProcessChatEntry(XivChatEntry chat)
        {
            var payloads = new List<IChatDisplayPayload>();
            var colour = this.messageColours.GetValueOrDefault((ExChatType) chat.Type, null) ??
                         chat.Type.DefaultColourAsVector4();
            payloads.AddTypeBeginningPayload((ExChatType) chat.Type, colour);

            switch (chat.Type)
            {
                case XivChatType.Say:
                    this.ProcessSeString(chat.Name, payloads);
                    payloads.AddTextPayload(": ");
                    this.ProcessSeString(chat.Message, payloads);

                    break;

                case XivChatType.Shout:
                    this.ProcessSeString(chat.Name, payloads);
                    payloads.AddTextPayload(": ");
                    this.ProcessSeString(chat.Message, payloads);
                    break;

                case XivChatType.Yell:
                    this.ProcessSeString(chat.Name, payloads);
                    this.ProcessSeString(": ", payloads);
                    this.ProcessSeString(chat.Message, payloads);

                    break;

                case XivChatType.ErrorMessage:
                    this.ProcessSeString(chat.Message, payloads);

                    break;

                case XivChatType.Party:
                    payloads.AddTextPayload("(");
                    this.ProcessSeString(chat.Name, payloads);
                    payloads.AddTextPayload(") ");

                    this.ProcessSeString(chat.Message, payloads);

                    break;

                case XivChatType.Alliance:
                    payloads.AddTextPayload("((");
                    this.ProcessSeString(chat.Name, payloads);
                    payloads.AddTextPayload(")) ");

                    break;

                case XivChatType.FreeCompany:
                    payloads.AddTextPayload("[FC]<");
                    this.ProcessSeString(chat.Name, payloads);
                    payloads.AddTextPayload("> ");

                    this.ProcessSeString(chat.Message, payloads);

                    break;

                case Event:
                    this.ProcessSeString(chat.Name, payloads);
                    payloads.AddTextPayload(": ");

                    this.ProcessSeString(chat.Message, payloads);

                    break;

                case XivChatType.TellOutgoing:
                    payloads.AddTextPayload(">> ");
                    this.ProcessSeString(chat.Name, payloads);
                    payloads.AddTextPayload(": ");

                    this.ProcessSeString(chat.Message, payloads);

                    break;
                case XivChatType.TellIncoming:

                    this.ProcessSeString(chat.Name, payloads);
                    payloads.AddTextPayload(" >> ");

                    this.ProcessSeString(chat.Message, payloads);

                    break;

                case XivChatType.Notice:
                    this.ProcessSeString(chat.Message, payloads);

                    break;

                case XivChatType.Urgent:
                    this.ProcessSeString(chat.Message, payloads);

                    break;

                // case XivChatType.StandardEmote:
                // case XivChatType.SystemMessage:
                // case JobChange:
                // case RetainerGainedExp:
                // case ObtainItemOrGil:
                // case LoggedOut:
                // case UseAction:
                // case ReadyAction:
                // case GainStatus:
                // case LostStatus:
                //
                // case GainedExp:
                //
                // case Heal:
                // // case DamageTaken:
                // // case EnemyTakesDamage:
                // case YouTakeDamageFromEngagedEnemy:
                // case EngagedEnemyUsesAction:
                // case DefeatedEngagedEnemy:
                //     if (colour != null)
                //     {
                //         this.PushForegroundColour(colour.Value);
                //         this.ProcessSeString(chat.Message, payloads);
                //         this.PopForegroundColour();
                //     }
                //     else
                //     {
                //         this.ProcessSeString(chat.Message, payloads);
                //     }
                //
                //     break;
                //
                // case XivChatType.None:
                // case XivChatType.Debug:
                // case XivChatType.Ls1:
                // case XivChatType.Ls2:
                // case XivChatType.Ls3:
                // case XivChatType.Ls4:
                // case XivChatType.Ls5:
                // case XivChatType.Ls6:
                // case XivChatType.Ls7:
                // case XivChatType.Ls8:
                // case XivChatType.NoviceNetwork:
                // case XivChatType.CustomEmote:
                // case XivChatType.CrossParty:
                // case XivChatType.PvPTeam:
                // case XivChatType.CrossLinkShell1:
                // case XivChatType.Echo:
                // case XivChatType.SystemError:
                // case XivChatType.GatheringSystemMessage:
                // case XivChatType.RetainerSale:
                // case XivChatType.CrossLinkShell2:
                // case XivChatType.CrossLinkShell3:
                // case XivChatType.CrossLinkShell4:
                // case XivChatType.CrossLinkShell5:
                // case XivChatType.CrossLinkShell6:
                // case XivChatType.CrossLinkShell7:
                // case XivChatType.CrossLinkShell8:
                //     ImGuiText($"[{Enum.GetName(chat.Type)}] ");
                //     this.ProcessSeString(chat.Name, payloads);
                //     this.ProcessSeString(": ", payloads);
                //
                //     this.ProcessSeString(chat.Message, payloads);
                //
                //     break;
                default:
                    payloads.AddTextPayload($"[{chat.Type.ToString()}] ");
                    this.ProcessSeString(chat.Name, payloads);
                    payloads.AddTextPayload(": ");

                    this.ProcessSeString(chat.Message, payloads);
                    break;
            }

            payloads.AddTypeEndingPayload((ExChatType) chat.Type, colour);

            return payloads.ToArray();
        }

        private void Clear()
        {
            lock (this.renderLock)
            {
                this.chatLog.Clear();

                this.chatTabs.ForEach(tab => tab.ChatLog.Clear());
            }
        }

        private void Refilter()
        {
            lock (this.renderLock)
            {
                this.chatTabs.ForEach(tab =>
                {
                    tab.ChatLog.Clear();

                    foreach (var entry in this.chatLog)
                    {
                        if (tab.ShouldAdd(entry))
                        {
                            tab.Add(entry);
                        }
                    }
                });
            }
        }

        private void Refilter(ChatTab tab)
        {
            lock (this.renderLock)
            {
                tab.ChatLog.Clear();

                foreach (var entry in this.chatLog)
                {
                    if (tab.ShouldAdd(entry))
                    {
                        tab.Add(entry);
                    }
                }
            }
        }

        private int selectedTab = 0;
        private readonly List<XivChatEntryWithPayloads> filteredChat = new();
        private string filter = string.Empty;
        private ChatDisplay display = new();
        private StringComparison filterComparison = StringComparison.CurrentCultureIgnoreCase;
        public Vector4 FilterColour = 0x80ce7323.AsVec4ARGB();
        private bool refilter = false;

        private void DrawOptions()
        {
            ImGuiChatHelpers.BeginGroupPanel("Filtering Options", new Vector2(16, 16));

            ImGui.NewLine();
            ImGuiChatHelpers.ColourPickerWithLabel(1, "Filter Colour", ref this.FilterColour);

            var caseInsensitive = this.filterComparison == StringComparison.CurrentCultureIgnoreCase;
            if (ImGui.Checkbox("Filter is case insensitive", ref caseInsensitive))
            {
                var newComparison = caseInsensitive
                    ? StringComparison.CurrentCultureIgnoreCase
                    : StringComparison.CurrentCulture;

                if (newComparison != this.filterComparison)
                {
                    this.filterComparison = newComparison;
                    refilter = true;
                }
            }

            ImGuiChatHelpers.EndGroupPanel();
        }

        private void RefilterSearch(ChatTab? openTab)
        {
            var filter = this.filter;
            if (string.IsNullOrWhiteSpace(filter))
            {
                lock (this.renderLock)
                {
                    this.filteredChat.Clear();
                }
            }
            else if (openTab != null)
            {
                lock (this.renderLock)
                {
                    this.filteredChat.Clear();
                    var filterLen = filter.Length;

                    openTab.ChatLog.ForEach(chat =>
                    {
                        if (chat.Payloads.Any(payload =>
                                payload is ChatDisplayPayloadText textPayload &&
                                textPayload.Text.Contains(filter, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            List<IChatDisplayPayload> payloads = new List<IChatDisplayPayload>(chat.Payloads);
                            var offset = 0;

                            foreach (var payload in chat.Payloads)
                            {
                                if (payload is ChatDisplayPayloadText textPayload &&
                                    textPayload.Text.Length >= filterLen)
                                {
                                    payloads.RemoveAt(offset);

                                    var str = textPayload.Text;
                                    var index = str.IndexOf(filter, this.filterComparison);
                                    while (index > -1)
                                    {
                                        if (index > 0)
                                        {
                                            payloads.InsertTextPayload(
                                                offset++,
                                                str.Substring(0, index));

                                            str = str.Substring(index);
                                        }

                                        // payloads.Insert(i + offset++, new ChatDisplayPayload));
                                        payloads.Insert(
                                            offset++,
                                            ChatDisplayPayloadFilterBackground.Instance);
                                        payloads.InsertTextPayload(offset++, str.Substring(0, filterLen));
                                        payloads.Insert(offset++, ChatDisplayPayloadPopBackground.Instance);
                                        // payloads.Insert(i + offset++, new ChatDisplayPayloadText(str.Substring(0, filterLen)));
                                        str = str.Substring(filterLen);


                                        index += filterLen;
                                        index = str.IndexOf(
                                            filter,
                                            this.filterComparison);
                                    }

                                    if (!str.IsNullOrWhitespace())
                                    {
                                        payloads.InsertTextPayload(offset++, str);
                                    }
                                }
                                else
                                {
                                    offset++;
                                }
                            }

                            this.filteredChat.Add(new XivChatEntryWithPayloads(chat.Entry, payloads.ToArray(),
                                chat.Type));
                        }
                    });

                    // this.filteredChat.AddRange(openTab.ChatLog.Where(chat =>
                    //     chat.Entry.Message.TextValue.Contains(this.filter, StringComparison.OrdinalIgnoreCase)));
                }
            }
        }

        private void DrawTabEdit(ChatTab tab)
        {
            ImGui.InputText("Chat Tab", ref tab.Name, 32);

            switch (tab)
            {
                case ChatTabWithTypeFilter typedChatTab:
                    this.DrawEditTypedChatTab(typedChatTab);
                    break;
            }
        }

        private (string Name, ExChatType[] Types) selectedGroup = XivChatTypes.TypeGroups[0];

        private void DrawEditTypedChatTab(ChatTabWithTypeFilter tab)
        {
            (ExChatType Type, ExChatTypeAttribute? Attr)? hovered = null;
            var refilter = false;

            if (ImGui.BeginCombo("##ChatGroups", this.selectedGroup.Name))
            {
                foreach ((string Name, ExChatType[] Types) group in XivChatTypes.TypeGroups)
                {
                    if (ImGui.Selectable(group.Name))
                    {
                        this.selectedGroup = group;
                    }
                }

                ImGui.EndCombo();
            }

            var groupStart = ImGui.GetCursorPos();
            ImGuiChatHelpers.BeginGroupPanel(this.selectedGroup.Name, new Vector2(16, 16));

            foreach (var type in this.selectedGroup.Types)
            {
                ImGui.BeginGroup();

                var attr = type.ExChatAttribute();
                bool filterEnabled = tab.AcceptedTypes.Contains(type);
                Vector4 color = tab.ChatColours.GetValueOrDefault(type, null) ?? Vector4.Zero;

                if (color != Vector4.Zero)
                {
                    ImGui.PushStyleColor(ImGuiCol.Text, color);
                }

                if (ImGui.Checkbox(attr?.DisplayName ?? ((int) type).ToString(), ref filterEnabled))
                {
                    refilter = true;

                    if (filterEnabled)
                    {
                        tab.AcceptedTypes.Add(type);
                    }
                    else
                    {
                        tab.AcceptedTypes.Remove(type);
                    }
                }

                if (color != Vector4.Zero)
                {
                    ImGui.PopStyleColor();
                }

                ImGui.EndGroup();

                if (ImGui.IsItemHovered())
                {
                    var desc = attr?.Description;
                    if (desc != null) ImGui.SetTooltip(desc);

                    hovered = (Type: type, Attr: attr);
                }

                if (ImGui.BeginPopupContextItem($"##Type{type}"))
                {
                    var enabled = color != Vector4.Zero;

                    if (ImGui.Checkbox("Custom Colour", ref enabled))
                    {
                        if (!enabled)
                        {
                            tab.ChatColours.Remove(type);
                        }
                        else
                        {
                            color = this.messageColours.GetValueOrDefault(type) ??
                                    new Vector4(1f, 1f, 1f, 1f);
                            tab.ChatColours[type] = color;
                        }
                    }

                    if (enabled)
                    {
                        if (ImGui.ColorPicker4(
                                $"###ColorPicker{type}",
                                ref color,
                                ImGuiColorEditFlags.NoSmallPreview |
                                ImGuiColorEditFlags.NoSidePreview |
                                ImGuiColorEditFlags.AlphaPreview))
                        {
                            tab.ChatColours[type] = color;
                        }
                    }

                    ImGui.EndPopup();
                }
            }
            
            ImGuiChatHelpers.EndGroupPanel();
            var width = ImGui.GetItemRectSize().X;
            var groupEnd = ImGui.GetCursorPos();
            
            ImGui.SetCursorPos(groupStart + new Vector2(16 + width, 0));
            
            ImGuiChatHelpers.BeginGroupPanel("Options", new Vector2(16, 16));
            
            ImGuiChatHelpers.EndGroupPanel();
            
            ImGui.SetCursorPos(groupEnd);

            ImGuiChatHelpers.BeginGroupPanel("Info", new Vector2(16, 16));

            if (hovered != null)
            {
                var colour = tab.ChatColours.GetValueOrDefault(hovered.Value.Type, null) ??
                             this.messageColours.GetValueOrDefault(hovered.Value.Type, null);
                if (colour != null)
                {
                    ImGui.PushStyleColor(ImGuiCol.Text, colour.Value);
                    ImGui.Text(hovered?.Attr?.Example ?? string.Empty);
                    ImGui.PopStyleColor();
                }
                else
                {
                    ImGui.Text(hovered?.Attr?.Example ?? string.Empty);
                }
            }

            ImGuiChatHelpers.EndGroupPanel();

            if (refilter) this.Refilter(tab);
        }

        public override void Draw()
        {
            ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(4, 4));

            if (ImGui.BeginPopup("Options"))
            {
                this.DrawOptions();

                ImGui.EndPopup();
            }

            if (ImGui.BeginPopup("Filters"))
            {
                ImGui.EndPopup();
            }

            if (ImGui.BeginPopup("Tab Visibility"))
            {
                for (int i = 0; i < this.chatTabs.Count; i++)
                {
                    var tab = this.chatTabs[i];

                    ImGui.Checkbox(tab.Name, ref tab.isOpen);
                }

                ImGui.EndPopup();
            }

            // ImGui.SameLine();

            var chatVisible = this.ChatVisible;
            if (ImGui.Checkbox("Chat Visible", ref chatVisible))
            {
                this.ChatVisible = chatVisible;
            }

            ImGui.Checkbox("Autoscroll", ref this._autoScroll);

            ImGui.NewLine();
            if (ImGuiComponents.IconButton(FontAwesomeIcon.Cog))
                ImGui.OpenPopup("Options");

            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("Options");

            ImGui.SameLine();
            if (ImGuiComponents.IconButton(FontAwesomeIcon.Eye))
                ImGui.OpenPopup("Tab Visibility");

            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("Tab Visibility");

            ImGui.SameLine();
            var clear = ImGuiComponents.IconButton(FontAwesomeIcon.Trash);

            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("Clear Log");

            ImGui.SameLine();
            var copy = ImGuiComponents.IconButton(FontAwesomeIcon.Copy);

            if (ImGui.IsItemHovered())
                ImGui.SetTooltip("Copy Log");

            ChatTab? openTab = this.chatTabs.ElementAtOrDefault(this.selectedTab);

            ImGui.SameLine();
            if (ImGui.InputText("Filter", ref this.filter, 64))
            {
                this.refilter = true;
            }

            if (this.refilter) this.RefilterSearch(openTab);

            if (clear)
            {
                lock (this.renderLock)
                {
                    this.Clear();
                }
            }

            // ImGui.SameLine();

            if (ImGui.BeginTabBar("Tabs"))
            {
                for (int i = 0; i < this.chatTabs.Count; i++)
                {
                    var tab = this.chatTabs[i];

                    if (ImGui.BeginTabItem(tab.Name, ref tab.isOpen))
                    {
                        this.selectedTab = i;
                        ImGui.EndTabItem();
                    }

                    if (ImGui.BeginPopupContextItem($"##Tab{i}"))
                    {
                        this.DrawTabEdit(tab);

                        ImGui.EndPopup();
                    }
                }

                ImGui.EndTabBar();
            }

            ImGui.BeginChild(
                "scrolling",
                Vector2.Zero,
                false,
                ImGuiWindowFlags.AlwaysVerticalScrollbar | ImGuiWindowFlags.AlwaysHorizontalScrollbar);

            ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, Vector2.Zero);

            ImGuiListClipperPtr clipper;
            unsafe
            {
                clipper = new ImGuiListClipperPtr(ImGuiNative.ImGuiListClipper_ImGuiListClipper());
            }

            ImGui.PushFont(UiBuilder.DefaultFont);
            this.display.Clear();

            lock (this.renderLock)
            {
                if (openTab != null)
                {
                    var log = this.filter.IsNullOrWhitespace() ? openTab.ChatLog : this.filteredChat;
                    clipper.Begin(log.Count);
                    IChatDisplayPayload[] chat = Array.Empty<IChatDisplayPayload>();

                    while (clipper.Step())
                    {
                        var start = clipper.DisplayStart;
                        var end = clipper.DisplayEnd;

                        for (var i = start; i < end; i++)
                        {
                            this.display.DisplayXivChatEntry(log[i], openTab, this);

                            ImGui.Separator();
                        }
                    }

                    clipper.End();
                }
            }

            ImGui.PopFont();

            if (this._autoScroll)
            {
                ImGui.SetScrollHereY(1.0f);
            }

            ImGui.EndChild();

            // ImGui.Text(DressUpPlugin.LocalizationManager.Localize("DalamudRichPresencePreface1", LocalizationLanguage.Plugin));
            // ImGui.Text(DressUpPlugin.LocalizationManager.Localize("DalamudRichPresencePreface2", LocalizationLanguage.Plugin));
            // ImGui.Separator();
            //
            // ImGui.BeginChild("scrolling", new Vector2(0, 400), true, ImGuiWindowFlags.HorizontalScrollbar);
            //
            // ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(1, 3));
            //
            // ImGui.Checkbox(DressUpPlugin.LocalizationManager.Localize("DalamudRichPresenceShowName", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowName);
            // ImGui.Checkbox(DressUpPlugin.LocalizationManager.Localize("DalamudRichPresenceShowFreeCompany", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowFreeCompany);
            // ImGui.Checkbox(DressUpPlugin.LocalizationManager.Localize("DalamudRichPresenceShowWorld", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowWorld);
            // ImGui.Separator();
            // ImGui.Checkbox(DressUpPlugin.LocalizationManager.Localize("DalamudRichPresenceShowStartTime", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowStartTime);
            // ImGui.Checkbox(DressUpPlugin.LocalizationManager.Localize("DalamudRichPresenceResetTimeWhenChangingZones", LocalizationLanguage.Plugin), ref RichPresenceConfig.ResetTimeWhenChangingZones);
            // ImGui.Separator();
            // ImGui.Checkbox(DressUpPlugin.LocalizationManager.Localize("DalamudRichPresenceShowJob", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowJob);
            // ImGui.Checkbox(DressUpPlugin.LocalizationManager.Localize("DalamudRichPresenceAbbreviateJob", LocalizationLanguage.Plugin), ref RichPresenceConfig.AbbreviateJob);
            // ImGui.Checkbox(DressUpPlugin.LocalizationManager.Localize("DalamudRichPresenceShowLevel", LocalizationLanguage.Plugin), ref RichPresenceConfig.ShowLevel);
            //
            // ImGui.PopStyleVar();
            //
            // ImGui.EndChild();
            //
            // ImGui.Separator();
            //
            // if (ImGui.Button(DressUpPlugin.LocalizationManager.Localize("DalamudRichPresenceSaveAndClose", LocalizationLanguage.Plugin)))
            // {
            //     this.Close();
            //     DressUpPlugin.DalamudPluginInterface.SavePluginConfig(RichPresenceConfig);
            //     DressUpPlugin.RichPresenceConfig = this.RichPresenceConfig;
            //     PluginLog.Log("Settings saved.");
            // }
        }

        // public bool IsOpen()
        // {
        //     return this.IsVisible;
        // }
        //
        // public void Close()
        // {
        //     this.IsVisible = false;
        // }
        //
        // public void Open()
        // {
        //     this.IsVisible = true;
        // }
        //
        // public void Toggle()
        // {
        //     this.IsVisible = !this.IsVisible;
        // }
    }
}