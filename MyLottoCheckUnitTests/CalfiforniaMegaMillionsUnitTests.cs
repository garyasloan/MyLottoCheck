using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyLottoCheck.Areas.CaliforniaMegaMillions.Controllers;
using MyLottoCheck.Models;
using MyLottoCheck.Areas.CaliforniaMegaMillions.ViewModels;
using System.ComponentModel.DataAnnotations;
using System;
using System.Web.Mvc;
using Moq;
using System.Web;
using System.Collections.Generic;

namespace MyLottoCheckUnitTests
{
    [TestClass]
    public class CalfiforniaMegaMillionsUnitTests
    {
        private Guid _pickId;

        [TestInitialize]
        public void Initialize()
        {
            _pickId = Guid.NewGuid();
        }

        [TestMethod]
        public void CreateMegaMillionsPickForUser()
        {
            var megaMillionPick = new CaliforniaMegaMillionsUserPickViewModel();
            var mockHttpContext = new Mock<HttpContextBase>();
            var response = new Mock<HttpResponseBase>();
            mockHttpContext.SetupGet(x => x.Response).Returns(response.Object);
            CaliforniaMegaMillionsRepositoryFake db = new CaliforniaMegaMillionsRepositoryFake();

            var controller = new HomeController(db)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockHttpContext.Object
                }
            };
            megaMillionPick.Id = _pickId;
            megaMillionPick.FirstPick = 1;
            megaMillionPick.SecondPick = 2;
            megaMillionPick.ThirdPick = 3;
            megaMillionPick.FourthPick = 4;
            megaMillionPick.FifthPick = 5;
            megaMillionPick.MegaPick = 6;
            ActionResult result = controller.Create(megaMillionPick);
            Assert.IsTrue(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void UpdateMegaMillionsPickForUser()
        {
            var megaMillionPick = new CaliforniaMegaMillionsUserPickViewModel();
            var mockHttpContext = new Mock<HttpContextBase>();
            var response = new Mock<HttpResponseBase>();
            mockHttpContext.SetupGet(x => x.Response).Returns(response.Object);
            CaliforniaMegaMillionsRepositoryFake db = new CaliforniaMegaMillionsRepositoryFake();

            var controller = new HomeController(db)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockHttpContext.Object
                }
            };
            megaMillionPick.Id = _pickId;
            megaMillionPick.FirstPick = 11;
            megaMillionPick.SecondPick = 2;
            megaMillionPick.ThirdPick = 3;
            megaMillionPick.FourthPick = 4;
            megaMillionPick.FifthPick = 5;
            megaMillionPick.MegaPick = 6;
            ActionResult result = controller.Update(megaMillionPick);
            Assert.IsTrue(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void DeleteMegaMillionsPickForUser()
        {
            var megaMillionPick = new CaliforniaMegaMillionsUserPickViewModel();
            var mockHttpContext = new Mock<HttpContextBase>();
            var response = new Mock<HttpResponseBase>();
            mockHttpContext.SetupGet(x => x.Response).Returns(response.Object);
            CaliforniaMegaMillionsRepositoryFake db = new CaliforniaMegaMillionsRepositoryFake();

            var controller = new HomeController(db)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockHttpContext.Object
                }
            };
            megaMillionPick.Id = _pickId;
            ActionResult result = controller.Delete(megaMillionPick.Id);
            Assert.IsTrue(((HttpStatusCodeResult)result).StatusCode == 200);
        }


        [TestMethod]
        public void GetMegaMillionPicksForUser()
        {
            var megaMillionPick = new CaliforniaMegaMillionsUserPickViewModel();
            var mockHttpContext = new Mock<HttpContextBase>();
            var response = new Mock<HttpResponseBase>();
            mockHttpContext.SetupGet(x => x.Response).Returns(response.Object);
            CaliforniaMegaMillionsRepositoryFake db = new CaliforniaMegaMillionsRepositoryFake();

            var controller = new HomeController(db)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockHttpContext.Object
                }
            };
            megaMillionPick.Id = _pickId;
            ActionResult result = controller.Index("");
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void MegaMillionsSecondNumberDuplicateCheck()
        {
            var megaMillionPick = new CaliforniaMegaMillionsUserPickViewModel();
            var mockHttpContext = new Mock<HttpContextBase>();
            var response = new Mock<HttpResponseBase>();
            mockHttpContext.SetupGet(x => x.Response).Returns(response.Object);
            CaliforniaMegaMillionsRepositoryFake db = new CaliforniaMegaMillionsRepositoryFake();

            var controller = new HomeController(db)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockHttpContext.Object
                }
            };
            megaMillionPick.Id = Guid.NewGuid();
            megaMillionPick.FirstPick = 1;
            megaMillionPick.SecondPick = 1;
            megaMillionPick.ThirdPick = 3;
            megaMillionPick.FourthPick = 4;
            megaMillionPick.FifthPick = 5;
            megaMillionPick.MegaPick = 6;
            var validationContext = new ValidationContext(megaMillionPick, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(megaMillionPick, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
            }
            ActionResult result = controller.Create(megaMillionPick);
            Assert.IsTrue(!controller.ModelState.IsValid);
        }

        [TestMethod]
        public void MegaMillionsThirdNumberDuplicateCheck()
        {
            var megaMillionPick = new CaliforniaMegaMillionsUserPickViewModel();
            var mockHttpContext = new Mock<HttpContextBase>();
            var response = new Mock<HttpResponseBase>();
            mockHttpContext.SetupGet(x => x.Response).Returns(response.Object);
            CaliforniaMegaMillionsRepositoryFake db = new CaliforniaMegaMillionsRepositoryFake();

            var controller = new HomeController(db)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockHttpContext.Object
                }
            };
            megaMillionPick.Id = Guid.NewGuid();
            megaMillionPick.FirstPick = 1;
            megaMillionPick.SecondPick = 2;
            megaMillionPick.ThirdPick = 2;
            megaMillionPick.FourthPick = 4;
            megaMillionPick.FifthPick = 5;
            megaMillionPick.MegaPick = 6;
            var validationContext = new ValidationContext(megaMillionPick, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(megaMillionPick, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
            }
            ActionResult result = controller.Create(megaMillionPick);
            Assert.IsTrue(!controller.ModelState.IsValid);
        }

        [TestMethod]
        public void MegaMillionsFourthNumberDuplicateCheck()
        {
            var megaMillionPick = new CaliforniaMegaMillionsUserPickViewModel();
            var mockHttpContext = new Mock<HttpContextBase>();
            var response = new Mock<HttpResponseBase>();
            mockHttpContext.SetupGet(x => x.Response).Returns(response.Object);
            CaliforniaMegaMillionsRepositoryFake db = new CaliforniaMegaMillionsRepositoryFake();
            var controller = new HomeController(db)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockHttpContext.Object
                }
            };
            megaMillionPick.Id = Guid.NewGuid();
            megaMillionPick.FirstPick = 1;
            megaMillionPick.SecondPick = 2;
            megaMillionPick.ThirdPick = 3;
            megaMillionPick.FourthPick = 3;
            megaMillionPick.FifthPick = 5;
            megaMillionPick.MegaPick = 6;

            var validationContext = new ValidationContext(megaMillionPick, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(megaMillionPick, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
            }
            ActionResult result = controller.Create(megaMillionPick);
            Assert.IsTrue(!controller.ModelState.IsValid);
        }
        [TestMethod]
        public void MegaMillionsFifthNumberDuplicateCheck()
        {
            var megaMillionPick = new CaliforniaMegaMillionsUserPickViewModel();
            var mockHttpContext = new Mock<HttpContextBase>();
            var response = new Mock<HttpResponseBase>();
            mockHttpContext.SetupGet(x => x.Response).Returns(response.Object);
            CaliforniaMegaMillionsRepositoryFake db = new CaliforniaMegaMillionsRepositoryFake();

            var controller = new HomeController(db)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = mockHttpContext.Object
                }
            };
            megaMillionPick.Id = Guid.NewGuid();
            megaMillionPick.FirstPick = 1;
            megaMillionPick.SecondPick = 2;
            megaMillionPick.ThirdPick = 3;
            megaMillionPick.FourthPick = 4;
            megaMillionPick.FifthPick = 4;
            megaMillionPick.MegaPick = 6;

            var validationContext = new ValidationContext(megaMillionPick, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(megaMillionPick, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(string.Empty, validationResult.ErrorMessage);
            }
            ActionResult result = controller.Create(megaMillionPick);
            Assert.IsTrue(!controller.ModelState.IsValid);
        }
    }
}
