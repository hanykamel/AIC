using AIC.Service.Entities.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AIC.Service.Interfaces
{
    public interface IUploadedDocumentsBusiness
    {
        Task<bool> DeleteUploadedDocument(DeleteUploadedDocumnetCommand deleteUploadedDocumnetCommand);
    }
}
