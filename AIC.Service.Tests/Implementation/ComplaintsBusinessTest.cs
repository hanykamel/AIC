using AIC.CrossCutting.Identity;
using AIC.CrossCutting.MailService;
using AIC.Data.Entities;
using AIC.Data.ViewModels.SPViewModels;
using AIC.Repository;
using AIC.Service.Implementation;
using AIC.Service.Interfaces;
using AIC.SP.Middleware.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Moq;
using AIC.Service.Tests.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using AIC.Service.Entities.ComplaintRequest.Commands;
using System.Threading.Tasks;
using AIC.CrossCutting.ExceptionHandling;

namespace AIC.Service.Tests.Implementation
{
    public class ComplaintsBusinessTest
    {
        //arrange 
        IRepository<ComplaintRequests, Guid> mockDb = new Mock<IRepository<ComplaintRequests, Guid>>().Object;
        IRepository<ComplaintType, int> mockDb1 = new Mock<IRepository<ComplaintType, int>>().Object;
        IRepository<CitizenRequestsLogs, Guid> mockDb2 = new Mock<IRepository<CitizenRequestsLogs, Guid>>().Object;
        IRepository<Steps, int> mockDb3 = new Mock<IRepository<Steps, int>>().Object;
        IRepository<StepStatues, int> mockDb4 = new Mock<IRepository<StepStatues, int>>().Object;
        IRepository<ComplaintRequestTypes, int> mockDb5 = new Mock<IRepository<ComplaintRequestTypes, int>>().Object;
        //var mockDb6 = new Mock<UserManager<ApplicationUser> >(null).Object;
        IMapper mockDb7 = Mock.Of<IMapper>();
        IEmailService mockDb8 = Mock.Of<IEmailService>();
        IConfiguration mockDb9 = Mock.Of<IConfiguration>();
        IIdentityProvider mockDb10 = Mock.Of<IIdentityProvider>();
        IService<DocumentsViewModel> mockDb11 = Mock.Of<IService<DocumentsViewModel>>();
        ISendEmail mockSendEmail = Mock.Of<ISendEmail>();
        ComplaintsRequestsBusiness complaintsRequestsBusiness;
        public ComplaintsBusinessTest()
        {
            complaintsRequestsBusiness = new ComplaintsRequestsBusiness(
         mockDb, mockDb1, mockDb7,
         mockDb2, mockDb3, mockDb4,
        null, mockDb8, mockDb9, mockDb10, mockDb11, null, mockDb5, Mock.Of<ICitizenBusiness>(), mockSendEmail,null);
        }

        [Fact]
        public void ComplaintsRequestsBusiness_CreateRequestCode_ReturnEmptyFalse()
        {
            //act
            var result = complaintsRequestsBusiness.CreateRequestCode();

            //asserts
            Assert.False(string.IsNullOrEmpty(result));
        }

        [Fact]
        public void ComplaintsRequestsBusiness_CreateRequestCode_Length15_ReturnTrue()
        {
            //act
            var result = complaintsRequestsBusiness.CreateRequestCode();

            //asserts
            Assert.True(result.Length == 15);
        }

        [Fact]
        public void ComplaintsRequestsBusiness_CreateRequestCode_StartWithCZ_ReturnTrue()
        {
            //act
            var result = complaintsRequestsBusiness.CreateRequestCode();

            //assert
            Assert.StartsWith("CZ", result);
        }

        [Fact]
        public void ComplaintsRequestsBusiness_CreateRequestCode_EndsWith10Digits_ReturnTrue()
        {
            //act
            var result = complaintsRequestsBusiness.CreateRequestCode().Substring(5);

            //assert
            Assert.True(result.Length == 10);
        }

        [Fact]
        public void ComplaintsRequestsBusiness_CreateRequestCode_StartWithCZAndCOMForComplaints_ReturnTrue()
        {
            //act
            var result = complaintsRequestsBusiness.CreateRequestCode().Substring(2);

            //assert
            Assert.StartsWith("COM", result);
        }

        [Fact]
        public void ComplaintsRequestsBusiness_SubmitComplaintRequest_SendEmptyDTO_ReturnFalse()
        {
            //arrange
            AddComplaintCommand complaintRequestDTO = new AddComplaintCommand();
            bool result;
            //act
            if (string.IsNullOrEmpty(complaintRequestDTO.DefendantEmployer)
             || string.IsNullOrEmpty(complaintRequestDTO.ComplainantEmployer)
             || string.IsNullOrEmpty(complaintRequestDTO.ComplaintContent)
             || complaintRequestDTO.ComplaintTypesIds.Count == 0)
                result = true;
            else
                result = false;

            //assert
            Assert.False(!result);
        }

        [Fact]
        public void ComplaintsRequestsBusiness_SubmitComplaintRequest_SendValidDTO_ReturnTrue()
        {
            //arrange
            List<string> complaintTypes = new List<string>();
            complaintTypes.Add("1");
            AddComplaintCommand complaintRequestDTO = new AddComplaintCommand()
            {
                DefendantEmployer = "yousef",
                ComplainantEmployer = "ashraf",
                ComplaintContent = "shakwa 1",
                ComplaintTypesIds = complaintTypes,
            };
            bool result;

            //act
            if (string.IsNullOrEmpty(complaintRequestDTO.DefendantEmployer)
             || string.IsNullOrEmpty(complaintRequestDTO.ComplainantEmployer)
             || string.IsNullOrEmpty(complaintRequestDTO.ComplaintContent)
             || complaintRequestDTO.ComplaintTypesIds.Count == 0)
                result = true;
            else
                result = false;

            //assert
            Assert.True(!result);
        }

        [Fact]
        public void ComplaintsRequestsBusiness_SaveComplaintRequest_SendEmptyDTO_ReturnTrue()
        {
            //arrange
            AddComplaintCommand complaintRequestDTO = new AddComplaintCommand();
            bool result;

            //act
            if (string.IsNullOrEmpty(complaintRequestDTO.DefendantEmployer)
             || string.IsNullOrEmpty(complaintRequestDTO.ComplainantEmployer)
             || string.IsNullOrEmpty(complaintRequestDTO.ComplaintContent)
             || complaintRequestDTO.ComplaintTypesIds.Count == 0)
                result = true;
            else
                result = false;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void ComplaintRequestsBusiness_ValidateAdminActions_ComplaintNotFoundException()
        {
            //arrange
            ComplaintRequests complaintRequests = new ComplaintRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException complaintNotFoundException = Assert.Throws<NotFoundException>(() => complaintsRequestsBusiness.ValidateComplaintAdminActions(complaintRequests.Id));
            Assert.Equal("ComplaintNotFound", complaintNotFoundException.Message);
        }

        [Fact]
        public void ComplaintRequestsBusiness_ValidateRejectComplaint_ComplaintNotAllowedException()
        {
            //arrange
            ComplaintRequests complaintRequests = new ComplaintRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException allowedRejectedComplaint = Assert.Throws<NotFoundException>(() => complaintsRequestsBusiness.AvailableComplaintToBeRejected(complaintRequests));
            Assert.Equal("NotAllowedRejectComplaint", allowedRejectedComplaint.Message);
        }
        [Fact]
        public void ComplaintRequestsBusiness_ValidateAcceptComplaint_ComplaintNotAllowedException()
        {
            //arrange
            ComplaintRequests complaintRequests = new ComplaintRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException allowedAcceptedComplaint = Assert.Throws<NotFoundException>(() => complaintsRequestsBusiness.AvailableComplaintToBeAccepted(complaintRequests));
            Assert.Equal("NotAllowedAcceptComplaint", allowedAcceptedComplaint.Message);
        }
        [Fact]
        public void ComplaintRequestsBusiness_ValidateCompleteComplaint_ComplaintNotAllowedException()
        {
            //arrange
            ComplaintRequests complaintRequests = new ComplaintRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException allowedCompletedComplaint = Assert.Throws<NotFoundException>(() => complaintsRequestsBusiness.AvailableComplaintToBeCompleted(complaintRequests));
            Assert.Equal("NotAllowedCompleteComplaint", allowedCompletedComplaint.Message);
        }
    }
}
