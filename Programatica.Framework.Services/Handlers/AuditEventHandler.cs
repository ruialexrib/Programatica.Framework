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

        public async void OnAfterCreated(T model)
        {
            var audit = await CreateAudit(model, "Create");
            var changes = await CreateTrackChanges(false, model, audit.Id);
        }

        public void OnAfterDeleted(T model)
        {
            _ = CreateAudit(model, "Deleted");
        }

        public void OnAfterDestroyed(T model)
        { }

        public void OnBeforeInspecting(T model)
        {
            _ = CreateAudit(model, "Inspect");
        }

        public void OnAfterModified(T model)
        { }

        public void OnBeforeCreating(T model)
        { }

        public void OnBeforeDeleting(T model)
        { }

        public void OnBeforeDestroying(T model)
        {
            var audit = CreateAudit(model, "Destroy");
            var changes = CreateTrackChanges(true, model, audit.Id);
        }

        public void OnBeforeModifying(T model)
        {
            var audit = CreateAudit(model, "Modify");
            var changes = CreateTrackChanges(true, model, audit.Id);
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
