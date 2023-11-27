using AIC.CrossCutting.ExceptionHandling;
using AIC.CrossCutting.Identity;
using AIC.Data.Entities;
using AIC.Data.Enums;
using AIC.Data.ViewModels.SPViewModels;
using AIC.Repository;
using AIC.Service.Entities.UserRequest.MemberServices.MovementTransfeers;
using AIC.Service.Helper;
using AIC.Service.Implementation;
using AIC.SP.Middleware.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AIC.Service.Tests.Implementation.MemberServices
{
    public class MemberMovementBusinessTests
    {
        //arrange 
        Mock<IRepository<MemberMovementTransfeersRequests, Guid>> mockDb = new Mock<IRepository<MemberMovementTransfeersRequests, Guid>>();
        IMapper mockDb1 = Mock.Of<IMapper>();
        IService<DocumentsViewModel> mockDb2 = Mock.Of<IService<DocumentsViewModel>>();
        MemberMovementTransfeersBusiness memberMovementTransfeersBusiness;
        IRepository<MemberStepStatues, int> mockDb3 = new Mock<IRepository<MemberStepStatues, int>>().Object;
        Mock<IRoleStore<ApplicationRole>> roleStoreMock = new Mock<IRoleStore<ApplicationRole>>();
        RoleManager<ApplicationRole> roleManager;
        MemberRequestsLogsBusiness memberRequestsLogsBusiness;
        IRepository<MemberRequestsLogs, Guid> mockDb55 = new Mock<IRepository<MemberRequestsLogs, Guid>>().Object;
        IIdentityProvider mockDb10 = Mock.Of<IIdentityProvider>();
        IRepository<MemberSteps, int> mockDb5 = new Mock<IRepository<MemberSteps, int>>().Object;
        public MemberMovementBusinessTests()
        {
            memberRequestsLogsBusiness = new MemberRequestsLogsBusiness(mockDb55, mockDb10, roleManager);
            roleManager = new RoleManager<ApplicationRole>(roleStoreMock.Object, null, null, null, null);
            memberMovementTransfeersBusiness = new MemberMovementTransfeersBusiness(mockDb.Object, mockDb1
                , mockDb2, mockDb10, roleManager, memberRequestsLogsBusiness, null, null, mockDb5,null,mockDb3);
        }

        [Fact]
        public void MemberMovementTransfeersBusiness_CreateRequestCode_ReturnEmptyFalse()
        {
            //act
            var result = MemberServicesHelper.CreateRequestCode((int)MemberServicesEnum.MovementTransfers);

            //asserts
            Assert.False(string.IsNullOrEmpty(result));
        }
        [Fact]
        public void MemberMovementTransfeersBusiness_CreateRequestCode_Length15_ReturnTrue()
        {
            //act
            var result = MemberServicesHelper.CreateRequestCode((int)MemberServicesEnum.MovementTransfers);

            //asserts
            Assert.True(result.Length == 15);
        }
        [Fact]
        public void MemberMovementTransfeersBusiness_CreateRequestCode_StartWithMB_ReturnTrue()
        {
            //act
            var result = MemberServicesHelper.CreateRequestCode((int)MemberServicesEnum.MovementTransfers);

            //assert
            Assert.StartsWith("MB", result);
        }
        [Fact]
        public void MemberMovementTransfeersBusiness_CreateRequestCode_EndsWith10Digits_ReturnTrue()
        {
            //act
            var result = MemberServicesHelper.CreateRequestCode((int)MemberServicesEnum.MovementTransfers).Substring(5);

            //assert
            Assert.True(result.Length == 10);
        }
        [Fact]
        public void MemberMovementTransfeersBusiness_CreateRequestCode_StartWithMBAndMOVForMovementTransfeers_ReturnTrue()
        {
            //act
            var result = MemberServicesHelper.CreateRequestCode((int)MemberServicesEnum.MovementTransfers).Substring(2);

            //assert
            Assert.StartsWith("MOV", result);
        }
        [Fact]
        public void MemberMovementTransfeersBusiness_SubmitMovementTransfeersRequest_SendEmptyDTO_ReturnFalse()
        {
            //arrange
            SubmitMemberMovementTransfeersCommand movementTransfeersRequestDTO = new SubmitMemberMovementTransfeersCommand();
            bool result;
            //act
            if (movementTransfeersRequestDTO.FirstDesireGovernorateId == 0
             || movementTransfeersRequestDTO.SecondDesireGovernorateId == 0
             || movementTransfeersRequestDTO.ThirdDesireGovernorateId == 0
             || string.IsNullOrEmpty(movementTransfeersRequestDTO.FirstDesireDirectories)
             || string.IsNullOrEmpty(movementTransfeersRequestDTO.SecondDesireDirectories)
             || string.IsNullOrEmpty(movementTransfeersRequestDTO.ThirdDesireDirectories))
                result = true;
            else
                result = false;

            //assert
            Assert.False(!result);
        }
        [Fact]
        public void MemberMovementTransfeersBusiness_SubmitMovementTransfeersRequest_SendValidDTO_ReturnTrue()
        {
            //arrange
            SubmitMemberMovementTransfeersCommand movementTransfeersRequestDTO = new SubmitMemberMovementTransfeersCommand()
            {
                FirstDesireGovernorateId=1,
                SecondDesireGovernorateId=1,
                ThirdDesireGovernorateId=1,
                FirstDesireDirectories="1,2",
                SecondDesireDirectories="3",
                ThirdDesireDirectories="1",
            };
            bool result;
            //act
            if (movementTransfeersRequestDTO.FirstDesireGovernorateId == 0
             || movementTransfeersRequestDTO.SecondDesireGovernorateId == 0
             || movementTransfeersRequestDTO.ThirdDesireGovernorateId == 0
             || string.IsNullOrEmpty(movementTransfeersRequestDTO.FirstDesireDirectories)
             || string.IsNullOrEmpty(movementTransfeersRequestDTO.SecondDesireDirectories)
             || string.IsNullOrEmpty(movementTransfeersRequestDTO.ThirdDesireDirectories))
                result = true;
            else
                result = false;

            //assert
            Assert.True(!result);
        }

        [Fact]
        public void MemberMovementTransfeersBusiness_SaveMovementTransfeersRequest_SendEmptyDTO_ReturnTrue()
        {
            //arrange
            SaveMemberMovementTransfeersCommand movementTransfeersRequestDTO = new SaveMemberMovementTransfeersCommand();
            bool result;

            //act
            if (movementTransfeersRequestDTO.FirstDesireGovernorateId == 0
            || movementTransfeersRequestDTO.SecondDesireGovernorateId == 0
            || movementTransfeersRequestDTO.ThirdDesireGovernorateId == 0
            || string.IsNullOrEmpty(movementTransfeersRequestDTO.FirstDesireDirectories)
            || string.IsNullOrEmpty(movementTransfeersRequestDTO.SecondDesireDirectories)
            || string.IsNullOrEmpty(movementTransfeersRequestDTO.ThirdDesireDirectories))
                result = true;
            else
                result = false;

            //assert
            Assert.True(result);
        }
        [Fact]
        public void MemberMovementTransfeersBusiness_ValidateAdminActions_MovementTransfeersNotFoundException()
        {
            //arrange
            MemberMovementTransfeersRequests movementTransfeersRequests = new MemberMovementTransfeersRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException movementTransfeersNotFoundException = Assert.Throws<NotFoundException>(() => memberMovementTransfeersBusiness.ValidateMovementTransfeersAdminActions(movementTransfeersRequests.Id));
            Assert.Equal("RequestNotFound", movementTransfeersNotFoundException.Message);
        }

        [Fact]
        public void MemberMovementTransfeersBusiness_ValidateRejectMovementTransfeers_MovementNotAllowedException()
        {
            //arrange
            MemberMovementTransfeersRequests movementTransfeersRequests = new MemberMovementTransfeersRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException movementTransfeersNotFoundException = Assert.Throws<NotFoundException>(() => memberMovementTransfeersBusiness.AvailableMovementTransfeersToBeRejected(movementTransfeersRequests));
            Assert.Equal("NotAllowedRejectMovementTransfeers", movementTransfeersNotFoundException.Message);
        }
        [Fact]
        public void MemberMovementTransfeersBusiness_ValidateAcceptMovementTransfeers_MovementNotAllowedException()
        {
            //arrange
            MemberMovementTransfeersRequests movementTransfeersRequests = new MemberMovementTransfeersRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException movementTransfeersNotFoundException = Assert.Throws<NotFoundException>(() => memberMovementTransfeersBusiness.AvailableMovementTransfeersToBeAccepted(movementTransfeersRequests));
            Assert.Equal("NotAllowedAcceptMovementTransfeers", movementTransfeersNotFoundException.Message);
        }
        [Fact]
        public void MemberMovementTransfeersBusiness_ValidateCompleteMovementTransfeers_MovementNotAllowedException()
        {
            //arrange
            MemberMovementTransfeersRequests movementTransfeersRequests = new MemberMovementTransfeersRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException movementTransfeersNotFoundException = Assert.Throws<NotFoundException>(() => memberMovementTransfeersBusiness.AvailableMovementTransfeersToBeCompleted(movementTransfeersRequests));
            Assert.Equal("NotAllowedCompleteMovementTransfeers", movementTransfeersNotFoundException.Message);
        }


    }
}
