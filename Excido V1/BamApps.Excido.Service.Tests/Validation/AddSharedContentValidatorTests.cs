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

namespace BamApps.Excido.Service.Validation.Tests {
    [TestClass()]
    public class AddSharedContentValidatorTests {
        [TestMethod()]
        public void AddSharedContentValidatorTest() {
            ICreateStampValidator<SharedContentUnit> createStampValidator = Mock.Of<ICreateStampValidator<SharedContentUnit>>();

            AddSharedContentValidator validator = new AddSharedContentValidator(createStampValidator);

            Assert.IsNotNull(validator);
        }

        [TestMethod()]
        public void IsSatisfiedByPassTest() {
            ICreateStampValidator<SharedContentUnit> createStampValidator = Mock.Of<ICreateStampValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == true);
            AddSharedContentValidator validator = new AddSharedContentValidator(createStampValidator);
            SharedContentUnit unit = new SharedContentUnit();

            var target = validator.IsSatisfiedBy(unit);

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void IsSatisfiedByFailTest() {
            ICreateStampValidator<SharedContentUnit> createStampValidator = Mock.Of<ICreateStampValidator<SharedContentUnit>>(x => x.IsSatisfiedBy(It.IsAny<SharedContentUnit>()) == false);
            AddSharedContentValidator validator = new AddSharedContentValidator(createStampValidator);
            SharedContentUnit unit = new SharedContentUnit();

            var target = validator.IsSatisfiedBy(unit);

            Assert.IsFalse(target);
        }

    }
}