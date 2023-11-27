using AIC.CrossCutting.ExceptionHandling;
using AIC.CrossCutting.Identity;
using AIC.CrossCutting.MailService;
using AIC.Data.Entities;
using AIC.Data.Enums;
using AIC.Data.ViewModels.SPViewModels;
using AIC.Repository;
using AIC.Service.Entities.UserRequest.MemberServices.UpdateStatus;
using AIC.Service.Implementation;
using AIC.SP.Middleware.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AIC.Service.Tests.Implementation
{
    public class UpdateStatusRequestsBusinessTests
    {
        UpdateStatusRequestsBusiness updateStatusRequestsBusiness;
        Mock<IRepository<MemberUpdateStatusRequests, Guid>> memberUpdateStatusRequestsRepo = new Mock<IRepository<MemberUpdateStatusRequests, Guid>>();
        IIdentityProvider mockDb10 = Mock.Of<IIdentityProvider>();
        IService<DocumentsViewModel> mockDb11 = Mock.Of<IService<DocumentsViewModel>>();
        IRepository<MemberUpdateRelativesRequests, int> mockDb3 = new Mock<IRepository<MemberUpdateRelativesRequests, int>>().Object;
        IRepository<MemberUpdateOtherQualificationsRequests, int> mockDb4 = new Mock<IRepository<MemberUpdateOtherQualificationsRequests, int>>().Object;
        Mock<IRepository<MemberSteps, int>> memberStepsRepo = new Mock<IRepository<MemberSteps, int>>();
        Mock<IRepository<MemberStepStatues, int>> memberStepStatuesRepo = new Mock<IRepository<MemberStepStatues, int>>();
        IRepository<MemberRequestsLogs, Guid> mockDb55 = new Mock<IRepository<MemberRequestsLogs, Guid>>().Object;
        MemberRequestsLogsBusiness memberRequestsLogsBusiness ;
        SendEmailBusiness mockDb8 = new SendEmailBusiness(null,null,null);
        Mock<IRepository<Governorate, int>> governoratesRepo = new Mock<IRepository<Governorate, int>>();
        Mock<IRepository<MaritalStatus, int>> maritalStatusRepo = new Mock<IRepository<MaritalStatus, int>>();
        Mock<IRepository<PoliceDepartment, int>> policeDepartmentRepo = new Mock<IRepository<PoliceDepartment, int>>();
        IMapper mockDb7 = Mock.Of<IMapper>();

        Mock<IRoleStore<ApplicationRole>> roleStoreMock = new Mock<IRoleStore<ApplicationRole>>();
        RoleManager<ApplicationRole> roleManager;
        public UpdateStatusRequestsBusinessTests()
        {
            roleManager = new RoleManager<ApplicationRole>(roleStoreMock.Object, null, null, null, null);
            memberRequestsLogsBusiness = new MemberRequestsLogsBusiness(mockDb55, mockDb10, roleManager);
            updateStatusRequestsBusiness = new UpdateStatusRequestsBusiness
                (memberUpdateStatusRequestsRepo.Object, mockDb10,null, mockDb11, mockDb3, 
                mockDb4, memberStepStatuesRepo.Object, memberStepsRepo.Object, memberRequestsLogsBusiness, null
                                            ,null, governoratesRepo.Object, maritalStatusRepo.Object, policeDepartmentRepo.Object, mockDb7,null,null,null,null);
        }

        #region Validate Save Request
        [Fact]
        public async Task ValidateSaveRequest_SubmittedRequest_NotFoundException_NotAllowedToBeUpdated()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid()
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Pending });

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSaveRequest(updateStatusRequestCommand));
            Assert.Equal("NotAllowedToBeUpdated", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateSaveRequest_CreateNewSavedRequestWhileHavingOne_NotFoundException_NotAllowedToBeSaved()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = new Guid()
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.GetAll(r => r.StatusId == (int)MemberRequestStatuesEnum.Draft, false)).Returns(new List<MemberUpdateStatusRequests> { new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft } }.AsQueryable());

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSaveRequest(updateStatusRequestCommand));
            Assert.Equal("NotAllowedToBeSaved", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateSaveRequest_HavingMultipleSavedRequest_NotFoundException_NotAllowedToBeSaved()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = new Guid()
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.GetAll(r => r.StatusId == (int)MemberRequestStatuesEnum.Draft, false)).Returns(new List<MemberUpdateStatusRequests> {
                new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft },
                new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft }}.AsQueryable());

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSaveRequest(updateStatusRequestCommand));
            Assert.Equal("NotAllowedToBeSaved", notFoundException.Message);

        }
        #endregion

        #region Validate Submit Request
        [Fact]
        public async Task ValidateSubmitRequest_HaveSubmittedRequest_NotFoundException_NotAllowedToBeSubmitted()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = new Guid()
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.GetAll(r => r.StatusId == (int)MemberRequestStatuesEnum.Pending || r.StatusId == (int)MemberRequestStatuesEnum.Accepted, false)).Returns(new List<MemberUpdateStatusRequests> {
                new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Pending }}.AsQueryable());

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("NotAllowedToBeSubmitted", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateSubmitRequest_SubmitSubmittedRequest_NotFoundException_NotAllowedToBeSubmitted()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid()
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Pending });

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("NotAllowedToBeSubmitted", notFoundException.Message);

        }
        [Fact]
        public async Task ValidateSubmitRequest_InvalidDateOfBirth_NotFoundException_InvalidDateOfBirth()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now.AddYears(-300)
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft });

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("InvalidDateOfBirth", notFoundException.Message);

        }
        [Fact]
        public async Task ValidateSubmitRequest_InvalidBirthGov_NotFoundException_GovernorateNotFound()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now.AddYears(-30),
                BirthGovernorateId = 1
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.BirthGovernorateId, false)).Returns((Governorate)null);
            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("GovernorateNotFound", notFoundException.Message);

        }
        [Fact]
        public async Task ValidateSubmitRequest_InvalidCurrentGov_NotFoundException_GovernorateNotFound()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now.AddYears(-30),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.CurrentGovernorateId, false)).Returns((Governorate)null);
            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("GovernorateNotFound", notFoundException.Message);

        }
        [Fact]
        public async Task ValidateSubmitRequest_InvalidMaritalStatus_NotFoundException_MaritalStatusNotFound()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now.AddYears(-30),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.MaritalStatusId, false)).Returns((MaritalStatus)null);
            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("MaritalStatusNotFound", notFoundException.Message);

        }
        [Fact]
        public async Task ValidateSubmitRequest_InvalidPoliceDepartment_NotFoundException_policeDepartmentNotFound()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now.AddYears(-30),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.MaritalStatusId, false)).Returns((new MaritalStatus { }));
            policeDepartmentRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.PoliceDepartmentId, false)).Returns((PoliceDepartment)null);
            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("policeDepartmentNotFound", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateSubmitRequest_InvalidEmail_NotFoundException_InvalidEmail()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now.AddYears(-30),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                Email = "walid.samy"
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.MaritalStatusId, false)).Returns((new MaritalStatus { }));
            policeDepartmentRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("InvalidEmail", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateSubmitRequest_InvalidNationalID_NotFoundException_InvalidNationalID()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now.AddYears(-30),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                Email = "walid.samy@asset.com.eg",
                NationalID = "2999"
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.MaritalStatusId, false)).Returns((new MaritalStatus { }));
            policeDepartmentRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("InvalidNationalID", notFoundException.Message);

        }
        [Fact]
        public async Task ValidateSubmitRequest_InvalidMobile1_NotFoundException_InvalidMobileNumber1()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now.AddYears(-30),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                Email = "walid.samy@asset.com.eg",
                NationalID = "29905051301420",
                MobileNumber1 = "012345678"
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.MaritalStatusId, false)).Returns((new MaritalStatus { }));
            policeDepartmentRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("InvalidMobileNumber1", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateSubmitRequest_InvalidMobile2_NotFoundException_InvalidMobileNumber2()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now.AddYears(-30),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                Email = "walid.samy@asset.com.eg",
                NationalID = "29905051301420",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "0123456"
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.MaritalStatusId, false)).Returns((new MaritalStatus { }));
            policeDepartmentRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("InvalidMobileNumber2", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateSubmitRequest_InvalidMobile3_NotFoundException_InvalidMobileNumber3()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now.AddYears(-30),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                Email = "walid.samy@asset.com.eg",
                NationalID = "29905051301420",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "0123456"
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.MaritalStatusId, false)).Returns((new MaritalStatus { }));
            policeDepartmentRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("InvalidMobileNumber3", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateSubmitRequest_InvalidHomePhone_NotFoundException_InvalidHomePhone()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now.AddYears(-30),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                Email = "walid.samy@asset.com.eg",
                NationalID = "29905051301420",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                HomePhone = "02212"
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.MaritalStatusId, false)).Returns((new MaritalStatus { }));
            policeDepartmentRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("InvalidHomePhone", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateSubmitRequest_InvalidAcademicQualificationDate_NotFoundException_InvalidAcademicQualificationDate()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now.AddYears(-30),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                Email = "walid.samy@asset.com.eg",
                NationalID = "29905051301420",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                HomePhone = "02212551",
                AcademicQualificationDate = DateTime.Now.AddYears(-200)
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.MaritalStatusId, false)).Returns((new MaritalStatus { }));
            policeDepartmentRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("InvalidAcademicQualificationDate", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateSubmitRequest_InvalidHigherQualificationDate_NotFoundException_InvalidHigherQualificationDate()
        {
            //Arrange
            UpdateStatusRequestCommand updateStatusRequestCommand = new UpdateStatusRequestCommand
            {
                Id = Guid.NewGuid(),
                DateOfBirth = DateTime.Now.AddYears(-30),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                Email = "walid.samy@asset.com.eg",
                NationalID = "29905051301420",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                HomePhone = "02212551",
                AcademicQualificationDate = DateTime.Now.AddYears(-20),
                HigherQualificationDate = DateTime.Now.AddYears(-150)
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(updateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Draft });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governoratesRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.MaritalStatusId, false)).Returns((new MaritalStatus { }));
            policeDepartmentRepo.Setup(g => g.GetFirst(g => g.Id == updateStatusRequestCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateSubmitRequest(updateStatusRequestCommand));
            Assert.Equal("InvalidHigherQualificationDate", notFoundException.Message);

        }
        #endregion

        #region Validate Reject Request
        [Fact]
        public async Task ValidateRejectRequest_NewGuidId_NotFoundException_NotFoundRequest()
        {
            //Arrange
            RejectUpdateStatusRequestCommand rejectUpdateStatusRequestCommand = new RejectUpdateStatusRequestCommand
            {
                Id = new Guid()
            };
            //memberUpdateStatusRequestsRepo.Setup(z => z.GetAll(r => r.StatusId == (int)MemberRequestStatuesEnum.Pending || r.StatusId == (int)MemberRequestStatuesEnum.Accepted, false)).Returns(new List<MemberUpdateStatusRequests> {
             //   new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Pending }}.AsQueryable());

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateRejectRequest(rejectUpdateStatusRequestCommand));
            Assert.Equal("NotFoundRequest", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateRejectRequest_NotFoundRequest_NotFoundException_NotFoundRequest()
        {
            //Arrange
            RejectUpdateStatusRequestCommand rejectUpdateStatusRequestCommand = new RejectUpdateStatusRequestCommand
            {
                Id = Guid.NewGuid()
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(rejectUpdateStatusRequestCommand.Id, false)).Returns((MemberUpdateStatusRequests)null);

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateRejectRequest(rejectUpdateStatusRequestCommand));
            Assert.Equal("NotFoundRequest", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateRejectRequest_NotAvailableActions_NotFoundException_NotAllowedToBeUpdated()
        {
            //Arrange
            RejectUpdateStatusRequestCommand rejectUpdateStatusRequestCommand = new RejectUpdateStatusRequestCommand
            {
                Id = Guid.NewGuid()
            };
            memberStepsRepo.Setup(z => z.GetFirst(s => s.MemberServiceId == (int)MemberServicesEnum.UpdateStatus && s.Order == (int)MemberRequestStatuesEnum.Rejected, false)).Returns(new MemberSteps {Id = 9 });
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(rejectUpdateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests {StepId = memberStepsRepo.Object.GetFirst(s => s.MemberServiceId == (int)MemberServicesEnum.UpdateStatus && s.Order == (int)MemberRequestStatuesEnum.Rejected, false).Id });
            memberStepStatuesRepo.Setup(z => z.GetAll(s => s.StepId == memberUpdateStatusRequestsRepo.Object.Get(rejectUpdateStatusRequestCommand.Id, false).StepId, false)).Returns((IQueryable<MemberStepStatues>)null);

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateRejectRequest(rejectUpdateStatusRequestCommand));
            Assert.Equal("NotAllowedToBeUpdated", notFoundException.Message);

        }
        [Fact]
        public async Task ValidateRejectRequest_RejectNotInAvailbelActions_NotFoundException_NotAllowedToBeUpdated()
        {
            //Arrange
            RejectUpdateStatusRequestCommand rejectUpdateStatusRequestCommand = new RejectUpdateStatusRequestCommand
            {
                Id = Guid.NewGuid()
            };
            memberStepsRepo.Setup(z => z.GetFirst(s => s.MemberServiceId == (int)MemberServicesEnum.UpdateStatus && s.Order == (int)MemberRequestStatuesEnum.Rejected, false)).Returns(new MemberSteps { Id = 9 });
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(rejectUpdateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StepId = memberStepsRepo.Object.GetFirst(s => s.MemberServiceId == (int)MemberServicesEnum.UpdateStatus && s.Order == (int)MemberRequestStatuesEnum.Rejected, false).Id });
            memberStepStatuesRepo.Setup(z => z.GetAll(s => s.StepId == memberUpdateStatusRequestsRepo.Object.Get(rejectUpdateStatusRequestCommand.Id, false).StepId, false)).Returns(new List<MemberStepStatues> { new MemberStepStatues { StatusId = (int)MemberRequestStatuesEnum.Completed },
                                                                                          new MemberStepStatues { StatusId = (int)MemberRequestStatuesEnum.Accepted }}.AsQueryable<MemberStepStatues>());

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateRejectRequest(rejectUpdateStatusRequestCommand));
            Assert.Equal("NotAllowedToBeUpdated", notFoundException.Message);

        }

        #endregion

        #region Validate Accept Request
        [Fact]
        public async Task ValidateAcceptRequest_NewGuidId_NotFoundException_NotFoundRequest()
        {
            //Arrange
            AcceptUpdateStatusRequestCommand acceptUpdateStatusRequestCommand = new AcceptUpdateStatusRequestCommand
            {
                Id = new Guid()
            };
            //memberUpdateStatusRequestsRepo.Setup(z => z.GetAll(r => r.StatusId == (int)MemberRequestStatuesEnum.Pending || r.StatusId == (int)MemberRequestStatuesEnum.Accepted, false)).Returns(new List<MemberUpdateStatusRequests> {
            //   new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Pending }}.AsQueryable());

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateAcceptRequest(acceptUpdateStatusRequestCommand));
            Assert.Equal("NotFoundRequest", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateAcceptRequest_NotFoundRequest_NotFoundException_NotFoundRequest()
        {
            //Arrange
            AcceptUpdateStatusRequestCommand acceptUpdateStatusRequestCommand = new AcceptUpdateStatusRequestCommand
            {
                Id = new Guid()
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(acceptUpdateStatusRequestCommand.Id, false)).Returns((MemberUpdateStatusRequests)null);

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateAcceptRequest(acceptUpdateStatusRequestCommand));
            Assert.Equal("NotFoundRequest", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateAcceptRequest_NotAvailableActions_NotFoundException_NotAllowedToBeUpdated()
        {
            //Arrange
            AcceptUpdateStatusRequestCommand acceptUpdateStatusRequestCommand = new AcceptUpdateStatusRequestCommand
            {
                Id = Guid.NewGuid()
            };
            memberStepsRepo.Setup(z => z.GetFirst(s => s.MemberServiceId == (int)MemberServicesEnum.UpdateStatus && s.Order == (int)MemberRequestStatuesEnum.Rejected, false)).Returns(new MemberSteps { Id = 5 });
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(acceptUpdateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StepId = memberStepsRepo.Object.GetFirst(s => s.MemberServiceId == (int)MemberServicesEnum.UpdateStatus && s.Order == (int)MemberRequestStatuesEnum.Rejected, false).Id });
            memberStepStatuesRepo.Setup(z => z.GetAll(s => s.StepId == memberUpdateStatusRequestsRepo.Object.Get(acceptUpdateStatusRequestCommand.Id, false).StepId, false)).Returns((IQueryable<MemberStepStatues>)null);

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateAcceptRequest(acceptUpdateStatusRequestCommand));
            Assert.Equal("NotAllowedToBeUpdated", notFoundException.Message);

        }
        [Fact]
        public async Task ValidateAcceptRequest_AcceptNotInAvailbelActions_NotFoundException_NotAllowedToBeUpdated()
        {
            //Arrange
            AcceptUpdateStatusRequestCommand acceptUpdateStatusRequestCommand = new AcceptUpdateStatusRequestCommand
            {
                Id = Guid.NewGuid()
            };
            memberStepsRepo.Setup(z => z.GetFirst(s => s.MemberServiceId == (int)MemberServicesEnum.UpdateStatus && s.Order == (int)MemberRequestStatuesEnum.Rejected, false)).Returns(new MemberSteps { Id = 5 });
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(acceptUpdateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StepId = memberStepsRepo.Object.GetFirst(s => s.MemberServiceId == (int)MemberServicesEnum.UpdateStatus && s.Order == (int)MemberRequestStatuesEnum.Rejected, false).Id });
            memberStepStatuesRepo.Setup(z => z.GetAll(s => s.StepId == memberUpdateStatusRequestsRepo.Object.Get(acceptUpdateStatusRequestCommand.Id, false).StepId, false)).Returns(new List<MemberStepStatues> { new MemberStepStatues { StatusId = (int)MemberRequestStatuesEnum.Rejected },
                                                                                          new MemberStepStatues { StatusId = (int)MemberRequestStatuesEnum.Completed }}.AsQueryable<MemberStepStatues>());

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateAcceptRequest(acceptUpdateStatusRequestCommand));
            Assert.Equal("NotAllowedToBeUpdated", notFoundException.Message);

        }

        #endregion

        #region Validate Complete Request

        [Fact]
        public async Task ValidateCompleteRequest_CompleteNotInAvailbelActions_NotFoundException_NotAllowedToBeUpdated()
        {
            //Arrange
            CompleteUpdateStatusRequestCommand completeUpdateStatusRequestCommand = new CompleteUpdateStatusRequestCommand
            {
                Id = Guid.NewGuid()
            };
            memberStepsRepo.Setup(z => z.GetFirst(s => s.MemberServiceId == (int)MemberServicesEnum.UpdateStatus && s.Order == (int)MemberRequestStatuesEnum.Rejected, false)).Returns(new MemberSteps { Id = 5 });
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(completeUpdateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StepId = memberStepsRepo.Object.GetFirst(s => s.MemberServiceId == (int)MemberServicesEnum.UpdateStatus && s.Order == (int)MemberRequestStatuesEnum.Rejected, false).Id });
            memberStepStatuesRepo.Setup(z => z.GetFirst(s => s.StepId == memberUpdateStatusRequestsRepo.Object.Get(completeUpdateStatusRequestCommand.Id, false).StepId && s.StatusId == (int)MemberRequestStatuesEnum.Completed, false)).Returns((MemberStepStatues)null);

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateCompleteRequest(completeUpdateStatusRequestCommand));
            Assert.Equal("NotAllowedToBeUpdated", notFoundException.Message);

        }

        #endregion


        #region Validate Delete Request
        [Fact]
        public async Task ValidateDeleteRequest_NewGuidId_NotFoundException_NotFoundRequest()
        {
            //Arrange
            DeleteUpdateStatusRequestCommand deleteUpdateStatusRequestCommand = new DeleteUpdateStatusRequestCommand
            {
                Id = new Guid()
            };
            
            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateDeleteRequest(deleteUpdateStatusRequestCommand));
            Assert.Equal("NotFoundRequest", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateDeleteRequest_NotFoundRequest_NotFoundException_NotFoundRequest()
        {
            //Arrange
            DeleteUpdateStatusRequestCommand deleteUpdateStatusRequestCommand = new DeleteUpdateStatusRequestCommand
            {
                Id = new Guid()
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(deleteUpdateStatusRequestCommand.Id, false)).Returns((MemberUpdateStatusRequests)null);

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateDeleteRequest(deleteUpdateStatusRequestCommand));
            Assert.Equal("NotFoundRequest", notFoundException.Message);

        }

        [Fact]
        public async Task ValidateDeleteRequestt_NotDraftRequest_NotFoundException_NotAllowedToBeDeleted()
        {
            //Arrange
            DeleteUpdateStatusRequestCommand deleteUpdateStatusRequestCommand = new DeleteUpdateStatusRequestCommand
            {
                Id = Guid.NewGuid()
            };
            memberUpdateStatusRequestsRepo.Setup(z => z.Get(deleteUpdateStatusRequestCommand.Id, false)).Returns(new MemberUpdateStatusRequests { StatusId = (int)MemberRequestStatuesEnum.Rejected });

            //act and arrange
            NotFoundException notFoundException = await Assert.ThrowsAsync<NotFoundException>(() => updateStatusRequestsBusiness.ValidateDeleteRequest(deleteUpdateStatusRequestCommand));
            Assert.Equal("NotAllowedToBeDeleted", notFoundException.Message);

        }

        #endregion

    }
}
