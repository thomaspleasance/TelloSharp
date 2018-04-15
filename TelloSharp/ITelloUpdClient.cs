using System;
using System.Threading.Tasks;

namespace TelloSharp
{
    internal interface ITelloUpdClient : IDisposable
    {
        Task<bool> SendActionAsync(string value);
        Task<string> SendAsync(string value);
    }
}