using System.Threading.Tasks;
using Palestras.Domain.Core.Commands;
using Palestras.Domain.Core.Events;

namespace Palestras.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}