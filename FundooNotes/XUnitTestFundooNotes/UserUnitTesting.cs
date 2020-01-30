using FundooBusinessLayer.Interfaces;
using FundooBusinessLayer.Services;
using FundooCommonLayer.UserRequestModel;
using FundooNotes.Controllers;
using FundooRepositoryLayer.Interfaces;
using FundooRepositoryLayer.ModelContext;
using FundooRepositoryLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace XUnitTestFundooNotes
{
    public class UserUnitTesting
    {
        private IUserRL userRL;
        private IUserBL userBL;
        private IConfiguration configuration;
        public static DbContextOptions<UserContext> DbContext { get; }
        public static string sqlConnection = "Data Source=.;Initial Catalog=FundooDb;Integrated Security=True";
        static UserUnitTesting()
        {
            DbContext = new DbContextOptionsBuilder<UserContext>().UseSqlServer(sqlConnection).Options;
        }
        public UserUnitTesting()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(@"D:\FundooProject\FundooNotes\XUnitTestFundooNotes\appsetting.json");
            configuration = builder.Build();
            var context = new UserContext(DbContext);
            userRL = new UserRL(context);
            userBL = new UserBL(userRL);

        }
        #region login
        [Fact]
        public void Task_Login_Return_Ok()
        {
            var controller = new AccountsController(userBL, configuration);
            var data = new Login()
            {
                Email = "vinitathopte1@gmail.com",
                Password = "Vinita@123"

            };
            var expected = controller.Login(data);
            Assert.IsType<OkObjectResult>(expected);
            
        }
        [Fact]
        public void Task_Login_Return_BadRequest_If_Email_Is_Null()
        {
            var controller = new AccountsController(userBL, configuration);
            var data = new Login()
            {
                Email = " ",
                Password = "Vinita@123"
            };
            var expected = controller.Login(data);
            Assert.IsType<BadRequestObjectResult>(expected);
        }
        [Fact]
        public void Task_Login_Return_BadRequest_If_Password_Is_Null()
        {
            var controller = new AccountsController(userBL, configuration);
            var data = new Login()
            {
                Email = "vinitathopte1@gmail.com",
                Password = " "
            };
            var expected = controller.Login(data);
            Assert.IsType<BadRequestObjectResult>(expected);
        }
        [Fact]
        public void Task_Login_Return_BadRequest_If_Email_Is_Incorrect()
        {
            var controller = new AccountsController(userBL, configuration);
            var data = new Login()
            {
                Email = "sedfgv1@gmail.com",
                Password = "Vinita@123"
            };
            var expected = controller.Login(data);
            Assert.IsType<BadRequestObjectResult>(expected);
        }
        [Fact]
        public void Task_Login_Return_BadRequest_If_Password_Is_Incorrect()
        {
            var controller = new AccountsController(userBL, configuration);
            var data = new Login()
            {
                Email = "vinitathopte1@gmail.com",
                Password = "sfvdsv@123"
            };
            var expected = controller.Login(data);
            Assert.IsType<BadRequestObjectResult>(expected);
        }
        #endregion
        #region registration
        [Fact]
        public void Task_Registration_Return_Ok()
        {
            var controller = new AccountsController(userBL, configuration);
            var data = new RegistrationRequestModel()
            {
                FirstName = "Janhavi",
                LastName = "Katkar",
                Email = "sawakarejayashree@gmail.com",
                Passwrod = "Jayashri@456",
                IsActive = true,
                Type = "Advanced"
            };
            var expected = controller.Registration(data);
            Assert.IsType<OkObjectResult>(expected);
        }
        [Fact]
        public void Task_Registration_Return_BadRequest_If_Field_Is_Empty()
        {
            var controller = new AccountsController(userBL, configuration);
            var data = new RegistrationRequestModel()
            {
                FirstName = " ",
                LastName = "Katkar",
                Email = "sawakarejayashree@gmail.com",
                Passwrod = "Jayashri@456",
                IsActive = true,
                Type = "Advanced"
            };
            var expected = controller.Registration(data);
            Assert.IsType<BadRequestObjectResult>(expected);
        }
        #endregion
        #region forgot password
        [Fact]
        public void Task_Forgot_Password_Return_Ok()
        {
            var controller = new AccountsController(userBL, configuration);
            var data = new ForgotPassword()
            {
               Email = "vinitathopte1@gmail.com"
            };
            var expected = controller.ForgotPassword(data);
            Assert.IsType<OkObjectResult>(expected);
        }
        [Fact]
        public void Task_Forgot_Password_Return_BadRequest_If_Email_Incorrect()
        {
            var controller = new AccountsController(userBL, configuration);
            var data = new ForgotPassword()
            {
                Email = "vinitathopte1.com"
            };
            var expected = controller.ForgotPassword(data);
            Assert.IsType<BadRequestObjectResult>(expected);
        }
        [Fact]
        public void Task_Forgot_Password_Return_BadRequest_If_Email_Is_Null()
        {
            var controller = new AccountsController(userBL, configuration);
            var data = new ForgotPassword()
            {
                Email = " "
            };
            var expected = controller.ForgotPassword(data);
            Assert.IsType<BadRequestObjectResult>(expected);
        }
        [Fact]
        public void Task_Forgot_Password_Return_BadRequest_If_Email_Is_Incorrect()
        {
            var controller = new AccountsController(userBL, configuration);
            var data = new ForgotPassword()
            {
                Email = "vinitathopte1@gmailcom"
            };
            var expected = controller.ForgotPassword(data);
            Assert.IsType<BadRequestObjectResult>(expected);
        }
        #endregion
        #region reset password
        [Fact]
        public void Task_Reset_Password_Return_Ok()
        {
            var controller = new AccountsController(userBL, configuration);
            var data = new ResetPassword()
            {
                Password = "Pooja@123"
            };
            var expected = controller.ResetPassword(data);
            Assert.IsType<OkObjectResult>(expected);
        }
        [Fact]
        public void Task_Reset_Password_Return_BadRequest_If_Password_Null()
        {
            var controller = new AccountsController(userBL, configuration);
            var data = new ResetPassword()
            {
                Password = " "
            };
            var expected = controller.ResetPassword(data);
            Assert.IsType<BadRequestObjectResult>(expected);
        }
        #endregion
    }
}
