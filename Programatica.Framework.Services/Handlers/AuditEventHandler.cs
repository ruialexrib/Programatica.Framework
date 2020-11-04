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

        public void OnAfterCreated(T model)
        { }

        public void OnAfterDeleted(T model)
        { }

        public void OnAfterDestroyed(T model)
        { }

        public void OnBeforeInspecting(T model)
        { }

        public void OnAfterModified(T model)
        { }

        public void OnBeforeCreating(T model)
        { }

        public void OnBeforeDeleting(T model)
        { }

        public void OnBeforeDestroying(T model)
        { }

        public void OnBeforeModifying(T model)
        { }

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

        public async Task OnAfterCreatedAsync(T model)
        {
            await Task.Delay(5);
            var audit = await CreateAudit(model, "Create");
            await CreateTrackChanges(false, model, audit.Id);
        }

        public async Task OnBeforeModifyingAsync(T model)
        {
            var audit = await CreateAudit(model, "Modify");
            await CreateTrackChanges(true, model, audit.Id);
        }

        public async Task OnBeforeDestroyingAsync(T model)
        {
            var audit = await CreateAudit(model, "Destroy");
            await CreateTrackChanges(true, model, audit.Id);
        }

        public async Task OnAfterDeletedAsync(T model)
        {
            await CreateAudit(model, "Deleted");
        }

        public async Task OnBeforeInspectingAsync(T model)
        {
            await CreateAudit(model, "Inspect");
        }

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
