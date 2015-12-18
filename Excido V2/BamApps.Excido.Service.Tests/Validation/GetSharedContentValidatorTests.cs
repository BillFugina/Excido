using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BamApps.Excido.Data.Model;
using BamApps.Excido.Interface.Service;
using Moq;

namespace BamApps.Excido.Service.Validation.Tests {
    [TestClass()]
    public class GetSharedContentValidatorTests {
        [TestMethod()]
        public void GetSharedContentValidatorTest() {
            IExpireStampValidator<SharedContentUnit> expireStampValidator = Mock.Of<IExpireStampValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == true);
            GetSharedContentValidator validator = new GetSharedContentValidator(expireStampValidator);

            Assert.IsNotNull(validator);
        }

        [TestMethod()]
        public void IsSatisfiedByPassTest() {
            IExpireStampValidator<SharedContentUnit> expireStampValidator = Mock.Of<IExpireStampValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == true);
            GetSharedContentValidator validator = new GetSharedContentValidator(expireStampValidator);

            var target = validator.IsSatisfiedBy(new SharedContentUnit());

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void IsSatisfiedByFailTest() {
            IExpireStampValidator<SharedContentUnit> expireStampValidator = Mock.Of<IExpireStampValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == false);
            GetSharedContentValidator validator = new GetSharedContentValidator(expireStampValidator);

            var target = validator.IsSatisfiedBy(new SharedContentUnit());

            Assert.IsFalse(target);
        }

    }
}