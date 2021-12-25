namespace EventChat
{
#nullable enable
    using System;

    [AttributeUsage(AttributeTargets.Field)]
    public class ExChatTypeAttribute : Attribute
    {
        internal ExChatTypeAttribute(string? displayName, string? example)
        {
            this.DisplayName = displayName;
            this.Description = null;
            this.Example = example;
            this.DefaultLuminaRow = null;
        }
        
        internal ExChatTypeAttribute(string? displayName, string? example, uint defaultLuminaRow)
        {
            this.DisplayName = displayName;
            this.Description = null;
            this.Example = example;
            this.DefaultLuminaRow = defaultLuminaRow;
        }
        
        internal ExChatTypeAttribute(string? displayName, string? description, string? example)
        {
            this.DisplayName = displayName;
            this.Description = description;
            this.Example = example;
            this.DefaultLuminaRow = null;
        }
        
        internal ExChatTypeAttribute(string? displayName, string? description, string? example, uint? defaultLuminaRow)
        {
            this.DisplayName = displayName;
            this.Description = description;
            this.Example = example;
            this.DefaultLuminaRow = defaultLuminaRow;
        }
        
        internal ExChatTypeAttribute(string? displayName, string? description, string? example, uint? defaultLuminaRow, uint? defaultColour)
        {
            this.DisplayName = displayName;
            this.Description = description;
            this.Example = example;
            this.DefaultLuminaRow = defaultLuminaRow;
            this.DefaultColour = defaultColour;
        }

        public string? DisplayName { get; }
        public string? Description { get; }
        public string? Example { get; }
        public uint? DefaultLuminaRow { get; }
        
        public uint? DefaultColour { get; }
    }
}