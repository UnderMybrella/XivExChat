namespace EventChat
{
    using System.Collections.Generic;
    using System.Numerics;
    using ImGuiNET;

    public class ChatDisplay
    {
        private List<Vector4> foregroundStack = new List<Vector4>();
        private List<Vector4> backgroundStack = new List<Vector4>();
        private List<Vector4> glowStack = new List<Vector4>();
        private bool italics = false;

        public void Clear()
        {
            this.foregroundStack.Clear();
            this.backgroundStack.Clear();
            this.glowStack.Clear();
            this.italics = false;
        }
        
        public Vector4? ForegroundColour()
        {
            var count = this.foregroundStack.Count;
            return count == 0 ? null : this.foregroundStack[count - 1];
        }
        
        public Vector4? BackgroundColour()
        {
            var count = this.backgroundStack.Count;
            return count == 0 ? null : this.backgroundStack[count - 1];
        }

        public Vector4? GlowColour()
        {
            var count = this.foregroundStack.Count;
            return count == 0 ? null : this.glowStack[count - 1];
        }

        public void PushForegroundColour(Vector4 colour)
        {
            this.foregroundStack.Add(colour);
        }
        
        public void PushBackgroundColour(Vector4 colour)
        {
            this.backgroundStack.Add(colour);
        }

        public void PushGlowColour(Vector4 colour)
        {
            this.glowStack.Add(colour);
        }

        public void PopForegroundColour()
        {
            var count = this.foregroundStack.Count;
            if (count > 0)
            {
                this.foregroundStack.RemoveAt(count - 1);
            }
        }
        
        public void PopBackgroundColour()
        {
            var count = this.backgroundStack.Count;
            if (count > 0)
            {
                this.backgroundStack.RemoveAt(count - 1);
            }
        }

        public void PopGlowColour()
        {
            var count = this.glowStack.Count;
            if (count > 0)
            {
                this.glowStack.RemoveAt(count - 1);
            }
        }

        public void ImGuiText(string text)
        {
            ImGuiChatHelpers.SameLineNoSpace();
            var foreground = this.ForegroundColour();
            var background = this.BackgroundColour();

            if (background != null) ImGuiChatHelpers.DrawTextBackground(text, background.Value.AsRGBAColour());

            if (foreground != null)
            {
                ImGui.PushStyleColor(ImGuiCol.Text, foreground.Value);
                ImGui.TextUnformatted(text);
                ImGui.PopStyleColor();
            }
            else
            {
                ImGui.TextUnformatted(text);
            }
        }
    }
}