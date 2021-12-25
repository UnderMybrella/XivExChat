using System.Numerics;

namespace EventChat
{
    using System;
    using System.Collections.Generic;
    using Dalamud.Game.Text;

    public class ChatTab
    {
        public List<XivChatEntryWithPayloads> ChatLog = new();
        public Dictionary<ExChatType, Vector4?> ChatColours = new();
        
        public string Name = string.Empty;
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
    
    public class ChatTabWithTypeFilter : ChatTab
    {
        public HashSet<ExChatType> AcceptedTypes = new();

        public ChatTabWithTypeFilter()
        {
        }

        public ChatTabWithTypeFilter(string name)
            : base(name)
        {
        }
        
        public ChatTabWithTypeFilter(string name, params ExChatType[] types)
            : base(name)
        {
            this.AcceptedTypes = new HashSet<ExChatType>(types);
        }

        public ChatTabWithTypeFilter(string name, List<XivChatEntryWithPayloads> chatLog)
            : base(name, chatLog)
        {
        }
        
        public ChatTabWithTypeFilter(string name, HashSet<ExChatType> acceptedTypes)
            : base(name)
        {
            this.AcceptedTypes = acceptedTypes;
        }
        
        public ChatTabWithTypeFilter(string name, List<XivChatEntryWithPayloads> chatLog, HashSet<ExChatType> acceptedTypes)
            : base(name, chatLog)
        {
            this.AcceptedTypes = acceptedTypes;
        }

        public override bool ShouldAdd(XivChatEntryWithPayloads entry)
        {
            return this.AcceptedTypes.Contains(entry.Type);
        }
    }
}