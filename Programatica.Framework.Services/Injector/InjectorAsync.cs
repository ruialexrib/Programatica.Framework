using Programatica.Framework.Core.Adapter;
using Programatica.Framework.Data.Models;
using Programatica.Framework.Data.Repository;
using Programatica.Framework.Services.Handlers;
using System.Collections.Generic;

namespace Programatica.Framework.Services.Injector
{
    public class InjectorAsync<T> : IInjectorAsync<T>
        where T : IModel
    {
        public IRepositoryAsync<T> TRepository { get; set; }
        public IDateTimeAdapter DateTimeAdapter { get; set; }
        public IAuthUserAdapter AuthUserAdapter { get; set; }
        public IJsonSerializerAdapter JsonSerializerAdapter { get; set; }
        public IList<IEventHandler<T>> EventHandlers { get; set; }

        public InjectorAsync(IRepositoryAsync<T> tRepository,
            IDateTimeAdapter dateTimeAdapter,
            IAuthUserAdapter authUserAdapter,
            IJsonSerializerAdapter jsonSerializerAdapter,
            IList<IEventHandler<T>> eventHandlers) 
            : base()
        {
            TRepository = tRepository;
            DateTimeAdapter = dateTimeAdapter;
            AuthUserAdapter = authUserAdapter;
            JsonSerializerAdapter = jsonSerializerAdapter;
            EventHandlers = eventHandlers;
        }
    }
}
