using Programatica.Framework.Core;
using Programatica.Framework.Core.Adapter;
using Programatica.Framework.Data.Models;
using Programatica.Framework.Data.Repository;
using Programatica.Framework.Services.Handlers;
using System.Collections.Generic;

namespace Programatica.Framework.Services.Injector
{
    public class Injector<T> : BaseObject, IInjector<T>
        where T : IModel
    {
        public IRepository<T> TRepository { get; set; }
        public IDateTimeAdapter DateTimeAdapter { get; set; }
        public IAuthUserAdapter AuthUserAdapter { get; set; }
        public IJsonSerializerAdapter JsonSerializerAdapter { get; set; }
        public IList<IEventHandler<T>> EventHandlers { get; set; }

        public Injector(IRepository<T> tRepository,
            IDateTimeAdapter dateTimeAdapter,
            IAuthUserAdapter authUserAdapter,
            IJsonSerializerAdapter jsonSerializerAdapter,
            IList<IEventHandler<T>> eventHandlers) : base()
        {
            TRepository = tRepository;
            DateTimeAdapter = dateTimeAdapter;
            AuthUserAdapter = authUserAdapter;
            EventHandlers = eventHandlers;
        }
    }
}
