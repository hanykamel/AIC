using AIC.Data.Enums;
using AIC.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Service.Entities.CommonActions.Synchronization
{
    public class SychronizeCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public SynchronizationEnum Type { get; set; }
        public bool IsDeleted { get; set; }
        public string ReferenceNumber { get; set; }

    }

    public class SychronizeCommandHandler : IRequestHandler<SychronizeCommand, bool>
    {
        private readonly ISynchronizationService _synchronizationService;
        public SychronizeCommandHandler(ISynchronizationService synchronizationService)
        {
            _synchronizationService = synchronizationService;
        }
        public async Task<bool> Handle(SychronizeCommand request, CancellationToken cancellationToken)
        {
            return await _synchronizationService.Sychronize(request);
        }
    }
}
