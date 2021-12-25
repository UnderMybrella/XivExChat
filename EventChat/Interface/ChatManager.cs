using System;
using Dalamud.Game.Gui;
using Dalamud.Interface.Windowing;
using Dalamud.Logging;
using ImGuiNET;

namespace EventChat
{
    public class ChatManager : IDisposable
    {
        public WindowSystem WindowSystem { get; init; }

        private readonly ChatWindow chatWindow;

        private EventChatPlugin _plugin;

        public ChatManager(EventChatPlugin plugin)
        {
            this._plugin = plugin;

            this.WindowSystem = new WindowSystem("EventChat");
            this.chatWindow = new ChatWindow(plugin) { IsOpen = true };
            
            this.WindowSystem.AddWindow(this.chatWindow);
            
            this._plugin.DalamudPluginInterface.UiBuilder.Draw += this.Draw;
        }

        public void Dispose()
        {
            this._plugin.DalamudPluginInterface.UiBuilder.Draw -= this.Draw;
            
            this.WindowSystem.RemoveAllWindows();
        }

        public void Draw()
        {
            try
            {
                // if (Service<GameGui>.Get().GameUiHidden)
                //     return;

                this.WindowSystem.Draw();

                // Release focus of any ImGui window if we click into the game.
                // var io = ImGui.GetIO();
                // if (!io.WantCaptureMouse && (User32.GetKeyState((int)User32.VirtualKey.VK_LBUTTON) & 0x8000) != 0)
                // {
                //     ImGui.SetWindowFocus(null);
                // }
            }
            catch (Exception ex)
            {
                PluginLog.Error(ex, "Error during OnDraw");
            }
        }

        public void OpenEventChat() => this.chatWindow.IsOpen = true;
        public void CloseEventChat() => this.chatWindow.IsOpen = false;
        public void ToggleEventChat() => this.chatWindow.Toggle();
    }
}