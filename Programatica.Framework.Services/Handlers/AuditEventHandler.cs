using Programatica.Framework.Core.Adapter;
using Programatica.Framework.Data.Extensions;
using Programatica.Framework.Data.Models;
using Programatica.Framework.Data.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Programatica.Framework.Services.Handlers
{
    public class AuditEventHandler<T> : IEventHandler<T>
        where T : IModel
    {
        private readonly IRepository<Audit> _auditRepository;
        private readonly IDateTimeAdapter _dateTimeAdapter;
        private readonly IAuthUserAdapter _authUserAdapter;
        private readonly IRepository<T> _modelRepository;
        private readonly IService<TrackChange> _trackChangesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditEventHandler{T}"/> class with the provided dependencies.
        /// </summary>
        /// <param name="auditRepository">The repository for storing <see cref="Audit"/> entities.</param>
        /// <param name="dateTimeAdapter">The adapter for getting the current UTC date and time.</param>
        /// <param name="authUserAdapter">The adapter for getting the current authenticated user name.</param>
        /// <param name="modelRepository">The repository for the <typeparamref name="T"/> entities being audited.</param>
        /// <param name="trackChangesService">The service for creating and storing <see cref="TrackChange"/> entities.</param>
        public AuditEventHandler(
            IRepository<Audit> auditRepository,
            IDateTimeAdapter dateTimeAdapter,
            IAuthUserAdapter authUserAdapter,
            IRepository<T> modelRepository,
            IService<TrackChange> trackChangesService)
        {
            _auditRepository = auditRepository;
            _dateTimeAdapter = dateTimeAdapter;
            _authUserAdapter = authUserAdapter;
            _modelRepository = modelRepository;
            _trackChangesService = trackChangesService;
        }

        #region unused events

        public Task OnBeforeCreatingAsync(T model)
        {
            return Task.CompletedTask;
        }

        public Task OnAfterModifiedAsync(T model)
        {
            return Task.CompletedTask;
        }

        public Task OnAfterDestroyedAsync(T model)
        {
            return Task.CompletedTask;
        }

        public Task OnBeforeDeletingAsync(T model)
        {
            return Task.CompletedTask;
        }

        #endregion

        /// <summary>
        /// Creates an audit record and track changes for a newly created entity.
        /// </summary>
        /// <param name="model">The newly created entity.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task OnAfterCreatedAsync(T model)
        {
            var audit = await CreateAudit(model, "Create");
            await CreateTrackChanges(false, model, audit.Id);
        }

        /// <summary>
        /// Creates an audit record and track changes before modifying an entity.
        /// </summary>
        /// <param name="model">The entity to be modified.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task OnBeforeModifyingAsync(T model)
        {
            var audit = await CreateAudit(model, "Modify");
            await CreateTrackChanges(true, model, audit.Id);
        }

        /// <summary>
        /// Creates an audit before destroying the given entity and also creates track changes for the entity being destroyed.
        /// </summary>
        /// <param name="model">The entity to be destroyed.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the 
        public async Task OnBeforeDestroyingAsync(T model)
        {
            var audit = await CreateAudit(model, "Destroy");
            await CreateTrackChanges(true, model, audit.Id);
        }

        /// <summary>
        /// Creates an audit entry for a model that has been deleted.
        /// </summary>
        /// <param name="model">The model that was deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task OnAfterDeletedAsync(T model)
        {
            await CreateAudit(model, "Deleted");
        }

        /// <summary>
        /// Creates an audit record for inspecting the given entity.
        /// </summary>
        /// <param name="model">The entity to inspect.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task OnBeforeInspectingAsync(T model)
        {
            await CreateAudit(model, "Inspect");
        }

        /// <summary>
        /// Creates an audit record for a given entity.
        /// </summary>
        /// <param name="model">The entity to create an audit record for.</param>
        /// <param name="functionTypeDefinitionProvider">The type definition of the function that caused the audit.</param>
        /// <returns>An <see cref="Audit"/> representing the created audit record.</returns>
        private async Task<Audit> CreateAudit(T model, string functionTypeDefinitionProvider)
        {
            var audit = new Audit
            {
                ContentSystemId = model.SystemId,
                ContentId = model.Id,
                ContentType = model.GetProxyType().Name,
                ContentFunction = functionTypeDefinitionProvider,

                CreatedDate = _dateTimeAdapter.UtcNow,
                CreatedUser = _authUserAdapter.Name,

                IsSystem = true
            };
            await _auditRepository.InsertAsync(audit);
            return audit;
        }

        /// <summary>
        /// Creates a list of variances between a new <see cref="IModel"/> and an old one.
        /// </summary>
        /// <param name="isModify">A boolean indicating if the <see cref="IModel"/> is being modified.</param>
        /// <param name="model">The <see cref="IModel"/> to compare against.</param>
        /// <param name="auditId">The audit ID for the changes.</param>
        /// <returns>A <see cref="Task{TResult}"/> of type <see cref="List{Variance}"/> representing the variances.</returns>
        /// <remarks>
        /// If <paramref name="isModify"/> is <c>true</c>, retrieves the old <see cref="IModel"/> using the ID from the new one.
        /// Then compares the old and new <see cref="IModel"/> using the <see cref="IModel.TrackChanges"/> method.
        /// Creates a <see cref="TrackChange"/> object for each change found and saves it in the database using the <see cref="ITrackChangesService.CreateAsync"/> method.
        /// Finally, returns a list of the <see cref="Variance"/> objects representing the changes.
        /// </remarks>
        private async Task<List<Variance>> CreateTrackChanges(bool isModify, IModel model, int auditId)
        {
            var old = Activator.CreateInstance<T>();
            old.SystemId = Guid.Empty;

            if (isModify)
            {
                old = await _modelRepository.GetDataAsync(model.Id);
            }
            var changes = model.TrackChanges(old);

            foreach (var change in changes)
            {
                await _trackChangesService.CreateAsync(new TrackChange
                {
                    AuditId = auditId,
                    FieldName = change.Prop,
                    OldValue = change.valB.ToString(),
                    NewValue = change.valA.ToString()
                });
            }
            return changes;
        }
    }
}
