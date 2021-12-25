using System.Numerics;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Utility;
using ImGuiNET;
using ImGuiScene;

namespace EventChat.Interface
{
    using Lumina.Excel.GeneratedSheets;

    public interface IHoverPayload
    {
        public void DrawIn(ChatDisplay display);
    }

    public record HoverPayloadText(string Text) : IHoverPayload
    {
        public void DrawIn(ChatDisplay display)
        {
            ImGui.SetTooltip(this.Text);
        }
    }
    
    public record HoverPayloadItem(Item Item) : IHoverPayload
    {
        private TextureWrap _icon = EventChatPlugin.DataManager.GetImGuiTextureIcon(false, Item.Icon);

        public void DrawIn(ChatDisplay display)
        {
            ImGui.BeginTooltip();

            var start = ImGui.GetCursorPos();
            ImGui.Image(this._icon.ImGuiHandle, new Vector2(this._icon.Width, this._icon.Height));
            var end = ImGui.GetCursorPos();
            
            ImGui.SetCursorPos(new Vector2(start.X + this._icon.Width + 8, start.Y));
            ImGui.TextUnformatted(this.Item.Name);

            var category = this.Item.ItemUICategory.Value?.Name?.RawString;
            ImGui.SetCursorPos(end);
            ImGui.TextUnformatted(category ?? "Unknown");
            
            ImGui.NewLine();

            var itemAction = this.Item.ItemAction.Value;

            if (itemAction != null && itemAction.Type != 0)
            {
                ImGui.TextUnformatted("Effects");
                ImGui.Separator();
                
                switch (itemAction.Type)
                {
                    case 844:
                    case 846:
                        var food = EventChatPlugin.DataManager.GetExcelSheet<ItemFood>()?.GetRow(itemAction.Data[1]);
                        var baseParams = EventChatPlugin.DataManager.GetExcelSheet<BaseParam>();

                        if (food != null)
                        {
                            foreach (var itemFoodUnkData1Obj in food.UnkData1)
                            {
                                if (itemFoodUnkData1Obj.BaseParam == 0) continue;

                                var builder = new SeStringBuilder();
                                var baseParam = baseParams?.GetRow(itemFoodUnkData1Obj.BaseParam)?.Name;

                                if (baseParam != null)
                                {
                                    builder.Append(baseParam.ToDalamudString());
                                }
                                else
                                {
                                    builder.AddText("(Unknown Stat)");
                                }
                                
                                if (itemFoodUnkData1Obj.IsRelative)
                                {
                                    builder.AddText($" +{itemFoodUnkData1Obj.Value}% (Max {itemFoodUnkData1Obj.Max})");
                                }
                                else
                                {
                                    builder.AddText($" +{itemFoodUnkData1Obj.Value}");
                                }
                                
                                ImGui.NewLine();
                                ImGuiChatHelpers.DisplaySeString(builder.Build());
                            }
                        }

                        break;
                    case 847:
                        ImGui.TextUnformatted($"Restores up to {itemAction.Data[0]}% of HP ({itemAction.Data[1]} points max).");
                        break;
                    
                    case 853:
                        ImGui.TextUnformatted("Obtain a minion");
                        break;
                    
                    case 1055:
                        ImGui.TextUnformatted($"A sweet, fermented concoction that instantly restores {itemAction.Data[0]} GP.");
                        break;
                    
                    case 4647:
                        ImGui.TextUnformatted("Contains a piece of gear");
                        break;
                    
                    case 9994:
                        ImGui.TextUnformatted("One free teleport");
                        break;
                    
                    default:
                        // ImGui.TextUnformatted($"(Uho, unknown action type {itemAction.Type}! Check ItemAction#{this.Item.ItemAction.Row}");
                        break;
                }
                
                ImGui.NewLine();
            }

            var desc = this.Item.Description;

            if (desc != null && desc.Payloads.Count != 0)
            {
                ImGui.Separator();
                ImGuiChatHelpers.DisplaySeString(desc.ToDalamudString());
            }

            ImGui.EndTooltip();
        }
    }
}