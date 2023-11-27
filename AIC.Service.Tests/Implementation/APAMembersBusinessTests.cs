using AIC.CrossCutting.ExceptionHandling;
using AIC.Data.Entities;
using AIC.Data.ViewModels.Import;
using AIC.Repository;
using AIC.Service.Entities.UserRequest.Commands;
using AIC.Service.Implementation;
using Moq;
using Npoi.Mapper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AIC.Service.Tests.Implementation
{
    public class APAMembersBusinessTests
    {
        APAMembersBusiness apaMembersBusiness;
        Mock<IRepository<MemberProfile, Guid>> memberProfileRepository = new Mock<IRepository<MemberProfile, Guid>>();
        Mock<IRepository<Governorate, int>> governorateRepository = new Mock<IRepository<Governorate, int>>();
        Mock<IRepository<MaritalStatus, int>> maritalStatusRepository = new Mock<IRepository<MaritalStatus, int>>();
        Mock<IRepository<PoliceDepartment, int>> policeDepartmentRepository = new Mock<IRepository<PoliceDepartment, int>>();
        Mock<IRepository<Grade, int>> gradeRepository = new Mock<IRepository<Grade, int>>();

        public APAMembersBusinessTests()
        {
            apaMembersBusiness = new APAMembersBusiness(memberProfileRepository.Object,null,null,null,null,null,null,
                                                        governorateRepository.Object, policeDepartmentRepository.Object, maritalStatusRepository.Object, gradeRepository.Object,
                                                        null, null, null, null);
        }

        [Theory]
        [InlineData("02215635")]
        [InlineData("05555311")]
        [InlineData("03054987")]
        public void FixPhoneNumbers_ValidHomePhoneNumber_ReturnSame(string number)
        {
            //Arrange
            var memberSheet = new MemberDataSheet
            {
                HomePhone = number
            };
            var memberData = new RowInfo<MemberDataSheet>(1,memberSheet,0,null);
            //act
            apaMembersBusiness.FixPhoneNumbers(memberData);

            //assert
            Assert.Equal(number, memberData.Value.HomePhone);
        }

        [Theory]
        [InlineData("01234567890")]
        [InlineData("01021587985")]
        [InlineData("01102456497")]
        public void FixPhoneNumbers_ValidMobilePhoneNumber_ReturnSame(string number)
        {
            //Arrange
            var memberSheet = new MemberDataSheet
            {
                MobileNumber1 = number,
                MobileNumber2 = number,
                MobileNumber3 = number,
            };
            var memberData = new RowInfo<MemberDataSheet>(1, memberSheet, 0, null);
            //act
            apaMembersBusiness.FixPhoneNumbers(memberData);

            //assert
            Assert.Equal(number, memberData.Value.MobileNumber1);
            Assert.Equal(number, memberData.Value.MobileNumber2);
            Assert.Equal(number, memberData.Value.MobileNumber3);
        }

        [Theory]
        [InlineData("2215635")]
        [InlineData("5555311")]
        [InlineData("3054987")]
        public void FixPhoneNumbers_invalidHomePhoneNumber_ReturnFixedNumber(string number)
        {
            //Arrange
            var memberSheet = new MemberDataSheet
            {
                HomePhone = number
            };
            var memberData = new RowInfo<MemberDataSheet>(1, memberSheet, 0, null);
            //act
            apaMembersBusiness.FixPhoneNumbers(memberData);

            //assert
            Assert.Equal("0" + number,  memberData.Value.HomePhone);
        }

        [Theory]
        [InlineData("1234567890")]
        [InlineData("1021587985")]
        [InlineData("1102456497")]
        public void FixPhoneNumbers_ValidMobilePhoneNumber_ReturnFixedNumber(string number)
        {
            //Arrange
            var memberSheet = new MemberDataSheet
            {
                MobileNumber1 = number,
                MobileNumber2 = number,
                MobileNumber3 = number,
            };
            var memberData = new RowInfo<MemberDataSheet>(1, memberSheet, 0, null);
            //act
            apaMembersBusiness.FixPhoneNumbers(memberData);

            //assert
            Assert.Equal("0" + number, memberData.Value.MobileNumber1);
            Assert.Equal("0" + number, memberData.Value.MobileNumber2);
            Assert.Equal("0" + number, memberData.Value.MobileNumber3);
        }

        [Fact]
        public void ValidateRequest_Invaliduser_UserNotFoundException()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid()
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns((MemberProfile)null);
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            UserNotFoundException userNotFoundException = Assert.Throws<UserNotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("UserNotFound", userNotFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidBirthGov_NotFoundException_GovernorateNotFound()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns((Governorate)null);
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("GovernorateNotFound", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidCurrentGov_NotFoundException_GovernorateNotFound()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 1,
                MaritalStatusId = 1
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns((Governorate)null);
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("GovernorateNotFound", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidMaritalStatus_NotFoundException_MaritalStatusNotFound()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns((MaritalStatus)null);
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("MaritalStatusNotFound", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidPoliceDepartment_NotFoundException_policeDepartmentNotFound()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus{ });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns((PoliceDepartment)null);
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("policeDepartmentNotFound", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidGrade_NotFoundException_GradeNotFound()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment{ });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns((Grade)null);
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("GradeNotFound", notFoundException.Message);
        }
        [Fact]
        public void ValidateRequest_InvalidEmail_NotFoundException_InvalidEmail()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami",
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidEmail", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidNationalID_NotFoundException_InvalidNationalID()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami@asset.com.eg",
                NationalID = "294102413015"
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidNationalID", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidMobile1_NotFoundException_InvalidMobileNumber1()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami@asset.com.eg",
                NationalID = "29410241301515",
                MobileNumber1 = "012345678"
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidMobileNumber1", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidMobile2_NotFoundException_InvalidMobileNumber2()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami@asset.com.eg",
                NationalID = "29410241301515",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "0123456789"
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidMobileNumber2", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidMobile3_NotFoundException_InvalidMobileNumber3()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami@asset.com.eg",
                NationalID = "29410241301515",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "0123456789"
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidMobileNumber3", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidHomePhone_NotFoundException_InvalidHomePhone()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami@asset.com.eg",
                NationalID = "29410241301515",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                HomePhone = "022345"
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidHomePhone", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidAcademicQualificationDate_NotFoundException_InvalidAcademicQualificationDate()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami@asset.com.eg",
                NationalID = "29410241301515",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                HomePhone = "22345678",
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidAcademicQualificationDate", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidCurrentGradeDate_NotFoundException_InvalidCurrentGradeDate()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami@asset.com.eg",
                NationalID = "29410241301515",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                HomePhone = "22345678",
                AcademicQualificationDate = DateTime.Now
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidCurrentGradeDate", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidDateOfBirth_NotFoundException_InvalidDateOfBirth()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami@asset.com.eg",
                NationalID = "29410241301515",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                HomePhone = "22345678",
                AcademicQualificationDate = DateTime.Now,
                CurrentGradeDate = DateTime.Now
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidDateOfBirth", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidHigherQualificationDate_NotFoundException_InvalidHigherQualificationDate()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami@asset.com.eg",
                NationalID = "29410241301515",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                HomePhone = "22345678",
                AcademicQualificationDate = DateTime.Now,
                CurrentGradeDate = DateTime.Now,
                DateOfBirth = DateTime.Now,
                HigherQualificationDate = DateTime.Now.AddYears(300)
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidHigherQualificationDate", notFoundException.Message);
        }


        [Fact]
        public void ValidateRequest_InvalidHiringDate_NotFoundException_InvalidHiringDate()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami@asset.com.eg",
                NationalID = "29410241301515",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                HomePhone = "22345678",
                AcademicQualificationDate = DateTime.Now,
                CurrentGradeDate = DateTime.Now,
                DateOfBirth = DateTime.Now,
                HigherQualificationDate = DateTime.Now
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidHiringDate", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidReceivingWorkDate_NotFoundException_InvalidReceivingWorkDate()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami@asset.com.eg",
                NationalID = "29410241301515",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                HomePhone = "22345678",
                AcademicQualificationDate = DateTime.Now,
                CurrentGradeDate = DateTime.Now,
                DateOfBirth = DateTime.Now,
                HigherQualificationDate = DateTime.Now,
                HiringDate = DateTime.Now
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidReceivingWorkDate", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidReferralDate_NotFoundException_InvalidReferralDate()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami@asset.com.eg",
                NationalID = "29410241301515",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                HomePhone = "22345678",
                AcademicQualificationDate = DateTime.Now,
                CurrentGradeDate = DateTime.Now,
                DateOfBirth = DateTime.Now,
                HigherQualificationDate = DateTime.Now,
                HiringDate = DateTime.Now,
                ReceivingWorkDate = DateTime.Now
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidReferralDate", notFoundException.Message);
        }

        [Fact]
        public void ValidateRequest_InvalidTransferProsecutionDate_NotFoundException_InvalidTransferProsecutionDate()
        {
            //Arrange
            UpdateProfileDetailsCommand updateProfileDetailsCommand = new UpdateProfileDetailsCommand
            {
                Id = new Guid(),
                BirthGovernorateId = 1,
                CurrentGovernorateId = 2,
                MaritalStatusId = 1,
                PoliceDepartmentId = 1,
                GradeId = 1,
                Email = "walid.sami@asset.com.eg",
                NationalID = "29410241301515",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                HomePhone = "22345678",
                AcademicQualificationDate = DateTime.Now,
                CurrentGradeDate = DateTime.Now,
                DateOfBirth = DateTime.Now,
                HigherQualificationDate = DateTime.Now,
                HiringDate = DateTime.Now,
                ReceivingWorkDate = DateTime.Now,
                ReferralDate = DateTime.Now,
                TransferProsecutionDate = DateTime.Now.AddYears(300)
            };
            memberProfileRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.Id, false)).Returns(new MemberProfile { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.BirthGovernorateId, false)).Returns(new Governorate { });
            governorateRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.CurrentGovernorateId, false)).Returns(new Governorate { });
            maritalStatusRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.MaritalStatusId, false)).Returns(new MaritalStatus { });
            policeDepartmentRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.PoliceDepartmentId, false)).Returns(new PoliceDepartment { });
            gradeRepository.Setup(z => z.GetFirst(m => m.Id == updateProfileDetailsCommand.GradeId, false)).Returns(new Grade { });
            //Act
            //Action act = apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand);

            //act and assert
            NotFoundException notFoundException = Assert.Throws<NotFoundException>(() => apaMembersBusiness.ValidateRequest(updateProfileDetailsCommand));
            Assert.Equal("InvalidTransferProsecutionDate", notFoundException.Message);
        }


        [Fact]
        public void ValidateMember_InvalidNationalID_NationalIDError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410242",
                Email = "w@s.com",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");

            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء إدخال رقم قومي مكون من 14 رقم بشكل صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidEmail_EmailError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");

            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء إدخال بريد الكتروني صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidName_NameError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");

            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء إدخال الاسم صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidDateOfBirth_DateOfBirthError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");

            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء ادخال تاريخ ميلاد صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidBirthGovernorate_BirthGovernorateError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء اختيار محافظة ميلاد صحيحة");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidAddress_AddressError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء إدخال عنوان صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidRegion_RegionError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء إدخال المنطقة بشكل صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidCurrentGovernorate_CurrentGovernorateError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء اختيار محافظة اقامة صحيحة");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidMaritalStatus_MaritalStatusError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء اختيار حالة اجتماعية صحيحة");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidPoliceDepartment_PoliceDepartmentError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء اختيار قسم شرطة صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidHomePhone_HomePhoneError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء ادخال تليفون منزل صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidMobileNumber1_MobileNumber1Error()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "012345678",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "01234567890",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء ادخال تليفون محمول 1 صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidMobileNumber2_MobileNumber2Error()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567",
                MobileNumber3 = "01234567890",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء ادخال تليفون محمول 2 صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidMobileNumber3_MobileNumber3Error()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "012345678",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء ادخال تليفون محمول 3 صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidGrade_GradeError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "012345678",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء اختيار درجة صحيحة");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidCurrentGradeDate_CurrentGradeDateError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "012345678",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء ادخال تاريخ الدرجة الحالية بشكل صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidCurrentProsecution_CurrentProsecutionError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "012345678",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء إدخال النيابة التي يعمل بها بشكل صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidReceivingWorkDate_ReceivingWorkDateError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "012345678",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء ادخال تاريخ استلام العمل بشكل صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidHiringDate_HiringDateError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "012345678",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء ادخال تاريخ التعيين بشكل صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidReferralDate_ReferralDateError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "012345678",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء ادخال تاريخ الاحالة للمعاش بشكل صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidTransferProsecutionDate_TransferProsecutionDateError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "012345678",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء ادخال تاريخ نقلة للنيابة بشكل صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidAcademicQualification_AcademicQualificationError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "012345678",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء إدخال المؤهل بشكل صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidAcademicQualificationAssessment_AcademicQualificationAssessmentError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "012345678",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء إدخال تقدير المؤهل بشكل صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidAcademicQualificationDate_AcademicQualificationDateError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "012345678",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء ادخال تاريخ الحصول علي المؤهل الدراسي بشكل صحيح");
            //assert
            Assert.NotNull(error);
        }

        [Fact]
        public void ValidateMember_InvalidAcademicQualificationUnv_AcademicQualificationUnvError()
        {
            //Arrange
            MemberDataSheet data = new MemberDataSheet
            {
                NationalID = "29410241305050",
                Email = "walid.sami@asset.com.eg",
                HomePhone = "22345678",
                MobileNumber1 = "01234567890",
                MobileNumber2 = "01234567890",
                MobileNumber3 = "012345678",
                DateOfBirth = DateTime.Now.AddYears(500)
            };
            RowInfo<MemberDataSheet> memberData = new RowInfo<MemberDataSheet>(1, data, 0, "");
            //Act
            List<ErrorsSheet> result = apaMembersBusiness.ValidateMember(memberData);
            var error = result.Find(e => e.Error == "برجاء إدخال جامعة المؤهل بشكل صحيح");
            //assert
            Assert.NotNull(error);
        }
    }
}
