using System;
using System.Threading.Tasks;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TelloSharp.UnitTests")]

namespace TelloSharp
{    
    internal interface ITelloUpdClient : IDisposable
    {
        Task<bool> SendActionAsync(string value);
        Task<string> SendAsync(string value);
    }
}