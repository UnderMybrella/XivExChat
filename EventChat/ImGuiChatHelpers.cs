using System;
using System.Collections.Generic;
using System.Numerics;
using ImGuiNET;

namespace EventChat
{
    public static class ImGuiChatHelpers
    {
        public static void SameLineNoSpace()
        {
            ImGui.SameLine(0f, 0f);
        }

        public static void DrawTextBackground(string text, uint colour)
        {
            var size = ImGui.CalcTextSize(text);
            var min = ImGui.GetWindowPos() + ImGui.GetCursorPos();
            var max = min + size;
            ImGui.GetWindowDrawList()
                .AddRectFilled(min, max, colour);
        }

        public static void ColourPickerWithLabel(object id, string label, ref Vector4 color)
        {
            ImGui.SameLine(0f, 0f);
            if (ImGui.ColorButton($"{label}###ColorPickerButton{id}", color))
                ImGui.OpenPopup($"###ColorPickerPopup{(object) id}");

            ImGui.SameLine();
            ImGui.Text(label);

            if (ImGui.BeginPopup($"###ColorPickerPopup{(object) id}"))
            {
                ImGui.ColorPicker4(
                    $"###ColorPicker{(object) id}",
                    ref color,
                    ImGuiColorEditFlags.NoSmallPreview | ImGuiColorEditFlags.NoSidePreview);
                ImGui.EndPopup();
            }
        }

        private static Vector2 _defaultBorderPadding = new(16, 16);
        private const uint _defaultBorderColour = 0xFFFFFFFF;

        public static void CollapsingHeaderWithBorder(string header, Action populate) =>
            CollapsingHeaderWithBorder(header, _defaultBorderPadding, _defaultBorderColour, populate);

        public static void CollapsingHeaderWithBorder(string header, Vector2 padding, Action populate) =>
            CollapsingHeaderWithBorder(header, padding, _defaultBorderColour, populate);

        private static readonly Vector2 _defaultGroupPanelSize = new Vector2(-1, -1);
        private static readonly List<Vector4> _groupPanelLabelStack = new();

        public static void BeginGroupPanel(string name) => BeginGroupPanel(name, _defaultGroupPanelSize);

        public static void BeginGroupPanel(string name, Vector2 size)
        {
            ImGui.BeginGroup();

            var cursorPos = ImGui.GetCursorPos();
            var itemSpacing = ImGui.GetStyle().ItemSpacing;
            ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, Vector2.Zero);
            ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, Vector2.Zero);

            var frameHeight = ImGui.GetFrameHeight();
            ImGui.BeginGroup();

            var effectiveSize = size;
            if (size.X < 0) effectiveSize.X = ImGui.GetContentRegionAvail().X;
            else effectiveSize.X = size.X;

            ImGui.Dummy(new Vector2(effectiveSize.X, 0));
            SameLineNoSpace();
            ImGui.BeginGroup();
            ImGui.Dummy(new Vector2(frameHeight * 0.5f, 0f));
            SameLineNoSpace();
            ImGui.TextUnformatted(name);
            var labelMin = ImGui.GetItemRectMin();
            var labelMax = ImGui.GetItemRectMax();
            SameLineNoSpace();
            ImGui.Dummy(new Vector2(0, frameHeight + itemSpacing.Y));
            ImGui.BeginGroup();

            ImGui.PopStyleVar(2);

            // ImGui::GetCurrentWindow()->ContentsRegionRect.Max.x -= frameHeight * 0.5f;
            // ImGui::GetCurrentWindow()->Size.x                   -= frameHeight;

            var contentRegionMax = ImGui.GetWindowContentRegionMax();
            contentRegionMax.X -= frameHeight * .5f;
            
            var workRect = ImGui.GetWindowViewport().WorkSize;
            workRect.X -= frameHeight * .5f;
            
            var windowSize = ImGui.GetWindowSize();
            windowSize.X -= frameHeight;
            
            var itemWidth = ImGui.CalcItemWidth();
            ImGui.PushItemWidth(itemWidth);
            
            // ImGui.SetCursorPos(cursorPos);

            _groupPanelLabelStack.Add(new Vector4(labelMin.X, labelMin.Y, labelMax.X, labelMax.Y));
        }

        public static unsafe void EndGroupPanel()
        {
            ImGui.PopItemWidth();

            var itemSpacing = ImGui.GetStyle().ItemSpacing;

            ImGui.PushStyleVar(ImGuiStyleVar.FramePadding, Vector2.Zero);
            ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, Vector2.Zero);

            var frameHeight = ImGui.GetFrameHeight();

            ImGui.EndGroup();

            ImGui.EndGroup();

            SameLineNoSpace();
            ImGui.Dummy(new Vector2(frameHeight * .5f, 0f));
            ImGui.Dummy(new Vector2(0f, frameHeight - (frameHeight * .5f)));

            ImGui.EndGroup();

            var itemMin = ImGui.GetItemRectMin();
            var itemMax = ImGui.GetItemRectMax();

            var groupPanelLabelStackIndex = _groupPanelLabelStack.Count - 1;
            var labelRect = _groupPanelLabelStack[groupPanelLabelStackIndex];
            _groupPanelLabelStack.RemoveAt(groupPanelLabelStackIndex);

            var halfFrame = new Vector2(frameHeight * .25f, frameHeight) * .5f;
            var frameRect = new Vector4(itemMin.X + halfFrame.X, itemMin.Y + halfFrame.Y, itemMax.X - halfFrame.X,
                itemMax.Y);
            labelRect.X -= itemSpacing.X;
            labelRect.Z += itemSpacing.X;

            for (int i = 0; i < 4; ++i)
            {
                switch (i)
                {
                    case 0:
                        ImGui.PushClipRect(
                            new Vector2(float.MinValue, float.MinValue),
                            new Vector2(labelRect.X, float.MaxValue),
                            true);
                        break;

                    case 1:
                        ImGui.PushClipRect(
                            new Vector2(labelRect.Z, float.MinValue),
                            new Vector2(float.MaxValue, float.MaxValue),
                            true);
                        break;

                    case 2:
                        ImGui.PushClipRect(
                            new Vector2(labelRect.X, float.MinValue),
                            new Vector2(labelRect.Z, labelRect.Y),
                            true);
                        break;

                    case 3:
                        ImGui.PushClipRect(
                            new Vector2(labelRect.X, labelRect.W),
                            new Vector2(labelRect.Z, float.MaxValue),
                            true);
                        break;
                }
                
                ImGui.GetWindowDrawList().AddRect(
                    new Vector2(frameRect.X, frameRect.Y),
                    new Vector2(frameRect.Z, frameRect.W),
                    (*ImGui.GetStyleColorVec4(ImGuiCol.Border)).AsRGBAColour(),
                    halfFrame.X);
                        
                ImGui.PopClipRect();
            }

            // ImGui.GetWindowDrawList().AddRect(
            //     itemMin + halfFrame,
            //     itemMax - new Vector2(halfFrame.X, 0),
            //     (*ImGui.GetStyleColorVec4(ImGuiCol.Border)).AsRGBAColour(),
            //     halfFrame.X);

            ImGui.PopStyleVar(2);

            // ImGui::GetCurrentWindow()->ContentsRegionRect.Max.x += frameHeight * 0.5f;
            // ImGui::GetCurrentWindow()->Size.x                   += frameHeight;

            var contentRegionMax = ImGui.GetWindowContentRegionMax();
            contentRegionMax.X += frameHeight * .5f;

            var workRect = ImGui.GetWindowViewport().WorkSize;
            workRect.X += frameHeight * .5f;
            
            var windowSize = ImGui.GetWindowSize();
            windowSize.X += frameHeight;

            ImGui.Dummy(Vector2.Zero);

            ImGui.EndGroup();
        }

        public static void CollapsingHeaderWithBorder(
            string header,
            Vector2 padding,
            uint borderColour,
            Action populate)
        {
            if (ImGui.CollapsingHeader(header))
            {
                ImGui.BeginGroup();

                populate();

                ImGui.EndGroup();
                ImGui.GetWindowDrawList().AddRect(
                    ImGui.GetItemRectMin() - padding,
                    ImGui.GetItemRectMax() + padding,
                    borderColour);
            }
        }
    }
}