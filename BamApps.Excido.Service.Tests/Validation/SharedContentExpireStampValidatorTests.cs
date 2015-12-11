using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BamApps.Excido.Service.Validation.Tests {
    [TestClass()]
    public class SharedContentExpireStampValidatorTests {
        [TestMethod()]
        public void SharedContentExpireStampValidatorTest() {
            SharedContentExpireStampValidator target = new SharedContentExpireStampValidator();
            Assert.IsNotNull(target);
        }
    }
}