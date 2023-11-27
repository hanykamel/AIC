using AIC.CrossCutting.Constant;
using AIC.CrossCutting.ExceptionHandling;
using AIC.CrossCutting.Identity;
using AIC.Data.Entities;
using AIC.Data.Enums;
using AIC.Repository;
using AIC.Service.Implementation;
using AIC.Service.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AIC.Service.Tests.Implementation
{
    public class OutcomeAndPositionJudiciaryBusinessTests
    {
        IIdentityProvider  mockDb = Mock.Of<IIdentityProvider>();
        IRepository<OutcomeAndPositionJudiciaryRequests, Guid> mockDb1 = new Mock<IRepository<OutcomeAndPositionJudiciaryRequests, Guid>>().Object;
        IRepository<APADirectory, int> mockDb2 = new Mock<IRepository<APADirectory, int>>().Object;
        IRepository<Steps, int> mockDb3 = new Mock<IRepository<Steps, int>>().Object;
        IRepository<CitizenService, int> mockDb4 = new Mock<IRepository<CitizenService, int>>().Object;
        IRepository<StepStatues, int> mockDb5 = new Mock<IRepository<StepStatues, int>>().Object;
        ICitizenBusiness mockDb6 = Mock.Of<ICitizenBusiness>();
        ISendEmail mockDb7 = Mock.Of<ISendEmail>();
        ICitizenRequestsLogsBusiness mockDb8 = Mock.Of<ICitizenRequestsLogsBusiness>();

        OutcomeAndPositionJudiciaryBusiness outcomeAndPositionJudiciaryBusiness;

        public OutcomeAndPositionJudiciaryBusinessTests()
        {
            outcomeAndPositionJudiciaryBusiness = new OutcomeAndPositionJudiciaryBusiness(
                mockDb,null,mockDb1,mockDb2,mockDb3,mockDb4
                ,null,null,null,null,null,mockDb5,null,null
                ,mockDb7,mockDb8);
        }


        [Fact]
        public void CreateRequestCode_OutcomService_NotNull()
        {
            //arrange
            int serviceId = (int)CitizenServicesEnum.OutcomeStatement;

            //act
            var res = outcomeAndPositionJudiciaryBusiness.CreateRequestCode(serviceId);

            //assert
            Assert.NotNull(res);
        }

        [Fact]
        public void CreateRequestCode_OutcomService_Length15()
        {
            //arrange
            int serviceId = (int)CitizenServicesEnum.OutcomeStatement;

            //act
            var res = outcomeAndPositionJudiciaryBusiness.CreateRequestCode(serviceId).Length;

            //assert
            Assert.Equal(15, res);
        }

        [Fact]
        public void CreateRequestCode_OutcomService_StartByCZ()
        {
            //arrange
            int serviceId = (int)CitizenServicesEnum.OutcomeStatement;

            //act
            var res = outcomeAndPositionJudiciaryBusiness.CreateRequestCode(serviceId).StartsWith(Constant.UserCategoriesCode.Citizen);

            //assert
            Assert.True(res);
        }

        [Fact]
        public void CreateRequestCode_OutcomService_ContainsOUT()
        {
            //arrange
            int serviceId = (int)CitizenServicesEnum.OutcomeStatement;

            //act
            var res = outcomeAndPositionJudiciaryBusiness.CreateRequestCode(serviceId).Contains(Constant.CitizenServicesCodes.OutCome);

            //assert
            Assert.True(res);
        }

        [Fact]
        public void CreateRequestCode_PositionService_NotNull()
        {
            //arrange
            int serviceId = (int)CitizenServicesEnum.PositionJudiciary;

            //act
            var res = outcomeAndPositionJudiciaryBusiness.CreateRequestCode(serviceId);

            //assert
            Assert.NotNull(res);
        }

        [Fact]
        public void CreateRequestCode_PositionService_Length15()
        {
            //arrange
            int serviceId = (int)CitizenServicesEnum.PositionJudiciary;

            //act
            var res = outcomeAndPositionJudiciaryBusiness.CreateRequestCode(serviceId).Length;

            //assert
            Assert.Equal(15, res);
        }

        [Fact]
        public void CreateRequestCode_PositionService_StartByCZ()
        {
            //arrange
            int serviceId = (int)CitizenServicesEnum.PositionJudiciary;

            //act
            var res = outcomeAndPositionJudiciaryBusiness.CreateRequestCode(serviceId).StartsWith(Constant.UserCategoriesCode.Citizen);

            //assert
            Assert.True(res);
        }

        [Fact]
        public void CreateRequestCode_PositionService_ContainsPOS()
        {
            //arrange
            int serviceId = (int)CitizenServicesEnum.PositionJudiciary;

            //act
            var res = outcomeAndPositionJudiciaryBusiness.CreateRequestCode(serviceId).Contains(Constant.CitizenServicesCodes.Position);

            //assert
            Assert.True(res);
        }
        [Fact]
        public void OutcomeAndPositionBusiness_ValidateAdminActions_OutComeAndPositionNotFoundException()
        {
            //arrange
            OutcomeAndPositionJudiciaryRequests outcomeAndPositionRequest = new OutcomeAndPositionJudiciaryRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException outcomeAndPositionNotFoundException = Assert.Throws<NotFoundException>(() => outcomeAndPositionJudiciaryBusiness.ValidateOutcomeAndPositionAdminActions(outcomeAndPositionRequest.Id));
            Assert.Equal("OutComeAndPositionNotFound", outcomeAndPositionNotFoundException.Message);
        }
        [Fact]
        public void OutcomeAndPositionRequestsBusiness_ValidateRejectOutcomeAndPosition_OutcomeAndPositionNotAllowedException()
        {
            //arrange
            OutcomeAndPositionJudiciaryRequests outcomeAndPositionRequest = new OutcomeAndPositionJudiciaryRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException allowedRejectedOutcomeAndPosition = Assert.Throws<NotFoundException>(() => outcomeAndPositionJudiciaryBusiness.AvailableOutComeAndPositionToBeRejected(outcomeAndPositionRequest));
            Assert.Equal("NotAllowedRejectOutcomeAndPosition", allowedRejectedOutcomeAndPosition.Message);
        }
        [Fact]
        public void OutcomeAndPositionRequestsBusiness_ValidateAcceptOutcomeAndPosition_OutcomeAndPositionNotAllowedException()
        {
            //arrange
            OutcomeAndPositionJudiciaryRequests outcomeAndPositionRequest = new OutcomeAndPositionJudiciaryRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException allowedAcceptedOutcomeAndPosition = Assert.Throws<NotFoundException>(() => outcomeAndPositionJudiciaryBusiness.AvailableOutComeAndPositionToBeAccepted(outcomeAndPositionRequest));
            Assert.Equal("NotAllowedAcceptOutcomeAndPosition", allowedAcceptedOutcomeAndPosition.Message);
        }
        [Fact]
        public void OutcomeAndPositionRequestsBusiness_ValidateReturnForCorrectionOutcomeAndPosition_OutcomeAndPositionNotAllowedException()
        {
            //arrange
            OutcomeAndPositionJudiciaryRequests outcomeAndPositionRequest = new OutcomeAndPositionJudiciaryRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException allowedReturnedForCorrectionOutcomeAndPosition = Assert.Throws<NotFoundException>(() => outcomeAndPositionJudiciaryBusiness.AvailableOutComeAndPositionToBeReturnnForCorrection(outcomeAndPositionRequest));
            Assert.Equal("NotAllowedReturnForCorrectionOutcomeAndPosition", allowedReturnedForCorrectionOutcomeAndPosition.Message);
        }
        [Fact]
        public void OutcomeAndPositionRequestsBusiness_ValidateCompleteOutcomeAndPosition_OutcomeAndPositionNotAllowedException()
        {
            //arrange
            OutcomeAndPositionJudiciaryRequests outcomeAndPositionRequest = new OutcomeAndPositionJudiciaryRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException allowedCompletedOutcomeAndPosition = Assert.Throws<NotFoundException>(() => outcomeAndPositionJudiciaryBusiness.AvailableOutComeAndPositionToBeCompleted(outcomeAndPositionRequest));
            Assert.Equal("NotAllowedCompleteOutcomeAndPosition", allowedCompletedOutcomeAndPosition.Message);
        }

    }
}
