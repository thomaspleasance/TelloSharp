using System;

namespace TelloSharp
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    
    sealed class TelloCommand : Attribute
    {
        public TelloCommand(string command)
        {
            Command = command;
        }

        public string Command { get; }
    }
}
