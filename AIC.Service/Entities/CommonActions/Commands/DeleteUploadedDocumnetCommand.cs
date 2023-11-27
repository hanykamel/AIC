using AIC.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIC.Service.Entities.Commands
{
    public class DeleteUploadedDocumnetCommand : IRequest<bool>
    {
        [Required]
        public string RequestCode { get; set; }
        [Required]
        public string FileURL { get; set; }
    }

    public class DeleteUploadedDocumnetCommandHandler : IRequestHandler<DeleteUploadedDocumnetCommand, bool>
    {
        private readonly IUploadedDocumentsBusiness _uploadedDocumentsBusiness;

        public DeleteUploadedDocumnetCommandHandler(IUploadedDocumentsBusiness uploadedDocumentsBusiness)
        {
            _uploadedDocumentsBusiness = uploadedDocumentsBusiness;
        }

        public async Task<bool> Handle(DeleteUploadedDocumnetCommand request, CancellationToken cancellationToken)
        {
            return await _uploadedDocumentsBusiness.DeleteUploadedDocument(request);
        }
    }
}
