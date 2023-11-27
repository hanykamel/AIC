using AIC.CrossCutting.ExceptionHandling;
using AIC.Repository;
using AIC.Service.Entities.Commands;
using AIC.Service.Interfaces;
using AIC.SP.Middleware;
using AIC.SP.Middleware.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AIC.CrossCutting.Constant.Constant;

namespace AIC.Service.Implementation
{
    public class UploadedDocumentsBusiness : IUploadedDocumentsBusiness
    {

        private readonly IService<DocumentsViewModel> _documentsService;
      
        public UploadedDocumentsBusiness(IService<DocumentsViewModel> documentsService
         
            )
        {
            _documentsService = documentsService;
        
        }
        public async Task<bool> DeleteUploadedDocument(DeleteUploadedDocumnetCommand deleteUploadedDocumnetCommand)
        {
            string ServiceType = deleteUploadedDocumnetCommand.RequestCode.Substring(0,2);

            //switch (ServiceType)
            //{
            //    case UserCategoriesCode.Member:
            //        await DeleteMemberServiceDocument(deleteUploadedDocumnetCommand);
            //        break;
            //    case UserCategoriesCode.Citizen:
            //        await DeleteCitizenServiceDocument(deleteUploadedDocumnetCommand);
            //        break;
            //    default:
            //        break;
            //}

            return true;
        }

    }
}
