using System.Numerics;

namespace EventChat
{
    using System;
    using System.Collections.Generic;
    using Dalamud.Game.Text;

    public class ChatTab
    {
        public List<XivChatEntryWithPayloads> ChatLog = new();
        public Dictionary<XivChatType, Vector4?> ChatColours = new();
        
        public string Name;
        public bool isOpen = true;

        public ChatTab()
        {
        }

        public ChatTab(string name)
        {
            this.Name = name;
        }

        public ChatTab(string name, List<XivChatEntryWithPayloads> chatLog)
        {
            this.Name = name;
            this.ChatLog = chatLog;
        }

        public virtual bool ShouldAdd(XivChatEntryWithPayloads entry)
        {
            return true;
        }
        
        public virtual void Add(XivChatEntryWithPayloads entry)
        {
            this.ChatLog.Add(entry);
        }
    }

    public class ChatTabWithFilter : ChatTab
    {
        public Predicate<XivChatEntryWithPayloads> Filter;

        public ChatTabWithFilter()
        {
        }

        public ChatTabWithFilter(string name, Predicate<XivChatEntryWithPayloads> filter)
            : base(name)
        {
            this.Filter = filter;
        }

        public ChatTabWithFilter(string name, List<XivChatEntryWithPayloads> chatLog, Predicate<XivChatEntryWithPayloads> filter)
            : base(name, chatLog)
        {
            this.Filter = filter;
        }

        public override bool ShouldAdd(XivChatEntryWithPayloads entry)
        {
            return this.Filter(entry);
        }
    }
}