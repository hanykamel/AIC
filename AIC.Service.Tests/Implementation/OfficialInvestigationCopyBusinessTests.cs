using AIC.CrossCutting.Constant;
using AIC.CrossCutting.ExceptionHandling;
using AIC.CrossCutting.Identity;
using AIC.CrossCutting.MailService;
using AIC.Data.Entities;
using AIC.Data.ViewModels.SPViewModels;
using AIC.Repository;
using AIC.Service.Implementation;
using AIC.Service.Interfaces;
using AIC.SP.Middleware.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AIC.Service.Tests.Implementation
{
    public class OfficialInvestigationCopyBusinessTests
    {
        OfficialInvestigationCopyBusiness officialInvestigationCopyBusiness;
        IMapper mockDb7 = Mock.Of<IMapper>();
        IEmailService mockDb8 = Mock.Of<IEmailService>();
        IConfiguration mockDb9 = Mock.Of<IConfiguration>();
        IIdentityProvider mockDb10 = Mock.Of<IIdentityProvider>();
        IService<DocumentsViewModel> mockDb11 = Mock.Of<IService<DocumentsViewModel>>();
        ISendEmail mockSendEmail = Mock.Of<ISendEmail>();
        IRepository<Governorate, int> mockDb = new Mock<IRepository<Governorate, int>>().Object;
        IRepository<APADirectory, int> mockDb1 = new Mock<IRepository<APADirectory, int>>().Object;
        CitizenRequestsLogsBusiness citizenRequestsLogsBusiness = new CitizenRequestsLogsBusiness(null, null,null);
        IRepository<OfficialInvCopyRequests, Guid> mockDb2 = new Mock<IRepository<OfficialInvCopyRequests, Guid>>().Object;
        IRepository<Steps, int> mockDb4 = new Mock<IRepository<Steps, int>>().Object;
        IRepository<StepStatues, int> mockDb5 = new Mock<IRepository<StepStatues, int>>().Object;
        IRepository<CitizenRequestsLogs, Guid> mockDb6 = new Mock<IRepository<CitizenRequestsLogs, Guid>>().Object;
        IRepository<CitizenService, int> mockDb12 = new Mock<IRepository<CitizenService, int>>().Object;
        public OfficialInvestigationCopyBusinessTests()
        {
            officialInvestigationCopyBusiness = new OfficialInvestigationCopyBusiness(mockDb, mockDb1, mockDb10,null, mockDb2, mockDb4,
                mockDb5, mockDb6, mockDb12, null, mockDb8, mockDb9, mockDb7, null,
                mockDb11, citizenRequestsLogsBusiness, mockSendEmail);
        }

        [Fact]
        public void CreateRequestCode_OfficialInvestigationService_NotNull()
        {
            //arrange

            //act
            var res = officialInvestigationCopyBusiness.CreateRequestCode();

            //assert
            Assert.NotNull(res);
        }

        [Fact]
        public void CreateRequestCode_OfficialInvestigationService_Length15()
        {
            //arrange

            //act
            var res = officialInvestigationCopyBusiness.CreateRequestCode().Length;

            //assert
            Assert.Equal(15, res);
        }

        [Fact]
        public void CreateRequestCode_OfficialInvestigationService_StartByCZ()
        {
            //arrange

            //act
            var res = officialInvestigationCopyBusiness.CreateRequestCode().StartsWith(Constant.UserCategoriesCode.Citizen);

            //assert
            Assert.True(res);
        }

        [Fact]
        public void CreateRequestCode_OfficialInvestigationService_ContainsING()
        {
            //arrange

            //act
            var res = officialInvestigationCopyBusiness.CreateRequestCode().Contains(Constant.CitizenServicesCodes.Investigation);

            //assert
            Assert.True(res);
        }

       
        [Fact]
        public void OfficialInvBusiness_CreateRequestCode_ReturnEmptyFalse()
        {
            //act
            var result = officialInvestigationCopyBusiness.CreateRequestCode();

            //asserts
            Assert.False(string.IsNullOrEmpty(result));
        }
        [Fact]
        public void OfficialInvBusiness_CreateRequestCode_Length15_ReturnTrue()
        {
            //act
            var result = officialInvestigationCopyBusiness.CreateRequestCode();

            //asserts
            Assert.True(result.Length == 15);
        }
        [Fact]
        public void OfficialInvBusiness_CreateRequestCode_StartWithCZ_ReturnTrue()
        {
            //act
            var result = officialInvestigationCopyBusiness.CreateRequestCode();

            //assert
            Assert.StartsWith("CZ", result);
        }
        [Fact]
        public void OfficialInvBusiness_CreateRequestCode_EndsWith10Digits_ReturnTrue()
        {
            //act
            var result = officialInvestigationCopyBusiness.CreateRequestCode().Substring(5);

            //assert
            Assert.True(result.Length == 10);
        }
        [Fact]
        public void OfficialInvBusiness_CreateRequestCode_StartWithCZAndINGForCopyInvestigation_ReturnTrue()
        {
            //act
            var result = officialInvestigationCopyBusiness.CreateRequestCode().Substring(2);

            //assert
            Assert.StartsWith("ING", result);
        }
        [Fact]
        public void OfficialInvBusiness_ValidateAdminActions_OfficialInvestigationsNotFoundException()
        {
            //arrange
            OfficialInvCopyRequests officialInvCopyRequests = new OfficialInvCopyRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException OfficialInvNotFoundException = Assert.Throws<NotFoundException>(() => officialInvestigationCopyBusiness.ValidateOfficialInvAdminActions(officialInvCopyRequests.Id));
            Assert.Equal("OfficialInvestigationNotFound", OfficialInvNotFoundException.Message);
        }
        [Fact]
        public void OfficialInvBusiness_ValidateRejectOfficialInv_OfficialInvNotAllowedException()
        {
            //arrange
            OfficialInvCopyRequests officialInvCopyRequests = new OfficialInvCopyRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException allowedRejectedOfficialInv = Assert.Throws<NotFoundException>(() => officialInvestigationCopyBusiness.AvailableOfficialInvToBeRejected(officialInvCopyRequests));
            Assert.Equal("NotAllowedRejectInvestigationCopy", allowedRejectedOfficialInv.Message);
        }
        [Fact]
        public void OfficialInvBusiness_ValidateAcceptOfficialInv_OfficialInvNotAllowedException()
        {
            //arrange
            OfficialInvCopyRequests officialInvCopyRequests = new OfficialInvCopyRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException allowedAcceptedOfficialInv = Assert.Throws<NotFoundException>(
                () => officialInvestigationCopyBusiness.AvailableOfficialInvToBeAccepted(officialInvCopyRequests));
            Assert.Equal("NotAllowedAcceptInvestigationCopy", allowedAcceptedOfficialInv.Message);
        }
        [Fact]
        public void OfficialInvBusiness_ValidateReturnForCorrectionOfficialInv_OfficialInvNotAllowedException()
        {
            //arrange
            OfficialInvCopyRequests officialInvCopyRequests = new OfficialInvCopyRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException allowedReturnForCorrectionOfficialInv = Assert.Throws<NotFoundException>(() => officialInvestigationCopyBusiness.AvailableOfficialInvToBeReturnForCorrection(officialInvCopyRequests));
            Assert.Equal("NotAllowedReturnForCorrectionInvestigationCopy", allowedReturnForCorrectionOfficialInv.Message);
        }
        [Fact]
        public void OfficialInvBusiness_ValidateCompleteOfficialInv_OfficialInvNotAllowedException()
        {
            //arrange
            OfficialInvCopyRequests officialInvCopyRequests = new OfficialInvCopyRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException allowedCompletedOfficialInv = Assert.Throws<NotFoundException>(() => officialInvestigationCopyBusiness.AvailableOfficialInvToBeComplete(officialInvCopyRequests));
            Assert.Equal("NotAllowedCompleteInvestigationCopy", allowedCompletedOfficialInv.Message);
        }
    }
}
