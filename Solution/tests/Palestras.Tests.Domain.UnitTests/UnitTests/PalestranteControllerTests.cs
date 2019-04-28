using Microsoft.AspNetCore.Mvc;
using Moq;
using Palestras.Application.Interfaces;
using Palestras.Application.ViewModels;
using Palestras.Domain.Core.Bus;
using Palestras.Domain.Core.Notifications;
using Palestras.WebApi.Controllers;
using System.Collections.Generic;
using Xunit;

namespace Palestras.Tests.Domain.UnitTests
{
    public class PalestranteControllerTests
    {
        /// <summary>
        ///  AAA => Arrange, Act, Assert
        /// </summary>
        /// 

        public Mock<IPalestranteAppService> mockIPalestranteAppService;
        public Mock<DomainNotificationHandler> mockNotification;
        public Mock<IMediatorHandler> mockIMediatorHandler;
        public PalestranteController palestranteController;

        [Fact]
        public void PalestranteController_CreatePalestrante_ReturnSuccess()
        {
            // Arrange
            var mockIPalestranteAppService = new Mock<IPalestranteAppService>();
            var mockIPalestraAppService = new Mock<IPalestraAppService>();
            var mockNotification = new Mock<DomainNotificationHandler>();
            var mockIMediatorHandler = new Mock<IMediatorHandler>();

            var palestranteViewModel = new PalestranteViewModel();

            mockNotification.Setup(m => m.GetNotifications()).Returns(new List<DomainNotification>());

            var palestranteController = new PalestranteController(
                mockIPalestranteAppService.Object,
                mockIPalestraAppService.Object,
                mockNotification.Object,
                mockIMediatorHandler.Object);

            //Act
            var result = palestranteController.Post(palestranteViewModel);

            //Assert
            mockIPalestranteAppService.Verify(m => m.Register(palestranteViewModel), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PalestranteController_CreatePalestrante_ReturnErrorsFromPresentationLayer()
        {
            // Arrange
            var mockIPalestranteAppService = new Mock<IPalestranteAppService>();
            var mockIPalestraAppService = new Mock<IPalestraAppService>();
            var mockNotification = new Mock<DomainNotificationHandler>();
            var mockIMediatorHandler = new Mock<IMediatorHandler>();

            var notificationList = new List<DomainNotification> { new DomainNotification("Error", "ModelError") };

            mockNotification.Setup(m => m.GetNotifications()).Returns(notificationList);
            mockNotification.Setup(c => c.HasNotifications()).Returns(true);

            var palestranteController = new PalestranteController(
                mockIPalestranteAppService.Object,
                mockIPalestraAppService.Object,
                mockNotification.Object,
                mockIMediatorHandler.Object);

            palestranteController.ModelState.AddModelError("Error", "ModelError");

            //Act
            var result = palestranteController.Post(new PalestranteViewModel());

            //Assert
            mockIPalestranteAppService.Verify(m => m.Register(It.IsAny<PalestranteViewModel>()), Times.Never);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void PalestranteController_CreatePalestrante_ReturnErrorsFromDomainLayer()
        {
            // Arrange
            var mockIPalestranteAppService = new Mock<IPalestranteAppService>();
            var mockIPalestraAppService = new Mock<IPalestraAppService>();
            var mockNotification = new Mock<DomainNotificationHandler>();
            var mockIMediatorHandler = new Mock<IMediatorHandler>();

            var palestranteViewModel = new PalestranteViewModel();

            mockNotification.Setup(m => m.GetNotifications()).Returns(new List<DomainNotification>());

            var palestranteController = new PalestranteController(
                mockIPalestranteAppService.Object,
                mockIPalestraAppService.Object,
                mockNotification.Object,
                mockIMediatorHandler.Object);

            var notificationList = new List<DomainNotification> { new DomainNotification("Error", "Erro ao adicionar Palestrante") };

            mockNotification.Setup(m => m.GetNotifications()).Returns(notificationList);
            mockNotification.Setup(c => c.HasNotifications()).Returns(true);

            //Act
            var result = palestranteController.Post(new PalestranteViewModel());

            //Assert
            mockIPalestranteAppService.Verify(m => m.Register(palestranteViewModel), Times.Never);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
