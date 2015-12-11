using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BamApps.Excido.Service.Validation.Tests {
    [TestClass()]
    public class GetSharedContentValidatorTests {
        [TestMethod()]
        public void GetSharedContentValidatorTest() {
            GetSharedContentValidator validator = new GetSharedContentValidator();

            Assert.IsNotNull(validator);
        }

        [TestMethod()]
        public void IsSatisfiedByTest() {
            GetSharedContentValidator validator = new GetSharedContentValidator();

            Assert.Fail();
        }

    }
}