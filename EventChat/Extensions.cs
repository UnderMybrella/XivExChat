using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using Dalamud.Game.Gui;
using Dalamud.Game.Text;
using FFXIVClientStructs.FFXIV.Component.GUI;
using ImGuiNET;
using Lumina.Excel.GeneratedSheets;

namespace EventChat
{
    using Dalamud.Game.Text.SeStringHandling;

    public static class Extensions
    {
        // public static SeString StripItemLinks(this SeString value)
        // {
        //     if (value.Payloads.Any(payload => payload.Type == PayloadType.Item))
        //     {
        //         var payloads = value.Payloads;
        //         
        //         
        //     }
        //     
        //     return value;
        // }

        public static Vector4 UiForegroundAsVector4(this UIColor uiColour)
        {
            var colour = uiColour.UIForeground;

            return new Vector4(
                ((colour >> 24) & 0xFF) / 255f,
                ((colour >> 16) & 0xFF) / 255f,
                ((colour >> 8) & 0xFF) / 255f,
                ((colour >> 0) & 0xFF) / 255f);
        }

        public static Vector4 UiGlowAsVector4(this UIColor uiColour)
        {
            var colour = uiColour.UIGlow;

            return new Vector4(
                ((colour >> 24) & 0xFF) / 255f,
                ((colour >> 16) & 0xFF) / 255f,
                ((colour >> 8) & 0xFF) / 255f,
                ((colour >> 0) & 0xFF) / 255f);
        }

        public static Vector4? DefaultColourAsVector4(this XivChatType value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            var details = name == null
                ? null
                : type.GetField(name)
                    ?.GetCustomAttributes(false)
                    ?.OfType<XivChatTypeInfoAttribute>()
                    ?.SingleOrDefault<XivChatTypeInfoAttribute>();

            if (details == null) return null;
            var colour = details.DefaultColor;

            return new Vector4(
                ((colour >> 24) & 0xFF) / 255f,
                ((colour >> 16) & 0xFF) / 255f,
                ((colour >> 8) & 0xFF) / 255f,
                ((colour >> 0) & 0xFF) / 255f);
        }

        public static Vector4 AsVec4Colour(this uint colour) => new Vector4(
            ((colour >> 24) & 0xFF) / 255f,
            ((colour >> 16) & 0xFF) / 255f,
            ((colour >> 8) & 0xFF) / 255f,
            ((colour >> 0) & 0xFF) / 255f);
        
        public static Vector4 AsVec4ARGB(this uint colour) => new Vector4(
            ((colour >> 16) & 0xFF) / 255f,
            ((colour >> 8) & 0xFF) / 255f,
            ((colour >> 0) & 0xFF) / 255f,
            ((colour >> 24) & 0xFF) / 255f);

        public static uint AsRGBAColour(this Vector4 colour) =>
            ((uint) (colour.X * 255) << 0) |
            ((uint) (colour.Y * 255) << 8) |
            ((uint) (colour.Z * 255) << 16) |
            ((uint) (colour.W * 255) << 24);

        public static Vector4 ColourAsVec4(int r, int g, int b, int a) =>
            new Vector4(r / 255f, g / 255f, b / 255f, a / 255f);

        public static unsafe byte* CalcWordWrapPositionA(
            this ImFontPtr value,
            float scale,
            byte* text,
            byte* textEnd,
            float wrapWidth) =>
            ImGuiNative.ImFont_CalcWordWrapPositionA(value.NativePtr, scale, text, textEnd, wrapWidth);

        public static unsafe string CalcWordWrapPositionA(
            this ImFontPtr value,
            float scale,
            string text,
            string textEnd,
            float wrapWidth)
        {
            var textPtr = Marshal.StringToHGlobalUni(text);
            var textEndPtr = Marshal.StringToHGlobalUni(textEnd);

            try
            {
                return Marshal.PtrToStringAuto(
                    (IntPtr) value.CalcWordWrapPositionA(scale, (byte*) textPtr, (byte*) textEndPtr, wrapWidth));
            }
            finally
            {
                Marshal.FreeHGlobal(textPtr);
                Marshal.FreeHGlobal(textEndPtr);
            }
        }

        public static unsafe bool IsChatWindowVisible(this GameGui gui)
        {
            IntPtr addr = gui.GetAddonByName("ChatLog", 1);
            if (addr != IntPtr.Zero) return ((AtkUnitBase*) addr)->IsVisible;
            return false;
        }

        public static unsafe void HideChatWindow(this GameGui gui) => gui.SetChatWindowVisibility(false);

        public static unsafe void ShowChatWindow(this GameGui gui) => gui.SetChatWindowVisibility(true);

        public static unsafe void SetChatWindowVisibility(this GameGui gui, bool isVisible)
        {
            IntPtr addr = gui.GetAddonByName("ChatLog", 1);
            if (addr != IntPtr.Zero)
            {
                var chatLog = (AtkUnitBase*) addr;

                if (isVisible)
                {
                    chatLog->IsVisible = true;

                    // This flag controls hiding the whole element, and being able to bring it back with the enter key
                    chatLog->Flags = (byte) (chatLog->Flags & ~((byte) 0x50));
                }
                else
                {
                    chatLog->IsVisible = false;

                    // This flag controls hiding the whole element, and being able to bring it back with the enter key
                    chatLog->Flags = (byte) (chatLog->Flags | 0x50);
                }
            }
        }
    }
}