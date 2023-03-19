using Programatica.Framework.Core.Adapter;
using Programatica.Framework.Data.Models;
using Programatica.Framework.Data.Repository;
using Programatica.Framework.Services.Handlers;
using System.Collections.Generic;

namespace Programatica.Framework.Services.Injector
{
    /// <summary>
    /// A dependency injection container for objects of type T that implement the IModel interface.
    /// </summary>
    /// <typeparam name="T">The type of object that implements the IModel interface.</typeparam>
    public class Injector<T> : IInjector<T>
        where T : IModel
    {
        /// <summary>
        /// The repository for objects of type T.
        /// </summary>
        public IRepository<T> TRepository { get; set; }

        /// <summary>
        /// An adapter for handling date and time functionality.
        /// </summary>
        public IDateTimeAdapter DateTimeAdapter { get; set; }

        /// <summary>
        /// An adapter for handling authentication and authorization functionality.
        /// </summary>
        public IAuthUserAdapter AuthUserAdapter { get; set; }

        /// <summary>
        /// An adapter for serializing and deserializing JSON data.
        /// </summary>
        public IJsonSerializerAdapter JsonSerializerAdapter { get; set; }

        /// <summary>
        /// A list of event handlers that will be triggered during CRUD operations.
        /// </summary>
        public IList<IEventHandler<T>> EventHandlers { get; set; }

        /// <summary>
        /// Initializes a new instance of the Injector class with the specified dependencies.
        /// </summary>
        /// <param name="tRepository">The repository for objects of type T.</param>
        /// <param name="dateTimeAdapter">An adapter for handling date and time functionality.</param>
        /// <param name="authUserAdapter">An adapter for handling authentication and authorization functionality.</param>
        /// <param name="jsonSerializerAdapter">An adapter for serializing and deserializing JSON data.</param>
        /// <param name="eventHandlers">A list of event handlers that will be triggered during CRUD operations.</param>
        public Injector(IRepository<T> tRepository,
            IDateTimeAdapter dateTimeAdapter,
            IAuthUserAdapter authUserAdapter,
            IJsonSerializerAdapter jsonSerializerAdapter,
            IList<IEventHandler<T>> eventHandlers) : base()
        {
            TRepository = tRepository;
            DateTimeAdapter = dateTimeAdapter;
            AuthUserAdapter = authUserAdapter;
            JsonSerializerAdapter = jsonSerializerAdapter;
            EventHandlers = eventHandlers;
        }
    }
}
