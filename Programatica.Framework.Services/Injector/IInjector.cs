using Programatica.Framework.Core;
using Programatica.Framework.Core.Adapter;
using Programatica.Framework.Data.Models;
using Programatica.Framework.Data.Repository;
using Programatica.Framework.Services.Handlers;
using System.Collections.Generic;

namespace Programatica.Framework.Services.Injector
{
    public interface IInjector<T> 
        where T : IModel
    {
        IRepository<T> TRepository { get; set; }
        IDateTimeAdapter DateTimeAdapter { get; set; }
        IAuthUserAdapter AuthUserAdapter { get; set; }
        IJsonSerializerAdapter JsonSerializerAdapter { get; set; }
        IList<IEventHandler<T>> EventHandlers { get; set; }
    }
}
