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
    }
}
