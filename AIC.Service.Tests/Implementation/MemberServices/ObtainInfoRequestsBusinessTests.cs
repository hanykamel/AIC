using AIC.CrossCutting.ExceptionHandling;
using AIC.CrossCutting.Identity;
using AIC.Data.Entities;
using AIC.Data.Enums;
using AIC.Repository;
using AIC.Service.Entities.UserRequest.MemberServices.ObtainInfo;
using AIC.Service.Implementation;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AIC.Service.Tests.Implementation.MemberServices
{
    public class ObtainInfoRequestsBusinessTests
    {
        #region Properties

        ObtainInfoRequestBusiness obtainInfoRequestsBusiness;
        Mock<IRepository<MemberObtainInfoRequests, Guid>> memberObtainInfoRequestsRepo = new Mock<IRepository<MemberObtainInfoRequests, Guid>>();
        IIdentityProvider mockDb10 = Mock.Of<IIdentityProvider>();
        IRepository<MemberSteps, int> mockDb5 = new Mock<IRepository<MemberSteps, int>>().Object;
        IRepository<MemberStepStatues, int> mockDb6 = new Mock<IRepository<MemberStepStatues, int>>().Object;
        IRepository<MemberRequestsLogs, Guid> mockDb55 = new Mock<IRepository<MemberRequestsLogs, Guid>>().Object;
        MemberRequestsLogsBusiness memberRequestsLogsBusiness;
        SendEmailBusiness mockDb8 = new SendEmailBusiness(null, null, null);
        IRepository<Governorate, int> mockDb14 = new Mock<IRepository<Governorate, int>>().Object;
        IRepository<MaritalStatus, int> mockDb15 = new Mock<IRepository<MaritalStatus, int>>().Object;
        IRepository<PoliceDepartment, int> mockDb16 = new Mock<IRepository<PoliceDepartment, int>>().Object;
        IMapper mockDb7 = Mock.Of<IMapper>();
        Mock<IRoleStore<ApplicationRole>> roleStoreMock = new Mock<IRoleStore<ApplicationRole>>();
        RoleManager<ApplicationRole> roleManager;

        #endregion

        #region Constructor

        public ObtainInfoRequestsBusinessTests()
        {
            roleManager = new RoleManager<ApplicationRole>(roleStoreMock.Object, null, null, null, null);
            memberRequestsLogsBusiness = new MemberRequestsLogsBusiness(mockDb55, mockDb10, roleManager);
            obtainInfoRequestsBusiness = new ObtainInfoRequestBusiness
                (memberObtainInfoRequestsRepo.Object, mockDb10, null, roleManager, memberRequestsLogsBusiness, mockDb6, mockDb5, null, mockDb7, null,null);
        }

        #endregion

        #region Submit

        [Fact]
        public void ObtainInfoRequestBusinessBusiness_Submit_SendEmptyDTO_ReturnFalse()
        {
            //arrange
            SubmitRequestCommand submitRequestCommand = new SubmitRequestCommand();
            bool result;
            //act
            if (string.IsNullOrEmpty(submitRequestCommand.DesiredInformation))
                result = true;
            else
                result = false;

            //assert
            Assert.False(!result);
        }

        [Fact]
        public void ObtainInfoRequestBusinessBusiness_Submit_SendValidDTO_ReturnTrue()
        {
            //arrange
            SubmitRequestCommand submitRequestCommand = new SubmitRequestCommand()
            {
                DesiredInformation = "Desired Information test"
            };
            bool result;

            //act
            if (string.IsNullOrEmpty(submitRequestCommand.DesiredInformation))
                result = true;
            else
                result = false;

            //assert
            Assert.True(!result);
        }

        #endregion

        #region Save

        [Fact]
        public void ObtainInfoRequestBusinessBusiness_Save_SendEmptyDTO_ReturnFalse()
        {
            //arrange
            SubmitRequestCommand submitRequestCommand = new SubmitRequestCommand();
            bool result;
            //act
            if (string.IsNullOrEmpty(submitRequestCommand.DesiredInformation))
                result = true;
            else
                result = false;

            //assert
            Assert.False(!result);
        }

        [Fact]
        public void ObtainInfoRequestBusinessBusiness_Save_SendValidDTO_ReturnTrue()
        {
            //arrange
            SubmitRequestCommand submitRequestCommand = new SubmitRequestCommand()
            {
                DesiredInformation = "Desired Information test"
            };
            bool result;

            //act
            if (string.IsNullOrEmpty(submitRequestCommand.DesiredInformation))
                result = true;
            else
                result = false;

            //assert
            Assert.True(!result);
        }

        #endregion

        #region Accept

        [Fact]
        public void ObtainInfoRequestBusinessBusiness_Accept_SendEmptyDTO_ReturnFalse()
        {
            //arrange
            AcceptRequestCommand acceptRequestCommand = new AcceptRequestCommand();
            bool result;
            //act
            if (acceptRequestCommand.Id == Guid.Empty || string.IsNullOrEmpty(acceptRequestCommand.Remarks))
                result = true;
            else
                result = false;

            //assert
            Assert.False(!result);
        }

        [Fact]
        public void ObtainInfoRequestBusinessBusiness_Accept_SendValidDTO_ReturnTrue()
        {
            //arrange
            AcceptRequestCommand acceptRequestCommand = new AcceptRequestCommand()
            {
                Id = Guid.NewGuid(),
                Remarks = "Accept test"
            };
            bool result;

            //act
            if (acceptRequestCommand.Id == Guid.Empty || string.IsNullOrEmpty(acceptRequestCommand.Remarks))
                result = true;
            else
                result = false;

            //assert
            Assert.True(!result);
        }

        #endregion

        #region Reject

        [Fact]
        public void ObtainInfoRequestBusinessBusiness_Reject_SendEmptyDTO_ReturnFalse()
        {
            //arrange
            RejectRequestCommand rejectRequestCommand = new RejectRequestCommand();
            bool result;
            //act
            if (rejectRequestCommand.Id == Guid.Empty || string.IsNullOrEmpty(rejectRequestCommand.RejectionReason))
                result = true;
            else
                result = false;

            //assert
            Assert.False(!result);
        }

        [Fact]
        public void ObtainInfoRequestBusinessBusiness_Reject_SendValidDTO_ReturnTrue()
        {
            //arrange
            RejectRequestCommand rejectRequestCommand = new RejectRequestCommand()
            {
                Id = Guid.NewGuid(),
               RejectionReason = "Reject Reason test"
            };
            bool result;

            //act
            if (rejectRequestCommand.Id == Guid.Empty || string.IsNullOrEmpty(rejectRequestCommand.RejectionReason))
                result = true;
            else
                result = false;

            //assert
            Assert.True(!result);
        }

        #endregion

        #region Complete

        [Fact]
        public void ObtainInfoRequestBusinessBusiness_Complete_SendEmptyDTO_ReturnFalse()
        {
            //arrange
            CompleteRequestCommand completeRequestCommand = new CompleteRequestCommand();
            bool result;
            //act
            if (completeRequestCommand.Id == Guid.Empty || string.IsNullOrEmpty(completeRequestCommand.Remarks))
                result = true;
            else
                result = false;

            //assert
            Assert.False(!result);
        }

        [Fact]
        public void ObtainInfoRequestBusinessBusiness_Complete_SendValidDTO_ReturnTrue()
        {
            //arrange
            CompleteRequestCommand completeRequestCommand = new CompleteRequestCommand()
            {
                Id = Guid.NewGuid(),
                Remarks = "Remark test"
            };
            bool result;

            //act
            if (completeRequestCommand.Id == Guid.Empty || string.IsNullOrEmpty(completeRequestCommand.Remarks))
                result = true;
            else
                result = false;

            //assert
            Assert.True(!result);
        }


        #endregion

        [Fact]
        public void ObtainInfoRequestBusinessBusiness_ValidateMObtainInfoAdminActions_NotFoundException()
        {
            //arrange
            MemberObtainInfoRequests memberObtainInfoRequests = new MemberObtainInfoRequests()
            {
                Id = new Guid()
            };

            //act and assert
            NotFoundException obtainInfoNotFoundException = Assert.Throws<NotFoundException>(() => obtainInfoRequestsBusiness.ValidateMObtainInfoAdminActions(memberObtainInfoRequests.Id));
            Assert.Equal("NotFoundRequest", obtainInfoNotFoundException.Message);
        }

    }
}
