using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EfficiencyClassWebAPI.Controllers;
using EfficiencyClassWebAPI.Models;
using EfficiencyClass.UnitTests.MockData;
using EfficiencyClassWebAPI.Repository;
using Moq;
using System.Web.Http;
using System.Net.Http;
using System.Linq.Expressions;

namespace EfficiencyClass.UnitTests.ControllersTests
{
    [TestClass]
    public class UserManagementTests
    {
        private UserManagementController controller;
        private MockUnitOfWork muow;
        Mock<UnitofWork> mocObj = new Mock<UnitofWork>();

        [TestInitialize]
        public void SetUp()
        {
            muow = new MockUnitOfWork();
            controller = new UserManagementController(mocObj.Object);
            controller.Request = new HttpRequestMessage()
            {
                Properties = { { System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            controller.Dispose();
        }

        [TestMethod]
        public void GetUserDetailDetails()
        {
            mocObj.Setup(x => x.RoleRepository.GetAll()).Returns(() => muow.RoleRepository.GetAll());
            mocObj.Setup(x => x.RoleRepository.Find(It.IsAny<Expression<Func<EfficiencyClassWebAPI.EF.Role, bool>>>())).Returns(() => muow.RoleRepository.Find(y => y.RoleName == "User"));

            mocObj.Setup(x => x.UserDetailRepository.GetAll()).Returns(() => muow.UserDetailRepository.GetAll());
            mocObj.Setup(x => x.UserMarketRepository.GetAll()).Returns(() => muow.UserMarketRepository.GetAll());

            mocObj.Setup(x => x.UserRoleRepository.GetAll()).Returns(() => muow.UserRoleRepository.GetAll());
            mocObj.Setup(x => x.UserRoleRepository.Find(It.IsAny<Expression<Func<EfficiencyClassWebAPI.EF.UserRole, bool>>>())).Returns(() => muow.UserRoleRepository.Find(y => y.RoleId == 2));

            var response = controller.GetUserDetails();
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void AddUserManagementDetails_Test()
        {
            UserManagementModel usermanagementmodel = new MockInputData().AddUserDetailsInput();

            mocObj.Setup(x => x.RoleRepository.GetAll()).Returns(() => muow.RoleRepository.GetAll());
            mocObj.Setup(x => x.UserDetailRepository.GetAll()).Returns(() => muow.UserDetailRepository.GetAll());
            mocObj.Setup(x => x.UserMarketRepository.GetAll()).Returns(() => muow.UserMarketRepository.GetAll());
            mocObj.Setup(x => x.UserRoleRepository.GetAll()).Returns(() => muow.UserRoleRepository.GetAll());
            mocObj.Setup(x => x.UserRoleRepository.Find(It.IsAny<Expression<Func<EfficiencyClassWebAPI.EF.UserRole, bool>>>())).Returns(() => muow.UserRoleRepository.Find(y => y.UserId == usermanagementmodel.Id));

            var response = controller.AddUserDetails(usermanagementmodel);

            Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void DeleteUserManagementDetails_Test()
        {
            int userId = 2;
            EfficiencyClassWebAPI.EF.UserDetail userData = new EfficiencyClassWebAPI.EF.UserDetail();
            EfficiencyClassWebAPI.EF.UserRole userRole = new EfficiencyClassWebAPI.EF.UserRole();
            EfficiencyClassWebAPI.EF.UserMarket userMarket = new EfficiencyClassWebAPI.EF.UserMarket();

            mocObj.Setup(x => x.UserMarketRepository.GetAll()).Returns(() => muow.UserMarketRepository.GetAll());
            mocObj.Setup(x => x.UserRoleRepository.GetAll()).Returns(() => muow.UserRoleRepository.GetAll());

            mocObj.Setup(x => x.UserDetailRepository.Find(It.IsAny<Expression<Func<EfficiencyClassWebAPI.EF.UserDetail, bool>>>())).Returns(() => muow.UserDetailRepository.Find(x => x.Id == userId));
            mocObj.Setup(x => x.UserDetailRepository.Remove(It.IsAny<EfficiencyClassWebAPI.EF.UserDetail>())).Callback(() => muow.UserDetailRepository.Remove(userData));

            var response = controller.DeleteUserDetails(userId);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

    }
}
