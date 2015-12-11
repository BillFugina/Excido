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
    public class DeleteSharedContentValidatorTests {
        [TestMethod()]
        public void DeleteSharedContentValidatorTest() {
            DeleteSharedContentValidator validator = new DeleteSharedContentValidator();

            Assert.IsNotNull(validator);
        }

        [TestMethod()]
        public void IsSatisfiedByTest() {
            DeleteSharedContentValidator validator = new DeleteSharedContentValidator();

            var target = validator.IsSatisfiedBy(new SharedContentUnit());

            Assert.IsTrue(target);
        }
    }
}