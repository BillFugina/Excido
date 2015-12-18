using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BamApps.Excido.Data.Model;

namespace BamApps.Excido.Service.Validation.Tests {
    [TestClass()]
    public class ExpireStampValidatorTests {
        [TestMethod()]
        public void ExpireStampValidatorTest() {
            ExpireStampValidator<SharedContentUnit> validator = new ExpireStampValidator<SharedContentUnit>();
            Assert.IsNotNull(validator);
        }

        [TestMethod()]
        public void IsSatisfiedByPassTest() {
            ExpireStampValidator<SharedContentUnit> validator = new ExpireStampValidator<SharedContentUnit>();
            SharedContentUnit s = new SharedContentUnit { Created = DateTime.Now, ExpireDate = DateTime.Now.AddDays(1) };

            var target = validator.IsSatisfiedBy(s);

            Assert.IsTrue(target);
        }

        [TestMethod()]
        public void IsSatisfiedByFailTest() {
            ExpireStampValidator<SharedContentUnit> validator = new ExpireStampValidator<SharedContentUnit>();
            SharedContentUnit s = new SharedContentUnit { Created = DateTime.Now, ExpireDate = DateTime.Now.AddDays(-1) };

            var target = validator.IsSatisfiedBy(s);

            Assert.IsFalse(target);
        }
    }
}