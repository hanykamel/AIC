using AIC.Service.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AIC.Service.Tests.Helper
{

    public class ValidationsTest
    {
        [Fact]
        public void IsValidEmail_ValidMail_True()
        {
            //arrange 
            string emailInput = "walid@samy.com";

            //act
            var result = Validations.IsValidEmail(emailInput);

            //asserts
            Assert.True(result);
        }

        [Fact]
        public void IsValidEmail_InValidMail_False()
        {
            //arrange 
            string emailInput = "walidsamy.com";

            //act
            var result = Validations.IsValidEmail(emailInput);

            //asserts
            Assert.False(result);
        }

        [Fact]
        public void IsValidMobileNumber_ValidMobileNumber_True()
        {
            //arrange 
            string mobileNumberInput = "01114789856";

            //act
            var result = Validations.IsValidMobileNumber(mobileNumberInput);

            //asserts
            Assert.True(result);
        }

        [Fact]
        public void IsValidMobileNumber_InValidMobileNumber_False()
        {
            //arrange 
            string mobileNumberInput = "01114@#sdf789856";

            //act
            var result = Validations.IsValidMobileNumber(mobileNumberInput);

            //asserts
            Assert.False(result);
        }

        [Fact]
        public void IsValidNationalID_ValidNationalID_True()
        {
            //arrange 
            string nationalIdInput = "25102121600635";

            //act
            var result = Validations.IsValidNationalID(nationalIdInput);

            //asserts
            Assert.True(result);
        }

        [Fact]
        public void IsValidNationalID_InValidNationalID_False()
        {
            //arrange 
            string nationalIdInput = "251021216006357";

            //act
            var result = Validations.IsValidNationalID(nationalIdInput);

            //asserts
            Assert.False(result);
        }

        [Fact]
        public void IsValidHomePhone_ValidHomePhone_True()
        {
            //arrange 
            string homePhoneInput = "0222879654";

            //act
            var result = Validations.IsValidHomePhone(homePhoneInput);

            //asserts
            Assert.True(result);
        }

        [Fact]
        public void IsValidHomePhone_InValidHomePhone_False()
        {
            //arrange 
            string homePhoneInput = "02";

            //act
            var result = Validations.IsValidHomePhone(homePhoneInput);

            //asserts
            Assert.False(result);
        }

        [Fact]
        public void IsValidDate_ValidDate_True()
        {
            //arrange 
            DateTime dateInput = DateTime.Now;

            //act
            var result = Validations.IsValidDate(dateInput.ToString());

            //asserts
            Assert.True(result);
        }

        [Fact]
        public void IsValidDate_InValidDate_False()
        {
            //arrange 
            string dateInput = "30/30/2022";

            //act
            var result = Validations.IsValidHomePhone(dateInput);

            //asserts
            Assert.False(result);
        }

    }
}
