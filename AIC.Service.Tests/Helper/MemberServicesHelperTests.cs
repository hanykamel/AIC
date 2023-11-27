using AIC.CrossCutting.Constant;
using AIC.Service.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AIC.Service.Tests.Helper
{
    public class MemberServicesHelperTests
    {
        [Fact]
        public void CreateRequestCode_UpdateStatusService_NotNull()
        {
            //arrange
            int Service = (int)MemberServicesEnum.UpdateStatus;

            //act
            var res = MemberServicesHelper.CreateRequestCode(Service);

            //assert
            Assert.NotNull(res);
        }
        [Fact]
        public void CreateRequestCode_UpdateStatusService_Length15()
        {
            //arrange
            int Service = (int)MemberServicesEnum.UpdateStatus;

            //act
            var res = MemberServicesHelper.CreateRequestCode(Service).Length;

            //assert
            Assert.Equal(15, res);
        }

        [Fact]
        public void CreateRequestCode_UpdateStatusService_StartByMB()
        {
            //arrange
            int Service = (int)MemberServicesEnum.UpdateStatus;

            //act
            var res = MemberServicesHelper.CreateRequestCode(Service).StartsWith(Constant.UserCategoriesCode.Member);

            //assert
            Assert.True(res);
        }

        [Fact]
        public void CreateRequestCode_UpdateStatusService_ContainsSSU()
        {
            //arrange
            int Service = (int)MemberServicesEnum.UpdateStatus;

            //act
            var res = MemberServicesHelper.CreateRequestCode(Service).Contains(Constant.MemberServicesCodes.StatusStatementUpdate);

            //assert
            Assert.True(res);
        }
    }
}
