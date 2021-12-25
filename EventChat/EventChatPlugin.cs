using System.Threading.Tasks;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace EventChat
{
    using System;
    using System.Collections.Generic;
    using Dalamud.Data;
    using Dalamud.Game.Command;
    using Dalamud.Game.Gui;
    using Dalamud.Game.Text;
    using Dalamud.Game.Text.SeStringHandling;
    using Dalamud.Game.Text.SeStringHandling.Payloads;
    using Dalamud.Interface;
    using Dalamud.IoC;
    using Dalamud.Logging;
    using Dalamud.Plugin;
    using FFXIVClientStructs;
    using FFXIVClientStructs.FFXIV.Client.UI;
    using Lumina.Excel.GeneratedSheets;

    public class EventChatPlugin : IDalamudPlugin, IDisposable
    {
        [PluginService] internal DalamudPluginInterface DalamudPluginInterface { get; private set; }
        [PluginService] internal ChatGui ChatGui { get; private set; }
        [PluginService] internal GameGui GameGui { get; private set; }

        [PluginService] internal DataManager DataManager { get; private set; }

        [PluginService] internal CommandManager CommandManager { get; private set; }
        public ChatManager ChatManager { get; private set; }

        public EventChatPlugin()
        {
            Resolver.Initialize();

            this.ChatManager = new ChatManager(this);

            // ReSharper disable once PossibleNullReferenceException
            this.DalamudPluginInterface.UiBuilder.DisableCutsceneUiHide = true;
            this.RegisterCommands();
        }

        public string Name => "EventChat";

        public void Dispose()
        {
            DeregisterCommands();
        }

        public void RegisterCommands()
        {
            this.CommandManager.AddHandler("/eventchat", new CommandInfo((command, arguments) =>
            {
                if (arguments.Equals("close", StringComparison.OrdinalIgnoreCase))
                {
                    this.ChatManager.CloseEventChat();
                }
                else if (arguments.Equals("open", StringComparison.OrdinalIgnoreCase))
                {
                    this.ChatManager.OpenEventChat();
                }
                else
                {
                    this.ChatManager.ToggleEventChat();
                }
            }));
            this.CommandManager.AddHandler("/hidechat", new CommandInfo((_, _) =>
            {
                unsafe
                {
                    IntPtr addr = this.GameGui.GetAddonByName("ChatLog", 1);
                    if (addr != IntPtr.Zero)
                    {
                        var chatLog = (AtkUnitBase*) addr;

                        // Make it invisible
                        chatLog->IsVisible = false;

                        // This flag controls hiding the whole element, and being able to bring it back with the enter key
                        chatLog->Flags = (byte) (chatLog->Flags | 0x50);
                    }
                }
            }));

            this.CommandManager.AddHandler("/testchat", new CommandInfo(this.TestChat));
        }

        public void DeregisterCommands()
        {
            this.CommandManager.RemoveHandler("/eventchat");
            this.CommandManager.RemoveHandler("/hidechat");
            this.CommandManager.RemoveHandler("/testchat");
        }

        public void TestChat(string command, string arguments)
        {
            ChatGui.Print(new SeString(
                (Payload) new UIForegroundPayload((ushort) 553), // blue
                (Payload) new UIGlowPayload((ushort) 554),
                (Payload) new UIForegroundPayload((ushort) 555), //purple
                (Payload) new UIGlowPayload((ushort) 556),
                (Payload) new ItemPayload(33584, true),
                (Payload) new UIForegroundPayload((ushort) 500),
                (Payload) new UIGlowPayload((ushort) 501),
                (Payload) new TextPayload("\uE0BB"),
                (Payload) new RawPayload(new byte[] { 0x02, 0x13, 0x02, 0xEC, 0x03 }),
                (Payload) new TextPayload("Hi there! This is a test!"),
                (Payload) RawPayload.LinkTerminator,
                (Payload) new RawPayload(new byte[] { 0x02, 0x13, 0x02, 0xEC, 0x03 }),
                (Payload) new TextPayload(" <<<")));
        }
    }
}