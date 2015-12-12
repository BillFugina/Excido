using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BamApps.Excido.Interface.Service;
using BamApps.Excido.Data.Model;
using Moq;
using MSTestExtensions;

namespace BamApps.Excido.Service.Validation.Tests {
    [TestClass()]
    public class UpdateSharedContentValidatorTests : BaseTest{
        [TestMethod()]
        public void UpdateSharedContentValidatorTest() {
            ICreateStampValidator<SharedContentUnit> _createStampValidator = Mock.Of<ICreateStampValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == true);
            UpdateSharedContentValidator validator = new UpdateSharedContentValidator(_createStampValidator);

            Assert.IsNotNull(validator);
        }

        [TestMethod()]
        public void UpdateSharedContentValidatorNullToConstructorTest() {
            ICreateStampValidator<SharedContentUnit> _createStampValidator = null;

            Assert.Throws<ArgumentNullException>(() => { new UpdateSharedContentValidator(_createStampValidator); });
        }

        [TestMethod()]
        public void IsSatisfiedByPassTest() {
            ICreateStampValidator<SharedContentUnit> _createStampValidator = Mock.Of<ICreateStampValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == true);
            UpdateSharedContentValidator validator = new UpdateSharedContentValidator(_createStampValidator);

            var target = validator.IsSatisfiedBy(new SharedContentUnit());

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void IsSatisfiedByFailTest() {
            ICreateStampValidator<SharedContentUnit> _createStampValidator = Mock.Of<ICreateStampValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == false);
            UpdateSharedContentValidator validator = new UpdateSharedContentValidator(_createStampValidator);

            var target = validator.IsSatisfiedBy(new SharedContentUnit());

            Assert.IsFalse(target);
        }
    }
}