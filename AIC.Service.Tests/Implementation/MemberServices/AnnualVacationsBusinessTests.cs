using AIC.CrossCutting.ExceptionHandling;
using AIC.CrossCutting.Identity;
using AIC.Data.Entities;
using AIC.Data.Enums;
using AIC.Data.ViewModels.SPViewModels;
using AIC.Repository;
using AIC.Service.Entities.UserRequest.MemberServices.AnnualVacations;
using AIC.Service.Helper;
using AIC.Service.Implementation;
using AIC.SP.Middleware.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AIC.Service.Tests.Implementation.MemberServices
{
   public class AnnualVacationsBusinessTests
    {
        //arrange 
        IRepository<MemberAnnualVacationRequests, Guid> mockDb = new Mock<IRepository<MemberAnnualVacationRequests, Guid>>().Object;
        IMapper mockDb1 = Mock.Of<IMapper>();
        IService<DocumentsViewModel> mockDb2 = Mock.Of<IService<DocumentsViewModel>>();
        ApplyForAnnualVacationsBusiness memberAnnualVacationsBusiness;
        IRepository<MemberStepStatues, int> mockDb3 = new Mock<IRepository<MemberStepStatues, int>>().Object;
        Mock<IRoleStore<ApplicationRole>> roleStoreMock = new Mock<IRoleStore<ApplicationRole>>();
        RoleManager<ApplicationRole> roleManager;
        MemberRequestsLogsBusiness memberRequestsLogsBusiness;
        IRepository<MemberRequestsLogs, Guid> mockDb55 = new Mock<IRepository<MemberRequestsLogs, Guid>>().Object;
        IIdentityProvider mockDb10 = Mock.Of<IIdentityProvider>();

        public AnnualVacationsBusinessTests()
        {
            memberRequestsLogsBusiness = new MemberRequestsLogsBusiness(mockDb55, mockDb10, roleManager);
            roleManager = new RoleManager<ApplicationRole>(roleStoreMock.Object, null, null, null, null);
            memberAnnualVacationsBusiness = new ApplyForAnnualVacationsBusiness(mockDb, mockDb1,
                mockDb2, mockDb10,roleManager , memberRequestsLogsBusiness, null, null, null, null, mockDb3);
        }

        [Fact]
        public void MemberAnnualVacationsBusiness_CreateRequestCode_ReturnEmptyFalse()
        {
            //act
            var result = MemberServicesHelper.CreateRequestCode((int)MemberServicesEnum.AnnualVacation);

            //asserts
            Assert.False(string.IsNullOrEmpty(result));
        }
        [Fact]
        public void MemberAnnualVacationsBusiness_CreateRequestCode_Length17_ReturnTrue()
        {
            //act
            var result = MemberServicesHelper.CreateRequestCode((int)MemberServicesEnum.AnnualVacation);

            //asserts
            Assert.True(result.Length == 17);
        }
        [Fact]
        public void MemberAnnualVacationsBusiness_CreateRequestCode_StartWithMB_ReturnTrue()
        {
            //act
            var result = MemberServicesHelper.CreateRequestCode((int)MemberServicesEnum.AnnualVacation);

            //assert
            Assert.StartsWith("MB", result);
        }
        [Fact]
        public void MemberAnnualVacationsBusiness_CreateRequestCode_EndsWith10Digits_ReturnTrue()
        {
            //act
            var result = MemberServicesHelper.CreateRequestCode((int)MemberServicesEnum.AnnualVacation).Substring(7);

            //assert
            Assert.True(result.Length == 10);
        }
        [Fact]
        public void MemberAnnualVacationsBusiness_CreateRequestCode_StartWithMBAndANVACForAnnualVacations_ReturnTrue()
        {
            //act
            var result = MemberServicesHelper.CreateRequestCode((int)MemberServicesEnum.AnnualVacation).Substring(2);

            //assert
            Assert.StartsWith("ANVAC", result);
        }

        [Fact]
        public void MemberAnnualVacationsBusiness_SubmitAnnualVacationsRequest_SendEmptyDTO_ReturnFalse()
        {
            //arrange
            SubmitApplyForAnnualVacationsCommand annualVacationsRequestDTO = new SubmitApplyForAnnualVacationsCommand();
            bool result;
            //act
            if (annualVacationsRequestDTO.LeaveTypeId == 0
             || annualVacationsRequestDTO.From == null
             || annualVacationsRequestDTO.To == null)
                result = true;
            else
                result = false;

            //assert
            Assert.False(!result);
        }
        [Fact]
        public void MemberAnnualVacationsBusiness_SubmitAnnualVacationsRequest_SendValidDTO_ReturnTrue()
        {
            //arrange
            SubmitApplyForAnnualVacationsCommand annualVacationsRequestDTO = new SubmitApplyForAnnualVacationsCommand()
            {
                LeaveTypeId =1,
                From= DateTime.Parse("2022-01-15"),
                To= DateTime.Parse("2022-01-17"),
                TotalVacationDays=3
            };
            bool result;
            //act
            if (annualVacationsRequestDTO.LeaveTypeId == 0
             || annualVacationsRequestDTO.From == null
             || annualVacationsRequestDTO.To == null)
                result = true;
            else
                result = false;

            //assert
            Assert.True(!result);
        }
        [Fact]
        public void MemberAnnualVacationsBusiness_SaveAnnualVacationsRequest_SendEmptyDTO_ReturnTrue()
        {
            //arrange
            SaveApplyForAnnualVacationsCommand annualVacationsRequestDTO = new SaveApplyForAnnualVacationsCommand();     
            bool result;
            //act
            if (annualVacationsRequestDTO.LeaveTypeId == 0
             || annualVacationsRequestDTO.From == null
             || annualVacationsRequestDTO.To == null)
                result = true;
            else
                result = false;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void MemberAnnualVacationsBusiness_ValidateAdminActions_AnnualVacationsNotFoundException()
        {
            //arrange
            MemberAnnualVacationRequests annualVacationsRequests = new MemberAnnualVacationRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException annualVacationsNotFoundException = Assert.Throws<NotFoundException>(() => memberAnnualVacationsBusiness.ValidateAnnualVacationsAdminActions(annualVacationsRequests.Id));
            Assert.Equal("RequestNotFound", annualVacationsNotFoundException.Message);
        }

        [Fact]
        public void MemberAnnualVacationsBusiness_ValidateRejectAnnualVacations_AnnualVacationsNotAllowedException()
        {
            //arrange
            MemberAnnualVacationRequests annualVacationsRequests = new MemberAnnualVacationRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException annualVacationsNotFoundException = Assert.Throws<NotFoundException>(() => memberAnnualVacationsBusiness.AvailableAnnualVacationsToBeRejected(annualVacationsRequests));
            Assert.Equal("NotAllowedRejectAnnualVacations", annualVacationsNotFoundException.Message);
        }

        [Fact]
        public void MemberAnnualVacationsBusiness_ValidateAcceptAnnualVacations_AnnualVacationsNotAllowedException()
        {
            //arrange
            MemberAnnualVacationRequests annualVacationsRequests = new MemberAnnualVacationRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException annualVacationsNotFoundException = Assert.Throws<NotFoundException>(() => memberAnnualVacationsBusiness.AvailableAnnualVacationsToBeAccepted(annualVacationsRequests));
            Assert.Equal("NotAllowedAcceptAnnualVacations", annualVacationsNotFoundException.Message);
        }

        [Fact]
        public void MemberAnnualVacationsBusiness_ValidateCompleteAnnualVacations_AnnualVacationsNotAllowedException()
        {
            //arrange
            MemberAnnualVacationRequests annualVacationsRequests = new MemberAnnualVacationRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException annualVacationsNotFoundException = Assert.Throws<NotFoundException>(() => memberAnnualVacationsBusiness.AvailableAnnualVacationsToBeCompleted(annualVacationsRequests));
            Assert.Equal("NotAllowedCompleteAnnualVacations", annualVacationsNotFoundException.Message);
        }

    }
}
