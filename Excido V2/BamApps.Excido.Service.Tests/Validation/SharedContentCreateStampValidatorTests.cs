using Microsoft.VisualStudio.TestTools.UnitTesting;
using BamApps.Excido.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BamApps.Excido.Data.Model;
using BamApps.Excido.Interface.Data;
using Moq;

namespace BamApps.Excido.Service.Validation.Tests {
    [TestClass()]
    public class SharedContentCreateStampValidatorTests {
        [TestMethod()]
        public void SharedContentCreateStampValidatorTest() {
            IReadRepository<SharedContentUnit> readRepository = Mock.Of<IReadRepository<SharedContentUnit>>();
            SharedContentCreateStampValidator target = new SharedContentCreateStampValidator(readRepository);
            Assert.IsNotNull(target);
        }
    }
}