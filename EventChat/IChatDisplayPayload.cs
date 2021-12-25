#pragma warning disable SA1402
namespace EventChat
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using Dalamud.Game.Text;
    using ImGuiNET;
    using ImGuiScene;

    public interface IChatDisplayPayload
    {
        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow);
    }

    public record XivChatEntryWithPayloads(XivChatEntry Entry, IChatDisplayPayload[] Payloads);

    public record ChatDisplayPayloadTypeStart(XivChatType Type, Vector4? DefaultColour) : IChatDisplayPayload
    {
        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow)
        {
            var colour = inTab.ChatColours.GetValueOrDefault(this.Type, null) ?? this.DefaultColour;

            if (colour != null)
            {
                display.PushForegroundColour(colour.Value);
            }
        }
    }

    public record ChatDisplayPayloadTypeEnd(XivChatType Type, Vector4? DefaultColour) : IChatDisplayPayload
    {
        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow)
        {
            var colour = inTab.ChatColours.GetValueOrDefault(this.Type, null) ?? this.DefaultColour;

            if (colour != null)
            {
                display.PopForegroundColour();
            }
        }
    }

    public record ChatDisplayPayloadText(string Text) : IChatDisplayPayload
    {
        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow)
        {
            display.ImGuiText(this.Text);
        }
    }

    public record ChatDisplayPayloadItalics(bool ItalicsEnabled) : IChatDisplayPayload
    {
        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow)
        {
            // display.Ita
        }
    }

    public record ChatDisplayPayloadForeground(Vector4 UiForeground) : IChatDisplayPayload
    {
        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow)
        {
            display.PushForegroundColour(this.UiForeground);
        }
    }
    
    public record ChatDisplayPayloadBackground(Vector4 Background) : IChatDisplayPayload
    {
        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow)
        {
            display.PushBackgroundColour(this.Background);
        }
    }
    
    public sealed class ChatDisplayPayloadFilterBackground : IChatDisplayPayload
    {
        private static readonly Lazy<ChatDisplayPayloadFilterBackground> Lazy =
            new(() => new());

        public static ChatDisplayPayloadFilterBackground Instance => Lazy.Value;

        private ChatDisplayPayloadFilterBackground()
        {
        }

        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow)
        {
            display.PushBackgroundColour(inWindow.FilterColour);
        }
    }

    public record ChatDisplayPayloadGlow(Vector4 UiGlow) : IChatDisplayPayload
    {
        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow)
        {
            display.PushGlowColour(this.UiGlow);
        }
    }

    public record ChatDisplayPayloadIcon(TextureWrap Texture, Vector2 Size) : IChatDisplayPayload
    {
        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow)
        {
            ImGuiChatHelpers.SameLineNoSpace();
            var fontGlobalScale = ImGui.GetIO().FontGlobalScale;
            var size = this.Size / 2 * fontGlobalScale;
            ImGui.Image(this.Texture.ImGuiHandle, size);
        }
    }

    public sealed class ChatDisplayPayloadNewLine : IChatDisplayPayload
    {
        private static readonly Lazy<ChatDisplayPayloadNewLine> Lazy =
            new(() => new());

        public static ChatDisplayPayloadNewLine Instance => Lazy.Value;

        private ChatDisplayPayloadNewLine()
        {
        }

        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow)
        {
            ImGui.NewLine();
        }
    }

    public sealed class ChatDisplayPayloadPopForeground : IChatDisplayPayload
    {
        private static readonly Lazy<ChatDisplayPayloadPopForeground> Lazy =
            new(() => new());

        public static ChatDisplayPayloadPopForeground Instance => Lazy.Value;

        private ChatDisplayPayloadPopForeground()
        {
        }

        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow)
        {
            display.PopForegroundColour();
        }
    }
    
    public sealed class ChatDisplayPayloadPopBackground : IChatDisplayPayload
    {
        private static readonly Lazy<ChatDisplayPayloadPopBackground> Lazy =
            new(() => new());

        public static ChatDisplayPayloadPopBackground Instance => Lazy.Value;

        private ChatDisplayPayloadPopBackground()
        {
        }

        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow)
        {
            display.PopBackgroundColour();
        }
    }

    public sealed class ChatDisplayPayloadPopGlow : IChatDisplayPayload
    {
        private static readonly Lazy<ChatDisplayPayloadPopGlow> Lazy =
            new(() => new());

        public static ChatDisplayPayloadPopGlow Instance => Lazy.Value;

        private ChatDisplayPayloadPopGlow()
        {
        }

        public void DisplayIn(ChatDisplay display, ChatTab inTab, ChatWindow inWindow)
        {
            display.PopGlowColour();
        }
    }

    public static class ChatPayloadExtensions
    {
        public static void AddTypeBeginningPayload(
            this List<IChatDisplayPayload> list,
            XivChatType type,
            Vector4? defaultColour) => list.Add(new ChatDisplayPayloadTypeStart(type, defaultColour));

        public static void AddTypeEndingPayload(
            this List<IChatDisplayPayload> list,
            XivChatType type,
            Vector4? defaultColour) => list.Add(new ChatDisplayPayloadTypeEnd(type, defaultColour));

        public static void AddUiForegroundPayload(this List<IChatDisplayPayload> list, Vector4 colour) =>
            list.Add(new ChatDisplayPayloadForeground(colour));

        public static void AddUiGlowPayload(this List<IChatDisplayPayload> list, Vector4 colour) =>
            list.Add(new ChatDisplayPayloadGlow(colour));

        public static void AddTextPayload(this List<IChatDisplayPayload> list, string text) =>
            list.Add(new ChatDisplayPayloadText(text));
        
        public static void InsertTextPayload(this List<IChatDisplayPayload> list, int index, string text) =>
            list.Insert(index, new ChatDisplayPayloadText(text));

        public static void AddItalicsPayload(this List<IChatDisplayPayload> list, bool enabled) =>
            list.Add(new ChatDisplayPayloadItalics(enabled));

        public static void AddIconPayload(this List<IChatDisplayPayload> list, TextureWrap icon, Vector2 size) =>
            list.Add(new ChatDisplayPayloadIcon(icon, size));

        public static void AddPopUiForegroundPayload(this List<IChatDisplayPayload> list) =>
            list.Add(ChatDisplayPayloadPopForeground.Instance);

        public static void AddPopUiGlowPayload(this List<IChatDisplayPayload> list) =>
            list.Add(ChatDisplayPayloadPopGlow.Instance);

        public static void AddNewLinePayload(this List<IChatDisplayPayload> list) =>
            list.Add(ChatDisplayPayloadNewLine.Instance);

        public static void DisplayXivChatEntry(this ChatDisplay display, XivChatEntryWithPayloads chat, ChatTab inTab, ChatWindow inWindow)
        {
            foreach (var payload in chat.Payloads)
            {
                payload.DisplayIn(display, inTab, inWindow);
            }
        }
    }
}
#pragma warning restore SA1402